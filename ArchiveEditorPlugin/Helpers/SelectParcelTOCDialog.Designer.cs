namespace ArchiveEditorPlugin.Helpers
{
    partial class SelectParcelTOCDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectParcelTOCDialog));
            selectButton = new Button();
            cancelButton = new Button();
            tocTreeView = new TreeView();
            SuspendLayout();
            // 
            // selectButton
            // 
            selectButton.DialogResult = DialogResult.OK;
            selectButton.Dock = DockStyle.Right;
            selectButton.Enabled = false;
            selectButton.Location = new Point(190, 314);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(191, 23);
            selectButton.TabIndex = 0;
            selectButton.Text = "Select";
            selectButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Dock = DockStyle.Fill;
            cancelButton.Location = new Point(0, 314);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(190, 23);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // tocTreeView
            // 
            tocTreeView.Dock = DockStyle.Top;
            tocTreeView.HideSelection = false;
            tocTreeView.Location = new Point(0, 0);
            tocTreeView.Name = "tocTreeView";
            tocTreeView.Size = new Size(381, 314);
            tocTreeView.TabIndex = 2;
            tocTreeView.AfterSelect += tocTreeView_AfterSelect;
            tocTreeView.MouseDoubleClick += tocTreeView_MouseDoubleClick;
            // 
            // SelectParcelTOCDialog
            // 
            AcceptButton = selectButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(381, 337);
            Controls.Add(cancelButton);
            Controls.Add(selectButton);
            Controls.Add(tocTreeView);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectParcelTOCDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select ParcelTOC";
            Load += SelectParcelTOCDialog_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button selectButton;
        private Button cancelButton;
        private TreeView tocTreeView;
    }
}