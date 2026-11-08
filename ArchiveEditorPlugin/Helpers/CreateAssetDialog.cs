using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using CSHO;
using HoArchive;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArchiveEditorPlugin.Helpers
{
    public partial class CreateAssetDialog : Form
    {
        private Handler handler;

        private AssetID _uid;

        public ulong UID { get { return _uid.uid; } }

        public TOCEntry? entry = null;


        public CreateAssetDialog(Handler handler)
        {
            InitializeComponent();
            this.handler = handler;

            _uid = new AssetID(handler.GenerateAssetID());


            var binding = new Binding(
                "Text",
                _uid,
                nameof(_uid.uid),
                true,
                DataSourceUpdateMode.OnValidation
            );

            // ulong → hexadecimal string
            binding.Format += (s, e) =>
            {
                if (e.Value is ulong value)
                    e.Value = value.ToString("X16");
            };

            // Hexadecimal string → ulong
            binding.Parse += (s, e) =>
            {
                e.Value = ulong.Parse(
                    (string)e.Value!,
                    NumberStyles.HexNumber,
                    CultureInfo.InvariantCulture
                );
            };

            uidTextBox.DataBindings.Add(binding);

            typeComboBox.DataSource = Enum.GetValues<wmlTypeID>()
                .OrderBy(x => x.ToString())
                .ToArray();
            typeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        private void uidTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Uri.IsHexDigit(e.KeyChar)
                        && !char.IsControl(e.KeyChar);

            if (ulong.TryParse(uidTextBox.Text, out ulong value))
            {
                _uid.uid = value;
            }
        }

        private void openPathButton_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var path = dlg.FileName;
                pathTextBox.Text = path;
            }
        }

        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            selectButton.Enabled = File.Exists(pathTextBox.Text);
        }

        private void selectButton_Click(object sender, EventArgs e)
        {

            List<byte> data = new();

            data = File.ReadAllBytes(pathTextBox.Text).ToList();

            entry = new TOCEntry(UID, (wmlTypeID)typeComboBox.SelectedItem, data_in: data);
            entry.CreateEntity();

        }
    }

    internal class AssetID
    {
        public ulong uid { get; set; }

        public AssetID(ulong uid)
        {
            this.uid = uid;
        }
    }
}
