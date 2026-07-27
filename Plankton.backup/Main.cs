using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;

namespace Plankton
{
    public partial class Plankton : Form
    {
        CSHO.Handler handler;
        OpenFileDialog openDialog;
        public Plankton()
        {
            handler = new CSHO.Handler();
            openDialog = new OpenFileDialog();

            openDialog.InitialDirectory = "c:\\";
            openDialog.Filter = "Havok Objects (*.ho)|*.ho";
            openDialog.FilterIndex = 0;
            openDialog.RestoreDirectory = true;
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

        private void openCTRLOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openDialog.ShowDialog() == DialogResult.OK){
                string filePath = openDialog.FileName;

                string errorcode = handler.Open(filePath);

                if(errorcode != "")
                {
                    MessageBox.Show(errorcode);
                }
            }
        }
    }
}
