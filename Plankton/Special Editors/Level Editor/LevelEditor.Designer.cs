namespace Plankton.Special_Editors.Level_Editor
{
    partial class LevelEditor
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelEditor));
            levelViewRightClickMenu = new System.Windows.Forms.ContextMenuStrip(components);
            testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            renderer = new Plankton.Custom_Controls.GeometryBaseRenderer();
            infoPanel = new System.Windows.Forms.Panel();
            cameraLabelValue = new System.Windows.Forms.Label();
            cameraLabelText = new System.Windows.Forms.Label();
            fpsLabelValue = new System.Windows.Forms.Label();
            fpsLabelText = new System.Windows.Forms.Label();
            assetNameLabel = new System.Windows.Forms.Label();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            easyEditPanel = new System.Windows.Forms.Panel();
            scaleGroupBox = new System.Windows.Forms.GroupBox();
            scaleVectorInputBox = new CustomControls.Vector3InputBox();
            rotationGroupBox = new System.Windows.Forms.GroupBox();
            rotationVectorInputBox = new CustomControls.Vector3InputBox();
            positionGroupBox = new System.Windows.Forms.GroupBox();
            positionVectorInputBox = new CustomControls.Vector3InputBox();
            editOptionsTabControl = new System.Windows.Forms.TabControl();
            propertyGridTabPage = new System.Windows.Forms.TabPage();
            selectedPropertyGrid = new System.Windows.Forms.PropertyGrid();
            displaySettingsTabPage = new System.Windows.Forms.TabPage();
            containerTypesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            eventsTabPage = new System.Windows.Forms.TabPage();
            listBox1 = new System.Windows.Forms.ListBox();
            openLinkAssetButton = new System.Windows.Forms.Button();
            openInMainEditorButton = new System.Windows.Forms.Button();
            levelViewRightClickMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            infoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            easyEditPanel.SuspendLayout();
            scaleGroupBox.SuspendLayout();
            rotationGroupBox.SuspendLayout();
            positionGroupBox.SuspendLayout();
            editOptionsTabControl.SuspendLayout();
            propertyGridTabPage.SuspendLayout();
            displaySettingsTabPage.SuspendLayout();
            eventsTabPage.SuspendLayout();
            SuspendLayout();
            // 
            // levelViewRightClickMenu
            // 
            levelViewRightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            levelViewRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { testToolStripMenuItem });
            levelViewRightClickMenu.Name = "levelViewRightClickMenu";
            levelViewRightClickMenu.Size = new System.Drawing.Size(105, 28);
            levelViewRightClickMenu.Opening += levelViewRightClickMenu_Opening;
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
            testToolStripMenuItem.Text = "Test";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(renderer);
            splitContainer1.Panel1.Controls.Add(infoPanel);
            splitContainer1.Panel1MinSize = 700;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Panel2MinSize = 200;
            splitContainer1.Size = new System.Drawing.Size(1401, 881);
            splitContainer1.SplitterDistance = 1077;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // renderer
            // 
            renderer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            renderer.Dock = System.Windows.Forms.DockStyle.Fill;
            renderer.Location = new System.Drawing.Point(0, 0);
            renderer.Name = "renderer";
            renderer.Size = new System.Drawing.Size(1077, 850);
            renderer.TabIndex = 1;
            // 
            // infoPanel
            // 
            infoPanel.Controls.Add(cameraLabelValue);
            infoPanel.Controls.Add(cameraLabelText);
            infoPanel.Controls.Add(fpsLabelValue);
            infoPanel.Controls.Add(fpsLabelText);
            infoPanel.Controls.Add(assetNameLabel);
            infoPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            infoPanel.Location = new System.Drawing.Point(0, 850);
            infoPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            infoPanel.Name = "infoPanel";
            infoPanel.Size = new System.Drawing.Size(1077, 31);
            infoPanel.TabIndex = 0;
            // 
            // cameraLabelValue
            // 
            cameraLabelValue.AutoSize = true;
            cameraLabelValue.Location = new System.Drawing.Point(213, 5);
            cameraLabelValue.Name = "cameraLabelValue";
            cameraLabelValue.Size = new System.Drawing.Size(47, 20);
            cameraLabelValue.TabIndex = 3;
            cameraLabelValue.Text = "0, 0, 0";
            // 
            // cameraLabelText
            // 
            cameraLabelText.AutoSize = true;
            cameraLabelText.Location = new System.Drawing.Point(95, 5);
            cameraLabelText.Name = "cameraLabelText";
            cameraLabelText.Size = new System.Drawing.Size(119, 20);
            cameraLabelText.TabIndex = 2;
            cameraLabelText.Text = "Camera Position:";
            // 
            // fpsLabelValue
            // 
            fpsLabelValue.AutoSize = true;
            fpsLabelValue.Location = new System.Drawing.Point(47, 4);
            fpsLabelValue.Name = "fpsLabelValue";
            fpsLabelValue.Size = new System.Drawing.Size(17, 20);
            fpsLabelValue.TabIndex = 1;
            fpsLabelValue.Text = "0";
            // 
            // fpsLabelText
            // 
            fpsLabelText.AutoSize = true;
            fpsLabelText.Location = new System.Drawing.Point(3, 4);
            fpsLabelText.Name = "fpsLabelText";
            fpsLabelText.Size = new System.Drawing.Size(39, 20);
            fpsLabelText.TabIndex = 0;
            fpsLabelText.Text = "FPS: ";
            // 
            // assetNameLabel
            // 
            assetNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            assetNameLabel.Location = new System.Drawing.Point(0, 0);
            assetNameLabel.Name = "assetNameLabel";
            assetNameLabel.Size = new System.Drawing.Size(1077, 31);
            assetNameLabel.TabIndex = 4;
            assetNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
            splitContainer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(easyEditPanel);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(editOptionsTabControl);
            splitContainer2.Panel2.Controls.Add(openInMainEditorButton);
            splitContainer2.Size = new System.Drawing.Size(319, 881);
            splitContainer2.SplitterDistance = 317;
            splitContainer2.SplitterWidth = 5;
            splitContainer2.TabIndex = 1;
            // 
            // easyEditPanel
            // 
            easyEditPanel.AutoScroll = true;
            easyEditPanel.Controls.Add(scaleGroupBox);
            easyEditPanel.Controls.Add(rotationGroupBox);
            easyEditPanel.Controls.Add(positionGroupBox);
            easyEditPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            easyEditPanel.Location = new System.Drawing.Point(0, 0);
            easyEditPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            easyEditPanel.Name = "easyEditPanel";
            easyEditPanel.Size = new System.Drawing.Size(319, 317);
            easyEditPanel.TabIndex = 0;
            // 
            // scaleGroupBox
            // 
            scaleGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            scaleGroupBox.Controls.Add(scaleVectorInputBox);
            scaleGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            scaleGroupBox.Location = new System.Drawing.Point(0, 258);
            scaleGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            scaleGroupBox.Name = "scaleGroupBox";
            scaleGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            scaleGroupBox.Size = new System.Drawing.Size(298, 129);
            scaleGroupBox.TabIndex = 2;
            scaleGroupBox.TabStop = false;
            scaleGroupBox.Text = "Scale";
            // 
            // scaleVectorInputBox
            // 
            scaleVectorInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            scaleVectorInputBox.Location = new System.Drawing.Point(3, 24);
            scaleVectorInputBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            scaleVectorInputBox.Name = "scaleVectorInputBox";
            scaleVectorInputBox.SetVector3Callback = null;
            scaleVectorInputBox.Size = new System.Drawing.Size(292, 101);
            scaleVectorInputBox.TabIndex = 0;
            scaleVectorInputBox.Value = ((float, float, float))resources.GetObject("scaleVectorInputBox.Value");
            scaleVectorInputBox.X = 0F;
            scaleVectorInputBox.Y = 0F;
            scaleVectorInputBox.Z = 0F;
            // 
            // rotationGroupBox
            // 
            rotationGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            rotationGroupBox.Controls.Add(rotationVectorInputBox);
            rotationGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            rotationGroupBox.Location = new System.Drawing.Point(0, 129);
            rotationGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            rotationGroupBox.Name = "rotationGroupBox";
            rotationGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            rotationGroupBox.Size = new System.Drawing.Size(298, 129);
            rotationGroupBox.TabIndex = 1;
            rotationGroupBox.TabStop = false;
            rotationGroupBox.Text = "Rotation";
            // 
            // rotationVectorInputBox
            // 
            rotationVectorInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            rotationVectorInputBox.Location = new System.Drawing.Point(3, 24);
            rotationVectorInputBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            rotationVectorInputBox.Name = "rotationVectorInputBox";
            rotationVectorInputBox.SetVector3Callback = null;
            rotationVectorInputBox.Size = new System.Drawing.Size(292, 101);
            rotationVectorInputBox.TabIndex = 0;
            rotationVectorInputBox.Value = ((float, float, float))resources.GetObject("rotationVectorInputBox.Value");
            rotationVectorInputBox.X = 0F;
            rotationVectorInputBox.Y = 0F;
            rotationVectorInputBox.Z = 0F;
            // 
            // positionGroupBox
            // 
            positionGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            positionGroupBox.Controls.Add(positionVectorInputBox);
            positionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            positionGroupBox.Location = new System.Drawing.Point(0, 0);
            positionGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            positionGroupBox.Name = "positionGroupBox";
            positionGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            positionGroupBox.Size = new System.Drawing.Size(298, 129);
            positionGroupBox.TabIndex = 0;
            positionGroupBox.TabStop = false;
            positionGroupBox.Text = "Position";
            // 
            // positionVectorInputBox
            // 
            positionVectorInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            positionVectorInputBox.Location = new System.Drawing.Point(3, 24);
            positionVectorInputBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            positionVectorInputBox.Name = "positionVectorInputBox";
            positionVectorInputBox.SetVector3Callback = null;
            positionVectorInputBox.Size = new System.Drawing.Size(292, 101);
            positionVectorInputBox.TabIndex = 0;
            positionVectorInputBox.Value = ((float, float, float))resources.GetObject("positionVectorInputBox.Value");
            positionVectorInputBox.X = 0F;
            positionVectorInputBox.Y = 0F;
            positionVectorInputBox.Z = 0F;
            // 
            // editOptionsTabControl
            // 
            editOptionsTabControl.Controls.Add(propertyGridTabPage);
            editOptionsTabControl.Controls.Add(displaySettingsTabPage);
            editOptionsTabControl.Controls.Add(eventsTabPage);
            editOptionsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            editOptionsTabControl.Location = new System.Drawing.Point(0, 0);
            editOptionsTabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            editOptionsTabControl.Multiline = true;
            editOptionsTabControl.Name = "editOptionsTabControl";
            editOptionsTabControl.SelectedIndex = 0;
            editOptionsTabControl.Size = new System.Drawing.Size(319, 528);
            editOptionsTabControl.TabIndex = 2;
            // 
            // propertyGridTabPage
            // 
            propertyGridTabPage.Controls.Add(selectedPropertyGrid);
            propertyGridTabPage.Location = new System.Drawing.Point(4, 29);
            propertyGridTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            propertyGridTabPage.Name = "propertyGridTabPage";
            propertyGridTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            propertyGridTabPage.Size = new System.Drawing.Size(311, 495);
            propertyGridTabPage.TabIndex = 0;
            propertyGridTabPage.Text = "Properties";
            propertyGridTabPage.UseVisualStyleBackColor = true;
            // 
            // selectedPropertyGrid
            // 
            selectedPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            selectedPropertyGrid.HelpVisible = false;
            selectedPropertyGrid.Location = new System.Drawing.Point(3, 4);
            selectedPropertyGrid.Name = "selectedPropertyGrid";
            selectedPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            selectedPropertyGrid.Size = new System.Drawing.Size(305, 487);
            selectedPropertyGrid.TabIndex = 0;
            selectedPropertyGrid.ToolbarVisible = false;
            selectedPropertyGrid.PropertyValueChanged += selectedPropertyGrid_PropertyValueChanged;
            // 
            // displaySettingsTabPage
            // 
            displaySettingsTabPage.AutoScroll = true;
            displaySettingsTabPage.Controls.Add(containerTypesCheckedListBox);
            displaySettingsTabPage.Location = new System.Drawing.Point(4, 29);
            displaySettingsTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            displaySettingsTabPage.Name = "displaySettingsTabPage";
            displaySettingsTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            displaySettingsTabPage.Size = new System.Drawing.Size(311, 495);
            displaySettingsTabPage.TabIndex = 1;
            displaySettingsTabPage.Text = "Display";
            displaySettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // containerTypesCheckedListBox
            // 
            containerTypesCheckedListBox.CheckOnClick = true;
            containerTypesCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            containerTypesCheckedListBox.FormattingEnabled = true;
            containerTypesCheckedListBox.IntegralHeight = false;
            containerTypesCheckedListBox.Location = new System.Drawing.Point(3, 4);
            containerTypesCheckedListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            containerTypesCheckedListBox.Name = "containerTypesCheckedListBox";
            containerTypesCheckedListBox.Size = new System.Drawing.Size(305, 487);
            containerTypesCheckedListBox.TabIndex = 0;
            containerTypesCheckedListBox.ItemCheck += containerTypesCheckedListBox_ItemCheck;
            // 
            // eventsTabPage
            // 
            eventsTabPage.Controls.Add(listBox1);
            eventsTabPage.Controls.Add(openLinkAssetButton);
            eventsTabPage.Location = new System.Drawing.Point(4, 29);
            eventsTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            eventsTabPage.Name = "eventsTabPage";
            eventsTabPage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            eventsTabPage.Size = new System.Drawing.Size(311, 495);
            eventsTabPage.TabIndex = 2;
            eventsTabPage.Text = "Events";
            eventsTabPage.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.IntegralHeight = false;
            listBox1.ItemHeight = 20;
            listBox1.Location = new System.Drawing.Point(3, 4);
            listBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(305, 456);
            listBox1.TabIndex = 1;
            // 
            // openLinkAssetButton
            // 
            openLinkAssetButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            openLinkAssetButton.Location = new System.Drawing.Point(3, 460);
            openLinkAssetButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            openLinkAssetButton.Name = "openLinkAssetButton";
            openLinkAssetButton.Size = new System.Drawing.Size(305, 31);
            openLinkAssetButton.TabIndex = 2;
            openLinkAssetButton.Text = "Open LinkAssetEditor";
            openLinkAssetButton.UseVisualStyleBackColor = true;
            openLinkAssetButton.Click += openLinkAssetButton_Click;
            // 
            // openInMainEditorButton
            // 
            openInMainEditorButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            openInMainEditorButton.Location = new System.Drawing.Point(0, 528);
            openInMainEditorButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            openInMainEditorButton.Name = "openInMainEditorButton";
            openInMainEditorButton.Size = new System.Drawing.Size(319, 31);
            openInMainEditorButton.TabIndex = 1;
            openInMainEditorButton.Text = "Open in Main Editor";
            openInMainEditorButton.UseVisualStyleBackColor = true;
            openInMainEditorButton.Click += openInMainEditorButton_Click;
            // 
            // LevelEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Margin = new System.Windows.Forms.Padding(0);
            MinimumSize = new System.Drawing.Size(1049, 515);
            Name = "LevelEditor";
            Size = new System.Drawing.Size(1401, 881);
            levelViewRightClickMenu.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            infoPanel.ResumeLayout(false);
            infoPanel.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            easyEditPanel.ResumeLayout(false);
            scaleGroupBox.ResumeLayout(false);
            rotationGroupBox.ResumeLayout(false);
            positionGroupBox.ResumeLayout(false);
            editOptionsTabControl.ResumeLayout(false);
            propertyGridTabPage.ResumeLayout(false);
            displaySettingsTabPage.ResumeLayout(false);
            eventsTabPage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid selectedPropertyGrid;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button openInMainEditorButton;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Label fpsLabelValue;
        private System.Windows.Forms.Label fpsLabelText;
        private System.Windows.Forms.Label cameraLabelValue;
        private System.Windows.Forms.Label cameraLabelText;
        private System.Windows.Forms.Label assetNameLabel;
        private System.Windows.Forms.CheckedListBox containerTypesCheckedListBox;
        private System.Windows.Forms.TabControl editOptionsTabControl;
        private System.Windows.Forms.TabPage propertyGridTabPage;
        private System.Windows.Forms.TabPage displaySettingsTabPage;
        private System.Windows.Forms.Panel easyEditPanel;
        private System.Windows.Forms.GroupBox positionGroupBox;
        private CustomControls.Vector3InputBox positionVectorInputBox;
        private System.Windows.Forms.GroupBox rotationGroupBox;
        private CustomControls.Vector3InputBox rotationVectorInputBox;
        private System.Windows.Forms.GroupBox scaleGroupBox;
        private CustomControls.Vector3InputBox scaleVectorInputBox;
        private System.Windows.Forms.ContextMenuStrip levelViewRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.TabPage eventsTabPage;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button openLinkAssetButton;
        private Custom_Controls.GeometryBaseRenderer renderer;
    }
}