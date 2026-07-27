namespace Plankton
{
    partial class PropertyGridWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyGridWindow));
            propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            assetIDLabel = new System.Windows.Forms.Label();
            checkBox1 = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // propertyGrid1
            // 
            propertyGrid1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            propertyGrid1.HelpVisible = false;
            propertyGrid1.Location = new System.Drawing.Point(0, 0);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            propertyGrid1.Size = new System.Drawing.Size(403, 461);
            propertyGrid1.TabIndex = 0;
            propertyGrid1.ToolbarVisible = false;
            propertyGrid1.PropertyValueChanged += propertyGrid1_PropertyValueChanged;
            // 
            // assetIDLabel
            // 
            assetIDLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            assetIDLabel.AutoSize = true;
            assetIDLabel.BackColor = System.Drawing.SystemColors.Control;
            assetIDLabel.Location = new System.Drawing.Point(5, 464);
            assetIDLabel.Name = "assetIDLabel";
            assetIDLabel.Size = new System.Drawing.Size(176, 15);
            assetIDLabel.TabIndex = 1;
            assetIDLabel.Text = "This message should not appear";
            assetIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkBox1.AutoSize = true;
            checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            checkBox1.Location = new System.Drawing.Point(287, 464);
            checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(104, 19);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Always On Top";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // PropertyGridWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(403, 485);
            Controls.Add(checkBox1);
            Controls.Add(assetIDLabel);
            Controls.Add(propertyGrid1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "PropertyGridWindow";
            Text = "PropertyGridWindow";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.PropertyGrid propertyGrid1;
        public System.Windows.Forms.Label assetIDLabel;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}