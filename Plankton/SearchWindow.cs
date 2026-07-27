using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HoArchive;

namespace Plankton
{
    public partial class SearchWindow : Form
    {
        public TreeView treeView;
        public List<TreeNode> nodes = new List<TreeNode>();
        public List<TreeNode> shownnodes = new List<TreeNode>();
        const string nonename = "None";

        public SearchWindow()
        {
            InitializeComponent();

            assetListBox_SelectedIndexChanged(null, null);

            List<string> types = Enum.GetNames(typeof(wmlTypeID)).ToList();
            
            types.Insert(0, nonename);
            typeComboBox.DataSource = types;
        }

        private void assetListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(assetListBox.SelectedIndex == -1) {
                uidLabel.Text = "";
                return;
            }
            uidLabel.Text = "UID: " + ((assetTreeNode)shownnodes[assetListBox.SelectedIndex]).asset.uidSelf.ToString("X16");
            treeView.SelectedNode = shownnodes[assetListBox.SelectedIndex];
            //treeView.TopNode = treeView.SelectedNode;
            //treeView.SelectedNode.EnsureVisible();
        }


        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(typeComboBox.SelectedIndex == -1) { return; }

            wmlTypeID? filter;
            if (typeComboBox.SelectedIndex == 0)
            {
                filter = null;
            }
            else
            {
                filter = (wmlTypeID)Enum.Parse(typeof(wmlTypeID), typeComboBox.SelectedItem.ToString(), true) ;
            }

            string displayText;
            assetListBox.Items.Clear();
            shownnodes.Clear();
            foreach(assetTreeNode node in nodes)
            {
                if (node.asset.delete) { continue ; }

                if(filter != null)
                {
                    if(node.asset.wmlTypeID != filter) { continue; }
                }

                displayText = "(" + node.asset.wmlTypeID.ToString() + ") " + node.Text;
                assetListBox.Items.Add(displayText);
                shownnodes.Add(node);
            }
        }
    }
}
