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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // selectButton
            // 
            selectButton.DialogResult = DialogResult.OK;
            selectButton.Dock = DockStyle.Fill;
            selectButton.Enabled = false;
            selectButton.Location = new Point(193, 3);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(185, 25);
            selectButton.TabIndex = 0;
            selectButton.Text = "Select";
            selectButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Dock = DockStyle.Fill;
            cancelButton.Location = new Point(3, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(184, 25);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // tocTreeView
            // 
            tocTreeView.Dock = DockStyle.Fill;
            tocTreeView.HideSelection = false;
            tocTreeView.Location = new Point(0, 0);
            tocTreeView.Name = "tocTreeView";
            tocTreeView.Size = new Size(381, 306);
            tocTreeView.TabIndex = 2;
            tocTreeView.AfterSelect += tocTreeView_AfterSelect;
            tocTreeView.MouseDoubleClick += tocTreeView_MouseDoubleClick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(cancelButton, 0, 0);
            tableLayoutPanel1.Controls.Add(selectButton, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(0, 306);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(381, 31);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // SelectParcelTOCDialog
            // 
            AcceptButton = selectButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(381, 337);
            Controls.Add(tocTreeView);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectParcelTOCDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select ParcelTOC";
            Load += SelectParcelTOCDialog_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button selectButton;
        private Button cancelButton;
        private TreeView tocTreeView;
        private TableLayoutPanel tableLayoutPanel1;
    }
}