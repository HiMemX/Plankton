using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Plankton.InternalEditors
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class DirectEditorWindow : Form
    {
        public DirectEditorWindow()
        {
            InitializeComponent();
        }

        private void DirectEditorWindow_Load(object sender, EventArgs e)
        {
            byteViewerBox.ByteProvider = new DynamicByteProvider(Plankton.assetForHexEditor);
        }

        private void btnSaveBytes_Click(object sender, EventArgs e)
        {
            byteViewerBox.SelectAll();
            byteViewerBox.CopyHex();
            byteViewerBox.SelectionLength = 0;
            Plankton form = Application.OpenForms.OfType<Plankton>().FirstOrDefault();
            if (form != null)
            {
                form.HexBox_CopiedHex(sender, e);
            }
        }
    }
}
