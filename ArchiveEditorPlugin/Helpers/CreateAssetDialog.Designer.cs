namespace ArchiveEditorPlugin.Helpers
{
    partial class CreateAssetDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAssetDialog));
            cancelButton = new Button();
            selectButton = new Button();
            optionsPanel = new Panel();
            openPathButton = new Button();
            wmlTypeIDLabel = new Label();
            uidLabel = new Label();
            pathTextBox = new TextBox();
            typeComboBox = new ComboBox();
            uidTextBox = new TextBox();
            buttonsLayoutpanel = new TableLayoutPanel();
            optionsPanel.SuspendLayout();
            buttonsLayoutpanel.SuspendLayout();
            SuspendLayout();
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Dock = DockStyle.Bottom;
            cancelButton.Location = new Point(3, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(143, 25);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // selectButton
            // 
            selectButton.DialogResult = DialogResult.OK;
            selectButton.Dock = DockStyle.Right;
            selectButton.Enabled = false;
            selectButton.Location = new Point(152, 3);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(144, 25);
            selectButton.TabIndex = 2;
            selectButton.Text = "Create";
            selectButton.UseVisualStyleBackColor = true;
            selectButton.Click += selectButton_Click;
            // 
            // optionsPanel
            // 
            optionsPanel.Controls.Add(openPathButton);
            optionsPanel.Controls.Add(wmlTypeIDLabel);
            optionsPanel.Controls.Add(uidLabel);
            optionsPanel.Controls.Add(pathTextBox);
            optionsPanel.Controls.Add(typeComboBox);
            optionsPanel.Controls.Add(uidTextBox);
            optionsPanel.Dock = DockStyle.Fill;
            optionsPanel.Location = new Point(0, 0);
            optionsPanel.Name = "optionsPanel";
            optionsPanel.Size = new Size(299, 104);
            optionsPanel.TabIndex = 4;
            // 
            // openPathButton
            // 
            openPathButton.Location = new Point(12, 69);
            openPathButton.Name = "openPathButton";
            openPathButton.Size = new Size(75, 23);
            openPathButton.TabIndex = 5;
            openPathButton.Text = "Open File";
            openPathButton.UseVisualStyleBackColor = true;
            openPathButton.Click += openPathButton_Click;
            // 
            // wmlTypeIDLabel
            // 
            wmlTypeIDLabel.AutoSize = true;
            wmlTypeIDLabel.Location = new Point(56, 44);
            wmlTypeIDLabel.Name = "wmlTypeIDLabel";
            wmlTypeIDLabel.Size = new Size(31, 15);
            wmlTypeIDLabel.TabIndex = 4;
            wmlTypeIDLabel.Text = "Type";
            // 
            // uidLabel
            // 
            uidLabel.AutoSize = true;
            uidLabel.Location = new Point(61, 15);
            uidLabel.Name = "uidLabel";
            uidLabel.Size = new Size(26, 15);
            uidLabel.TabIndex = 3;
            uidLabel.Text = "UID";
            // 
            // pathTextBox
            // 
            pathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pathTextBox.Location = new Point(95, 70);
            pathTextBox.Name = "pathTextBox";
            pathTextBox.Size = new Size(193, 23);
            pathTextBox.TabIndex = 2;
            pathTextBox.TextChanged += pathTextBox_TextChanged;
            // 
            // typeComboBox
            // 
            typeComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            typeComboBox.FormattingEnabled = true;
            typeComboBox.Location = new Point(95, 41);
            typeComboBox.Name = "typeComboBox";
            typeComboBox.Size = new Size(193, 23);
            typeComboBox.TabIndex = 1;
            // 
            // uidTextBox
            // 
            uidTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            uidTextBox.Location = new Point(95, 12);
            uidTextBox.Name = "uidTextBox";
            uidTextBox.Size = new Size(193, 23);
            uidTextBox.TabIndex = 0;
            uidTextBox.KeyPress += uidTextBox_KeyPress;
            // 
            // buttonsLayoutpanel
            // 
            buttonsLayoutpanel.ColumnCount = 2;
            buttonsLayoutpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonsLayoutpanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonsLayoutpanel.Controls.Add(cancelButton, 0, 0);
            buttonsLayoutpanel.Controls.Add(selectButton, 1, 0);
            buttonsLayoutpanel.Dock = DockStyle.Bottom;
            buttonsLayoutpanel.Location = new Point(0, 104);
            buttonsLayoutpanel.Name = "buttonsLayoutpanel";
            buttonsLayoutpanel.RowCount = 1;
            buttonsLayoutpanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonsLayoutpanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonsLayoutpanel.Size = new Size(299, 31);
            buttonsLayoutpanel.TabIndex = 0;
            // 
            // CreateAssetDialog
            // 
            AcceptButton = selectButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(299, 135);
            Controls.Add(optionsPanel);
            Controls.Add(buttonsLayoutpanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreateAssetDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Create Asset";
            optionsPanel.ResumeLayout(false);
            optionsPanel.PerformLayout();
            buttonsLayoutpanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button cancelButton;
        private Button selectButton;
        private Panel optionsPanel;
        private TableLayoutPanel buttonsLayoutpanel;
        private TextBox pathTextBox;
        private ComboBox typeComboBox;
        private TextBox uidTextBox;
        private Button openPathButton;
        private Label wmlTypeIDLabel;
        private Label uidLabel;
    }
}