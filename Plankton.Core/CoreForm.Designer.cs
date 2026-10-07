namespace Plankton.Core
{
    partial class CoreForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoreForm));
            mainMenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            mainDockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            parentPanel = new Panel();
            editToolStripMenuItem = new ToolStripMenuItem();
            mainMenuStrip.SuspendLayout();
            parentPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.Font = new Font("Segoe UI", 9F);
            mainMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem });
            mainMenuStrip.Location = new Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.RenderMode = ToolStripRenderMode.System;
            mainMenuStrip.Size = new Size(1214, 24);
            mainMenuStrip.TabIndex = 0;
            mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // mainDockPanel
            // 
            mainDockPanel.Dock = DockStyle.Fill;
            mainDockPanel.DockBackColor = Color.FromArgb(238, 238, 242);
            mainDockPanel.Location = new Point(0, 0);
            mainDockPanel.Name = "mainDockPanel";
            mainDockPanel.ShowAutoHideContentOnHover = false;
            mainDockPanel.Size = new Size(1214, 608);
            mainDockPanel.TabIndex = 1;
            // 
            // parentPanel
            // 
            parentPanel.Controls.Add(mainDockPanel);
            parentPanel.Dock = DockStyle.Fill;
            parentPanel.Location = new Point(0, 24);
            parentPanel.Name = "parentPanel";
            parentPanel.Size = new Size(1214, 608);
            parentPanel.TabIndex = 2;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // CoreForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 632);
            Controls.Add(parentPanel);
            Controls.Add(mainMenuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mainMenuStrip;
            Name = "CoreForm";
            Text = "Plankton";
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            parentPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip mainMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel mainDockPanel;
        private Panel parentPanel;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
    }
}
