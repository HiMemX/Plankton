namespace Plankton
{
    partial class EditHeaderWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditHeaderWindow));
            this.editHeaderPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // editHeaderPropertyGrid
            // 
            this.editHeaderPropertyGrid.Location = new System.Drawing.Point(12, 12);
            this.editHeaderPropertyGrid.Name = "editHeaderPropertyGrid";
            this.editHeaderPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.editHeaderPropertyGrid.Size = new System.Drawing.Size(591, 632);
            this.editHeaderPropertyGrid.TabIndex = 0;
            this.editHeaderPropertyGrid.ToolbarVisible = false;
            // 
            // EditHeaderWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 656);
            this.Controls.Add(this.editHeaderPropertyGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditHeaderWindow";
            this.Text = "Edit Header";
            this.Load += new System.EventHandler(this.EditHeaderWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PropertyGrid editHeaderPropertyGrid;
    }
}