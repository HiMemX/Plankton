namespace Plankton.Special_Editors.Model_Editor
{
    partial class ModelEditor
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
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            geometryBaseRenderer = new Plankton.Custom_Controls.GeometryBaseRenderer();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(geometryBaseRenderer);
            splitContainer1.Size = new System.Drawing.Size(1139, 610);
            splitContainer1.SplitterDistance = 800;
            splitContainer1.TabIndex = 0;
            // 
            // geometryBaseRenderer
            // 
            geometryBaseRenderer.Dock = System.Windows.Forms.DockStyle.Fill;
            geometryBaseRenderer.Location = new System.Drawing.Point(0, 0);
            geometryBaseRenderer.Name = "geometryBaseRenderer";
            geometryBaseRenderer.Size = new System.Drawing.Size(800, 610);
            geometryBaseRenderer.TabIndex = 0;
            // 
            // ModelEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Name = "ModelEditor";
            Size = new System.Drawing.Size(1139, 610);
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Custom_Controls.GeometryBaseRenderer geometryBaseRenderer;
    }
}
