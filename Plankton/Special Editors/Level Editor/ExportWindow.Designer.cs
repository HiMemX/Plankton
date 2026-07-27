namespace Plankton.Special_Editors.Level_Editor
{
    partial class ExportWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportWindow));
            cancelButton = new System.Windows.Forms.Button();
            exportButton = new System.Windows.Forms.Button();
            selectedOnlyCheckbox = new System.Windows.Forms.CheckBox();
            applyTransformsCheckbox = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // cancelButton
            // 
            cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            cancelButton.Location = new System.Drawing.Point(12, 76);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(215, 29);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            exportButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            exportButton.Location = new System.Drawing.Point(235, 76);
            exportButton.Name = "exportButton";
            exportButton.Size = new System.Drawing.Size(215, 29);
            exportButton.TabIndex = 1;
            exportButton.Text = "Export";
            exportButton.UseVisualStyleBackColor = true;
            exportButton.Click += this.exportButton_Click;
            // 
            // selectedOnlyCheckbox
            // 
            selectedOnlyCheckbox.AutoSize = true;
            selectedOnlyCheckbox.Checked = true;
            selectedOnlyCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            selectedOnlyCheckbox.Location = new System.Drawing.Point(12, 12);
            selectedOnlyCheckbox.Name = "selectedOnlyCheckbox";
            selectedOnlyCheckbox.Size = new System.Drawing.Size(122, 24);
            selectedOnlyCheckbox.TabIndex = 2;
            selectedOnlyCheckbox.Text = "Selected Only";
            selectedOnlyCheckbox.UseVisualStyleBackColor = true;
            // 
            // applyTransformsCheckbox
            // 
            applyTransformsCheckbox.AutoSize = true;
            applyTransformsCheckbox.Location = new System.Drawing.Point(12, 42);
            applyTransformsCheckbox.Name = "applyTransformsCheckbox";
            applyTransformsCheckbox.Size = new System.Drawing.Size(146, 24);
            applyTransformsCheckbox.TabIndex = 3;
            applyTransformsCheckbox.Text = "Apply Transforms";
            applyTransformsCheckbox.UseVisualStyleBackColor = true;
            // 
            // ExportWindow
            // 
            AcceptButton = exportButton;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(462, 117);
            Controls.Add(applyTransformsCheckbox);
            Controls.Add(selectedOnlyCheckbox);
            Controls.Add(exportButton);
            Controls.Add(cancelButton);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExportWindow";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Export...";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.CheckBox selectedOnlyCheckbox;
        private System.Windows.Forms.CheckBox applyTransformsCheckbox;
    }
}