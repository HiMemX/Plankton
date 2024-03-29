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
            this.label1 = new System.Windows.Forms.Label();
            this.minFilterComboBox = new System.Windows.Forms.ComboBox();
            this.magFilterComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // wrapSComboBox
            // 
            this.wrapSComboBox.FormattingEnabled = true;
            this.wrapSComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
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
            this.okButton.Location = new System.Drawing.Point(12, 128);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(235, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "MinFilter";
            // 
            // minFilterComboBox
            // 
            this.minFilterComboBox.FormattingEnabled = true;
            this.minFilterComboBox.Items.AddRange(new object[] {
            "Nearest",
            "Linear",
            "Nearest_Mipmap_Nearest",
            "Linear_Mipmap_Nearest",
            "Nearest_Mipmap_Linear",
            "Linear_Mipmap_Linear"});
            this.minFilterComboBox.Location = new System.Drawing.Point(93, 70);
            this.minFilterComboBox.Name = "minFilterComboBox";
            this.minFilterComboBox.Size = new System.Drawing.Size(154, 23);
            this.minFilterComboBox.TabIndex = 6;
            // 
            // magFilterComboBox
            // 
            this.magFilterComboBox.FormattingEnabled = true;
            this.magFilterComboBox.Items.AddRange(new object[] {
            "Nearest",
            "Linear"});
            this.magFilterComboBox.Location = new System.Drawing.Point(93, 99);
            this.magFilterComboBox.Name = "magFilterComboBox";
            this.magFilterComboBox.Size = new System.Drawing.Size(154, 23);
            this.magFilterComboBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "MagFilter";
            // 
            // TextureEditorImportPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 162);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.magFilterComboBox);
            this.Controls.Add(this.minFilterComboBox);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox minFilterComboBox;
        public System.Windows.Forms.ComboBox magFilterComboBox;
        private System.Windows.Forms.Label label2;
    }
}