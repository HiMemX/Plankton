namespace Plankton.Special_Editors
{
    partial class ScriptEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptEditor));
            eventPreviewListBox = new System.Windows.Forms.ListBox();
            eventSettingsGroupBox = new System.Windows.Forms.GroupBox();
            delayTextBox = new System.Windows.Forms.TextBox();
            delayLabel = new System.Windows.Forms.Label();
            targetAssetLabel = new System.Windows.Forms.Label();
            targetAssetTextBox = new System.Windows.Forms.TextBox();
            eventTypeComboBox = new System.Windows.Forms.ComboBox();
            deleteButton = new System.Windows.Forms.Button();
            newButton = new System.Windows.Forms.Button();
            upButton = new System.Windows.Forms.Button();
            downButton = new System.Windows.Forms.Button();
            hexEditorPanel = new System.Windows.Forms.Panel();
            eventSettingsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // eventPreviewListBox
            // 
            eventPreviewListBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            eventPreviewListBox.Font = new System.Drawing.Font("Courier New", 9F);
            eventPreviewListBox.FormattingEnabled = true;
            eventPreviewListBox.HorizontalScrollbar = true;
            eventPreviewListBox.IntegralHeight = false;
            eventPreviewListBox.ItemHeight = 15;
            eventPreviewListBox.Location = new System.Drawing.Point(12, 12);
            eventPreviewListBox.Name = "eventPreviewListBox";
            eventPreviewListBox.Size = new System.Drawing.Size(431, 431);
            eventPreviewListBox.TabIndex = 1;
            eventPreviewListBox.SelectedIndexChanged += eventPreviewListBox_SelectedIndexChanged;
            // 
            // eventSettingsGroupBox
            // 
            eventSettingsGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            eventSettingsGroupBox.Controls.Add(delayTextBox);
            eventSettingsGroupBox.Controls.Add(delayLabel);
            eventSettingsGroupBox.Controls.Add(targetAssetLabel);
            eventSettingsGroupBox.Controls.Add(targetAssetTextBox);
            eventSettingsGroupBox.Controls.Add(eventTypeComboBox);
            eventSettingsGroupBox.Location = new System.Drawing.Point(548, 12);
            eventSettingsGroupBox.Name = "eventSettingsGroupBox";
            eventSettingsGroupBox.Size = new System.Drawing.Size(313, 117);
            eventSettingsGroupBox.TabIndex = 2;
            eventSettingsGroupBox.TabStop = false;
            eventSettingsGroupBox.Text = "Event Settings";
            // 
            // delayTextBox
            // 
            delayTextBox.Location = new System.Drawing.Point(86, 80);
            delayTextBox.Name = "delayTextBox";
            delayTextBox.Size = new System.Drawing.Size(221, 23);
            delayTextBox.TabIndex = 4;
            delayTextBox.Validating += delayTextBox_Validating;
            // 
            // delayLabel
            // 
            delayLabel.AutoSize = true;
            delayLabel.Location = new System.Drawing.Point(42, 83);
            delayLabel.Name = "delayLabel";
            delayLabel.Size = new System.Drawing.Size(36, 15);
            delayLabel.TabIndex = 3;
            delayLabel.Text = "Delay";
            // 
            // targetAssetLabel
            // 
            targetAssetLabel.AutoSize = true;
            targetAssetLabel.Location = new System.Drawing.Point(6, 54);
            targetAssetLabel.Name = "targetAssetLabel";
            targetAssetLabel.Size = new System.Drawing.Size(70, 15);
            targetAssetLabel.TabIndex = 2;
            targetAssetLabel.Text = "Target Asset";
            // 
            // targetAssetTextBox
            // 
            targetAssetTextBox.Location = new System.Drawing.Point(86, 51);
            targetAssetTextBox.Name = "targetAssetTextBox";
            targetAssetTextBox.Size = new System.Drawing.Size(221, 23);
            targetAssetTextBox.TabIndex = 1;
            targetAssetTextBox.Validating += targetAssetTextBox_Validating;
            // 
            // eventTypeComboBox
            // 
            eventTypeComboBox.AllowDrop = true;
            eventTypeComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            eventTypeComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            eventTypeComboBox.FormattingEnabled = true;
            eventTypeComboBox.Location = new System.Drawing.Point(6, 22);
            eventTypeComboBox.Name = "eventTypeComboBox";
            eventTypeComboBox.Size = new System.Drawing.Size(301, 23);
            eventTypeComboBox.Sorted = true;
            eventTypeComboBox.TabIndex = 0;
            eventTypeComboBox.SelectedIndexChanged += eventTypeComboBox_SelectedIndexChanged;
            // 
            // deleteButton
            // 
            deleteButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            deleteButton.BackColor = System.Drawing.Color.Red;
            deleteButton.Location = new System.Drawing.Point(519, 70);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new System.Drawing.Size(23, 23);
            deleteButton.TabIndex = 4;
            deleteButton.Text = "X";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += deleteButton_Click;
            // 
            // newButton
            // 
            newButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            newButton.Location = new System.Drawing.Point(449, 70);
            newButton.Name = "newButton";
            newButton.Size = new System.Drawing.Size(42, 23);
            newButton.TabIndex = 5;
            newButton.Text = "New";
            newButton.UseVisualStyleBackColor = true;
            newButton.Click += newButton_Click;
            // 
            // upButton
            // 
            upButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            upButton.BackColor = System.Drawing.SystemColors.ControlLight;
            upButton.Location = new System.Drawing.Point(449, 12);
            upButton.Name = "upButton";
            upButton.Size = new System.Drawing.Size(93, 23);
            upButton.TabIndex = 6;
            upButton.Text = "Move Up";
            upButton.UseVisualStyleBackColor = false;
            upButton.Click += upButton_Click;
            // 
            // downButton
            // 
            downButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            downButton.BackColor = System.Drawing.SystemColors.ControlLight;
            downButton.Location = new System.Drawing.Point(449, 41);
            downButton.Name = "downButton";
            downButton.Size = new System.Drawing.Size(93, 23);
            downButton.TabIndex = 7;
            downButton.Text = "Move Down";
            downButton.UseVisualStyleBackColor = false;
            downButton.Click += downButton_Click;
            // 
            // hexEditorPanel
            // 
            hexEditorPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            hexEditorPanel.Location = new System.Drawing.Point(473, 149);
            hexEditorPanel.Name = "hexEditorPanel";
            hexEditorPanel.Size = new System.Drawing.Size(388, 294);
            hexEditorPanel.TabIndex = 8;
            // 
            // ScriptEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(873, 455);
            Controls.Add(hexEditorPanel);
            Controls.Add(downButton);
            Controls.Add(upButton);
            Controls.Add(newButton);
            Controls.Add(deleteButton);
            Controls.Add(eventSettingsGroupBox);
            Controls.Add(eventPreviewListBox);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(700, 350);
            Name = "ScriptEditor";
            Text = "Script Editor";
            FormClosing += ScriptEditor_FormClosing;
            Load += ScriptEditor_Load;
            Leave += ScriptEditor_Leave;
            eventSettingsGroupBox.ResumeLayout(false);
            eventSettingsGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ListBox eventPreviewListBox;
        private System.Windows.Forms.GroupBox eventSettingsGroupBox;
        private System.Windows.Forms.ComboBox eventTypeComboBox;
        private System.Windows.Forms.TextBox delayTextBox;
        private System.Windows.Forms.Label delayLabel;
        private System.Windows.Forms.Label targetAssetLabel;
        private System.Windows.Forms.TextBox targetAssetTextBox;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Panel hexEditorPanel;
    }
}