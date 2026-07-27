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
    public partial class PropertyGridWindow : Form
    {
        TOCEntry entry;

        public PropertyGridWindow()
        {
            InitializeComponent();
        }

        public void SetObject(TOCEntry entry)
        {
            this.entry = entry;
            propertyGrid1.SelectedObject = entry.entity;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            entry.Update(0x40, true);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = checkBox1.Checked;
        }
    }
}
