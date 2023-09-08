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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Plankton));
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openCTRLOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newFromCTRLSHIFTOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveCTRLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsCTRLSHIFTSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            editHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            reloadTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportLSETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ArchiveTreeGroup = new System.Windows.Forms.GroupBox();
            archiveView = new System.Windows.Forms.TreeView();
            assetGroupBox = new System.Windows.Forms.GroupBox();
            btnDirectEdit = new System.Windows.Forms.Button();
            updateEntityButton = new System.Windows.Forms.Button();
            entityPropertyGrid = new System.Windows.Forms.PropertyGrid();
            renameAssetTextBox = new System.Windows.Forms.TextBox();
            deleteAssetButton = new System.Windows.Forms.Button();
            importRawDataButton = new System.Windows.Forms.Button();
            exportRawDataButton = new System.Windows.Forms.Button();
            assetPropertyGrid = new System.Windows.Forms.PropertyGrid();
            assetTreeNodeBindingSource = new System.Windows.Forms.BindingSource(components);
            assetTreeNodeBindingSource1 = new System.Windows.Forms.BindingSource(components);
            tOCEntryBindingSource = new System.Windows.Forms.BindingSource(components);
            tOCEntryBindingSource1 = new System.Windows.Forms.BindingSource(components);
            tOCEntryBindingSource2 = new System.Windows.Forms.BindingSource(components);
            parcelGroupBox = new System.Windows.Forms.GroupBox();
            deleteParcelButton = new System.Windows.Forms.Button();
            newParcelTOCButton = new System.Windows.Forms.Button();
            parcelPropertyGrid = new System.Windows.Forms.PropertyGrid();
            parcelTOCGroupBox = new System.Windows.Forms.GroupBox();
            newAssetButton = new System.Windows.Forms.Button();
            deleteParcelTOCButton = new System.Windows.Forms.Button();
            tableGroupBox = new System.Windows.Forms.GroupBox();
            newParcelButton = new System.Windows.Forms.Button();
            stringTableGroupBox = new System.Windows.Forms.GroupBox();
            deleteStringTableEntryButton = new System.Windows.Forms.Button();
            newStringTableEntryButton = new System.Windows.Forms.Button();
            stringTableEntryTextBox = new System.Windows.Forms.TextBox();
            domainStringLabel = new System.Windows.Forms.Label();
            stringTableListBox = new System.Windows.Forms.ListBox();
            domainStringTextBox = new System.Windows.Forms.TextBox();
            tableHeaderPropertyGrid = new System.Windows.Forms.PropertyGrid();
            deleteTableButton = new System.Windows.Forms.Button();
            tablePropertyGrid = new System.Windows.Forms.PropertyGrid();
            searchGroupBox = new System.Windows.Forms.GroupBox();
            nextButton = new System.Windows.Forms.Button();
            previousButton = new System.Windows.Forms.Button();
            searchForDataButton = new System.Windows.Forms.Button();
            searchForNameButton = new System.Windows.Forms.Button();
            searchAssetIDButton = new System.Windows.Forms.Button();
            searchTextBox = new System.Windows.Forms.TextBox();
            menuStrip1.SuspendLayout();
            ArchiveTreeGroup.SuspendLayout();
            assetGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)assetTreeNodeBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)assetTreeNodeBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tOCEntryBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tOCEntryBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tOCEntryBindingSource2).BeginInit();
            parcelGroupBox.SuspendLayout();
            parcelTOCGroupBox.SuspendLayout();
            tableGroupBox.SuspendLayout();
            stringTableGroupBox.SuspendLayout();
            searchGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, toolsToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            menuStrip1.Size = new System.Drawing.Size(1289, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openCTRLOToolStripMenuItem, newFromCTRLSHIFTOToolStripMenuItem, saveCTRLSToolStripMenuItem, saveAsCTRLSHIFTSToolStripMenuItem, closeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // openCTRLOToolStripMenuItem
            // 
            openCTRLOToolStripMenuItem.Name = "openCTRLOToolStripMenuItem";
            openCTRLOToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            openCTRLOToolStripMenuItem.Text = "Open... [CTRL+O]";
            openCTRLOToolStripMenuItem.Click += openCTRLOToolStripMenuItem_Click;
            // 
            // newFromCTRLSHIFTOToolStripMenuItem
            // 
            newFromCTRLSHIFTOToolStripMenuItem.Name = "newFromCTRLSHIFTOToolStripMenuItem";
            newFromCTRLSHIFTOToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            newFromCTRLSHIFTOToolStripMenuItem.Text = "New From... [CTRL+SHIFT+O]";
            newFromCTRLSHIFTOToolStripMenuItem.Click += newFromCTRLSHIFTOToolStripMenuItem_Click;
            // 
            // saveCTRLSToolStripMenuItem
            // 
            saveCTRLSToolStripMenuItem.Enabled = false;
            saveCTRLSToolStripMenuItem.Name = "saveCTRLSToolStripMenuItem";
            saveCTRLSToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            saveCTRLSToolStripMenuItem.Text = "Save... [CTRL+S]";
            saveCTRLSToolStripMenuItem.Click += saveCTRLSToolStripMenuItem_Click;
            // 
            // saveAsCTRLSHIFTSToolStripMenuItem
            // 
            saveAsCTRLSHIFTSToolStripMenuItem.Enabled = false;
            saveAsCTRLSHIFTSToolStripMenuItem.Name = "saveAsCTRLSHIFTSToolStripMenuItem";
            saveAsCTRLSHIFTSToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            saveAsCTRLSHIFTSToolStripMenuItem.Text = "Save As... [CTRL+SHIFT+S]";
            saveAsCTRLSHIFTSToolStripMenuItem.Click += saveAsCTRLSHIFTSToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Enabled = false;
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new System.Drawing.Size(285, 26);
            closeToolStripMenuItem.Text = "Close...";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { editHeaderToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            editToolStripMenuItem.Text = "Edit";
            // 
            // editHeaderToolStripMenuItem
            // 
            editHeaderToolStripMenuItem.Name = "editHeaderToolStripMenuItem";
            editHeaderToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            editHeaderToolStripMenuItem.Text = "Edit Header [CTRL+H]";
            editHeaderToolStripMenuItem.Click += editHeaderToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { reloadTreeToolStripMenuItem, exportAllToolStripMenuItem, exportLSETToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            toolsToolStripMenuItem.Text = "Tools";
            toolsToolStripMenuItem.Click += toolsToolStripMenuItem_Click;
            // 
            // reloadTreeToolStripMenuItem
            // 
            reloadTreeToolStripMenuItem.Name = "reloadTreeToolStripMenuItem";
            reloadTreeToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            reloadTreeToolStripMenuItem.Text = "Reload tree [CTRL+R]";
            reloadTreeToolStripMenuItem.Click += reloadTreeToolStripMenuItem_Click;
            // 
            // exportAllToolStripMenuItem
            // 
            exportAllToolStripMenuItem.Name = "exportAllToolStripMenuItem";
            exportAllToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            exportAllToolStripMenuItem.Text = "Export All";
            exportAllToolStripMenuItem.Click += exportAllToolStripMenuItem_Click;
            // 
            // exportLSETToolStripMenuItem
            // 
            exportLSETToolStripMenuItem.Name = "exportLSETToolStripMenuItem";
            exportLSETToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            exportLSETToolStripMenuItem.Text = "Export LSET";
            exportLSETToolStripMenuItem.Click += exportLSETToolStripMenuItem_Click;
            // 
            // ArchiveTreeGroup
            // 
            ArchiveTreeGroup.Controls.Add(archiveView);
            ArchiveTreeGroup.Location = new System.Drawing.Point(15, 41);
            ArchiveTreeGroup.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            ArchiveTreeGroup.Name = "ArchiveTreeGroup";
            ArchiveTreeGroup.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            ArchiveTreeGroup.Size = new System.Drawing.Size(899, 1103);
            ArchiveTreeGroup.TabIndex = 1;
            ArchiveTreeGroup.TabStop = false;
            ArchiveTreeGroup.Text = "Archive Tree";
            // 
            // archiveView
            // 
            archiveView.Location = new System.Drawing.Point(8, 29);
            archiveView.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            archiveView.Name = "archiveView";
            archiveView.Size = new System.Drawing.Size(882, 1063);
            archiveView.TabIndex = 0;
            archiveView.AfterSelect += archiveView_AfterSelect;
            // 
            // assetGroupBox
            // 
            assetGroupBox.Controls.Add(btnDirectEdit);
            assetGroupBox.Controls.Add(updateEntityButton);
            assetGroupBox.Controls.Add(entityPropertyGrid);
            assetGroupBox.Controls.Add(renameAssetTextBox);
            assetGroupBox.Controls.Add(deleteAssetButton);
            assetGroupBox.Controls.Add(importRawDataButton);
            assetGroupBox.Controls.Add(exportRawDataButton);
            assetGroupBox.Controls.Add(assetPropertyGrid);
            assetGroupBox.Location = new System.Drawing.Point(922, 41);
            assetGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            assetGroupBox.Name = "assetGroupBox";
            assetGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            assetGroupBox.Size = new System.Drawing.Size(355, 1103);
            assetGroupBox.TabIndex = 2;
            assetGroupBox.TabStop = false;
            assetGroupBox.Text = "Asset Options";
            assetGroupBox.Visible = false;
            // 
            // btnDirectEdit
            // 
            btnDirectEdit.Location = new System.Drawing.Point(7, 360);
            btnDirectEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnDirectEdit.Name = "btnDirectEdit";
            btnDirectEdit.Size = new System.Drawing.Size(342, 51);
            btnDirectEdit.TabIndex = 10;
            btnDirectEdit.Text = "Direct Edit this Asset...";
            btnDirectEdit.UseVisualStyleBackColor = true;
            btnDirectEdit.Click += btnDirectEdit_Click;
            // 
            // updateEntityButton
            // 
            updateEntityButton.Location = new System.Drawing.Point(7, 321);
            updateEntityButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            updateEntityButton.Name = "updateEntityButton";
            updateEntityButton.Size = new System.Drawing.Size(342, 31);
            updateEntityButton.TabIndex = 9;
            updateEntityButton.Text = "Update Entity";
            updateEntityButton.UseVisualStyleBackColor = true;
            updateEntityButton.Click += updateEntityButton_Click;
            // 
            // entityPropertyGrid
            // 
            entityPropertyGrid.BackColor = System.Drawing.SystemColors.Control;
            entityPropertyGrid.HelpVisible = false;
            entityPropertyGrid.Location = new System.Drawing.Point(7, 419);
            entityPropertyGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            entityPropertyGrid.Name = "entityPropertyGrid";
            entityPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            entityPropertyGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            entityPropertyGrid.Size = new System.Drawing.Size(342, 589);
            entityPropertyGrid.TabIndex = 8;
            entityPropertyGrid.ToolbarVisible = false;
            entityPropertyGrid.SelectedGridItemChanged += entityPropertyGrid_SelectedGridItemChanged;
            entityPropertyGrid.Click += entityPropertyGrid_Click;
            entityPropertyGrid.Paint += entityPropertyGrid_Paint;
            // 
            // renameAssetTextBox
            // 
            renameAssetTextBox.Location = new System.Drawing.Point(7, 244);
            renameAssetTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            renameAssetTextBox.Name = "renameAssetTextBox";
            renameAssetTextBox.Size = new System.Drawing.Size(341, 27);
            renameAssetTextBox.TabIndex = 7;
            renameAssetTextBox.TextChanged += renameAssetTextBox_TextChanged;
            // 
            // deleteAssetButton
            // 
            deleteAssetButton.BackColor = System.Drawing.Color.Red;
            deleteAssetButton.Location = new System.Drawing.Point(7, 1063);
            deleteAssetButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            deleteAssetButton.Name = "deleteAssetButton";
            deleteAssetButton.Size = new System.Drawing.Size(342, 31);
            deleteAssetButton.TabIndex = 3;
            deleteAssetButton.Text = "Delete Asset";
            deleteAssetButton.UseVisualStyleBackColor = false;
            deleteAssetButton.Click += deleteAssetButton_Click;
            // 
            // importRawDataButton
            // 
            importRawDataButton.Location = new System.Drawing.Point(181, 283);
            importRawDataButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            importRawDataButton.Name = "importRawDataButton";
            importRawDataButton.Size = new System.Drawing.Size(168, 31);
            importRawDataButton.TabIndex = 2;
            importRawDataButton.Text = "Import Raw Data";
            importRawDataButton.UseVisualStyleBackColor = true;
            importRawDataButton.Click += importRawDataButton_Click;
            // 
            // exportRawDataButton
            // 
            exportRawDataButton.Location = new System.Drawing.Point(7, 283);
            exportRawDataButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            exportRawDataButton.Name = "exportRawDataButton";
            exportRawDataButton.Size = new System.Drawing.Size(168, 31);
            exportRawDataButton.TabIndex = 1;
            exportRawDataButton.Text = "Export Raw Data";
            exportRawDataButton.UseVisualStyleBackColor = true;
            exportRawDataButton.Click += exportRawDataButton_Click;
            // 
            // assetPropertyGrid
            // 
            assetPropertyGrid.BackColor = System.Drawing.SystemColors.Control;
            assetPropertyGrid.HelpVisible = false;
            assetPropertyGrid.Location = new System.Drawing.Point(7, 29);
            assetPropertyGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            assetPropertyGrid.Name = "assetPropertyGrid";
            assetPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            assetPropertyGrid.Size = new System.Drawing.Size(342, 207);
            assetPropertyGrid.TabIndex = 0;
            assetPropertyGrid.ToolbarVisible = false;
            // 
            // tOCEntryBindingSource
            // 
            tOCEntryBindingSource.DataSource = typeof(HoArchive.TOCEntry);
            // 
            // tOCEntryBindingSource1
            // 
            tOCEntryBindingSource1.DataSource = typeof(HoArchive.TOCEntry);
            // 
            // tOCEntryBindingSource2
            // 
            tOCEntryBindingSource2.DataSource = typeof(HoArchive.TOCEntry);
            // 
            // parcelGroupBox
            // 
            parcelGroupBox.Controls.Add(deleteParcelButton);
            parcelGroupBox.Controls.Add(newParcelTOCButton);
            parcelGroupBox.Controls.Add(parcelPropertyGrid);
            parcelGroupBox.Location = new System.Drawing.Point(922, 41);
            parcelGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            parcelGroupBox.Name = "parcelGroupBox";
            parcelGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            parcelGroupBox.Size = new System.Drawing.Size(355, 1103);
            parcelGroupBox.TabIndex = 3;
            parcelGroupBox.TabStop = false;
            parcelGroupBox.Text = "Parcel Options";
            parcelGroupBox.Visible = false;
            // 
            // deleteParcelButton
            // 
            deleteParcelButton.BackColor = System.Drawing.Color.Red;
            deleteParcelButton.ForeColor = System.Drawing.SystemColors.ControlText;
            deleteParcelButton.Location = new System.Drawing.Point(7, 1063);
            deleteParcelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            deleteParcelButton.Name = "deleteParcelButton";
            deleteParcelButton.Size = new System.Drawing.Size(342, 31);
            deleteParcelButton.TabIndex = 2;
            deleteParcelButton.Text = "Delete Parcel";
            deleteParcelButton.UseVisualStyleBackColor = false;
            deleteParcelButton.Click += deleteParcelButton_Click;
            // 
            // newParcelTOCButton
            // 
            newParcelTOCButton.Location = new System.Drawing.Point(7, 497);
            newParcelTOCButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            newParcelTOCButton.Name = "newParcelTOCButton";
            newParcelTOCButton.Size = new System.Drawing.Size(342, 31);
            newParcelTOCButton.TabIndex = 1;
            newParcelTOCButton.Text = "New ParcelTOC";
            newParcelTOCButton.UseVisualStyleBackColor = true;
            newParcelTOCButton.Click += newParcelTOCButton_Click;
            // 
            // parcelPropertyGrid
            // 
            parcelPropertyGrid.HelpVisible = false;
            parcelPropertyGrid.Location = new System.Drawing.Point(7, 29);
            parcelPropertyGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            parcelPropertyGrid.Name = "parcelPropertyGrid";
            parcelPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            parcelPropertyGrid.Size = new System.Drawing.Size(342, 460);
            parcelPropertyGrid.TabIndex = 0;
            parcelPropertyGrid.ToolbarVisible = false;
            // 
            // parcelTOCGroupBox
            // 
            parcelTOCGroupBox.Controls.Add(newAssetButton);
            parcelTOCGroupBox.Controls.Add(deleteParcelTOCButton);
            parcelTOCGroupBox.Location = new System.Drawing.Point(922, 41);
            parcelTOCGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            parcelTOCGroupBox.Name = "parcelTOCGroupBox";
            parcelTOCGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            parcelTOCGroupBox.Size = new System.Drawing.Size(355, 1103);
            parcelTOCGroupBox.TabIndex = 4;
            parcelTOCGroupBox.TabStop = false;
            parcelTOCGroupBox.Text = "ParcelTOC Options";
            parcelTOCGroupBox.Visible = false;
            // 
            // newAssetButton
            // 
            newAssetButton.Location = new System.Drawing.Point(7, 29);
            newAssetButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            newAssetButton.Name = "newAssetButton";
            newAssetButton.Size = new System.Drawing.Size(342, 31);
            newAssetButton.TabIndex = 1;
            newAssetButton.Text = "New Asset";
            newAssetButton.UseVisualStyleBackColor = true;
            newAssetButton.Click += newAssetButton_Click;
            // 
            // deleteParcelTOCButton
            // 
            deleteParcelTOCButton.BackColor = System.Drawing.Color.Red;
            deleteParcelTOCButton.ForeColor = System.Drawing.SystemColors.ControlText;
            deleteParcelTOCButton.Location = new System.Drawing.Point(7, 1063);
            deleteParcelTOCButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            deleteParcelTOCButton.Name = "deleteParcelTOCButton";
            deleteParcelTOCButton.Size = new System.Drawing.Size(342, 31);
            deleteParcelTOCButton.TabIndex = 0;
            deleteParcelTOCButton.Text = "Delete ParcelTOC";
            deleteParcelTOCButton.UseVisualStyleBackColor = false;
            deleteParcelTOCButton.Click += deleteParcelTOCButton_Click;
            // 
            // tableGroupBox
            // 
            tableGroupBox.Controls.Add(newParcelButton);
            tableGroupBox.Controls.Add(stringTableGroupBox);
            tableGroupBox.Controls.Add(tableHeaderPropertyGrid);
            tableGroupBox.Controls.Add(deleteTableButton);
            tableGroupBox.Controls.Add(tablePropertyGrid);
            tableGroupBox.Location = new System.Drawing.Point(922, 41);
            tableGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            tableGroupBox.Name = "tableGroupBox";
            tableGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            tableGroupBox.Size = new System.Drawing.Size(355, 1103);
            tableGroupBox.TabIndex = 5;
            tableGroupBox.TabStop = false;
            tableGroupBox.Text = "Table Options";
            tableGroupBox.Visible = false;
            // 
            // newParcelButton
            // 
            newParcelButton.Location = new System.Drawing.Point(7, 1024);
            newParcelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            newParcelButton.Name = "newParcelButton";
            newParcelButton.Size = new System.Drawing.Size(342, 31);
            newParcelButton.TabIndex = 7;
            newParcelButton.Text = "New Parcel";
            newParcelButton.UseVisualStyleBackColor = true;
            newParcelButton.Click += newParcelButton_Click;
            // 
            // stringTableGroupBox
            // 
            stringTableGroupBox.Controls.Add(deleteStringTableEntryButton);
            stringTableGroupBox.Controls.Add(newStringTableEntryButton);
            stringTableGroupBox.Controls.Add(stringTableEntryTextBox);
            stringTableGroupBox.Controls.Add(domainStringLabel);
            stringTableGroupBox.Controls.Add(stringTableListBox);
            stringTableGroupBox.Controls.Add(domainStringTextBox);
            stringTableGroupBox.Location = new System.Drawing.Point(7, 712);
            stringTableGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            stringTableGroupBox.Name = "stringTableGroupBox";
            stringTableGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            stringTableGroupBox.Size = new System.Drawing.Size(342, 304);
            stringTableGroupBox.TabIndex = 6;
            stringTableGroupBox.TabStop = false;
            stringTableGroupBox.Text = "String Table";
            // 
            // deleteStringTableEntryButton
            // 
            deleteStringTableEntryButton.Location = new System.Drawing.Point(174, 227);
            deleteStringTableEntryButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            deleteStringTableEntryButton.Name = "deleteStringTableEntryButton";
            deleteStringTableEntryButton.Size = new System.Drawing.Size(161, 31);
            deleteStringTableEntryButton.TabIndex = 9;
            deleteStringTableEntryButton.Text = "Delete Entry";
            deleteStringTableEntryButton.UseVisualStyleBackColor = true;
            deleteStringTableEntryButton.Click += deleteStringTableEntryButton_Click;
            // 
            // newStringTableEntryButton
            // 
            newStringTableEntryButton.Location = new System.Drawing.Point(7, 227);
            newStringTableEntryButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            newStringTableEntryButton.Name = "newStringTableEntryButton";
            newStringTableEntryButton.Size = new System.Drawing.Size(161, 31);
            newStringTableEntryButton.TabIndex = 8;
            newStringTableEntryButton.Text = "Add New Entry";
            newStringTableEntryButton.UseVisualStyleBackColor = true;
            newStringTableEntryButton.Click += newStringTableEntryButton_Click;
            // 
            // stringTableEntryTextBox
            // 
            stringTableEntryTextBox.Location = new System.Drawing.Point(7, 188);
            stringTableEntryTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            stringTableEntryTextBox.Name = "stringTableEntryTextBox";
            stringTableEntryTextBox.Size = new System.Drawing.Size(327, 27);
            stringTableEntryTextBox.TabIndex = 7;
            // 
            // domainStringLabel
            // 
            domainStringLabel.AutoSize = true;
            domainStringLabel.Location = new System.Drawing.Point(7, 269);
            domainStringLabel.Name = "domainStringLabel";
            domainStringLabel.Size = new System.Drawing.Size(108, 20);
            domainStringLabel.TabIndex = 6;
            domainStringLabel.Text = "DomainString: ";
            // 
            // stringTableListBox
            // 
            stringTableListBox.FormattingEnabled = true;
            stringTableListBox.ItemHeight = 20;
            stringTableListBox.Location = new System.Drawing.Point(7, 27);
            stringTableListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            stringTableListBox.Name = "stringTableListBox";
            stringTableListBox.Size = new System.Drawing.Size(327, 164);
            stringTableListBox.TabIndex = 4;
            // 
            // domainStringTextBox
            // 
            domainStringTextBox.Location = new System.Drawing.Point(110, 265);
            domainStringTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            domainStringTextBox.Name = "domainStringTextBox";
            domainStringTextBox.Size = new System.Drawing.Size(225, 27);
            domainStringTextBox.TabIndex = 5;
            domainStringTextBox.TextChanged += domainStringTextBox_TextChanged;
            // 
            // tableHeaderPropertyGrid
            // 
            tableHeaderPropertyGrid.HelpVisible = false;
            tableHeaderPropertyGrid.Location = new System.Drawing.Point(7, 497);
            tableHeaderPropertyGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            tableHeaderPropertyGrid.Name = "tableHeaderPropertyGrid";
            tableHeaderPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            tableHeaderPropertyGrid.Size = new System.Drawing.Size(342, 207);
            tableHeaderPropertyGrid.TabIndex = 3;
            tableHeaderPropertyGrid.ToolbarVisible = false;
            // 
            // deleteTableButton
            // 
            deleteTableButton.BackColor = System.Drawing.Color.Red;
            deleteTableButton.Location = new System.Drawing.Point(7, 1063);
            deleteTableButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            deleteTableButton.Name = "deleteTableButton";
            deleteTableButton.Size = new System.Drawing.Size(342, 31);
            deleteTableButton.TabIndex = 2;
            deleteTableButton.Text = "Delete Table";
            deleteTableButton.UseVisualStyleBackColor = false;
            deleteTableButton.Click += deleteTableButton_Click;
            // 
            // tablePropertyGrid
            // 
            tablePropertyGrid.HelpVisible = false;
            tablePropertyGrid.Location = new System.Drawing.Point(7, 29);
            tablePropertyGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            tablePropertyGrid.Name = "tablePropertyGrid";
            tablePropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            tablePropertyGrid.Size = new System.Drawing.Size(342, 460);
            tablePropertyGrid.TabIndex = 1;
            tablePropertyGrid.ToolbarVisible = false;
            // 
            // searchGroupBox
            // 
            searchGroupBox.Controls.Add(nextButton);
            searchGroupBox.Controls.Add(previousButton);
            searchGroupBox.Controls.Add(searchForDataButton);
            searchGroupBox.Controls.Add(searchForNameButton);
            searchGroupBox.Controls.Add(searchAssetIDButton);
            searchGroupBox.Controls.Add(searchTextBox);
            searchGroupBox.Location = new System.Drawing.Point(15, 1143);
            searchGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            searchGroupBox.Name = "searchGroupBox";
            searchGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            searchGroupBox.Size = new System.Drawing.Size(1263, 69);
            searchGroupBox.TabIndex = 6;
            searchGroupBox.TabStop = false;
            searchGroupBox.Text = "Search";
            // 
            // nextButton
            // 
            nextButton.Location = new System.Drawing.Point(984, 28);
            nextButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            nextButton.Name = "nextButton";
            nextButton.Size = new System.Drawing.Size(133, 31);
            nextButton.TabIndex = 5;
            nextButton.Text = "Next";
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += nextButton_Click;
            // 
            // previousButton
            // 
            previousButton.Location = new System.Drawing.Point(1123, 28);
            previousButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            previousButton.Name = "previousButton";
            previousButton.Size = new System.Drawing.Size(133, 31);
            previousButton.TabIndex = 4;
            previousButton.Text = "Previous";
            previousButton.UseVisualStyleBackColor = true;
            previousButton.Click += previousButton_Click;
            // 
            // searchForDataButton
            // 
            searchForDataButton.Location = new System.Drawing.Point(635, 29);
            searchForDataButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            searchForDataButton.Name = "searchForDataButton";
            searchForDataButton.Size = new System.Drawing.Size(133, 31);
            searchForDataButton.TabIndex = 3;
            searchForDataButton.Text = "Search for Data";
            searchForDataButton.UseVisualStyleBackColor = true;
            searchForDataButton.Click += searchForDataButton_Click;
            // 
            // searchForNameButton
            // 
            searchForNameButton.Location = new System.Drawing.Point(357, 29);
            searchForNameButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            searchForNameButton.Name = "searchForNameButton";
            searchForNameButton.Size = new System.Drawing.Size(133, 31);
            searchForNameButton.TabIndex = 2;
            searchForNameButton.Text = "Search for Name";
            searchForNameButton.UseVisualStyleBackColor = true;
            searchForNameButton.Click += searchForNameButton_Click;
            // 
            // searchAssetIDButton
            // 
            searchAssetIDButton.Location = new System.Drawing.Point(496, 29);
            searchAssetIDButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            searchAssetIDButton.Name = "searchAssetIDButton";
            searchAssetIDButton.Size = new System.Drawing.Size(133, 31);
            searchAssetIDButton.TabIndex = 1;
            searchAssetIDButton.Text = "Search for AssetID";
            searchAssetIDButton.UseVisualStyleBackColor = true;
            searchAssetIDButton.Click += searchAssetIDButton_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new System.Drawing.Point(8, 29);
            searchTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new System.Drawing.Size(341, 27);
            searchTextBox.TabIndex = 0;
            // 
            // Plankton
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(1289, 1055);
            Controls.Add(assetGroupBox);
            Controls.Add(searchGroupBox);
            Controls.Add(parcelTOCGroupBox);
            Controls.Add(tableGroupBox);
            Controls.Add(parcelGroupBox);
            Controls.Add(ArchiveTreeGroup);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            MaximumSize = new System.Drawing.Size(1307, 1264);
            MinimumSize = new System.Drawing.Size(1307, 1018);
            Name = "Plankton";
            Text = "Plankton";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ArchiveTreeGroup.ResumeLayout(false);
            assetGroupBox.ResumeLayout(false);
            assetGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)assetTreeNodeBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)assetTreeNodeBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)tOCEntryBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)tOCEntryBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)tOCEntryBindingSource2).EndInit();
            parcelGroupBox.ResumeLayout(false);
            parcelTOCGroupBox.ResumeLayout(false);
            tableGroupBox.ResumeLayout(false);
            stringTableGroupBox.ResumeLayout(false);
            stringTableGroupBox.PerformLayout();
            searchGroupBox.ResumeLayout(false);
            searchGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.PropertyGrid entityPropertyGrid;
        private System.Windows.Forms.ToolStripMenuItem exportAllToolStripMenuItem;
        private System.Windows.Forms.Button updateEntityButton;
        private System.Windows.Forms.ToolStripMenuItem exportLSETToolStripMenuItem;
        private System.Windows.Forms.Button btnDirectEdit;
    }
}

