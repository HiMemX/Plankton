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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Plankton));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCTRLOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFromCTRLSHIFTOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCTRLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsCTRLSHIFTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArchiveTreeGroup = new System.Windows.Forms.GroupBox();
            this.archiveView = new System.Windows.Forms.TreeView();
            this.assetGroupBox = new System.Windows.Forms.GroupBox();
            this.renameAssetTextBox = new System.Windows.Forms.TextBox();
            this.deleteAssetButton = new System.Windows.Forms.Button();
            this.importRawDataButton = new System.Windows.Forms.Button();
            this.exportRawDataButton = new System.Windows.Forms.Button();
            this.assetPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.assetTreeNodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.assetTreeNodeBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tOCEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tOCEntryBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tOCEntryBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.parcelGroupBox = new System.Windows.Forms.GroupBox();
            this.deleteParcelButton = new System.Windows.Forms.Button();
            this.newParcelTOCButton = new System.Windows.Forms.Button();
            this.parcelPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.parcelTOCGroupBox = new System.Windows.Forms.GroupBox();
            this.newAssetButton = new System.Windows.Forms.Button();
            this.deleteParcelTOCButton = new System.Windows.Forms.Button();
            this.tableGroupBox = new System.Windows.Forms.GroupBox();
            this.newParcelButton = new System.Windows.Forms.Button();
            this.stringTableGroupBox = new System.Windows.Forms.GroupBox();
            this.deleteStringTableEntryButton = new System.Windows.Forms.Button();
            this.newStringTableEntryButton = new System.Windows.Forms.Button();
            this.stringTableEntryTextBox = new System.Windows.Forms.TextBox();
            this.domainStringLabel = new System.Windows.Forms.Label();
            this.stringTableListBox = new System.Windows.Forms.ListBox();
            this.domainStringTextBox = new System.Windows.Forms.TextBox();
            this.tableHeaderPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.deleteTableButton = new System.Windows.Forms.Button();
            this.tablePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.searchGroupBox = new System.Windows.Forms.GroupBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.searchForDataButton = new System.Windows.Forms.Button();
            this.searchForNameButton = new System.Windows.Forms.Button();
            this.searchAssetIDButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.ArchiveTreeGroup.SuspendLayout();
            this.assetGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.assetTreeNodeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.assetTreeNodeBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tOCEntryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tOCEntryBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tOCEntryBindingSource2)).BeginInit();
            this.parcelGroupBox.SuspendLayout();
            this.parcelTOCGroupBox.SuspendLayout();
            this.tableGroupBox.SuspendLayout();
            this.stringTableGroupBox.SuspendLayout();
            this.searchGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1130, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCTRLOToolStripMenuItem,
            this.newFromCTRLSHIFTOToolStripMenuItem,
            this.saveCTRLSToolStripMenuItem,
            this.saveAsCTRLSHIFTSToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openCTRLOToolStripMenuItem
            // 
            this.openCTRLOToolStripMenuItem.Name = "openCTRLOToolStripMenuItem";
            this.openCTRLOToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.openCTRLOToolStripMenuItem.Text = "Open... [CTRL+O]";
            this.openCTRLOToolStripMenuItem.Click += new System.EventHandler(this.openCTRLOToolStripMenuItem_Click);
            // 
            // newFromCTRLSHIFTOToolStripMenuItem
            // 
            this.newFromCTRLSHIFTOToolStripMenuItem.Name = "newFromCTRLSHIFTOToolStripMenuItem";
            this.newFromCTRLSHIFTOToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.newFromCTRLSHIFTOToolStripMenuItem.Text = "New From... [CTRL+SHIFT+O]";
            this.newFromCTRLSHIFTOToolStripMenuItem.Click += new System.EventHandler(this.newFromCTRLSHIFTOToolStripMenuItem_Click);
            // 
            // saveCTRLSToolStripMenuItem
            // 
            this.saveCTRLSToolStripMenuItem.Enabled = false;
            this.saveCTRLSToolStripMenuItem.Name = "saveCTRLSToolStripMenuItem";
            this.saveCTRLSToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.saveCTRLSToolStripMenuItem.Text = "Save... [CTRL+S]";
            this.saveCTRLSToolStripMenuItem.Click += new System.EventHandler(this.saveCTRLSToolStripMenuItem_Click);
            // 
            // saveAsCTRLSHIFTSToolStripMenuItem
            // 
            this.saveAsCTRLSHIFTSToolStripMenuItem.Enabled = false;
            this.saveAsCTRLSHIFTSToolStripMenuItem.Name = "saveAsCTRLSHIFTSToolStripMenuItem";
            this.saveAsCTRLSHIFTSToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.saveAsCTRLSHIFTSToolStripMenuItem.Text = "Save As... [CTRL+SHIFT+S]";
            this.saveAsCTRLSHIFTSToolStripMenuItem.Click += new System.EventHandler(this.saveAsCTRLSHIFTSToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Enabled = false;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.closeToolStripMenuItem.Text = "Close...";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editHeaderToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // editHeaderToolStripMenuItem
            // 
            this.editHeaderToolStripMenuItem.Name = "editHeaderToolStripMenuItem";
            this.editHeaderToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.editHeaderToolStripMenuItem.Text = "Edit Header [CTRL+H]";
            this.editHeaderToolStripMenuItem.Click += new System.EventHandler(this.editHeaderToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadTreeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.Click += new System.EventHandler(this.toolsToolStripMenuItem_Click);
            // 
            // reloadTreeToolStripMenuItem
            // 
            this.reloadTreeToolStripMenuItem.Name = "reloadTreeToolStripMenuItem";
            this.reloadTreeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.reloadTreeToolStripMenuItem.Text = "Reload tree [CTRL+R]";
            this.reloadTreeToolStripMenuItem.Click += new System.EventHandler(this.reloadTreeToolStripMenuItem_Click);
            // 
            // ArchiveTreeGroup
            // 
            this.ArchiveTreeGroup.Controls.Add(this.archiveView);
            this.ArchiveTreeGroup.Location = new System.Drawing.Point(13, 31);
            this.ArchiveTreeGroup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ArchiveTreeGroup.Name = "ArchiveTreeGroup";
            this.ArchiveTreeGroup.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ArchiveTreeGroup.Size = new System.Drawing.Size(787, 827);
            this.ArchiveTreeGroup.TabIndex = 1;
            this.ArchiveTreeGroup.TabStop = false;
            this.ArchiveTreeGroup.Text = "Archive Tree";
            // 
            // archiveView
            // 
            this.archiveView.Location = new System.Drawing.Point(7, 22);
            this.archiveView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.archiveView.Name = "archiveView";
            this.archiveView.Size = new System.Drawing.Size(772, 798);
            this.archiveView.TabIndex = 0;
            this.archiveView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.archiveView_AfterSelect);
            // 
            // assetGroupBox
            // 
            this.assetGroupBox.Controls.Add(this.renameAssetTextBox);
            this.assetGroupBox.Controls.Add(this.deleteAssetButton);
            this.assetGroupBox.Controls.Add(this.importRawDataButton);
            this.assetGroupBox.Controls.Add(this.exportRawDataButton);
            this.assetGroupBox.Controls.Add(this.assetPropertyGrid);
            this.assetGroupBox.Location = new System.Drawing.Point(807, 31);
            this.assetGroupBox.Name = "assetGroupBox";
            this.assetGroupBox.Size = new System.Drawing.Size(311, 827);
            this.assetGroupBox.TabIndex = 2;
            this.assetGroupBox.TabStop = false;
            this.assetGroupBox.Text = "Asset Options";
            this.assetGroupBox.Visible = false;
            // 
            // renameAssetTextBox
            // 
            this.renameAssetTextBox.Location = new System.Drawing.Point(6, 183);
            this.renameAssetTextBox.Name = "renameAssetTextBox";
            this.renameAssetTextBox.Size = new System.Drawing.Size(299, 23);
            this.renameAssetTextBox.TabIndex = 7;
            this.renameAssetTextBox.TextChanged += new System.EventHandler(this.renameAssetTextBox_TextChanged);
            // 
            // deleteAssetButton
            // 
            this.deleteAssetButton.BackColor = System.Drawing.Color.Red;
            this.deleteAssetButton.Location = new System.Drawing.Point(6, 797);
            this.deleteAssetButton.Name = "deleteAssetButton";
            this.deleteAssetButton.Size = new System.Drawing.Size(299, 23);
            this.deleteAssetButton.TabIndex = 3;
            this.deleteAssetButton.Text = "Delete Asset";
            this.deleteAssetButton.UseVisualStyleBackColor = false;
            this.deleteAssetButton.Click += new System.EventHandler(this.deleteAssetButton_Click);
            // 
            // importRawDataButton
            // 
            this.importRawDataButton.Location = new System.Drawing.Point(158, 212);
            this.importRawDataButton.Name = "importRawDataButton";
            this.importRawDataButton.Size = new System.Drawing.Size(147, 23);
            this.importRawDataButton.TabIndex = 2;
            this.importRawDataButton.Text = "Import Raw Data";
            this.importRawDataButton.UseVisualStyleBackColor = true;
            this.importRawDataButton.Click += new System.EventHandler(this.importRawDataButton_Click);
            // 
            // exportRawDataButton
            // 
            this.exportRawDataButton.Location = new System.Drawing.Point(6, 212);
            this.exportRawDataButton.Name = "exportRawDataButton";
            this.exportRawDataButton.Size = new System.Drawing.Size(147, 23);
            this.exportRawDataButton.TabIndex = 1;
            this.exportRawDataButton.Text = "Export Raw Data";
            this.exportRawDataButton.UseVisualStyleBackColor = true;
            this.exportRawDataButton.Click += new System.EventHandler(this.exportRawDataButton_Click);
            // 
            // assetPropertyGrid
            // 
            this.assetPropertyGrid.BackColor = System.Drawing.SystemColors.Control;
            this.assetPropertyGrid.HelpVisible = false;
            this.assetPropertyGrid.Location = new System.Drawing.Point(6, 22);
            this.assetPropertyGrid.Name = "assetPropertyGrid";
            this.assetPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.assetPropertyGrid.Size = new System.Drawing.Size(299, 155);
            this.assetPropertyGrid.TabIndex = 0;
            this.assetPropertyGrid.ToolbarVisible = false;
            // 
            // tOCEntryBindingSource
            // 
            this.tOCEntryBindingSource.DataSource = typeof(HoArchive.TOCEntry);
            // 
            // tOCEntryBindingSource1
            // 
            this.tOCEntryBindingSource1.DataSource = typeof(HoArchive.TOCEntry);
            // 
            // tOCEntryBindingSource2
            // 
            this.tOCEntryBindingSource2.DataSource = typeof(HoArchive.TOCEntry);
            // 
            // parcelGroupBox
            // 
            this.parcelGroupBox.Controls.Add(this.deleteParcelButton);
            this.parcelGroupBox.Controls.Add(this.newParcelTOCButton);
            this.parcelGroupBox.Controls.Add(this.parcelPropertyGrid);
            this.parcelGroupBox.Location = new System.Drawing.Point(807, 31);
            this.parcelGroupBox.Name = "parcelGroupBox";
            this.parcelGroupBox.Size = new System.Drawing.Size(311, 827);
            this.parcelGroupBox.TabIndex = 3;
            this.parcelGroupBox.TabStop = false;
            this.parcelGroupBox.Text = "Parcel Options";
            this.parcelGroupBox.Visible = false;
            // 
            // deleteParcelButton
            // 
            this.deleteParcelButton.BackColor = System.Drawing.Color.Red;
            this.deleteParcelButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deleteParcelButton.Location = new System.Drawing.Point(6, 797);
            this.deleteParcelButton.Name = "deleteParcelButton";
            this.deleteParcelButton.Size = new System.Drawing.Size(299, 23);
            this.deleteParcelButton.TabIndex = 2;
            this.deleteParcelButton.Text = "Delete Parcel";
            this.deleteParcelButton.UseVisualStyleBackColor = false;
            this.deleteParcelButton.Click += new System.EventHandler(this.deleteParcelButton_Click);
            // 
            // newParcelTOCButton
            // 
            this.newParcelTOCButton.Location = new System.Drawing.Point(6, 373);
            this.newParcelTOCButton.Name = "newParcelTOCButton";
            this.newParcelTOCButton.Size = new System.Drawing.Size(299, 23);
            this.newParcelTOCButton.TabIndex = 1;
            this.newParcelTOCButton.Text = "New ParcelTOC";
            this.newParcelTOCButton.UseVisualStyleBackColor = true;
            this.newParcelTOCButton.Click += new System.EventHandler(this.newParcelTOCButton_Click);
            // 
            // parcelPropertyGrid
            // 
            this.parcelPropertyGrid.HelpVisible = false;
            this.parcelPropertyGrid.Location = new System.Drawing.Point(6, 22);
            this.parcelPropertyGrid.Name = "parcelPropertyGrid";
            this.parcelPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.parcelPropertyGrid.Size = new System.Drawing.Size(299, 345);
            this.parcelPropertyGrid.TabIndex = 0;
            this.parcelPropertyGrid.ToolbarVisible = false;
            // 
            // parcelTOCGroupBox
            // 
            this.parcelTOCGroupBox.Controls.Add(this.newAssetButton);
            this.parcelTOCGroupBox.Controls.Add(this.deleteParcelTOCButton);
            this.parcelTOCGroupBox.Location = new System.Drawing.Point(807, 31);
            this.parcelTOCGroupBox.Name = "parcelTOCGroupBox";
            this.parcelTOCGroupBox.Size = new System.Drawing.Size(311, 827);
            this.parcelTOCGroupBox.TabIndex = 4;
            this.parcelTOCGroupBox.TabStop = false;
            this.parcelTOCGroupBox.Text = "ParcelTOC Options";
            this.parcelTOCGroupBox.Visible = false;
            // 
            // newAssetButton
            // 
            this.newAssetButton.Location = new System.Drawing.Point(6, 22);
            this.newAssetButton.Name = "newAssetButton";
            this.newAssetButton.Size = new System.Drawing.Size(299, 23);
            this.newAssetButton.TabIndex = 1;
            this.newAssetButton.Text = "New Asset";
            this.newAssetButton.UseVisualStyleBackColor = true;
            this.newAssetButton.Click += new System.EventHandler(this.newAssetButton_Click);
            // 
            // deleteParcelTOCButton
            // 
            this.deleteParcelTOCButton.BackColor = System.Drawing.Color.Red;
            this.deleteParcelTOCButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deleteParcelTOCButton.Location = new System.Drawing.Point(6, 797);
            this.deleteParcelTOCButton.Name = "deleteParcelTOCButton";
            this.deleteParcelTOCButton.Size = new System.Drawing.Size(299, 23);
            this.deleteParcelTOCButton.TabIndex = 0;
            this.deleteParcelTOCButton.Text = "Delete ParcelTOC";
            this.deleteParcelTOCButton.UseVisualStyleBackColor = false;
            this.deleteParcelTOCButton.Click += new System.EventHandler(this.deleteParcelTOCButton_Click);
            // 
            // tableGroupBox
            // 
            this.tableGroupBox.Controls.Add(this.newParcelButton);
            this.tableGroupBox.Controls.Add(this.stringTableGroupBox);
            this.tableGroupBox.Controls.Add(this.tableHeaderPropertyGrid);
            this.tableGroupBox.Controls.Add(this.deleteTableButton);
            this.tableGroupBox.Controls.Add(this.tablePropertyGrid);
            this.tableGroupBox.Location = new System.Drawing.Point(807, 31);
            this.tableGroupBox.Name = "tableGroupBox";
            this.tableGroupBox.Size = new System.Drawing.Size(311, 827);
            this.tableGroupBox.TabIndex = 5;
            this.tableGroupBox.TabStop = false;
            this.tableGroupBox.Text = "Table Options";
            this.tableGroupBox.Visible = false;
            // 
            // newParcelButton
            // 
            this.newParcelButton.Location = new System.Drawing.Point(6, 768);
            this.newParcelButton.Name = "newParcelButton";
            this.newParcelButton.Size = new System.Drawing.Size(299, 23);
            this.newParcelButton.TabIndex = 7;
            this.newParcelButton.Text = "New Parcel";
            this.newParcelButton.UseVisualStyleBackColor = true;
            this.newParcelButton.Click += new System.EventHandler(this.newParcelButton_Click);
            // 
            // stringTableGroupBox
            // 
            this.stringTableGroupBox.Controls.Add(this.deleteStringTableEntryButton);
            this.stringTableGroupBox.Controls.Add(this.newStringTableEntryButton);
            this.stringTableGroupBox.Controls.Add(this.stringTableEntryTextBox);
            this.stringTableGroupBox.Controls.Add(this.domainStringLabel);
            this.stringTableGroupBox.Controls.Add(this.stringTableListBox);
            this.stringTableGroupBox.Controls.Add(this.domainStringTextBox);
            this.stringTableGroupBox.Location = new System.Drawing.Point(6, 534);
            this.stringTableGroupBox.Name = "stringTableGroupBox";
            this.stringTableGroupBox.Size = new System.Drawing.Size(299, 228);
            this.stringTableGroupBox.TabIndex = 6;
            this.stringTableGroupBox.TabStop = false;
            this.stringTableGroupBox.Text = "String Table";
            // 
            // deleteStringTableEntryButton
            // 
            this.deleteStringTableEntryButton.Location = new System.Drawing.Point(152, 170);
            this.deleteStringTableEntryButton.Name = "deleteStringTableEntryButton";
            this.deleteStringTableEntryButton.Size = new System.Drawing.Size(141, 23);
            this.deleteStringTableEntryButton.TabIndex = 9;
            this.deleteStringTableEntryButton.Text = "Delete Entry";
            this.deleteStringTableEntryButton.UseVisualStyleBackColor = true;
            this.deleteStringTableEntryButton.Click += new System.EventHandler(this.deleteStringTableEntryButton_Click);
            // 
            // newStringTableEntryButton
            // 
            this.newStringTableEntryButton.Location = new System.Drawing.Point(6, 170);
            this.newStringTableEntryButton.Name = "newStringTableEntryButton";
            this.newStringTableEntryButton.Size = new System.Drawing.Size(141, 23);
            this.newStringTableEntryButton.TabIndex = 8;
            this.newStringTableEntryButton.Text = "Add New Entry";
            this.newStringTableEntryButton.UseVisualStyleBackColor = true;
            this.newStringTableEntryButton.Click += new System.EventHandler(this.newStringTableEntryButton_Click);
            // 
            // stringTableEntryTextBox
            // 
            this.stringTableEntryTextBox.Location = new System.Drawing.Point(6, 141);
            this.stringTableEntryTextBox.Name = "stringTableEntryTextBox";
            this.stringTableEntryTextBox.Size = new System.Drawing.Size(287, 23);
            this.stringTableEntryTextBox.TabIndex = 7;
            // 
            // domainStringLabel
            // 
            this.domainStringLabel.AutoSize = true;
            this.domainStringLabel.Location = new System.Drawing.Point(6, 202);
            this.domainStringLabel.Name = "domainStringLabel";
            this.domainStringLabel.Size = new System.Drawing.Size(89, 15);
            this.domainStringLabel.TabIndex = 6;
            this.domainStringLabel.Text = "DomainString: ";
            // 
            // stringTableListBox
            // 
            this.stringTableListBox.FormattingEnabled = true;
            this.stringTableListBox.ItemHeight = 15;
            this.stringTableListBox.Location = new System.Drawing.Point(6, 20);
            this.stringTableListBox.Name = "stringTableListBox";
            this.stringTableListBox.Size = new System.Drawing.Size(287, 124);
            this.stringTableListBox.TabIndex = 4;
            // 
            // domainStringTextBox
            // 
            this.domainStringTextBox.Location = new System.Drawing.Point(96, 199);
            this.domainStringTextBox.Name = "domainStringTextBox";
            this.domainStringTextBox.Size = new System.Drawing.Size(197, 23);
            this.domainStringTextBox.TabIndex = 5;
            this.domainStringTextBox.TextChanged += new System.EventHandler(this.domainStringTextBox_TextChanged);
            // 
            // tableHeaderPropertyGrid
            // 
            this.tableHeaderPropertyGrid.HelpVisible = false;
            this.tableHeaderPropertyGrid.Location = new System.Drawing.Point(6, 373);
            this.tableHeaderPropertyGrid.Name = "tableHeaderPropertyGrid";
            this.tableHeaderPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.tableHeaderPropertyGrid.Size = new System.Drawing.Size(299, 155);
            this.tableHeaderPropertyGrid.TabIndex = 3;
            this.tableHeaderPropertyGrid.ToolbarVisible = false;
            // 
            // deleteTableButton
            // 
            this.deleteTableButton.BackColor = System.Drawing.Color.Red;
            this.deleteTableButton.Location = new System.Drawing.Point(6, 797);
            this.deleteTableButton.Name = "deleteTableButton";
            this.deleteTableButton.Size = new System.Drawing.Size(299, 23);
            this.deleteTableButton.TabIndex = 2;
            this.deleteTableButton.Text = "Delete Table";
            this.deleteTableButton.UseVisualStyleBackColor = false;
            this.deleteTableButton.Click += new System.EventHandler(this.deleteTableButton_Click);
            // 
            // tablePropertyGrid
            // 
            this.tablePropertyGrid.HelpVisible = false;
            this.tablePropertyGrid.Location = new System.Drawing.Point(6, 22);
            this.tablePropertyGrid.Name = "tablePropertyGrid";
            this.tablePropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.tablePropertyGrid.Size = new System.Drawing.Size(299, 345);
            this.tablePropertyGrid.TabIndex = 1;
            this.tablePropertyGrid.ToolbarVisible = false;
            // 
            // searchGroupBox
            // 
            this.searchGroupBox.Controls.Add(this.nextButton);
            this.searchGroupBox.Controls.Add(this.previousButton);
            this.searchGroupBox.Controls.Add(this.searchForDataButton);
            this.searchGroupBox.Controls.Add(this.searchForNameButton);
            this.searchGroupBox.Controls.Add(this.searchAssetIDButton);
            this.searchGroupBox.Controls.Add(this.searchTextBox);
            this.searchGroupBox.Location = new System.Drawing.Point(13, 857);
            this.searchGroupBox.Name = "searchGroupBox";
            this.searchGroupBox.Size = new System.Drawing.Size(1105, 52);
            this.searchGroupBox.TabIndex = 6;
            this.searchGroupBox.TabStop = false;
            this.searchGroupBox.Text = "Search";
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(861, 21);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(116, 23);
            this.nextButton.TabIndex = 5;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // previousButton
            // 
            this.previousButton.Location = new System.Drawing.Point(983, 21);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(116, 23);
            this.previousButton.TabIndex = 4;
            this.previousButton.Text = "Previous";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // searchForDataButton
            // 
            this.searchForDataButton.Location = new System.Drawing.Point(556, 22);
            this.searchForDataButton.Name = "searchForDataButton";
            this.searchForDataButton.Size = new System.Drawing.Size(116, 23);
            this.searchForDataButton.TabIndex = 3;
            this.searchForDataButton.Text = "Search for Data";
            this.searchForDataButton.UseVisualStyleBackColor = true;
            this.searchForDataButton.Click += new System.EventHandler(this.searchForDataButton_Click);
            // 
            // searchForNameButton
            // 
            this.searchForNameButton.Location = new System.Drawing.Point(312, 22);
            this.searchForNameButton.Name = "searchForNameButton";
            this.searchForNameButton.Size = new System.Drawing.Size(116, 23);
            this.searchForNameButton.TabIndex = 2;
            this.searchForNameButton.Text = "Search for Name";
            this.searchForNameButton.UseVisualStyleBackColor = true;
            this.searchForNameButton.Click += new System.EventHandler(this.searchForNameButton_Click);
            // 
            // searchAssetIDButton
            // 
            this.searchAssetIDButton.Location = new System.Drawing.Point(434, 22);
            this.searchAssetIDButton.Name = "searchAssetIDButton";
            this.searchAssetIDButton.Size = new System.Drawing.Size(116, 23);
            this.searchAssetIDButton.TabIndex = 1;
            this.searchAssetIDButton.Text = "Search for AssetID";
            this.searchAssetIDButton.UseVisualStyleBackColor = true;
            this.searchAssetIDButton.Click += new System.EventHandler(this.searchAssetIDButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(7, 22);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(299, 23);
            this.searchTextBox.TabIndex = 0;
            // 
            // Plankton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1130, 921);
            this.Controls.Add(this.searchGroupBox);
            this.Controls.Add(this.parcelTOCGroupBox);
            this.Controls.Add(this.assetGroupBox);
            this.Controls.Add(this.tableGroupBox);
            this.Controls.Add(this.parcelGroupBox);
            this.Controls.Add(this.ArchiveTreeGroup);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(1146, 960);
            this.MinimumSize = new System.Drawing.Size(1146, 960);
            this.Name = "Plankton";
            this.Text = "Plankton";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ArchiveTreeGroup.ResumeLayout(false);
            this.assetGroupBox.ResumeLayout(false);
            this.assetGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.assetTreeNodeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.assetTreeNodeBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tOCEntryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tOCEntryBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tOCEntryBindingSource2)).EndInit();
            this.parcelGroupBox.ResumeLayout(false);
            this.parcelTOCGroupBox.ResumeLayout(false);
            this.tableGroupBox.ResumeLayout(false);
            this.stringTableGroupBox.ResumeLayout(false);
            this.stringTableGroupBox.PerformLayout();
            this.searchGroupBox.ResumeLayout(false);
            this.searchGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCTRLOToolStripMenuItem;
        private System.Windows.Forms.GroupBox ArchiveTreeGroup;
        private System.Windows.Forms.TreeView archiveView;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadTreeToolStripMenuItem;
        private System.Windows.Forms.GroupBox assetGroupBox;
        private System.Windows.Forms.BindingSource assetTreeNodeBindingSource;
        private System.Windows.Forms.BindingSource assetTreeNodeBindingSource1;
        private System.Windows.Forms.BindingSource tOCEntryBindingSource;
        private System.Windows.Forms.BindingSource tOCEntryBindingSource1;
        private System.Windows.Forms.BindingSource tOCEntryBindingSource2;
        private System.Windows.Forms.GroupBox parcelGroupBox;
        private System.Windows.Forms.PropertyGrid parcelPropertyGrid;
        private System.Windows.Forms.PropertyGrid assetPropertyGrid;
        private System.Windows.Forms.Button importRawDataButton;
        private System.Windows.Forms.Button exportRawDataButton;
        private System.Windows.Forms.Button newParcelTOCButton;
        private System.Windows.Forms.Button deleteParcelButton;
        private System.Windows.Forms.Button deleteAssetButton;
        private System.Windows.Forms.GroupBox parcelTOCGroupBox;
        private System.Windows.Forms.Button deleteParcelTOCButton;
        private System.Windows.Forms.TextBox renameAssetTextBox;
        private System.Windows.Forms.Button newAssetButton;
        private System.Windows.Forms.GroupBox tableGroupBox;
        private System.Windows.Forms.PropertyGrid tableHeaderPropertyGrid;
        private System.Windows.Forms.Button deleteTableButton;
        private System.Windows.Forms.PropertyGrid tablePropertyGrid;
        private System.Windows.Forms.TextBox domainStringTextBox;
        private System.Windows.Forms.ListBox stringTableListBox;
        private System.Windows.Forms.ToolStripMenuItem saveCTRLSToolStripMenuItem;
        private System.Windows.Forms.GroupBox stringTableGroupBox;
        private System.Windows.Forms.Button deleteStringTableEntryButton;
        private System.Windows.Forms.Button newStringTableEntryButton;
        private System.Windows.Forms.TextBox stringTableEntryTextBox;
        private System.Windows.Forms.Label domainStringLabel;
        private System.Windows.Forms.ToolStripMenuItem newFromCTRLSHIFTOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editHeaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsCTRLSHIFTSToolStripMenuItem;
        private System.Windows.Forms.Button newParcelButton;
        private System.Windows.Forms.GroupBox searchGroupBox;
        private System.Windows.Forms.Button searchAssetIDButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchForNameButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button searchForDataButton;
    }
}

