namespace Plankton
{
    partial class DebugWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugWindow));
            debugListBox = new System.Windows.Forms.ListBox();
            SuspendLayout();
            // 
            // debugListBox
            // 
            debugListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            debugListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            debugListBox.FormattingEnabled = true;
            debugListBox.ItemHeight = 20;
            debugListBox.Location = new System.Drawing.Point(0, 0);
            debugListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            debugListBox.Name = "debugListBox";
            debugListBox.Size = new System.Drawing.Size(850, 600);
            debugListBox.TabIndex = 0;
            debugListBox.DrawItem += debugListBox_DrawItem;
            // 
            // DebugWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(850, 600);
            Controls.Add(debugListBox);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "DebugWindow";
            Text = "DebugWindow";
            FormClosing += DebugWindow_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox debugListBox;
    }
}