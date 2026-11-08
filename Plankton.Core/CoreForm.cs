using System.IO;
using CSHO;
using Plankton.Core;
using Plankton.Core.CustomUIEditors;
using Plankton.Core.Plugins;
using Plankton.PluginApiImplementation;
using Plankton.UI;
using PluginApi;
using WeifenLuo.WinFormsUI.Docking;

namespace Plankton.Core
{
    public partial class CoreForm : Form
    {
        // Host services
        private readonly CommandService commandService;
        private readonly ArchiveService archiveService;
        private readonly EditorService editorService;
        private readonly UIService uiService;
        private readonly PreferencesService preferencesService;

        private readonly Host host;
        private readonly PluginLoader pluginLoader;

        // Maps plugin editor instances to their actual DockPanel windows.
        private readonly Dictionary<IEditor, EditorDockContent> editorWindows =
            new();


        private readonly DockTabRenameFilter tabRenameFilter;

        private CorePreferences preferences = null!;

        private bool unsavedChanges = false;

        public CoreForm()
        {
            InitializeComponent();

            ConfigureDocking();

            // ---------------------------------------------------------
            // Create host services
            // ---------------------------------------------------------

            commandService = new CommandService();

            archiveService = new ArchiveService();

            uiService = new UIService(
                commandService);

            editorService = new EditorService(
                ShowEditor,
                ActivateEditor);

            preferencesService = new PreferencesService();


            tabRenameFilter = new DockTabRenameFilter(mainDockPanel);
            Application.AddMessageFilter(tabRenameFilter);

            host = new Host(
                archiveService,
                editorService,
                commandService,
                uiService,
                preferencesService);

            // ---------------------------------------------------------
            // Register UI locations owned by CoreForm
            // ---------------------------------------------------------

            RegisterCoreUILocations();
            RegisterCoreCommands();
            RegisterCoreUI();
            RegisterPreferences();

            // ---------------------------------------------------------
            // Plugin loader
            // ---------------------------------------------------------

            pluginLoader = new PluginLoader(host);

            SubscribeUnsavedChangesEvents();
        }


        private void SubscribeUnsavedChangesEvents()
        {

            host.Archive.Saved += (_, _) => { SetUnsavedChanges(false); };
            host.Archive.Opened += (_, _) => { SetUnsavedChanges(false); };
            host.Archive.Closed += (_, _) => { SetUnsavedChanges(false); };
            host.Archive.AssetsModified += (_, _) => { SetUnsavedChanges(true); };
            host.Archive.AssetsCreated += (_, _) => { SetUnsavedChanges(true); };
            host.Archive.AssetsDeleted += (_, _) => { SetUnsavedChanges(true); };
            host.Archive.AssetsRenamed += (_, _) => { SetUnsavedChanges(true); };
        }

        private void SetUnsavedChanges(bool unsavedChanges)
        {
            this.unsavedChanges = unsavedChanges;
            UpdateFormText();
        }

