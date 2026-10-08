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
            renderSettingsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            eventsTabPage = new System.Windows.Forms.TabPage();
            listBox1 = new System.Windows.Forms.ListBox();
            openLinkAssetButton = new System.Windows.Forms.Button();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
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
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            SuspendLayout();
            // 
            // levelViewRightClickMenu
            // 
            levelViewRightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            levelViewRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { testToolStripMenuItem });
            levelViewRightClickMenu.Name = "levelViewRightClickMenu";
            levelViewRightClickMenu.Size = new System.Drawing.Size(95, 26);
            levelViewRightClickMenu.Opening += levelViewRightClickMenu_Opening;
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            testToolStripMenuItem.Text = "Test";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(renderer);
            splitContainer1.Panel1.Controls.Add(infoPanel);
            splitContainer1.Panel1MinSize = 20;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Panel2MinSize = 20;
            splitContainer1.Size = new System.Drawing.Size(1226, 661);
            splitContainer1.SplitterDistance = 942;
            splitContainer1.TabIndex = 0;
            // 
            // renderer
            // 
            renderer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            renderer.Dock = System.Windows.Forms.DockStyle.Fill;
            renderer.Location = new System.Drawing.Point(0, 0);
            renderer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            renderer.Name = "renderer";
            renderer.Size = new System.Drawing.Size(942, 638);
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
            infoPanel.Location = new System.Drawing.Point(0, 638);
            infoPanel.Name = "infoPanel";
            infoPanel.Size = new System.Drawing.Size(942, 23);
            infoPanel.TabIndex = 0;
            // 
            // cameraLabelValue
            // 
            cameraLabelValue.AutoSize = true;
            cameraLabelValue.Location = new System.Drawing.Point(186, 4);
            cameraLabelValue.Name = "cameraLabelValue";
            cameraLabelValue.Size = new System.Drawing.Size(37, 15);
            cameraLabelValue.TabIndex = 3;
            cameraLabelValue.Text = "0, 0, 0";
            // 
            // cameraLabelText
            // 
            cameraLabelText.AutoSize = true;
            cameraLabelText.Location = new System.Drawing.Point(83, 4);
            cameraLabelText.Name = "cameraLabelText";
            cameraLabelText.Size = new System.Drawing.Size(97, 15);
            cameraLabelText.TabIndex = 2;
            cameraLabelText.Text = "Camera Position:";
            // 
            // fpsLabelValue
            // 
            fpsLabelValue.AutoSize = true;
            fpsLabelValue.Location = new System.Drawing.Point(41, 3);
            fpsLabelValue.Name = "fpsLabelValue";
            fpsLabelValue.Size = new System.Drawing.Size(13, 15);
            fpsLabelValue.TabIndex = 1;
            fpsLabelValue.Text = "0";
            // 
            // fpsLabelText
            // 
            fpsLabelText.AutoSize = true;
            fpsLabelText.Location = new System.Drawing.Point(3, 3);
            fpsLabelText.Name = "fpsLabelText";
            fpsLabelText.Size = new System.Drawing.Size(32, 15);
            fpsLabelText.TabIndex = 0;
            fpsLabelText.Text = "FPS: ";
            // 
            // assetNameLabel
            // 
            assetNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            assetNameLabel.Location = new System.Drawing.Point(0, 0);
            assetNameLabel.Name = "assetNameLabel";
            assetNameLabel.Size = new System.Drawing.Size(942, 23);
            assetNameLabel.TabIndex = 4;
            assetNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
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
            splitContainer2.Size = new System.Drawing.Size(280, 661);
            splitContainer2.SplitterDistance = 238;
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
            easyEditPanel.Name = "easyEditPanel";
            easyEditPanel.Size = new System.Drawing.Size(280, 238);
            easyEditPanel.TabIndex = 0;
            // 
            // scaleGroupBox
            // 
            scaleGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            scaleGroupBox.Controls.Add(scaleVectorInputBox);
            scaleGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            scaleGroupBox.Location = new System.Drawing.Point(0, 194);
            scaleGroupBox.Name = "scaleGroupBox";
            scaleGroupBox.Size = new System.Drawing.Size(263, 97);
            scaleGroupBox.TabIndex = 2;
            scaleGroupBox.TabStop = false;
            scaleGroupBox.Text = "Scale";
            // 
            // scaleVectorInputBox
            // 
            scaleVectorInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            scaleVectorInputBox.Location = new System.Drawing.Point(3, 19);
            scaleVectorInputBox.Name = "scaleVectorInputBox";
            scaleVectorInputBox.SetVector3Callback = null;
            scaleVectorInputBox.Size = new System.Drawing.Size(257, 75);
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
            rotationGroupBox.Location = new System.Drawing.Point(0, 97);
            rotationGroupBox.Name = "rotationGroupBox";
            rotationGroupBox.Size = new System.Drawing.Size(263, 97);
            rotationGroupBox.TabIndex = 1;
            rotationGroupBox.TabStop = false;
            rotationGroupBox.Text = "Rotation";
            // 
            // rotationVectorInputBox
            // 
            rotationVectorInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            rotationVectorInputBox.Location = new System.Drawing.Point(3, 19);
            rotationVectorInputBox.Name = "rotationVectorInputBox";
            rotationVectorInputBox.SetVector3Callback = null;
            rotationVectorInputBox.Size = new System.Drawing.Size(257, 75);
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
            positionGroupBox.Name = "positionGroupBox";
            positionGroupBox.Size = new System.Drawing.Size(263, 97);
            positionGroupBox.TabIndex = 0;
            positionGroupBox.TabStop = false;
            positionGroupBox.Text = "Position";
            // 
            // positionVectorInputBox
            // 
            positionVectorInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            positionVectorInputBox.Location = new System.Drawing.Point(3, 19);
            positionVectorInputBox.Name = "positionVectorInputBox";
            positionVectorInputBox.SetVector3Callback = null;
            positionVectorInputBox.Size = new System.Drawing.Size(257, 75);
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
            editOptionsTabControl.Multiline = true;
            editOptionsTabControl.Name = "editOptionsTabControl";
            editOptionsTabControl.SelectedIndex = 0;
            editOptionsTabControl.Size = new System.Drawing.Size(280, 419);
            editOptionsTabControl.TabIndex = 2;
            // 
            // propertyGridTabPage
            // 
            propertyGridTabPage.Controls.Add(selectedPropertyGrid);
            propertyGridTabPage.Location = new System.Drawing.Point(4, 24);
            propertyGridTabPage.Name = "propertyGridTabPage";
            propertyGridTabPage.Padding = new System.Windows.Forms.Padding(3);
            propertyGridTabPage.Size = new System.Drawing.Size(272, 391);
            propertyGridTabPage.TabIndex = 0;
            propertyGridTabPage.Text = "Properties";
            propertyGridTabPage.UseVisualStyleBackColor = true;
            // 
            // selectedPropertyGrid
            // 
            selectedPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            selectedPropertyGrid.HelpVisible = false;
            selectedPropertyGrid.Location = new System.Drawing.Point(3, 3);
            selectedPropertyGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            selectedPropertyGrid.Name = "selectedPropertyGrid";
            selectedPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            selectedPropertyGrid.Size = new System.Drawing.Size(266, 385);
            selectedPropertyGrid.TabIndex = 0;
            selectedPropertyGrid.ToolbarVisible = false;
            selectedPropertyGrid.PropertyValueChanged += selectedPropertyGrid_PropertyValueChanged;
            // 
            // displaySettingsTabPage
            // 
            displaySettingsTabPage.AutoScroll = true;
            displaySettingsTabPage.Controls.Add(splitContainer3);
            displaySettingsTabPage.Location = new System.Drawing.Point(4, 24);
            displaySettingsTabPage.Name = "displaySettingsTabPage";
            displaySettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            displaySettingsTabPage.Size = new System.Drawing.Size(272, 391);
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
            containerTypesCheckedListBox.Location = new System.Drawing.Point(0, 0);
            containerTypesCheckedListBox.Name = "containerTypesCheckedListBox";
            containerTypesCheckedListBox.Size = new System.Drawing.Size(266, 189);
            containerTypesCheckedListBox.TabIndex = 0;
            containerTypesCheckedListBox.ItemCheck += containerTypesCheckedListBox_ItemCheck;
            // 
            // renderSettingsPropertyGrid
            // 
            renderSettingsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            renderSettingsPropertyGrid.HelpVisible = false;
            renderSettingsPropertyGrid.Location = new System.Drawing.Point(0, 0);
            renderSettingsPropertyGrid.Name = "renderSettingsPropertyGrid";
            renderSettingsPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            renderSettingsPropertyGrid.Size = new System.Drawing.Size(266, 192);
            renderSettingsPropertyGrid.TabIndex = 1;
            renderSettingsPropertyGrid.ToolbarVisible = false;
            // 
            // eventsTabPage
            // 
            eventsTabPage.Controls.Add(listBox1);
            eventsTabPage.Controls.Add(openLinkAssetButton);
            eventsTabPage.Location = new System.Drawing.Point(4, 24);
            eventsTabPage.Name = "eventsTabPage";
            eventsTabPage.Padding = new System.Windows.Forms.Padding(3);
            eventsTabPage.Size = new System.Drawing.Size(272, 391);
            eventsTabPage.TabIndex = 2;
            eventsTabPage.Text = "Events";
            eventsTabPage.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.IntegralHeight = false;
            listBox1.ItemHeight = 15;
            listBox1.Location = new System.Drawing.Point(3, 3);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(266, 362);
            listBox1.TabIndex = 1;
            // 
            // openLinkAssetButton
            // 
            openLinkAssetButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            openLinkAssetButton.Location = new System.Drawing.Point(3, 365);
            openLinkAssetButton.Name = "openLinkAssetButton";
            openLinkAssetButton.Size = new System.Drawing.Size(266, 23);
            openLinkAssetButton.TabIndex = 2;
            openLinkAssetButton.Text = "Open LinkAssetEditor";
            openLinkAssetButton.UseVisualStyleBackColor = true;
            openLinkAssetButton.Click += openLinkAssetButton_Click;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer3.Location = new System.Drawing.Point(3, 3);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(containerTypesCheckedListBox);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(renderSettingsPropertyGrid);
            splitContainer3.Size = new System.Drawing.Size(266, 385);
            splitContainer3.SplitterDistance = 189;
            splitContainer3.TabIndex = 2;
            // 
            // LevelEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Margin = new System.Windows.Forms.Padding(0);
            MinimumSize = new System.Drawing.Size(20, 20);
            Name = "LevelEditor";
            Size = new System.Drawing.Size(1226, 661);
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
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid selectedPropertyGrid;
        private System.Windows.Forms.SplitContainer splitContainer2;
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
        private System.Windows.Forms.PropertyGrid renderSettingsPropertyGrid;
        private System.Windows.Forms.SplitContainer splitContainer3;
    }
}