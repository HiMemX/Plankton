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
            imageModeComboBox = new System.Windows.Forms.ComboBox();
            optionsGroupBox = new System.Windows.Forms.GroupBox();
            rotateCButton = new System.Windows.Forms.Button();
            rotateCCButton = new System.Windows.Forms.Button();
            mirrorHButton = new System.Windows.Forms.Button();
            mirrorVButton = new System.Windows.Forms.Button();
            importButton = new System.Windows.Forms.Button();
            exportButton = new System.Windows.Forms.Button();
            updateLabel = new System.Windows.Forms.Label();
            updateCheckBox = new System.Windows.Forms.CheckBox();
            imageModeLabel = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            optionsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // imageModeComboBox
            // 
            imageModeComboBox.DisplayMember = "0";
            imageModeComboBox.FormattingEnabled = true;
            imageModeComboBox.Items.AddRange(new object[] { "Color + Alpha", "Color", "Alpha" });
            imageModeComboBox.Location = new System.Drawing.Point(100, 22);
            imageModeComboBox.Name = "imageModeComboBox";
            imageModeComboBox.Size = new System.Drawing.Size(98, 23);
            imageModeComboBox.TabIndex = 1;
            imageModeComboBox.Tag = "";
            imageModeComboBox.SelectedIndexChanged += imageModeComboBox_SelectedIndexChanged;
            // 
            // optionsGroupBox
            // 
            optionsGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            optionsGroupBox.Controls.Add(rotateCButton);
            optionsGroupBox.Controls.Add(rotateCCButton);
            optionsGroupBox.Controls.Add(mirrorHButton);
            optionsGroupBox.Controls.Add(mirrorVButton);
            optionsGroupBox.Controls.Add(importButton);
            optionsGroupBox.Controls.Add(exportButton);
            optionsGroupBox.Controls.Add(updateLabel);
            optionsGroupBox.Controls.Add(updateCheckBox);
            optionsGroupBox.Controls.Add(imageModeLabel);
            optionsGroupBox.Controls.Add(imageModeComboBox);
            optionsGroupBox.Location = new System.Drawing.Point(419, 12);
            optionsGroupBox.Name = "optionsGroupBox";
            optionsGroupBox.Size = new System.Drawing.Size(204, 400);
            optionsGroupBox.TabIndex = 2;
            optionsGroupBox.TabStop = false;
            optionsGroupBox.Text = "Options";
            // 
            // rotateCButton
            // 
            rotateCButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            rotateCButton.Location = new System.Drawing.Point(105, 226);
            rotateCButton.Name = "rotateCButton";
            rotateCButton.Size = new System.Drawing.Size(93, 23);
            rotateCButton.TabIndex = 11;
            rotateCButton.Text = "Rotate 90° R";
            rotateCButton.UseVisualStyleBackColor = true;
            rotateCButton.Click += rotateCButton_Click;
            // 
            // rotateCCButton
            // 
            rotateCCButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            rotateCCButton.Location = new System.Drawing.Point(6, 226);
            rotateCCButton.Name = "rotateCCButton";
            rotateCCButton.Size = new System.Drawing.Size(93, 23);
            rotateCCButton.TabIndex = 10;
            rotateCCButton.Text = "Rotate 90° L";
            rotateCCButton.UseVisualStyleBackColor = true;
            rotateCCButton.Click += rotateCCButton_Click;
            // 
            // mirrorHButton
            // 
            mirrorHButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            mirrorHButton.Location = new System.Drawing.Point(6, 255);
            mirrorHButton.Name = "mirrorHButton";
            mirrorHButton.Size = new System.Drawing.Size(192, 23);
            mirrorHButton.TabIndex = 9;
            mirrorHButton.Text = "Mirror horizontally |";
            mirrorHButton.UseVisualStyleBackColor = true;
            mirrorHButton.Click += mirrorHButton_Click;
            // 
            // mirrorVButton
            // 
            mirrorVButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            mirrorVButton.Location = new System.Drawing.Point(6, 284);
            mirrorVButton.Name = "mirrorVButton";
            mirrorVButton.Size = new System.Drawing.Size(192, 23);
            mirrorVButton.TabIndex = 8;
            mirrorVButton.Text = "Mirror vertically -";
            mirrorVButton.UseVisualStyleBackColor = true;
            mirrorVButton.Click += mirrorVButton_Click;
            // 
            // importButton
            // 
            importButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            importButton.Location = new System.Drawing.Point(6, 342);
            importButton.Name = "importButton";
            importButton.Size = new System.Drawing.Size(192, 23);
            importButton.TabIndex = 6;
            importButton.Text = "Import Texture";
            importButton.UseVisualStyleBackColor = true;
            importButton.Click += importButton_Click;
            // 
            // exportButton
            // 
            exportButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            exportButton.Location = new System.Drawing.Point(6, 371);
            exportButton.Name = "exportButton";
            exportButton.Size = new System.Drawing.Size(192, 23);
            exportButton.TabIndex = 5;
            exportButton.Text = "Export Texture";
            exportButton.UseVisualStyleBackColor = true;
            exportButton.Click += exportButton_Click;
            // 
            // updateLabel
            // 
            updateLabel.AutoSize = true;
            updateLabel.Location = new System.Drawing.Point(6, 50);
            updateLabel.Name = "updateLabel";
            updateLabel.Size = new System.Drawing.Size(86, 15);
            updateLabel.TabIndex = 4;
            updateLabel.Text = "Update Texture";
            // 
            // updateCheckBox
            // 
            updateCheckBox.AutoSize = true;
            updateCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            updateCheckBox.Checked = true;
            updateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            updateCheckBox.Location = new System.Drawing.Point(100, 51);
            updateCheckBox.Name = "updateCheckBox";
            updateCheckBox.Size = new System.Drawing.Size(15, 14);
            updateCheckBox.TabIndex = 3;
            updateCheckBox.UseVisualStyleBackColor = true;
            // 
            // imageModeLabel
            // 
            imageModeLabel.AutoSize = true;
            imageModeLabel.Location = new System.Drawing.Point(6, 25);
            imageModeLabel.Name = "imageModeLabel";
            imageModeLabel.Size = new System.Drawing.Size(79, 15);
            imageModeLabel.TabIndex = 2;
            imageModeLabel.Text = "Display Mode";
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.Location = new System.Drawing.Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(401, 401);
            panel1.TabIndex = 3;
            // 
            // TextureEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(635, 425);
            Controls.Add(panel1);
            Controls.Add(optionsGroupBox);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(400, 350);
            Name = "TextureEditor";
            Text = "Texture Editor";
            FormClosing += TextureEditor_FormClosing;
            optionsGroupBox.ResumeLayout(false);
            optionsGroupBox.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Panel panel1;
    }
}