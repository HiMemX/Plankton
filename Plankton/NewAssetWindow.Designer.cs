namespace Plankton
{
    partial class NewAssetWindow
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
            this.components = new System.ComponentModel.Container();
            this.blobAlignTextBox = new System.Windows.Forms.TextBox();
            this.uidSelfTextBox = new System.Windows.Forms.TextBox();
            this.subTypeTextBox = new System.Windows.Forms.TextBox();
            this.blobFlagsTextBox = new System.Windows.Forms.TextBox();
            this.dataTextBox = new System.Windows.Forms.TextBox();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.addAssetButton = new System.Windows.Forms.Button();
            this.blobAlignLabel = new System.Windows.Forms.Label();
            this.uidSelfLabel = new System.Windows.Forms.Label();
            this.wmlTypeIDLabel = new System.Windows.Forms.Label();
            this.subTypeLabel = new System.Windows.Forms.Label();
            this.blobFlagsLabel = new System.Windows.Forms.Label();
            this.dataLabel = new System.Windows.Forms.Label();
            this.wmlTypeIDComboBox = new System.Windows.Forms.ComboBox();
            this.assetKeyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.assetKeyBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.assetKeyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.assetKeyBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // blobAlignTextBox
            // 
            this.blobAlignTextBox.Location = new System.Drawing.Point(97, 12);
            this.blobAlignTextBox.Name = "blobAlignTextBox";
            this.blobAlignTextBox.Size = new System.Drawing.Size(232, 23);
            this.blobAlignTextBox.TabIndex = 0;
            // 
            // uidSelfTextBox
            // 
            this.uidSelfTextBox.Location = new System.Drawing.Point(97, 41);
            this.uidSelfTextBox.Name = "uidSelfTextBox";
            this.uidSelfTextBox.Size = new System.Drawing.Size(232, 23);
            this.uidSelfTextBox.TabIndex = 1;
            this.uidSelfTextBox.TextChanged += new System.EventHandler(this.uidSelfTextBox_TextChanged);
            // 
            // subTypeTextBox
            // 
            this.subTypeTextBox.Location = new System.Drawing.Point(97, 99);
            this.subTypeTextBox.Name = "subTypeTextBox";
            this.subTypeTextBox.Size = new System.Drawing.Size(232, 23);
            this.subTypeTextBox.TabIndex = 3;
            // 
            // blobFlagsTextBox
            // 
            this.blobFlagsTextBox.Location = new System.Drawing.Point(97, 128);
            this.blobFlagsTextBox.Name = "blobFlagsTextBox";
            this.blobFlagsTextBox.Size = new System.Drawing.Size(232, 23);
            this.blobFlagsTextBox.TabIndex = 4;
            // 
            // dataTextBox
            // 
            this.dataTextBox.Location = new System.Drawing.Point(178, 185);
            this.dataTextBox.Name = "dataTextBox";
            this.dataTextBox.Size = new System.Drawing.Size(151, 23);
            this.dataTextBox.TabIndex = 5;
            // 
            // selectFileButton
            // 
            this.selectFileButton.Location = new System.Drawing.Point(97, 185);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(75, 23);
            this.selectFileButton.TabIndex = 6;
            this.selectFileButton.Text = "Select File";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // addAssetButton
            // 
            this.addAssetButton.Location = new System.Drawing.Point(12, 243);
            this.addAssetButton.Name = "addAssetButton";
            this.addAssetButton.Size = new System.Drawing.Size(317, 23);
            this.addAssetButton.TabIndex = 7;
            this.addAssetButton.Text = "Add Asset";
            this.addAssetButton.UseVisualStyleBackColor = true;
            this.addAssetButton.Click += new System.EventHandler(this.addAssetButton_Click);
            // 
            // blobAlignLabel
            // 
            this.blobAlignLabel.AutoSize = true;
            this.blobAlignLabel.Location = new System.Drawing.Point(12, 15);
            this.blobAlignLabel.Name = "blobAlignLabel";
            this.blobAlignLabel.Size = new System.Drawing.Size(58, 15);
            this.blobAlignLabel.TabIndex = 8;
            this.blobAlignLabel.Text = "blobAlign";
            // 
            // uidSelfLabel
            // 
            this.uidSelfLabel.AutoSize = true;
            this.uidSelfLabel.Location = new System.Drawing.Point(12, 44);
            this.uidSelfLabel.Name = "uidSelfLabel";
            this.uidSelfLabel.Size = new System.Drawing.Size(45, 15);
            this.uidSelfLabel.TabIndex = 9;
            this.uidSelfLabel.Text = "uidSelf";
            // 
            // wmlTypeIDLabel
            // 
            this.wmlTypeIDLabel.AutoSize = true;
            this.wmlTypeIDLabel.Location = new System.Drawing.Point(12, 73);
            this.wmlTypeIDLabel.Name = "wmlTypeIDLabel";
            this.wmlTypeIDLabel.Size = new System.Drawing.Size(67, 15);
            this.wmlTypeIDLabel.TabIndex = 10;
            this.wmlTypeIDLabel.Text = "wmlTypeID";
            // 
            // subTypeLabel
            // 
            this.subTypeLabel.AutoSize = true;
            this.subTypeLabel.Location = new System.Drawing.Point(12, 102);
            this.subTypeLabel.Name = "subTypeLabel";
            this.subTypeLabel.Size = new System.Drawing.Size(53, 15);
            this.subTypeLabel.TabIndex = 11;
            this.subTypeLabel.Text = "subType";
            // 
            // blobFlagsLabel
            // 
            this.blobFlagsLabel.AutoSize = true;
            this.blobFlagsLabel.Location = new System.Drawing.Point(12, 131);
            this.blobFlagsLabel.Name = "blobFlagsLabel";
            this.blobFlagsLabel.Size = new System.Drawing.Size(62, 15);
            this.blobFlagsLabel.TabIndex = 12;
            this.blobFlagsLabel.Text = "blobFlags";
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Location = new System.Drawing.Point(12, 189);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(33, 15);
            this.dataLabel.TabIndex = 13;
            this.dataLabel.Text = "Data";
            // 
            // wmlTypeIDComboBox
            // 
            this.wmlTypeIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wmlTypeIDComboBox.FormattingEnabled = true;
            this.wmlTypeIDComboBox.Location = new System.Drawing.Point(97, 70);
            this.wmlTypeIDComboBox.Name = "wmlTypeIDComboBox";
            this.wmlTypeIDComboBox.Size = new System.Drawing.Size(232, 23);
            this.wmlTypeIDComboBox.TabIndex = 14;
            // 
            // assetKeyBindingSource
            // 
            // 
            // assetKeyBindingSource1
            // 
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(97, 214);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(232, 23);
            this.nameTextBox.TabIndex = 15;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 217);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(41, 15);
            this.nameLabel.TabIndex = 16;
            this.nameLabel.Text = "Name";
            // 
            // NewAssetWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 278);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.wmlTypeIDComboBox);
            this.Controls.Add(this.dataLabel);
            this.Controls.Add(this.blobFlagsLabel);
            this.Controls.Add(this.subTypeLabel);
            this.Controls.Add(this.wmlTypeIDLabel);
            this.Controls.Add(this.uidSelfLabel);
            this.Controls.Add(this.blobAlignLabel);
            this.Controls.Add(this.addAssetButton);
            this.Controls.Add(this.selectFileButton);
            this.Controls.Add(this.dataTextBox);
            this.Controls.Add(this.blobFlagsTextBox);
            this.Controls.Add(this.subTypeTextBox);
            this.Controls.Add(this.uidSelfTextBox);
            this.Controls.Add(this.blobAlignTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewAssetWindow";
            this.ShowIcon = false;
            this.Text = "New Asset";
            ((System.ComponentModel.ISupportInitialize)(this.assetKeyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.assetKeyBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox blobAlignTextBox;
        private System.Windows.Forms.TextBox subTypeTextBox;
        private System.Windows.Forms.TextBox blobFlagsTextBox;
        private System.Windows.Forms.TextBox dataTextBox;
        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.Button addAssetButton;
        private System.Windows.Forms.Label blobAlignLabel;
        private System.Windows.Forms.Label uidSelfLabel;
        private System.Windows.Forms.Label wmlTypeIDLabel;
        private System.Windows.Forms.Label subTypeLabel;
        private System.Windows.Forms.Label blobFlagsLabel;
        private System.Windows.Forms.Label dataLabel;
        private System.Windows.Forms.ComboBox wmlTypeIDComboBox;
        private System.Windows.Forms.BindingSource assetKeyBindingSource;
        private System.Windows.Forms.BindingSource assetKeyBindingSource1;
        public System.Windows.Forms.TextBox uidSelfTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
    }
}