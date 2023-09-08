using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Plankton.Plankton;

namespace Plankton
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class NewParcelWindow : Form
    {
        public tableTreeNode currentNode;
        public NewParcelWindow()
        {

            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            packLangIDListBox.SelectedIndex = 0;
            parcelTypeListBox.SelectedIndex = 0;
        }

        private void NewParcelWindow_Load(object sender, EventArgs e)
        {

        }

        private void addParcelButton_Click(object sender, EventArgs e)
        {

            string sectionType = (string)comboBox1.SelectedItem;
            HoArchive.TableEntry newTableEntry = new HoArchive.TableEntry(sectionType, packLangID_in: (HoArchive.LanguageID)packLangIDListBox.SelectedItem, parcelType_in: (HoArchive.enParcelType)parcelTypeListBox.SelectedItem);
            List<HoArchive.SliceMeta> newSliceMeta = new List<HoArchive.SliceMeta>();
            HoArchive.ParcelBase newParcel;
            TreeNode newTreeNode;

            switch (sectionType)
            {
                case "SECT":
                    newParcel = new HoArchive.Table(sectionType, "");
                    newTreeNode = new tableTreeNode((HoArchive.Table)newParcel, sectionType, newTableEntry);
                    break;

                default:
                    newParcel = new HoArchive.Parcel();
                    newSliceMeta.Add(new HoArchive.ParcelSliceMeta(reversedCheckBox.Checked));
                    newTreeNode = new parcelTreeNode((HoArchive.Parcel)newParcel, newTableEntry, sectionType);
                    break;
            }


            currentNode.table.TableEntries.Add(newTableEntry);
            currentNode.table.Parcels.Add(newParcel);
            currentNode.table.MetaTableEntries.Add(newSliceMeta);
            currentNode.Nodes.Add(newTreeNode);
            currentNode.Expand();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
