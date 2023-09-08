
using HoArchive;
using Plankton.InternalEditors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Plankton
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class Plankton : Form
    {
        CSHO.Handler handler;
        OpenFileDialog openDialog;
        SaveFileDialog saveDialog;
        FolderBrowserDialog folderBrowserDialog;
        EditHeaderWindow editHeaderWindow;
        NewParcelWindow newParcelWindow;
        NewAssetWindow newAssetWindow;
        ExportAllWindow exportAllWindow;
        int searchResultCount;
        string currentSearch;
        searchType currentSearchState;
        public Plankton()
        {
            handler = new CSHO.Handler();
            openDialog = new OpenFileDialog();
            saveDialog = new SaveFileDialog();
            folderBrowserDialog = new FolderBrowserDialog();
            editHeaderWindow = null;
            newParcelWindow = null;
            newAssetWindow = null;
            exportAllWindow = null;
            searchResultCount = -1;
            currentSearch = "";
            currentSearchState = searchType.None;

            openDialog.InitialDirectory = "c:\\";
            openDialog.Filter = "HO Archives (*.ho)|*.ho|LSET Files (*.lset)|*.lset";
            openDialog.FilterIndex = 0;
            InitializeComponent();
        }
        public static byte[] assetForHexEditor;
        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private List<byte> hexStringToByteList(string hex)
        {
            List<byte> output = new List<byte>();
            string tempbytestring = "";
            byte convertresult;

            foreach (char c in hex)
            {
                tempbytestring += c.ToString();

                if (tempbytestring.Length >= 2)
                {
                    if (!byte.TryParse(tempbytestring, NumberStyles.HexNumber, null, out convertresult)) { return null; }
                    output.Add(convertresult);
                    tempbytestring = "";
                }
            }

            return output;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void updateToolBar()
        {

            bool archivecheck = handler.Archive == null;
            bool pathcheck = handler.path == "";

            saveCTRLSToolStripMenuItem.Enabled = (!pathcheck && !archivecheck);
            saveAsCTRLSHIFTSToolStripMenuItem.Enabled = !archivecheck;


            closeToolStripMenuItem.Enabled = !archivecheck;


        }

        public void closeNewAssetWindow()
        {
            if (newAssetWindow != null) { newAssetWindow.Close(); }
        }

        public void closeNewParcelWindow()
        {
            if (newParcelWindow != null) { newParcelWindow.Close(); }
        }

        public void closeEditHeaderWindow()
        {
            if (editHeaderWindow != null) { editHeaderWindow.Close(); }
        }
        public void closeExportAllWindow()
        {
            if (exportAllWindow != null) { exportAllWindow.Close(); }
        }

        public void closeToolWindows()
        {
            closeEditHeaderWindow();
            closeNewParcelWindow();
            closeNewAssetWindow();

        }

        private void hideAllClassGroupBoxes()
        {
            parcelGroupBox.Visible = false;
            assetGroupBox.Visible = false;
            parcelTOCGroupBox.Visible = false;
            tableGroupBox.Visible = false;
        }

        private void updateTree()
        {
            closeToolWindows();

            archiveView.Nodes.Clear();
            hideAllClassGroupBoxes();
            if (handler.Archive == null) { return; }
            handler.Update();
            archiveView.Nodes.Add(new tableTreeNode(handler, (HoArchive.Table)handler.Archive.MasterTable, ((HoArchive.Table)handler.Archive.MasterTable).TableHeader.tableTypeTag));

        }

        private void openCTRLOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (openDialog.FilterIndex != 1) { return; }

            string filePath = openDialog.FileName;

            string errorcode = handler.Open(filePath);

            if (errorcode != "")
            {
                MessageBox.Show(errorcode, "Error");
                return;
            }

            HoArchive.Header header = handler.Archive.Header;
            string stats = "Magic: " + header.cMagic + "\n";
            stats += "Compilation Date: " + header.timeString; // No linefeed, timestring already has one
            stats += "Sectorsize: " + header.sectorSize + "\n";
            stats += "Platform: " + header.platform + "\n";
            stats += "User: " + header.user + "\n";
            stats += "Target: " + header.target + "\n";
            stats += "Creator: " + header.creator + "\n";
            stats += "Comment: " + header.comment + "\n";
            stats += "Hash: " + header.hash + "\n";

            updateTree();
            updateToolBar();

            Text = "Plankton - " + filePath;

            MessageBox.Show(stats, "Success");
        }

        private void reloadTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (handler.Archive == null) { return; }
            handler.Update();
            updateTree();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            handler.Close();
            Text = "Plankton";
            updateTree();
            updateToolBar();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void archiveView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string nodeType = archiveView.SelectedNode.GetType().ToString();
            closeNewParcelWindow();
            closeNewAssetWindow();

            switch (nodeType)
            {
                case "Plankton.assetTreeNode":
                    parcelGroupBox.Visible = false;
                    parcelTOCGroupBox.Visible = false;
                    assetGroupBox.Visible = true;
                    tableGroupBox.Visible = false;

                    assetPropertyGrid.SelectedObject = ((assetTreeNode)archiveView.SelectedNode).asset;
                    entityPropertyGrid.SelectedObject = ((assetTreeNode)archiveView.SelectedNode).asset.entity;
                    renameAssetTextBox.Text = archiveView.SelectedNode.Text;
                    break;

                case "Plankton.tocTreeNode":
                    parcelGroupBox.Visible = false;
                    parcelTOCGroupBox.Visible = true;
                    assetGroupBox.Visible = false;
                    tableGroupBox.Visible = false;

                    break;


                case "Plankton.parcelTreeNode":
                    parcelGroupBox.Visible = true;
                    parcelTOCGroupBox.Visible = false;
                    assetGroupBox.Visible = false;
                    tableGroupBox.Visible = false;

                    parcelPropertyGrid.SelectedObject = ((parcelTreeNode)archiveView.SelectedNode).tableentry;

                    break;

                case "Plankton.tableTreeNode":
                    parcelGroupBox.Visible = false;
                    parcelTOCGroupBox.Visible = false;
                    assetGroupBox.Visible = false;
                    tableGroupBox.Visible = true;

                    tablePropertyGrid.SelectedObject = ((tableTreeNode)archiveView.SelectedNode).tableentry;
                    tableHeaderPropertyGrid.SelectedObject = ((tableTreeNode)archiveView.SelectedNode).table.TableHeader;
                    domainStringTextBox.Text = ((tableTreeNode)archiveView.SelectedNode).table.StringTable.DomainString;
                    stringTableListBox.Items.Clear();
                    stringTableListBox.Items.AddRange(((tableTreeNode)archiveView.SelectedNode).table.StringTable.StringTableEntries.ToArray());

                    break;
            }
        }


        private void exportRawDataButton_Click(object sender, EventArgs e)
        {
            TreeNode node = archiveView.SelectedNode;
            TOCEntry asset = ((assetTreeNode)node).asset;

            saveDialog.DefaultExt = ".dat";
            saveDialog.FileName = handler.GetName(asset.uidSelf) + " [" + System.Convert.ToHexString(BitConverter.GetBytes(asset.uidSelf).Reverse().ToArray()) + "]";

            if (saveDialog.ShowDialog() != DialogResult.OK) { return; }

            string filepath = saveDialog.FileName;

            if (filepath == null) { return; }

            asset.Update(0x40);


            HoArchive.BinaryWriterEndian writer = new HoArchive.BinaryWriterEndian(filepath, false);

            writer.WriteE(((assetTreeNode)archiveView.SelectedNode).asset.data.ToArray());

            writer.Dispose();

            archiveView.Focus();
            archiveView.SelectedNode = node;
            archiveView.SelectedNode.EnsureVisible();
        }

        private void importRawDataButton_Click(object sender, EventArgs e)
        {
            openDialog.FilterIndex = 2;

            if (openDialog.ShowDialog() != DialogResult.OK) { return; }


            string filepath = openDialog.FileName;

            HoArchive.BinaryReaderEndian reader = new HoArchive.BinaryReaderEndian(filepath, false);

            HoArchive.TOCEntry asset = ((assetTreeNode)archiveView.SelectedNode).asset;
            List<byte> data = reader.ReadBytes((int)reader.BaseStream.Length).ToList();

            Asset.AssetEntity entity;
            try { entity = Asset.AssetCaster.ReadAsset(new HoArchive.MemoryStreamEndian(data.ToArray(), handler.endian), asset.wmlTypeID, handler.Archive.Header.target, handler.Archive.Header.platform); }
            catch
            {
                MessageBox.Show("Error", "Error Casting Asset");
                return;
            }
            asset.entity = entity;
            asset.data = data;

            entityPropertyGrid.SelectedObject = entity;
            entityPropertyGrid.Refresh();


            reader.Dispose();
        }

        private void newParcelTOCButton_Click(object sender, EventArgs e)
        {
            handler.NewParcelTOC(((parcelTreeNode)archiveView.SelectedNode).parcel);


            archiveView.SelectedNode.Nodes.Add(new tocTreeNode(handler, ((parcelTreeNode)archiveView.SelectedNode).parcel.ParcelTOCs.Last()));
            archiveView.SelectedNode.Expand();
        }

        private void deleteParcelButton_Click(object sender, EventArgs e)
        {
            ((parcelTreeNode)archiveView.SelectedNode).parcel.delete = true;
            archiveView.SelectedNode.Parent.Nodes.Remove(archiveView.SelectedNode);
        }

        private void deleteAssetButton_Click(object sender, EventArgs e)
        {
            ((assetTreeNode)archiveView.SelectedNode).asset.delete = true;
            archiveView.SelectedNode.Parent.Nodes.Remove(archiveView.SelectedNode);
        }

        private void deleteParcelTOCButton_Click(object sender, EventArgs e)
        {
            ((tocTreeNode)archiveView.SelectedNode).toc.delete = true;
            archiveView.SelectedNode.Parent.Nodes.Remove(archiveView.SelectedNode);
        }

        private void renameAssetTextBox_TextChanged(object sender, EventArgs e)
        {
            HoArchive.NameTableEntry tableentry = handler.GetNameEntry(((assetTreeNode)archiveView.SelectedNode).asset.uidSelf);

            if (tableentry == null) { return; }

            tableentry.name = renameAssetTextBox.Text;
            archiveView.SelectedNode.Text = renameAssetTextBox.Text;
        }

        private void deleteTableButton_Click(object sender, EventArgs e)
        {
            if (((tableTreeNode)archiveView.SelectedNode).tableentry == null) { return; }
            ((tableTreeNode)archiveView.SelectedNode).table.delete = true;
            archiveView.SelectedNode.Parent.Nodes.Remove(archiveView.SelectedNode);
        }

        private void domainStringTextBox_TextChanged(object sender, EventArgs e)
        {
            ((tableTreeNode)archiveView.SelectedNode).table.StringTable.DomainString = domainStringTextBox.Text;
        }

        private void saveCTRLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (handler.Archive == null) { return; }

            string errorcode = handler.Save();

            if (errorcode != "")
            {
                MessageBox.Show(errorcode, "Error");
                return;
            }

            MessageBox.Show("Saved!", "Success");

        }

        private void newStringTableEntryButton_Click(object sender, EventArgs e)
        {
            string newentry = stringTableEntryTextBox.Text;

            if (string.IsNullOrEmpty(newentry)) { return; }

            stringTableListBox.Items.Add(newentry);
            ((tableTreeNode)archiveView.SelectedNode).table.StringTable.StringTableEntries.Add(newentry);
        }

        private void deleteStringTableEntryButton_Click(object sender, EventArgs e)
        {
            if (stringTableListBox.Items.Count == 0 || stringTableListBox.SelectedIndex == -1) { return; }


            ((tableTreeNode)archiveView.SelectedNode).table.StringTable.StringTableEntries.RemoveAt(stringTableListBox.SelectedIndex);
            stringTableListBox.Items.RemoveAt(stringTableListBox.SelectedIndex);
        }

        private void newFromCTRLSHIFTOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDialog.FilterIndex = 0;
            if (openDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string filePath = openDialog.FileName;
            string errorcode = "ERR_INTERNAL_FAILURE";

            if (openDialog.FilterIndex == 1) { errorcode = handler.NewFrom(filePath); }
            else { errorcode = handler.NewFromLSET(filePath); }

            if (errorcode != "")
            {
                MessageBox.Show(errorcode, "Error");
                return;
            }
            handler.path = "";

            HoArchive.Header header = handler.Archive.Header;
            string stats = "Magic: " + header.cMagic + "\n";
            stats += "Compilation Date: " + header.timeString; // No linefeed, timestring already has one
            stats += "Sectorsize: " + header.sectorSize + "\n";
            stats += "Platform: " + header.platform + "\n";
            stats += "User: " + header.user + "\n";
            stats += "Target: " + header.target + "\n";
            stats += "Creator: " + header.creator + "\n";
            stats += "Comment: " + header.comment + "\n";
            stats += "Hash: " + header.hash + "\n";

            updateTree();
            updateToolBar();
            Text = "Plankton";

            MessageBox.Show(stats, "Success");
        }

        private void editHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (handler.Archive == null) { return; }

            closeEditHeaderWindow();

            editHeaderWindow = new EditHeaderWindow();
            editHeaderWindow.editHeaderPropertyGrid.SelectedObject = handler.Archive.Header;
            editHeaderWindow.Show();
        }

        private void saveAsCTRLSHIFTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (handler.Archive == null) { return; }


            saveDialog.DefaultExt = ".ho";

            if (saveDialog.ShowDialog() != DialogResult.OK) { return; }


            string filepath = saveDialog.FileName;

            if (filepath == null) { return; }

            handler.path = filepath;
            handler.Save();

            updateToolBar();
            Text = "Plankton - " + filepath;
        }

        private void newParcelButton_Click(object sender, EventArgs e)
        {
            closeNewParcelWindow();
            newParcelWindow = new NewParcelWindow();

            newParcelWindow.currentNode = (tableTreeNode)archiveView.SelectedNode;
            newParcelWindow.Show();
        }

        private void newAssetButton_Click(object sender, EventArgs e)
        {
            closeNewAssetWindow();
            newAssetWindow = new NewAssetWindow();

            newAssetWindow.uidSelfTextBox.Text = string.Format("{0:X}", handler.GenerateAssetID());
            newAssetWindow.tocTreeNode = (tocTreeNode)archiveView.SelectedNode;

            HoArchive.ParcelDebug parcelDebug = GetAvailableParcelDebug();


            newAssetWindow.endian = handler.endian;
            newAssetWindow.target = handler.Archive.Header.target;
            newAssetWindow.platform = handler.Archive.Header.platform;
            newAssetWindow.debugParcel = parcelDebug;
            newAssetWindow.treeView = archiveView;
            newAssetWindow.Show();
        }

        private HoArchive.ParcelDebug GetAvailableParcelDebug()
        {
            HoArchive.ParcelDebug parcelDebug = null;

            foreach (HoArchive.ParcelBase parcel in handler.GetParcels())
            {
                if (parcel is HoArchive.ParcelDebug) { if (((HoArchive.ParcelDebug)parcel).delete != true) { parcelDebug = (HoArchive.ParcelDebug)parcel; break; } }
            }

            if (parcelDebug == null)
            {

                foreach (HoArchive.ParcelBase table in handler.GetTables())
                {
                    if (((HoArchive.Table)table).TableHeader.tableTypeTag == "SECT" & ((HoArchive.Table)table).delete != true)
                    {
                        parcelDebug = new HoArchive.ParcelDebug();
                        ((HoArchive.Table)table).Parcels.Add(parcelDebug);
                        ((HoArchive.Table)table).TableEntries.Add(new HoArchive.TableEntry("PD  "));
                        ((HoArchive.Table)table).MetaTableEntries.Add(new List<HoArchive.SliceMeta>() { new HoArchive.ParcelDebugSliceMeta() });
                    }
                }
            }

            if (parcelDebug == null)
            {
                HoArchive.Table newsecttable = new HoArchive.Table("SECT", "");
                HoArchive.TableEntry newsecttableentry = new HoArchive.TableEntry("SECT");

                handler.Archive.MasterTable.TableEntries.Add(newsecttableentry);
                handler.Archive.MasterTable.MetaTableEntries.Add(new List<HoArchive.SliceMeta>());
                handler.Archive.MasterTable.Parcels.Add(newsecttable);
                archiveView.Nodes[0].Nodes.Add(new tableTreeNode(newsecttable, "SECT", newsecttableentry));

                parcelDebug = new HoArchive.ParcelDebug();
                newsecttable.Parcels.Add(parcelDebug);
                newsecttable.TableEntries.Add(new HoArchive.TableEntry("PD  "));
                newsecttable.MetaTableEntries.Add(new List<HoArchive.SliceMeta>() { new HoArchive.ParcelDebugSliceMeta() });
            }

            return parcelDebug;
        }

        private void searchAssetIDButton_Click(object sender, EventArgs e)
        {
            if (handler.Archive == null) { return; }
            currentSearchState = searchType.AssetID;

            string text = searchTextBox.Text.Replace(" ", "");
            long output;

            if (!long.TryParse(text, NumberStyles.HexNumber, null, out output)) { return; }

            assetTreeNode node = ((tableTreeNode)archiveView.Nodes[0]).getAssetNode((ulong)output);


            if (node == null) { return; }

            archiveView.SelectedNode = node;
            archiveView.Focus();
        }

        private void searchForNameButton_Click(object sender, EventArgs e)
        {
            if (handler.Archive == null) { return; }

            searchResultCount = -1;
            currentSearch = searchTextBox.Text;
            currentSearchState = searchType.Name;

            nextSearchResult();
        }
        private void searchForDataButton_Click(object sender, EventArgs e)
        {
            if (handler.Archive == null) { return; }

            searchResultCount = -1;
            currentSearch = searchTextBox.Text;
            currentSearchState = searchType.Data;

            nextSearchResult();
        }

        private List<assetTreeNode> getFilteredNodes()
        {
            List<assetTreeNode> nodes = new List<assetTreeNode>();
            if (currentSearchState == searchType.Name)
            {
                List<HoArchive.NameTableEntry> nameTableEntries = handler.GetNameEntries(currentSearch);

                foreach (HoArchive.NameTableEntry entry in nameTableEntries)
                {
                    nodes.Add(((tableTreeNode)archiveView.Nodes[0]).getAssetNode(entry.uidAsset));
                }
            }
            else if (currentSearchState == searchType.Data)
            {
                List<byte> data = hexStringToByteList(currentSearch.Replace(" ", ""));


                if (data == null) { return nodes; }

                List<HoArchive.TOCEntry> entries = handler.GetAssetsFromDataSnippet(data);

                foreach (HoArchive.TOCEntry entry in entries)
                {
                    nodes.Add(((tableTreeNode)archiveView.Nodes[0]).getAssetNode(entry.uidSelf));
                }
            }


            return nodes;
        }

        private void nextSearchResult()
        {
            if (currentSearch == "") { return; }

            List<assetTreeNode> nodes = getFilteredNodes();

            searchResultCount++;

            if (searchResultCount >= nodes.Count) { searchResultCount--; return; }

            archiveView.SelectedNode = nodes[searchResultCount];
            archiveView.Focus();
        }

        private void previousSearchResult()
        {
            if (currentSearch == "" || searchResultCount <= 0) { return; }

            List<assetTreeNode> nodes = getFilteredNodes();

            searchResultCount--;

            if (searchResultCount >= nodes.Count) { searchResultCount++; return; }

            archiveView.SelectedNode = nodes[searchResultCount];
            archiveView.Focus();
        }


        private void nextButton_Click(object sender, EventArgs e)
        {
            nextSearchResult();
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            previousSearchResult();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // FILE OPERATIONS
            if (keyData == (Keys.Control | Keys.O))
            {
                openCTRLOToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.O | Keys.Shift))
            {
                newFromCTRLSHIFTOToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                saveCTRLSToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.S | Keys.Shift))
            {
                saveAsCTRLSHIFTSToolStripMenuItem_Click(null, null);
                return true;
            }

            // MISC

            if (keyData == (Keys.Control | Keys.H))
            {
                editHeaderToolStripMenuItem_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.R))
            {
                reloadTreeToolStripMenuItem_Click(null, null);
                return true;
            }

            if (keyData == (Keys.Control | Keys.C))
            {
                if (archiveView.SelectedNode is assetTreeNode && archiveView.Focused)
                {
                    HoArchive.TOCEntry asset = ((assetTreeNode)archiveView.SelectedNode).asset;
                    Clipboard.SetText("HOASSET - " + asset.Serialize() + "; " + handler.GetNameEntry(asset.uidSelf).Serialize());
                    return true;
                }
            }
            if (keyData == (Keys.Control | Keys.V) && handler.Archive != null)
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);

                if (clipboardText.Split(" - ")[0] != "HOASSET") { return false; }

                clipboardText = clipboardText.Split(" - ")[1];

                string[] assetstrings = clipboardText.Split("; ")[0].Split(", ");
                string[] namestrings = clipboardText.Split("; ")[1].Split(", ");

                MessageBox.Show(clipboardText);
                MessageBox.Show(String.Join(", ", assetstrings));
                MessageBox.Show(String.Join(", ", namestrings));


                uint blobAlign = UInt32.Parse(assetstrings[0]);
                ulong uidSelf = UInt64.Parse(assetstrings[1]);

                if (handler.GetAsset(uidSelf) != null) { uidSelf = handler.GenerateAssetID(); }

                HoArchive.wmlTypeID wmlTypeID = (wmlTypeID)UInt32.Parse(assetstrings[2]);
                ushort subType = UInt16.Parse(assetstrings[3]);
                ushort blobFlags = UInt16.Parse(assetstrings[4]);
                List<byte> data = Convert.FromBase64String(assetstrings[5]).ToList();
                MemoryStreamEndian stream = new MemoryStreamEndian(data.ToArray(), handler.endian);
                Asset.AssetEntity entity = Asset.AssetCaster.ReadAsset(stream, wmlTypeID, handler.Archive.Header.target, handler.Archive.Header.platform);
                stream.Dispose();

                uint typeID = UInt32.Parse(namestrings[1]);
                string name = namestrings[2];

                HoArchive.TOCEntry asset = new HoArchive.TOCEntry(uidSelf, wmlTypeID, blobAlign, subType, blobFlags, data, entity);
                assetTreeNode assetnode = new assetTreeNode(asset, name);
                HoArchive.NameTableEntry nametableentry = new HoArchive.NameTableEntry(uidSelf, name, typeID);

                tocTreeNode node;
                HoArchive.ParcelDebug parcelDebug = GetAvailableParcelDebug();

                if (archiveView.SelectedNode is assetTreeNode)
                {
                    node = (tocTreeNode)archiveView.SelectedNode.Parent;
                }

                else if (archiveView.SelectedNode is tocTreeNode)
                {
                    node = (tocTreeNode)archiveView.SelectedNode;
                }
                else { return false; }

                node.toc.Entries.Add(asset);
                node.Nodes.Add(assetnode);
                parcelDebug.NameTableEntries.Add(nametableentry);
            }






            // NODE OPERATIONS
            if (archiveView.SelectedNode == null) { return base.ProcessCmdKey(ref msg, keyData); }

            if (archiveView.SelectedNode is assetTreeNode)
            {
                if (keyData == (Keys.Delete))
                {
                    deleteAssetButton_Click(null, null);
                    return true;
                }
            }
            if (archiveView.SelectedNode is tocTreeNode)
            {
                if (keyData == (Keys.Delete))
                {
                    deleteParcelTOCButton_Click(null, null);
                    return true;
                }
                if (keyData == (Keys.Control | Keys.N))
                {
                    newAssetButton_Click(null, null);
                    return true;
                }
            }
            if (archiveView.SelectedNode is parcelTreeNode)
            {
                if (keyData == (Keys.Delete))
                {
                    deleteParcelButton_Click(null, null);
                    return true;
                }
                if (keyData == (Keys.Control | Keys.N))
                {
                    newParcelTOCButton_Click(null, null);
                    return true;
                }
            }
            if (archiveView.SelectedNode is tableTreeNode)
            {
                if (keyData == (Keys.Delete))
                {
                    deleteTableButton_Click(null, null);
                    return true;
                }
                if (keyData == (Keys.Control | Keys.N))
                {
                    newParcelButton_Click(null, null);
                    return true;
                }
            }

            // TREE NAVIGATION
            if (keyData == (Keys.Enter))
            {
                archiveView.SelectedNode.Expand();
                return true;
            }
            if (keyData == (Keys.Escape))
            {
                archiveView.SelectedNode.Collapse();
                return true;
            }
            /*
            if (keyData == (Keys.Down))
            {
                if (archiveView.SelectedNode.IsExpanded && archiveView.SelectedNode.Nodes.Count != 0)
                {
                    archiveView.SelectedNode = archiveView.SelectedNode.Nodes[0];
                }
                else if(archiveView.SelectedNode.Parent.Nodes.Count > archiveView.SelectedNode.Index + 1)
                {
                    archiveView.SelectedNode = archiveView.SelectedNode.Parent.Nodes[archiveView.SelectedNode.Index + 1];
                }
                archiveView.Focus();
                return true;
            }*/



            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void entityPropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            HoArchive.TOCEntry asset = ((assetTreeNode)archiveView.SelectedNode).asset;
            HoArchive.MemoryStreamEndian stream = new HoArchive.MemoryStreamEndian(false);
            if (asset.entity != null && Asset.AvailableAssetsTEMP.available.Contains(asset.wmlTypeID))
            {
                asset.entity.Update(asset);
                asset.entity.Save(stream);
                asset.data = stream.ToArray().ToList<byte>();
            }
            stream.Dispose();

            entityPropertyGrid.Refresh();
        }

        private void exportAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (handler.Archive == null) { return; }
            closeExportAllWindow();
            exportAllWindow = new ExportAllWindow();
            exportAllWindow.handler = handler;
            exportAllWindow.Show();
        }

        private void entityPropertyGrid_Click(object sender, EventArgs e)
        {
            entityPropertyGrid_SelectedGridItemChanged(sender, null);
        }

        private void entityPropertyGrid_Paint(object sender, PaintEventArgs e)
        {
            entityPropertyGrid_SelectedGridItemChanged(sender, null);
        }

        private void updateEntityButton_Click(object sender, EventArgs e)
        {
            entityPropertyGrid_SelectedGridItemChanged(sender, null);
        }

        private void exportLSETToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (handler.Archive == null) { return; }


            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) { return; }


            string filepath = folderBrowserDialog.SelectedPath;

            if (filepath == null) { return; }

            handler.ExportLSET(filepath);
        }

        private void btnDirectEdit_Click(object sender, EventArgs e)
        {
            TreeNode node = archiveView.SelectedNode;
            TOCEntry asset = ((assetTreeNode)node).asset;

            asset.Update(0x40);




            assetForHexEditor = ((assetTreeNode)archiveView.SelectedNode).asset.data.ToArray();
            DirectEditorWindow directEditorWindow = new DirectEditorWindow();
            directEditorWindow.ShowDialog();

            archiveView.Focus();
            archiveView.SelectedNode = node;
            archiveView.SelectedNode.EnsureVisible();
        }

        public void HexBox_CopiedHex(object sender, EventArgs e)
        {
            var hex = Clipboard.GetText();
            var hexHex = hex.Split(' ');
            var hexArr = new byte[hexHex.Length];
            for (var i = 0; i < hexHex.Length; i++)
            {
                hexArr[i] = byte.Parse(hexHex[i], NumberStyles.AllowHexSpecifier);
            }


            HoArchive.TOCEntry asset = ((assetTreeNode)archiveView.SelectedNode).asset;
            
            Asset.AssetEntity entity;
            try { entity = Asset.AssetCaster.ReadAsset(new HoArchive.MemoryStreamEndian(hexArr.ToArray(), handler.endian), asset.wmlTypeID, handler.Archive.Header.target, handler.Archive.Header.platform); }
            catch
            {
                MessageBox.Show("Error", "Error Casting Asset");
                return;
            }
            asset.entity = entity;
            asset.data = hexArr.ToList();

            entityPropertyGrid.SelectedObject = entity;
            entityPropertyGrid.Refresh();
        }

    }
        public class assetTreeNode : TreeNode
        {
            public HoArchive.TOCEntry asset;
            public assetTreeNode(HoArchive.TOCEntry asset_in, string tag_in) : base(tag_in)
            {
                asset = asset_in;
            }
        }
        public class tocTreeNode : TreeNode
        {
            public HoArchive.ParcelTOC toc;
            public tocTreeNode(CSHO.Handler handler, HoArchive.ParcelTOC toc_in) : base("(ParcelTOC)")
            {
                toc = toc_in;

                foreach (HoArchive.TOCEntry asset in toc.Entries)
                {
                    Nodes.Add(new assetTreeNode(asset, handler.GetName(asset.uidSelf)));
                }
            }

            public assetTreeNode getAssetNode(ulong assetid)
            {
                foreach (assetTreeNode node in Nodes)
                {
                    if (node.asset.uidSelf == assetid)
                    {
                        return node;
                    }
                }
                return null;
            }



        }

        public class parcelTreeNode : TreeNode
        {
            public HoArchive.Parcel parcel;
            public HoArchive.TableEntry tableentry;

            public parcelTreeNode(HoArchive.Parcel parcel_in, HoArchive.TableEntry tableentry_in, string tag_in) : base(tag_in)
            {
                parcel = parcel_in;
                tableentry = tableentry_in;
            }

            public assetTreeNode getAssetNode(ulong assetid)
            {
                assetTreeNode output;
                foreach (tocTreeNode node in Nodes)
                {
                    output = node.getAssetNode(assetid);
                    if (output != null) { return output; }
                }
                return null;
            }


            public parcelTreeNode(CSHO.Handler handler, HoArchive.Parcel parcel_in, HoArchive.TableEntry tableentry_in, string tag_in) : base(tag_in)
            {
                parcel = parcel_in;
                tableentry = tableentry_in;

                foreach (HoArchive.ParcelTOC toc in parcel.ParcelTOCs)
                {
                    Nodes.Add(new tocTreeNode(handler, toc));
                }
            }


        }

        public class tableTreeNode : TreeNode
        {
            public HoArchive.Table table;
            public HoArchive.TableEntry tableentry;

            public tableTreeNode(HoArchive.Table table_in, string tag_in, HoArchive.TableEntry tableentry_in = null) : base(tag_in)
            {
                table = table_in;
                tableentry = tableentry_in;
            }

            public tableTreeNode(CSHO.Handler handler, HoArchive.Table table_in, string tag_in, HoArchive.TableEntry tableentry_in = null) : base(tag_in)
            {
                table = table_in;
                tableentry = tableentry_in;

                for (int i = 0; i < table.Parcels.Count; i++)
                {
                    switch (table.TableEntries[i].sectionType)
                    {
                        case "SECT":
                            Nodes.Add(new tableTreeNode(handler, (HoArchive.Table)table.Parcels[i], ((HoArchive.Table)table.Parcels[i]).TableHeader.tableTypeTag, table.TableEntries[i]));
                            break;
                        case "PD  ":
                            break;
                        default:
                            Nodes.Add(new parcelTreeNode(handler, (HoArchive.Parcel)table.Parcels[i], table.TableEntries[i], table.TableEntries[i].sectionType));
                            break;
                    }
                }
            }

            public assetTreeNode getAssetNode(ulong assetid)
            {
                assetTreeNode output = null;
                foreach (TreeNode node in Nodes)
                {
                    if (node is tableTreeNode) { output = ((tableTreeNode)node).getAssetNode(assetid); }
                    else if (node is parcelTreeNode) { output = ((parcelTreeNode)node).getAssetNode(assetid); }

                    if (output != null) { return output; }
                }
                return null;
            }

        }

        public enum searchType
        {
            None,
            Name,
            AssetID,
            Data
        }
}
