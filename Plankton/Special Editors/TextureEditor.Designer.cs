namespace Plankton.Special_Editors
{
    partial class TextureEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureEditor));
            this.textureViewer = new System.Windows.Forms.PictureBox();
            this.imageModeComboBox = new System.Windows.Forms.ComboBox();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.rotateCButton = new System.Windows.Forms.Button();
            this.rotateCCButton = new System.Windows.Forms.Button();
            this.mirrorHButton = new System.Windows.Forms.Button();
            this.mirrorVButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.updateLabel = new System.Windows.Forms.Label();
            this.updateCheckBox = new System.Windows.Forms.CheckBox();
            this.imageModeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.textureViewer)).BeginInit();
            this.optionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // textureViewer
            // 
            this.textureViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textureViewer.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textureViewer.Location = new System.Drawing.Point(12, 12);
            this.textureViewer.Name = "textureViewer";
            this.textureViewer.Size = new System.Drawing.Size(400, 400);
            this.textureViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.textureViewer.TabIndex = 0;
            this.textureViewer.TabStop = false;
            // 
            // imageModeComboBox
            // 
            this.imageModeComboBox.DisplayMember = "0";
            this.imageModeComboBox.FormattingEnabled = true;
            this.imageModeComboBox.Items.AddRange(new object[] {
            "Color + Alpha",
            "Color",
            "Alpha"});
            this.imageModeComboBox.Location = new System.Drawing.Point(100, 22);
            this.imageModeComboBox.Name = "imageModeComboBox";
            this.imageModeComboBox.Size = new System.Drawing.Size(98, 23);
            this.imageModeComboBox.TabIndex = 1;
            this.imageModeComboBox.Tag = "";
            this.imageModeComboBox.SelectedIndexChanged += new System.EventHandler(this.imageModeComboBox_SelectedIndexChanged);
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsGroupBox.Controls.Add(this.rotateCButton);
            this.optionsGroupBox.Controls.Add(this.rotateCCButton);
            this.optionsGroupBox.Controls.Add(this.mirrorHButton);
            this.optionsGroupBox.Controls.Add(this.mirrorVButton);
            this.optionsGroupBox.Controls.Add(this.importButton);
            this.optionsGroupBox.Controls.Add(this.exportButton);
            this.optionsGroupBox.Controls.Add(this.updateLabel);
            this.optionsGroupBox.Controls.Add(this.updateCheckBox);
            this.optionsGroupBox.Controls.Add(this.imageModeLabel);
            this.optionsGroupBox.Controls.Add(this.imageModeComboBox);
            this.optionsGroupBox.Location = new System.Drawing.Point(419, 12);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(204, 400);
            this.optionsGroupBox.TabIndex = 2;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // rotateCButton
            // 
            this.rotateCButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.rotateCButton.Location = new System.Drawing.Point(105, 226);
            this.rotateCButton.Name = "rotateCButton";
            this.rotateCButton.Size = new System.Drawing.Size(93, 23);
            this.rotateCButton.TabIndex = 11;
            this.rotateCButton.Text = "Rotate 90° R";
            this.rotateCButton.UseVisualStyleBackColor = true;
            this.rotateCButton.Click += new System.EventHandler(this.rotateCButton_Click);
            // 
            // rotateCCButton
            // 
            this.rotateCCButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.rotateCCButton.Location = new System.Drawing.Point(6, 226);
            this.rotateCCButton.Name = "rotateCCButton";
            this.rotateCCButton.Size = new System.Drawing.Size(93, 23);
            this.rotateCCButton.TabIndex = 10;
            this.rotateCCButton.Text = "Rotate 90° L";
            this.rotateCCButton.UseVisualStyleBackColor = true;
            this.rotateCCButton.Click += new System.EventHandler(this.rotateCCButton_Click);
            // 
            // mirrorHButton
            // 
            this.mirrorHButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mirrorHButton.Location = new System.Drawing.Point(6, 255);
            this.mirrorHButton.Name = "mirrorHButton";
            this.mirrorHButton.Size = new System.Drawing.Size(192, 23);
            this.mirrorHButton.TabIndex = 9;
            this.mirrorHButton.Text = "Mirror horizontally |";
            this.mirrorHButton.UseVisualStyleBackColor = true;
            this.mirrorHButton.Click += new System.EventHandler(this.mirrorHButton_Click);
            // 
            // mirrorVButton
            // 
            this.mirrorVButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mirrorVButton.Location = new System.Drawing.Point(6, 284);
            this.mirrorVButton.Name = "mirrorVButton";
            this.mirrorVButton.Size = new System.Drawing.Size(192, 23);
            this.mirrorVButton.TabIndex = 8;
            this.mirrorVButton.Text = "Mirror vertically -";
            this.mirrorVButton.UseVisualStyleBackColor = true;
            this.mirrorVButton.Click += new System.EventHandler(this.mirrorVButton_Click);
            // 
            // importButton
            // 
            this.importButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.importButton.Location = new System.Drawing.Point(6, 342);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(192, 23);
            this.importButton.TabIndex = 6;
            this.importButton.Text = "Import Texture";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.exportButton.Location = new System.Drawing.Point(6, 371);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(192, 23);
            this.exportButton.TabIndex = 5;
            this.exportButton.Text = "Export Texture";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // updateLabel
            // 
            this.updateLabel.AutoSize = true;
            this.updateLabel.Location = new System.Drawing.Point(6, 50);
            this.updateLabel.Name = "updateLabel";
            this.updateLabel.Size = new System.Drawing.Size(89, 15);
            this.updateLabel.TabIndex = 4;
            this.updateLabel.Text = "Update Texture";
            // 
            // updateCheckBox
            // 
            this.updateCheckBox.AutoSize = true;
            this.updateCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.updateCheckBox.Checked = true;
            this.updateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.updateCheckBox.Location = new System.Drawing.Point(100, 51);
            this.updateCheckBox.Name = "updateCheckBox";
            this.updateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.updateCheckBox.TabIndex = 3;
            this.updateCheckBox.UseVisualStyleBackColor = true;
            // 
            // imageModeLabel
            // 
            this.imageModeLabel.AutoSize = true;
            this.imageModeLabel.Location = new System.Drawing.Point(6, 25);
            this.imageModeLabel.Name = "imageModeLabel";
            this.imageModeLabel.Size = new System.Drawing.Size(81, 15);
            this.imageModeLabel.TabIndex = 2;
            this.imageModeLabel.Text = "Display Mode";
            // 
            // TextureEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 425);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.textureViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 350);
            this.Name = "TextureEditor";
            this.Text = "Texture Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextureEditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.textureViewer)).EndInit();
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox textureViewer;
        private System.Windows.Forms.ComboBox imageModeComboBox;
        private System.Windows.Forms.GroupBox optionsGroupBox;
        private System.Windows.Forms.Label imageModeLabel;
        private System.Windows.Forms.CheckBox updateCheckBox;
        private System.Windows.Forms.Label updateLabel;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button rotateCButton;
        private System.Windows.Forms.Button rotateCCButton;
        private System.Windows.Forms.Button mirrorHButton;
        private System.Windows.Forms.Button mirrorVButton;
    }
}