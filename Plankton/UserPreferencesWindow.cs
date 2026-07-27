using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plankton.GeneralTools;
using Plankton.Special_Editors.Level_Editor;

namespace Plankton
{
    public partial class UserPreferencesWindow : Form
    {
        public UserConfiguration userConfig { get { return UserConfigurationManager.userConfig; } set { UserConfigurationManager.userConfig = value; } }

        string userConfigPath;
        List<string> previouslyActivePlugins = new();
        Action<string> loadPluginCallback = (string path) => { };

        public UserPreferencesWindow(string userConfigPath, Action<string> loadPluginCallback)
        {
            InitializeComponent();

            LevelEditorKeybinds.Load();
            keybindsCollectionEditor.Bind(LevelEditorKeybinds.binds.binds);
            keybindsCollectionEditor.DisplayMember = nameof(Keybind.name);
            keybindsCollectionEditor.CollectionChanged += (object sender, EventArgs e) => { LevelEditorKeybinds.Save(); };

            //LevelEditorKeybinds.binds.binds.Add(new Keybind("Test", "Test", Keys.A));
            //LevelEditorKeybinds.Save();

            //keybindsPropertyGrid.SelectedObject = LevelEditorKeybinds.binds;

            this.userConfigPath = userConfigPath;
            UserConfigurationManager.Load(userConfigPath);
            this.loadPluginCallback = loadPluginCallback;

            renderingPropertyGrid.SelectedObject = userConfig.renderingConfiguration;
            renderingPropertyGrid.PropertyValueChanged += (object s, PropertyValueChangedEventArgs e) => { UserConfigurationManager.Save(userConfigPath); };

            SetupPluginListbox();
        }

        private void ConditionalLoadPlugin(string path) {
            if (previouslyActivePlugins.Contains(path)) return;
            loadPluginCallback(PluginPathHelper.Resolve(path));
        }

        private void SetupPluginListbox()
        {
            foreach (string pluginpath in userConfig.allPluginPaths)
            {
                int idx = pluginsCheckedListBox.Items.Add(pluginpath);
                if (userConfig.loadedPluginPaths.Contains(pluginpath))
                {
                    pluginsCheckedListBox.SetItemChecked(idx, true);
                    
                    //loadPluginCallback(pluginpath);
                }
            }
        }

        private void UserPreferencesWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void pluginsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string path = pluginsCheckedListBox.Items[e.Index]?.ToString();
            if (string.IsNullOrWhiteSpace(path)) return;
            
            if (e.NewValue == CheckState.Checked)
            {
                if (!userConfig.loadedPluginPaths.Contains(path))
                {
                    userConfig.loadedPluginPaths.Add(path);
                }

                ConditionalLoadPlugin(path);
                previouslyActivePlugins.Add(path);
            }
            else
                userConfig.loadedPluginPaths.Remove(path);

            SaveUserConfig();
        }

        public void SaveUserConfig()
        {
            UserConfigurationManager.Save(userConfigPath);
        }

        private void installPluginButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Dynamic Link Library (*.dll)|*.dll";

            DialogResult result = dialog.ShowDialog();

            if (result != DialogResult.OK) return;

            string path = dialog.FileName;

            path = PluginPathHelper.MakePortable(path);

            if (userConfig.allPluginPaths.Contains(path)) return;
            
            userConfig.allPluginPaths.Add(path);
            pluginsCheckedListBox.Items.Add(path);
            SaveUserConfig();
        }

        private void removePluginButton_Click(object sender, EventArgs e)
        {
            int index = pluginsCheckedListBox.SelectedIndex;
            if (index == -1) return;


            string path = pluginsCheckedListBox.Items[index].ToString();
            pluginsCheckedListBox.Items.RemoveAt(index);
            userConfig.allPluginPaths.RemoveAt(index);
            userConfig.loadedPluginPaths.Remove(path);

            SaveUserConfig();
        }
    }
}
