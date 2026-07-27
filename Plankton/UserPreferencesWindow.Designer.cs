namespace Plankton
{
    partial class UserPreferencesWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserPreferencesWindow));
            levelEditorGroupBox = new System.Windows.Forms.GroupBox();
            renderingGroupBox = new System.Windows.Forms.GroupBox();
            renderingPropertyGrid = new System.Windows.Forms.PropertyGrid();
            keybindsGroupBox = new System.Windows.Forms.GroupBox();
            keybindsCollectionEditor = new Plankton.Custom_Controls.CollectionEditorControl();
            pluginGroupBox = new System.Windows.Forms.GroupBox();
            removePluginButton = new System.Windows.Forms.Button();
            installPluginButton = new System.Windows.Forms.Button();
            pluginsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            levelEditorGroupBox.SuspendLayout();
            renderingGroupBox.SuspendLayout();
            keybindsGroupBox.SuspendLayout();
            pluginGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // levelEditorGroupBox
            // 
            levelEditorGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            levelEditorGroupBox.Controls.Add(renderingGroupBox);
            levelEditorGroupBox.Controls.Add(keybindsGroupBox);
            levelEditorGroupBox.Location = new System.Drawing.Point(11, 12);
            levelEditorGroupBox.Name = "levelEditorGroupBox";
            levelEditorGroupBox.Size = new System.Drawing.Size(762, 536);
            levelEditorGroupBox.TabIndex = 0;
            levelEditorGroupBox.TabStop = false;
            levelEditorGroupBox.Text = "3D Editor";
            // 
            // renderingGroupBox
            // 
            renderingGroupBox.Controls.Add(renderingPropertyGrid);
            renderingGroupBox.Dock = System.Windows.Forms.DockStyle.Right;
            renderingGroupBox.Location = new System.Drawing.Point(462, 23);
            renderingGroupBox.Name = "renderingGroupBox";
            renderingGroupBox.Size = new System.Drawing.Size(297, 510);
            renderingGroupBox.TabIndex = 2;
            renderingGroupBox.TabStop = false;
            renderingGroupBox.Text = "Rendering";
            // 
            // renderingPropertyGrid
            // 
            renderingPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            renderingPropertyGrid.Location = new System.Drawing.Point(3, 23);
            renderingPropertyGrid.Name = "renderingPropertyGrid";
            renderingPropertyGrid.Size = new System.Drawing.Size(291, 484);
            renderingPropertyGrid.TabIndex = 0;
            // 
            // keybindsGroupBox
            // 
            keybindsGroupBox.Controls.Add(keybindsCollectionEditor);
            keybindsGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            keybindsGroupBox.Location = new System.Drawing.Point(3, 23);
            keybindsGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            keybindsGroupBox.Name = "keybindsGroupBox";
            keybindsGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            keybindsGroupBox.Size = new System.Drawing.Size(456, 510);
            keybindsGroupBox.TabIndex = 1;
            keybindsGroupBox.TabStop = false;
            keybindsGroupBox.Text = "Keybinds";
            // 
            // keybindsCollectionEditor
            // 
            keybindsCollectionEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            keybindsCollectionEditor.ListPanelWidth = 200;
            keybindsCollectionEditor.Location = new System.Drawing.Point(3, 24);
            keybindsCollectionEditor.MinimumSize = new System.Drawing.Size(320, 180);
            keybindsCollectionEditor.Name = "keybindsCollectionEditor";
            keybindsCollectionEditor.Size = new System.Drawing.Size(450, 482);
            keybindsCollectionEditor.TabIndex = 0;
            // 
            // pluginGroupBox
            // 
            pluginGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pluginGroupBox.Controls.Add(removePluginButton);
            pluginGroupBox.Controls.Add(installPluginButton);
            pluginGroupBox.Controls.Add(pluginsCheckedListBox);
            pluginGroupBox.Location = new System.Drawing.Point(779, 12);
            pluginGroupBox.Name = "pluginGroupBox";
            pluginGroupBox.Size = new System.Drawing.Size(514, 536);
            pluginGroupBox.TabIndex = 1;
            pluginGroupBox.TabStop = false;
            pluginGroupBox.Text = "Plugins";
            // 
            // removePluginButton
            // 
            removePluginButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            removePluginButton.Location = new System.Drawing.Point(414, 58);
            removePluginButton.Name = "removePluginButton";
            removePluginButton.Size = new System.Drawing.Size(94, 29);
            removePluginButton.TabIndex = 3;
            removePluginButton.Text = "Remove";
            removePluginButton.UseVisualStyleBackColor = true;
            removePluginButton.Click += removePluginButton_Click;
            // 
            // installPluginButton
            // 
            installPluginButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            installPluginButton.Location = new System.Drawing.Point(414, 23);
            installPluginButton.Name = "installPluginButton";
            installPluginButton.Size = new System.Drawing.Size(94, 29);
            installPluginButton.TabIndex = 2;
            installPluginButton.Text = "Install new";
            installPluginButton.UseVisualStyleBackColor = true;
            installPluginButton.Click += installPluginButton_Click;
            // 
            // pluginsCheckedListBox
            // 
            pluginsCheckedListBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pluginsCheckedListBox.FormattingEnabled = true;
            pluginsCheckedListBox.IntegralHeight = false;
            pluginsCheckedListBox.Location = new System.Drawing.Point(3, 23);
            pluginsCheckedListBox.Name = "pluginsCheckedListBox";
            pluginsCheckedListBox.Size = new System.Drawing.Size(405, 502);
            pluginsCheckedListBox.TabIndex = 0;
            pluginsCheckedListBox.ItemCheck += pluginsCheckedListBox_ItemCheck;
            // 
            // UserPreferencesWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1305, 560);
            Controls.Add(pluginGroupBox);
            Controls.Add(levelEditorGroupBox);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(900, 600);
            Name = "UserPreferencesWindow";
            Text = "Edit Preferences";
            FormClosing += UserPreferencesWindow_FormClosing;
            levelEditorGroupBox.ResumeLayout(false);
            renderingGroupBox.ResumeLayout(false);
            keybindsGroupBox.ResumeLayout(false);
            pluginGroupBox.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox levelEditorGroupBox;
        private System.Windows.Forms.GroupBox keybindsGroupBox;
        private System.Windows.Forms.GroupBox pluginGroupBox;
        private System.Windows.Forms.CheckedListBox pluginsCheckedListBox;
        private System.Windows.Forms.Button removePluginButton;
        private System.Windows.Forms.Button installPluginButton;
        private Custom_Controls.CollectionEditorControl keybindsCollectionEditor;
        private System.Windows.Forms.GroupBox renderingGroupBox;
        private System.Windows.Forms.PropertyGrid renderingPropertyGrid;
    }
}