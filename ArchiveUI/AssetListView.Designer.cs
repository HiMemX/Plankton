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
            treeListView = new BrightIdeasSoftware.FastObjectListView();
            ((System.ComponentModel.ISupportInitialize)treeListView).BeginInit();
            SuspendLayout();
            // 
            // treeListView
            // 
            treeListView.Dock = DockStyle.Fill;
            treeListView.Location = new Point(0, 0);
            treeListView.Name = "treeListView";
            treeListView.ShowGroups = false;
            treeListView.Size = new Size(360, 336);
            treeListView.TabIndex = 0;
            treeListView.View = View.Details;
            treeListView.VirtualMode = true;
            // 
            // AssetListView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(treeListView);
            Name = "AssetListView";
            Size = new Size(360, 336);
            ((System.ComponentModel.ISupportInitialize)treeListView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView treeListView;
    }
}
