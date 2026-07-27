namespace Plankton
{
    partial class SearchWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchWindow));
            this.assetListBox = new System.Windows.Forms.ListBox();
            this.uidLabel = new System.Windows.Forms.Label();
            this.filterLabel = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // assetListBox
            // 
            this.assetListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.assetListBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assetListBox.FormattingEnabled = true;
            this.assetListBox.IntegralHeight = false;
            this.assetListBox.ItemHeight = 20;
            this.assetListBox.Location = new System.Drawing.Point(0, 28);
            this.assetListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.assetListBox.Name = "assetListBox";
            this.assetListBox.Size = new System.Drawing.Size(550, 547);
            this.assetListBox.TabIndex = 0;
            this.assetListBox.SelectedIndexChanged += new System.EventHandler(this.assetListBox_SelectedIndexChanged);
            // 
            // uidLabel
            // 
            this.uidLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uidLabel.Location = new System.Drawing.Point(383, 580);
            this.uidLabel.Name = "uidLabel";
            this.uidLabel.Size = new System.Drawing.Size(168, 20);
            this.uidLabel.TabIndex = 1;
            this.uidLabel.Text = "This message shouldn\'t appear";
            this.uidLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // filterLabel
            // 
            this.filterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterLabel.Location = new System.Drawing.Point(14, 4);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(186, 20);
            this.filterLabel.TabIndex = 2;
            this.filterLabel.Text = "wmlTypeID Filter:";
            // 
            // typeComboBox
            // 
            this.typeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.typeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.typeComboBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(134, 0);
            this.typeComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(417, 28);
            this.typeComboBox.TabIndex = 3;
            this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.typeComboBox_SelectedIndexChanged);
            // 
            // SearchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 600);
            this.Controls.Add(this.assetListBox);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.filterLabel);
            this.Controls.Add(this.uidLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SearchWindow";
            this.Text = "Search";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox assetListBox;
        private System.Windows.Forms.Label uidLabel;
        private System.Windows.Forms.Label filterLabel;
        private System.Windows.Forms.ComboBox typeComboBox;
    }
}