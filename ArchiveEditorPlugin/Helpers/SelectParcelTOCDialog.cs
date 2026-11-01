using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSHO;
using HoArchive;

namespace ArchiveEditorPlugin.Helpers
{
    public partial class SelectParcelTOCDialog : Form
    {
        Handler handler;

        public SelectParcelTOCDialog(CSHO.Handler handler)
        {
            InitializeComponent();


            this.handler = handler;

            Populate();
        }


        private void Populate()
        {
            tocTreeView.Nodes.Add(
                ArchiveTreeViewTools.CreateNode(handler.Archive.MasterTable)
            );
            tocTreeView.Focus();
            tocTreeView.ExpandAll();

            SelectFirstTOC(tocTreeView.Nodes[0]);
        }

        private bool SelectFirstTOC(TreeNode node)
        {
            if ((node.Tag as NodeTag).obj is ParcelTOC)
            {
                tocTreeView.SelectedNode = node;

                node.EnsureVisible();
                return true;
            }
            foreach (TreeNode child in node.Nodes)
            {
                if (SelectFirstTOC(child)) return true;
            }

            return false;
        }

        private void SelectParcelTOCDialog_Load(object sender, EventArgs e)
        {

        }

        private void tocTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectButton.Enabled = (e.Node.Tag as NodeTag).obj is ParcelTOC;
        }

        private void tocTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AcceptButton.PerformClick();
        }
    }
}