        private void UpdateFormText()
        {
            if (host.Archive.Current.Archive == null)
            {
                Text = "Plankton";
                return;
            }

            Text = "Plankton - " + host.Archive.Current.path + (unsavedChanges ? "*" : "");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData) // Maybe replace this with a global shortcut system
            {
                case Keys.Control | Keys.Z:
                    host.Commands.Execute("plankton.undo-action"); // Require undo and redo plugin installed
                    return true;

                case Keys.Control | Keys.Y:
                    host.Commands.Execute("plankton.redo-action");
                    return true;

                case Keys.Control | Keys.O:
                    host.Commands.Execute("plankton.archive.open");
                    return true;
                case Keys.Control | Keys.S:
                    host.Commands.Execute("plankton.archive.save");
                    return true;

                case Keys.Control | Keys.Shift | Keys.S:
                    host.Commands.Execute("plankton.archive.save-as");
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void RegisterPreferences()
        {
            preferences = host.Preferences.Register<CorePreferences>(
                "plankton.core",
                "Core");

            StartupEditorsEditor.GetProviders =
                () => editorService.Providers;
        }


        private void RegisterCoreUI()
        {
            uiService.AddCommand("plankton.main.file", "plankton.archive.open");
            uiService.AddCommand("plankton.main.file", "plankton.archive.save");
            uiService.AddCommand("plankton.main.file", "plankton.archive.save-as");
            uiService.AddCommand("plankton.main.file", "plankton.archive.close");
            uiService.AddCommand("plankton.main.edit", "plankton.preferences.open");
        }

        private void RegisterCoreCommands()
        {
            commandService.Register(new CommandDefinition(
                "plankton.preferences.open",
                "Edit Preferences...",
                parameter =>
                {
                    using var dialog = new PreferenceEditor.PreferencesForm(preferencesService);
                    dialog.ShowDialog();
                })
            );

            commandService.Register(new CommandDefinition(
                "plankton.archive.open",
                "Open Archive... [Ctrl+O]",
                parameter =>
                {
                    if (!UnsavedChangesPrompt()) return;

                    string? path = parameter as string;

                    if (path == null)
                    {
                        using var dialog = new OpenFileDialog
                        {
                            Filter = "HO Archives (*.ho)|*.ho|All files (*.*)|*.*"
                        };

                        if (dialog.ShowDialog() != DialogResult.OK)
                            return;

                        path = dialog.FileName;
                    }

                    OpenArchive(path);
                })
            );

            commandService.Register(new CommandDefinition(
                "plankton.archive.save",
                "Save Archive... [Ctrl+S]",
                parameter =>
                {
                    if (host.Archive.Current.Archive != null)
                        SaveArchive();
                }        
                ));

            commandService.Register(new CommandDefinition(
                "plankton.archive.save-as",
                "Save As... [Ctrl+Shift+S]",
                parameter =>
                {
                    if (host.Archive.Current.Archive != null)
                        SaveArchiveAs();
                }
                ));

            commandService.Register(new CommandDefinition(
                "plankton.archive.close",
                "Close...",
                parameter =>
                {
                    if (host.Archive.Current.Archive != null)
                        CloseArchive();
                }
                ));
        }

        private void CloseArchive()
        {
            if(UnsavedChangesPrompt()) archiveService.Close();
        }

        private bool UnsavedChangesPrompt()
        {
            if (!unsavedChanges) return true;
            
            DialogResult result = MessageBox.Show(
                "Save changes?",
                "Unsaved Changes",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning
            );

            switch (result)
            {
                case DialogResult.Yes:
                    SaveArchive();
                    break;

                case DialogResult.No:
                    break;

                case DialogResult.Cancel:
                    return false;
            }

            return true;
            
        }

        private void OpenArchive(string path)
        {
            archiveService.Open(path);
        }

        private void SaveArchive()
        {
            archiveService.Save();
        }

        private void SaveArchiveAs()
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "HO Archives (*.ho)|*.ho|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            string path = dialog.FileName;
        
            archiveService.SetPath(path);
            archiveService.Save();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string pluginDirectory =
                Path.Combine(
                    AppContext.BaseDirectory,
                    "Plugins");


            pluginLoader.LoadAll(pluginDirectory);


            OpenStartupEditors();
        }

        private void OpenStartupEditors()
        {
            foreach (string providerId in preferences.StartupEditors)
            {
                IEditorProvider? provider =
                    editorService.Providers.FirstOrDefault(
                        x => x.Id == providerId);

                if (provider == null)
                    continue;

                editorService.Open(provider);
            }
        }

        private void ConfigureDocking()
        {
            mainDockPanel.Theme =
                new PlanktonTheme();

            mainDockPanel.ShowDocumentIcon = true;

            mainDockPanel.DocumentStyle =
                DocumentStyle.DockingWindow;
        }

        private void RegisterCoreUILocations()
        {
            uiService.RegisterToolStripLocation(
                "plankton.main.file",
                ToolStripLocationKind.Menu,
                fileToolStripMenuItem.DropDownItems);

            uiService.RegisterToolStripLocation(
                "plankton.main.view",
                ToolStripLocationKind.Menu,
                viewToolStripMenuItem.DropDownItems);

            uiService.RegisterToolStripLocation(
                "plankton.main.edit",
                ToolStripLocationKind.Menu,
                editToolStripMenuItem.DropDownItems);

            uiService.RegisterToolStripLocation(
                 "plankton.main.toolbar",
                 ToolStripLocationKind.Toolbar,
                 mainMenuStrip.Items);
        }

        // -------------------------------------------------------------
        // EditorService -> DockPanel Suite bridge
        // -------------------------------------------------------------

        private void ShowEditor(IEditor editor)
        {
            if (editorWindows.ContainsKey(editor))
                return;

            var window =
                new EditorDockContent(editor);

            editorWindows.Add(
                editor,
                window);

            window.FormClosed += (_, _) =>
            {
                editorWindows.Remove(editor);
            };

            window.Show(
                mainDockPanel,
                DockState.Document);
        }

        private void ActivateEditor(IEditor editor)
        {
            if (!editorWindows.TryGetValue(
                    editor,
                    out EditorDockContent? window))
            {
                return;
            }

            window.Activate();
        }



        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!UnsavedChangesPrompt()) e.Cancel = true;
        }

    }
}