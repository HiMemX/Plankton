using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plankton
{
    public partial class GenericTextWindow : Form
    {
        public GenericTextWindow(string title, string content)
        {
            InitializeComponent();
            Text = title;
            textBox.Text = content;
            textBox.Select(0, 0);
        }

    }
}
