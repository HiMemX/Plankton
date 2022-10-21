using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plankton
{
    public partial class Plankton : Form  
    {
        CSHO.Handler handler;
        Asset.AssetKey assetkey;
        OpenFileDialog openDialog;
        SaveFileDialog saveDialog;
        EditHeaderWindow editHeaderWindow;
        public Plankton()
        {
            handler = new CSHO.Handler();
            assetkey = new Asset.AssetKey();
            openDialog = new OpenFileDialog();
            saveDialog = new SaveFileDialog(); 

            openDialog.InitialDirectory = "c:\\";
            openDialog.Filter = "HO Archives (*.ho)|*.ho";
            openDialog.FilterIndex = 0;
            InitializeComponent();
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            archiveView.Nodes.Clear();
            hideAllClassGroupBoxes();
            if (handler.Archive == null) { return; }
            handler.Update();
            archiveView.Nodes.Add(new tableTreeNode(handler, (HoArchive.Table)handler.Archive.MasterTable, ((HoArchive.Table)handler.Archive.MasterTable).TableHeader.tableTypeTag));

        }

        private void openCTRLOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openDialog.ShowDialog() != DialogResult.OK){
                return;
            }

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
            Text = "Plankton - " + filePath;

            MessageBox.Show(stats, "Success");
        }

        private void reloadTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateTree();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            handler.Close();
            Text = "Plankton";
            updateTree();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void archiveView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string nodeType = archiveView.SelectedNode.GetType().ToString();

            switch (nodeType)
            {
                case "Plankton.assetTreeNode":
                    parcelGroupBox.Visible = false;
                    parcelTOCGroupBox.Visible = false;
                    assetGroupBox.Visible = true;
                    tableGroupBox.Visible = false;

                    assetPropertyGrid.SelectedObject = ((assetTreeNode)archiveView.SelectedNode).asset;
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
            if(saveDialog.ShowDialog() != DialogResult.OK) { return; }

            string filepath = saveDialog.FileName;

            if(filepath == null) { return; }

            HoArchive.BinaryWriterEndian writer = new HoArchive.BinaryWriterEndian(filepath, false);

            writer.WriteE(((assetTreeNode)archiveView.SelectedNode).asset.data.ToArray());

            writer.Dispose();
        }

        private void importRawDataButton_Click(object sender, EventArgs e)
        {
            if(openDialog.ShowDialog() != DialogResult.OK) { return; }

            string filepath = openDialog.FileName;

            HoArchive.BinaryReaderEndian reader = new HoArchive.BinaryReaderEndian(filepath, false);

            ((assetTreeNode)archiveView.SelectedNode).asset.data = reader.ReadBytes((int)reader.BaseStream.Length).ToList();
        }

        private void newParcelTOCButton_Click(object sender, EventArgs e)
        {
            handler.NewParcelTOC(((parcelTreeNode)archiveView.SelectedNode).parcel);

            archiveView.SelectedNode.Nodes.Add(new tocTreeNode(handler, ((parcelTreeNode)archiveView.SelectedNode).parcel.ParcelTOCs.Last()));
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
            tableentry.name = renameAssetTextBox.Text;
            archiveView.SelectedNode.Text = renameAssetTextBox.Text;
        }

        private void deleteTableButton_Click(object sender, EventArgs e)
        {
            if(((tableTreeNode)archiveView.SelectedNode).tableentry == null) { return; }
            ((tableTreeNode)archiveView.SelectedNode).table.delete = true;
            archiveView.SelectedNode.Parent.Nodes.Remove(archiveView.SelectedNode);
        }

        private void domainStringTextBox_TextChanged(object sender, EventArgs e)
        {
            ((tableTreeNode)archiveView.SelectedNode).table.StringTable.DomainString = domainStringTextBox.Text;
        }

        private void saveCTRLSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (handler.Archive == null) { return;}

            string errorcode = handler.Save();

            if(errorcode != "")
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
            if(stringTableListBox.Items.Count == 0) { return; }

            ((tableTreeNode)archiveView.SelectedNode).table.StringTable.StringTableEntries.RemoveAt(stringTableListBox.SelectedIndex);
            stringTableListBox.Items.RemoveAt(stringTableListBox.SelectedIndex);
        }

        private void newFromCTRLSHIFTOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string filePath = openDialog.FileName;

            string errorcode = handler.NewFrom(filePath);

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
            Text = "Plankton - " + filePath;

            MessageBox.Show(stats, "Success");
        }

        private void editHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(handler.Archive == null) { return; }

            editHeaderWindow = new EditHeaderWindow();
            editHeaderWindow.editHeaderPropertyGrid.SelectedObject = handler.Archive.Header;
            editHeaderWindow.Show();
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

            foreach(HoArchive.TOCEntry asset in toc.Entries)
            {
                Nodes.Add(new assetTreeNode(asset, handler.GetName(asset.uidSelf)));
            }
        }
    }

    public class parcelTreeNode : TreeNode
    {
        public HoArchive.Parcel parcel;
        public HoArchive.TableEntry tableentry;

        public parcelTreeNode(CSHO.Handler handler, HoArchive.Parcel parcel_in, HoArchive.TableEntry tableentry_in, string tag_in) : base(tag_in)
        {
            parcel = parcel_in;
            tableentry = tableentry_in;

            foreach(HoArchive.ParcelTOC toc in parcel.ParcelTOCs)
            {
                Nodes.Add(new tocTreeNode(handler, toc));
            }
        }
    }

    public class tableTreeNode : TreeNode
    {
        public HoArchive.Table table;
        public HoArchive.TableEntry tableentry;
        public tableTreeNode(CSHO.Handler handler, HoArchive.Table table_in, string tag_in, HoArchive.TableEntry tableentry_in = null) : base(tag_in)
        {
            table = table_in;
            tableentry = tableentry_in;

            for(int i=0; i<table.Parcels.Count; i++)
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
    }
}
