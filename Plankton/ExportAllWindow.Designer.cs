namespace Plankton
{
    partial class ExportAllWindow
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
            this.differentiateParcelsCheck = new System.Windows.Forms.CheckBox();
            this.differentiateTablesCheck = new System.Windows.Forms.CheckBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // differentiateParcelsCheck
            // 
            this.differentiateParcelsCheck.AutoSize = true;
            this.differentiateParcelsCheck.Location = new System.Drawing.Point(12, 12);
            this.differentiateParcelsCheck.Name = "differentiateParcelsCheck";
            this.differentiateParcelsCheck.Size = new System.Drawing.Size(137, 19);
            this.differentiateParcelsCheck.TabIndex = 0;
            this.differentiateParcelsCheck.Text = "Differentiate Parcels";
            this.differentiateParcelsCheck.UseVisualStyleBackColor = true;
            // 
            // differentiateTablesCheck
            // 
            this.differentiateTablesCheck.AutoSize = true;
            this.differentiateTablesCheck.Location = new System.Drawing.Point(155, 12);
            this.differentiateTablesCheck.Name = "differentiateTablesCheck";
            this.differentiateTablesCheck.Size = new System.Drawing.Size(132, 19);
            this.differentiateTablesCheck.TabIndex = 1;
            this.differentiateTablesCheck.Text = "Differentiate Tables";
            this.differentiateTablesCheck.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(12, 45);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(275, 23);
            this.exportButton.TabIndex = 2;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click_1);
            // 
            // ExportAllWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 80);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.differentiateTablesCheck);
            this.Controls.Add(this.differentiateParcelsCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExportAllWindow";
            this.Text = "Export All";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox differentiateParcelsCheck;
        private System.Windows.Forms.CheckBox differentiateTablesCheck;
        private System.Windows.Forms.Button exportButton;
    }
}