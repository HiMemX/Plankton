namespace Plankton.Special_Editors
{
    partial class TextureEditorImportPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureEditorImportPrompt));
            this.wrapSComboBox = new System.Windows.Forms.ComboBox();
            this.wrapSModeLabel = new System.Windows.Forms.Label();
            this.wrapTComboBox = new System.Windows.Forms.ComboBox();
            this.wrapTModeLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.imageScaleCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // wrapSComboBox
            // 
            this.wrapSComboBox.FormattingEnabled = true;
            this.wrapSComboBox.Items.AddRange(new object[] {
            "Clamp",
            "Repeat",
            "Mirror"});
            this.wrapSComboBox.Location = new System.Drawing.Point(93, 12);
            this.wrapSComboBox.Name = "wrapSComboBox";
            this.wrapSComboBox.Size = new System.Drawing.Size(154, 23);
            this.wrapSComboBox.TabIndex = 0;
            // 
            // wrapSModeLabel
            // 
            this.wrapSModeLabel.AutoSize = true;
            this.wrapSModeLabel.Location = new System.Drawing.Point(12, 20);
            this.wrapSModeLabel.Name = "wrapSModeLabel";
            this.wrapSModeLabel.Size = new System.Drawing.Size(75, 15);
            this.wrapSModeLabel.TabIndex = 1;
            this.wrapSModeLabel.Text = "UWrapMode";
            // 
            // wrapTComboBox
            // 
            this.wrapTComboBox.FormattingEnabled = true;
            this.wrapTComboBox.Items.AddRange(new object[] {
            "Clamp",
            "Repeat",
            "Mirror"});
            this.wrapTComboBox.Location = new System.Drawing.Point(93, 41);
            this.wrapTComboBox.Name = "wrapTComboBox";
            this.wrapTComboBox.Size = new System.Drawing.Size(154, 23);
            this.wrapTComboBox.TabIndex = 2;
            // 
            // wrapTModeLabel
            // 
            this.wrapTModeLabel.AutoSize = true;
            this.wrapTModeLabel.Location = new System.Drawing.Point(12, 49);
            this.wrapTModeLabel.Name = "wrapTModeLabel";
            this.wrapTModeLabel.Size = new System.Drawing.Size(73, 15);
            this.wrapTModeLabel.TabIndex = 3;
            this.wrapTModeLabel.Text = "VWrapMode";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 96);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(235, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // imageScaleCheckbox
            // 
            this.imageScaleCheckbox.AutoSize = true;
            this.imageScaleCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.imageScaleCheckbox.Checked = true;
            this.imageScaleCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.imageScaleCheckbox.Location = new System.Drawing.Point(12, 71);
            this.imageScaleCheckbox.Name = "imageScaleCheckbox";
            this.imageScaleCheckbox.Size = new System.Drawing.Size(94, 19);
            this.imageScaleCheckbox.TabIndex = 5;
            this.imageScaleCheckbox.Text = "Limit Size     ";
            this.imageScaleCheckbox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.imageScaleCheckbox.UseVisualStyleBackColor = true;
            // 
            // TextureEditorImportPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 131);
            this.Controls.Add(this.imageScaleCheckbox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.wrapTModeLabel);
            this.Controls.Add(this.wrapTComboBox);
            this.Controls.Add(this.wrapSModeLabel);
            this.Controls.Add(this.wrapSComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextureEditorImportPrompt";
            this.Text = "Import Texture";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label wrapSModeLabel;
        private System.Windows.Forms.Label wrapTModeLabel;
        private System.Windows.Forms.Button okButton;
        public System.Windows.Forms.ComboBox wrapSComboBox;
        public System.Windows.Forms.ComboBox wrapTComboBox;
        public System.Windows.Forms.CheckBox imageScaleCheckbox;
    }
}