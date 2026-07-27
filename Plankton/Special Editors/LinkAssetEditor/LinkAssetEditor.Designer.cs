namespace Plankton.Special_Editors
{
    partial class LinkAssetEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkAssetEditorWindow));
            eventsEventsplitContainer = new System.Windows.Forms.SplitContainer();
            eventsListBox = new System.Windows.Forms.ListBox();
            hexEditPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)eventsEventsplitContainer).BeginInit();
            eventsEventsplitContainer.Panel1.SuspendLayout();
            eventsEventsplitContainer.Panel2.SuspendLayout();
            eventsEventsplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // eventsEventsplitContainer
            // 
            eventsEventsplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            eventsEventsplitContainer.Location = new System.Drawing.Point(0, 0);
            eventsEventsplitContainer.Name = "eventsEventsplitContainer";
            // 
            // eventsEventsplitContainer.Panel1
            // 
            eventsEventsplitContainer.Panel1.Controls.Add(eventsListBox);
            // 
            // eventsEventsplitContainer.Panel2
            // 
            eventsEventsplitContainer.Panel2.Controls.Add(hexEditPanel);
            eventsEventsplitContainer.Size = new System.Drawing.Size(850, 450);
            eventsEventsplitContainer.SplitterDistance = 425;
            eventsEventsplitContainer.TabIndex = 0;
            // 
            // eventsListBox
            // 
            eventsListBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            eventsListBox.FormattingEnabled = true;
            eventsListBox.ItemHeight = 15;
            eventsListBox.Location = new System.Drawing.Point(12, 12);
            eventsListBox.Name = "eventsListBox";
            eventsListBox.Size = new System.Drawing.Size(410, 424);
            eventsListBox.TabIndex = 0;
            // 
            // hexEditPanel
            // 
            hexEditPanel.Location = new System.Drawing.Point(3, 204);
            hexEditPanel.Name = "hexEditPanel";
            hexEditPanel.Size = new System.Drawing.Size(406, 232);
            hexEditPanel.TabIndex = 0;
            // 
            // LinkAssetEditorWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(850, 450);
            Controls.Add(eventsEventsplitContainer);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "LinkAssetEditorWindow";
            Text = "LinkAssetEditor";
            eventsEventsplitContainer.Panel1.ResumeLayout(false);
            eventsEventsplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)eventsEventsplitContainer).EndInit();
            eventsEventsplitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer eventsEventsplitContainer;
        private System.Windows.Forms.ListBox eventsListBox;
        private System.Windows.Forms.Panel hexEditPanel;
    }
}