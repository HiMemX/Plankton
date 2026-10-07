using CSHO;
using Plankton.Core;
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

            // ---------------------------------------------------------
            // Plugin loader
            // ---------------------------------------------------------

            pluginLoader = new PluginLoader(host);
        }

        private void RegisterCoreUI()
        {
            uiService.AddCommand("plankton.main.file", "plankton.archive.open");
            uiService.AddCommand("plankton.main.edit", "plankton.preferences.open");
        }

        private void RegisterCoreCommands()
        {
            commandService.Register(new CommandDefinition(
                "plankton.preferences.open",
                "Edit Preferences...",
                parameter => {
                    using var dialog = new PreferenceEditor.PreferencesForm(preferencesService);
                    dialog.ShowDialog();
                })
            );

            commandService.Register(new CommandDefinition(
                "plankton.archive.open",
                "Open Archive...",
                parameter =>
                {
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
        }

        private void OpenArchive(string path)
        {
            archiveService.Open(path);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string pluginDirectory =
                Path.Combine(
                    AppContext.BaseDirectory,
                    "Plugins");


            pluginLoader.LoadAll(pluginDirectory);

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
    }
}