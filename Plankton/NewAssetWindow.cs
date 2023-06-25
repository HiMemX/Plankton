using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plankton
{
    public partial class NewAssetWindow : Form
    {
        public bool endian;
        public string target;
        public string platform;
        public List<string> hexpattern;
        public OpenFileDialog openDialog;
        public HoArchive.ParcelDebug debugParcel;
        public tocTreeNode tocTreeNode;
        public TreeView treeView;

        public NewAssetWindow()
        {
            InitializeComponent();

            openDialog = new OpenFileDialog();

            hexpattern = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "a", "b", "c", "d", "e", "f"};
            wmlTypeIDComboBox.DataSource = Enum.GetValues(typeof(HoArchive.wmlTypeID)).Cast<HoArchive.wmlTypeID>().OrderBy(e => e.ToString()).ToList();
            wmlTypeIDComboBox.SelectedIndex = 0;
            blobAlignTextBox.Text = "4";
            subTypeTextBox.Text = "0";
            blobFlagsTextBox.Text = "1";
            nameTextBox.Text = "NewAsset";
        }

        private void addAssetButton_Click(object sender, EventArgs e)
        {
            uint blobAlign = (uint)Int32.Parse(blobAlignTextBox.Text);
            ulong uidSelf = (ulong)Int64.Parse(uidSelfTextBox.Text, System.Globalization.NumberStyles.HexNumber);
            HoArchive.wmlTypeID wmlTypeID = (HoArchive.wmlTypeID)wmlTypeIDComboBox.SelectedItem;
            ushort subType = (ushort)Int16.Parse(subTypeTextBox.Text);
            ushort blobFlags = (ushort)Int16.Parse(blobFlagsTextBox.Text);
            string name = nameTextBox.Text;

            string path = dataTextBox.Text;
            List<byte> data = new List<byte>();
 
            if(path != "") {
                HoArchive.BinaryReaderEndian reader = new HoArchive.BinaryReaderEndian(path, false);

                data = reader.ReadBytes((int)reader.BaseStream.Length).ToList();

                reader.Dispose();
            }
            Asset.AssetEntity entity;
            try {entity = Asset.AssetCaster.ReadAsset(new HoArchive.MemoryStreamEndian(data.ToArray(), endian), wmlTypeID, target, platform) ; }
            catch {
                MessageBox.Show("Error", "Error Casting Asset");
                return;
            }

            HoArchive.TOCEntry entry = new HoArchive.TOCEntry(uidSelf, wmlTypeID, blobAlign_in: blobAlign, subType_in: subType, blobFlags_in: blobFlags, data_in: data, entity_in: entity);
            HoArchive.NameTableEntry nameTableEntry = new HoArchive.NameTableEntry(uidSelf, name);
            assetTreeNode node = new assetTreeNode(entry, name);

            tocTreeNode.Nodes.Add(node);
            tocTreeNode.toc.Entries.Add(entry);
            debugParcel.NameTableEntries.Add(nameTableEntry);

            treeView.SelectedNode = node;
            treeView.Focus();

            tocTreeNode.Expand();

            this.Close();
        }

        private void uidSelfTextBox_TextChanged(object sender, EventArgs e)
        {
            if(uidSelfTextBox.Text.Length > 16) { uidSelfTextBox.Text = uidSelfTextBox.Text.Remove(16); }

            string newText = "";
            foreach(char chr in uidSelfTextBox.Text)
            {
                if (hexpattern.Contains(chr.ToString())) { newText += chr; }
            }

            uidSelfTextBox.Text = newText;
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            if(openDialog.ShowDialog() != DialogResult.OK) { return; }

            dataTextBox.Text = openDialog.FileName;
        }
    }
}
