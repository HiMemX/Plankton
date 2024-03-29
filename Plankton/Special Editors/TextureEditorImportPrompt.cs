using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plankton.Special_Editors
{
    public partial class TextureEditorImportPrompt : Form
    {

        public TextureEditorImportPrompt()
        {
            DialogResult = DialogResult.Cancel;
            InitializeComponent();
        
            wrapSComboBox.SelectedIndex = 0;
            wrapTComboBox.SelectedIndex = 0;
            minFilterComboBox.SelectedIndex = 0;
            magFilterComboBox.SelectedIndex = 0;
            wrapSComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            wrapTComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            minFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            magFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.OK;
            Close();
        }
    }
}
