namespace Plankton
{
    partial class Plankton
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Plankton));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCTRLOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArchiveTreeGroup = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            this.ArchiveTreeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(732, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCTRLOToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openCTRLOToolStripMenuItem
            // 
            this.openCTRLOToolStripMenuItem.Name = "openCTRLOToolStripMenuItem";
            this.openCTRLOToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openCTRLOToolStripMenuItem.Text = "Open... [CTRL+O]";
            this.openCTRLOToolStripMenuItem.Click += new System.EventHandler(this.openCTRLOToolStripMenuItem_Click);
            // 
            // ArchiveTreeGroup
            // 
            this.ArchiveTreeGroup.Controls.Add(this.treeView1);
            this.ArchiveTreeGroup.Location = new System.Drawing.Point(12, 27);
            this.ArchiveTreeGroup.Name = "ArchiveTreeGroup";
            this.ArchiveTreeGroup.Size = new System.Drawing.Size(544, 692);
            this.ArchiveTreeGroup.TabIndex = 1;
            this.ArchiveTreeGroup.TabStop = false;
            this.ArchiveTreeGroup.Text = "Archive Tree";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(6, 19);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(532, 667);
            this.treeView1.TabIndex = 0;
            // 
            // Plankton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(732, 731);
            this.Controls.Add(this.ArchiveTreeGroup);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Plankton";
            this.Text = "Plankton";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ArchiveTreeGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCTRLOToolStripMenuItem;
        private System.Windows.Forms.GroupBox ArchiveTreeGroup;
        private System.Windows.Forms.TreeView treeView1;
    }
}

