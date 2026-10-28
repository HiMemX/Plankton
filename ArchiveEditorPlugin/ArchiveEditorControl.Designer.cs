using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveEditorPlugin
{
    public partial class ArchiveEditorControl
    {
        private System.ComponentModel.IContainer? components = null;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            archiveTreeView = new TreeView();
            treeViewContextMenu = new ContextMenuStrip(components);
            newToolStripMenuItem = new ToolStripMenuItem();
            sECTToolStripMenuItem = new ToolStripMenuItem();
            pToolStripMenuItem = new ToolStripMenuItem();
            pTEXToolStripMenuItem = new ToolStripMenuItem();
            pFSTToolStripMenuItem = new ToolStripMenuItem();
            parcelTOCToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            propertyGridTabControl = new TabControl();
            headerTabPage = new TabPage();
            headerPropertyGrid = new PropertyGrid();
            headerLabel = new Label();
            dataTabPage = new TabPage();
            dataPropertyGrid = new PropertyGrid();
            dataLabel = new Label();
            splitContainer2 = new SplitContainer();
            tocEntryListView = new ListView();
            assetNameHeader = new ColumnHeader();
            assetTypeHeader = new ColumnHeader();
            assetUIDHeader = new ColumnHeader();
            assetSizeHeader = new ColumnHeader();
            assetListViewContextMenu = new ContextMenuStrip(components);
            newToolStripMenuItem1 = new ToolStripMenuItem();
            searchPanel = new Panel();
            filterGroupBox = new GroupBox();
            searchTextBox = new TextBox();
            wmlTypeIDComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            treeViewContextMenu.SuspendLayout();
            propertyGridTabControl.SuspendLayout();
            headerTabPage.SuspendLayout();
            dataTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            assetListViewContextMenu.SuspendLayout();
            searchPanel.SuspendLayout();
            filterGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(archiveTreeView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(propertyGridTabControl);
            splitContainer1.Size = new Size(480, 667);
            splitContainer1.SplitterDistance = 221;
            splitContainer1.TabIndex = 0;
            // 
            // archiveTreeView
            // 
            archiveTreeView.ContextMenuStrip = treeViewContextMenu;
            archiveTreeView.Dock = DockStyle.Fill;
            archiveTreeView.HideSelection = false;
            archiveTreeView.Location = new Point(0, 0);
            archiveTreeView.Name = "archiveTreeView";
            archiveTreeView.Size = new Size(480, 221);
            archiveTreeView.TabIndex = 0;
            archiveTreeView.BeforeSelect += archiveTreeView_BeforeSelect;
            // 
            // treeViewContextMenu
            // 
            treeViewContextMenu.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem, deleteToolStripMenuItem });
            treeViewContextMenu.Name = "treeViewContextMenu";
            treeViewContextMenu.Size = new Size(108, 48);
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sECTToolStripMenuItem, pToolStripMenuItem, pTEXToolStripMenuItem, pFSTToolStripMenuItem, parcelTOCToolStripMenuItem });
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(107, 22);
            newToolStripMenuItem.Text = "New...";
            // 
            // sECTToolStripMenuItem
            // 
            sECTToolStripMenuItem.Name = "sECTToolStripMenuItem";
            sECTToolStripMenuItem.Size = new Size(128, 22);
            sECTToolStripMenuItem.Text = "SECT";
            // 
            // pToolStripMenuItem
            // 
            pToolStripMenuItem.Name = "pToolStripMenuItem";
            pToolStripMenuItem.Size = new Size(128, 22);
            pToolStripMenuItem.Text = "P";
            // 
            // pTEXToolStripMenuItem
            // 
            pTEXToolStripMenuItem.Name = "pTEXToolStripMenuItem";
            pTEXToolStripMenuItem.Size = new Size(128, 22);
            pTEXToolStripMenuItem.Text = "PTEX";
            // 
            // pFSTToolStripMenuItem
            // 
            pFSTToolStripMenuItem.Name = "pFSTToolStripMenuItem";
            pFSTToolStripMenuItem.Size = new Size(128, 22);
            pFSTToolStripMenuItem.Text = "PFST";
            // 
            // parcelTOCToolStripMenuItem
            // 
            parcelTOCToolStripMenuItem.Name = "parcelTOCToolStripMenuItem";
            parcelTOCToolStripMenuItem.Size = new Size(128, 22);
            parcelTOCToolStripMenuItem.Text = "ParcelTOC";
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(107, 22);
            deleteToolStripMenuItem.Text = "Delete";
            // 
            // propertyGridTabControl
            // 
            propertyGridTabControl.Controls.Add(headerTabPage);
            propertyGridTabControl.Controls.Add(dataTabPage);
            propertyGridTabControl.Dock = DockStyle.Fill;
            propertyGridTabControl.Location = new Point(0, 0);
            propertyGridTabControl.Multiline = true;
            propertyGridTabControl.Name = "propertyGridTabControl";
            propertyGridTabControl.SelectedIndex = 0;
            propertyGridTabControl.Size = new Size(480, 442);
            propertyGridTabControl.TabIndex = 1;
            // 
            // headerTabPage
            // 
            headerTabPage.Controls.Add(headerPropertyGrid);
            headerTabPage.Controls.Add(headerLabel);
            headerTabPage.Location = new Point(4, 24);
            headerTabPage.Name = "headerTabPage";
            headerTabPage.Padding = new Padding(3);
            headerTabPage.Size = new Size(472, 414);
            headerTabPage.TabIndex = 0;
            headerTabPage.Text = "Header";
            headerTabPage.UseVisualStyleBackColor = true;
            // 
            // headerPropertyGrid
            // 
            headerPropertyGrid.CommandsForeColor = SystemColors.ControlText;
            headerPropertyGrid.Dock = DockStyle.Fill;
            headerPropertyGrid.HelpVisible = false;
            headerPropertyGrid.Location = new Point(3, 3);
            headerPropertyGrid.Name = "headerPropertyGrid";
            headerPropertyGrid.PropertySort = PropertySort.NoSort;
            headerPropertyGrid.Size = new Size(466, 394);
            headerPropertyGrid.TabIndex = 0;
            headerPropertyGrid.ToolbarVisible = false;
            // 
            // headerLabel
            // 
            headerLabel.Dock = DockStyle.Bottom;
            headerLabel.Location = new Point(3, 397);
            headerLabel.Name = "headerLabel";
            headerLabel.RightToLeft = RightToLeft.No;
            headerLabel.Size = new Size(466, 14);
            headerLabel.TabIndex = 1;
            headerLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dataTabPage
            // 
            dataTabPage.Controls.Add(dataPropertyGrid);
            dataTabPage.Controls.Add(dataLabel);
            dataTabPage.Location = new Point(4, 24);
            dataTabPage.Name = "dataTabPage";
            dataTabPage.Padding = new Padding(3);
            dataTabPage.Size = new Size(472, 414);
            dataTabPage.TabIndex = 1;
            dataTabPage.Text = "Data";
            dataTabPage.UseVisualStyleBackColor = true;
            // 
            // dataPropertyGrid
            // 
            dataPropertyGrid.Dock = DockStyle.Fill;
            dataPropertyGrid.HelpVisible = false;
            dataPropertyGrid.Location = new Point(3, 3);
            dataPropertyGrid.Name = "dataPropertyGrid";
            dataPropertyGrid.PropertySort = PropertySort.NoSort;
            dataPropertyGrid.Size = new Size(466, 394);
            dataPropertyGrid.TabIndex = 0;
            dataPropertyGrid.ToolbarVisible = false;
            dataPropertyGrid.PropertyValueChanged += dataPropertyGrid_PropertyValueChanged;
            // 
            // dataLabel
            // 
            dataLabel.Dock = DockStyle.Bottom;
            dataLabel.Location = new Point(3, 397);
            dataLabel.Name = "dataLabel";
            dataLabel.RightToLeft = RightToLeft.No;
            dataLabel.Size = new Size(466, 14);
            dataLabel.TabIndex = 2;
            dataLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(tocEntryListView);
            splitContainer2.Panel1.Controls.Add(searchPanel);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(splitContainer1);
            splitContainer2.Size = new Size(1409, 667);
            splitContainer2.SplitterDistance = 925;
            splitContainer2.TabIndex = 0;
            // 
            // tocEntryListView
            // 
            tocEntryListView.Columns.AddRange(new ColumnHeader[] { assetNameHeader, assetTypeHeader, assetUIDHeader, assetSizeHeader });
            tocEntryListView.ContextMenuStrip = assetListViewContextMenu;
            tocEntryListView.Dock = DockStyle.Fill;
            tocEntryListView.FullRowSelect = true;
            tocEntryListView.Location = new Point(0, 59);
            tocEntryListView.Name = "tocEntryListView";
            tocEntryListView.Size = new Size(925, 608);
            tocEntryListView.TabIndex = 0;
            tocEntryListView.UseCompatibleStateImageBehavior = false;
            tocEntryListView.View = View.Details;
            tocEntryListView.SelectedIndexChanged += tocEntryListView_SelectedIndexChanged;
            // 
            // assetNameHeader
            // 
            assetNameHeader.Tag = "Name";
            assetNameHeader.Text = "Name";
            assetNameHeader.Width = 600;
            // 
            // assetTypeHeader
            // 
            assetTypeHeader.Tag = "Type";
            assetTypeHeader.Text = "Type";
            assetTypeHeader.Width = 120;
            // 
            // assetUIDHeader
            // 
            assetUIDHeader.Tag = "UID";
            assetUIDHeader.Text = "UID";
            assetUIDHeader.Width = 120;
            // 
            // assetSizeHeader
            // 
            assetSizeHeader.Tag = "Size";
            assetSizeHeader.Text = "Size";
            assetSizeHeader.Width = 80;
            // 
            // assetListViewContextMenu
            // 
            assetListViewContextMenu.Items.AddRange(new ToolStripItem[] { newToolStripMenuItem1 });
            assetListViewContextMenu.Name = "assetListViewContextMenu";
            assetListViewContextMenu.Size = new Size(108, 26);
            // 
            // newToolStripMenuItem1
            // 
            newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            newToolStripMenuItem1.Size = new Size(107, 22);
            newToolStripMenuItem1.Text = "New...";
            // 
            // searchPanel
            // 
            searchPanel.Controls.Add(filterGroupBox);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Name = "searchPanel";
            searchPanel.Size = new Size(925, 59);
            searchPanel.TabIndex = 1;
            // 
            // filterGroupBox
            // 
            filterGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            filterGroupBox.Controls.Add(searchTextBox);
            filterGroupBox.Controls.Add(wmlTypeIDComboBox);
            filterGroupBox.Location = new Point(3, 5);
            filterGroupBox.Name = "filterGroupBox";
            filterGroupBox.Size = new Size(919, 48);
            filterGroupBox.TabIndex = 3;
            filterGroupBox.TabStop = false;
            filterGroupBox.Text = "Filters";
            // 
            // searchTextBox
            // 
            searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchTextBox.Location = new Point(6, 19);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(733, 23);
            searchTextBox.TabIndex = 1;
            searchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // wmlTypeIDComboBox
            // 
            wmlTypeIDComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            wmlTypeIDComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            wmlTypeIDComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            wmlTypeIDComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            wmlTypeIDComboBox.FormattingEnabled = true;
            wmlTypeIDComboBox.Location = new Point(745, 19);
            wmlTypeIDComboBox.Name = "wmlTypeIDComboBox";
            wmlTypeIDComboBox.Size = new Size(168, 23);
            wmlTypeIDComboBox.TabIndex = 2;
            wmlTypeIDComboBox.SelectedIndexChanged += wmlTypeIDComboBox_SelectedIndexChanged;
            // 
            // ArchiveEditorControl
            // 
            Controls.Add(splitContainer2);
            Name = "ArchiveEditorControl";
            Size = new Size(1409, 667);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            treeViewContextMenu.ResumeLayout(false);
            propertyGridTabControl.ResumeLayout(false);
            headerTabPage.ResumeLayout(false);
            dataTabPage.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            assetListViewContextMenu.ResumeLayout(false);
            searchPanel.ResumeLayout(false);
            filterGroupBox.ResumeLayout(false);
            filterGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();

            base.Dispose(disposing);
        }
        private SplitContainer splitContainer1;
        private TreeView archiveTreeView;
        private SplitContainer splitContainer2;
        private ListView tocEntryListView;
        private ColumnHeader assetNameHeader;
        private ColumnHeader assetTypeHeader;
        private ColumnHeader assetUIDHeader;
        private PropertyGrid headerPropertyGrid;
        private TabControl propertyGridTabControl;
        private TabPage headerTabPage;
        private TabPage dataTabPage;
        private PropertyGrid dataPropertyGrid;
        private ContextMenuStrip treeViewContextMenu;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem sECTToolStripMenuItem;
        private ToolStripMenuItem pToolStripMenuItem;
        private ToolStripMenuItem pTEXToolStripMenuItem;
        private ToolStripMenuItem pFSTToolStripMenuItem;
        private ToolStripMenuItem parcelTOCToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private Label headerLabel;
        private Label dataLabel;
        private ContextMenuStrip assetListViewContextMenu;
        private ToolStripMenuItem newToolStripMenuItem1;
        private ColumnHeader assetSizeHeader;
        private Panel searchPanel;
        private ComboBox wmlTypeIDComboBox;
        private TextBox searchTextBox;
        private GroupBox filterGroupBox;
    }
}
