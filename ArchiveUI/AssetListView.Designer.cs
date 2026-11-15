namespace ArchiveUI
{
    partial class AssetListView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            treeListView = new BrightIdeasSoftware.ObjectListView();
            contextMenuStrip = new ContextMenuStrip(components);
            ((System.ComponentModel.ISupportInitialize)treeListView).BeginInit();
            SuspendLayout();
            // 
            // treeListView
            // 
            treeListView.ContextMenuStrip = contextMenuStrip;
            treeListView.Dock = DockStyle.Fill;
            treeListView.FullRowSelect = true;
            treeListView.Location = new Point(0, 0);
            treeListView.Name = "treeListView";
            treeListView.OwnerDraw = false;
            treeListView.Size = new Size(315, 252);
            treeListView.TabIndex = 0;
            treeListView.UseExplorerTheme = true;
            treeListView.View = View.Details;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(61, 4);
            // 
            // AssetListView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(treeListView);
            Margin = new Padding(3, 2, 3, 2);
            Name = "AssetListView";
            Size = new Size(315, 252);
            ((System.ComponentModel.ISupportInitialize)treeListView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private BrightIdeasSoftware.ObjectListView treeListView;
        private ContextMenuStrip contextMenuStrip;
    }
}
