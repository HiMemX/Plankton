namespace Plankton.InternalEditors
{
    partial class DirectEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectEditorWindow));
            grpAssetBytesBox = new System.Windows.Forms.GroupBox();
            byteViewerBox = new Be.Windows.Forms.HexBox();
            grpOptions = new System.Windows.Forms.GroupBox();
            btnSaveBytes = new System.Windows.Forms.Button();
            grpAssetBytesBox.SuspendLayout();
            grpOptions.SuspendLayout();
            SuspendLayout();
            // 
            // grpAssetBytesBox
            // 
            grpAssetBytesBox.Controls.Add(byteViewerBox);
            grpAssetBytesBox.Location = new System.Drawing.Point(12, 12);
            grpAssetBytesBox.Name = "grpAssetBytesBox";
            grpAssetBytesBox.Size = new System.Drawing.Size(1098, 661);
            grpAssetBytesBox.TabIndex = 0;
            grpAssetBytesBox.TabStop = false;
            grpAssetBytesBox.Text = "Directly Edit Asset Bytes";
            // 
            // byteViewerBox
            // 
            byteViewerBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            byteViewerBox.Location = new System.Drawing.Point(6, 26);
            byteViewerBox.Name = "byteViewerBox";
            byteViewerBox.SelectionBackColor = System.Drawing.Color.Lime;
            byteViewerBox.ShadowSelectionColor = System.Drawing.Color.Cyan;
            byteViewerBox.Size = new System.Drawing.Size(1086, 629);
            byteViewerBox.StringViewVisible = true;
            byteViewerBox.TabIndex = 0;
            byteViewerBox.VScrollBarVisible = true;
            // 
            // grpOptions
            // 
            grpOptions.Controls.Add(btnSaveBytes);
            grpOptions.Location = new System.Drawing.Point(1116, 12);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new System.Drawing.Size(187, 661);
            grpOptions.TabIndex = 1;
            grpOptions.TabStop = false;
            grpOptions.Text = "Options";
            // 
            // btnSaveBytes
            // 
            btnSaveBytes.Location = new System.Drawing.Point(6, 26);
            btnSaveBytes.Name = "btnSaveBytes";
            btnSaveBytes.Size = new System.Drawing.Size(175, 52);
            btnSaveBytes.TabIndex = 0;
            btnSaveBytes.Text = "Save Edited Bytes";
            btnSaveBytes.UseVisualStyleBackColor = true;
            btnSaveBytes.Click += btnSaveBytes_Click;
            // 
            // DirectEditorWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1315, 685);
            Controls.Add(grpOptions);
            Controls.Add(grpAssetBytesBox);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "DirectEditorWindow";
            Text = "DirectEditor";
            Load += DirectEditorWindow_Load;
            grpAssetBytesBox.ResumeLayout(false);
            grpOptions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpAssetBytesBox;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Button btnSaveBytes;
        private Be.Windows.Forms.HexBox byteViewerBox;
    }
}