using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plankton.Special_Editors.Level_Editor
{
    public partial class ExportWindow : Form
    {
        public ExportWindow()
        {
            InitializeComponent();
        }

        public void exportButton_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
