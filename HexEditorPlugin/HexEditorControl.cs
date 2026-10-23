using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginApi;
using Be.Windows.Forms;

namespace HexEditorPlugin
{
    public partial class HexEditorControl : UserControl
    {
        private readonly HexBox hexBox;
        private readonly StatusStrip statusStrip;
        private readonly ToolStripStatusLabel offsetLabel;
        private readonly ToolStripStatusLabel selectionLabel;
        private readonly ToolStripStatusLabel lengthLabel;
        private readonly ToolStripStatusLabel modifiedLabel;

        private DynamicByteProvider? byteProvider;
        private bool loading;

        public event EventHandler? DataChanged;
        public event EventHandler? ModifiedChanged;

        public bool IsModified { get; private set; }

        public bool ReadOnly
        {
            get => hexBox.ReadOnly;
            set => hexBox.ReadOnly = value;
        }

        public long SelectionStart => hexBox.SelectionStart;
        public long SelectionLength => hexBox.SelectionLength;

        public HexBox HexBox => hexBox;

        public HexEditorControl()
        {
            hexBox = new HexBox
            {
                Dock = DockStyle.Fill,

                LineInfoVisible = true,
                ColumnInfoVisible = true,
                StringViewVisible = true,

                VScrollBarVisible = true,

                UseFixedBytesPerLine = true,
                BytesPerLine = 16,

                BorderStyle = BorderStyle.None
            };

            offsetLabel = new ToolStripStatusLabel
            {
                Text = "Offset: 0x00000000"
            };

            selectionLabel = new ToolStripStatusLabel
            {
                Text = "Selection: 0"
            };

            lengthLabel = new ToolStripStatusLabel
            {
                Text = "Length: 0"
            };

            modifiedLabel = new ToolStripStatusLabel
            {
                Text = ""
            };

            statusStrip = new StatusStrip
            {
                Dock = DockStyle.Bottom,
                SizingGrip = false
            };

            statusStrip.Items.Add(offsetLabel);
            statusStrip.Items.Add(new ToolStripSeparator());
            statusStrip.Items.Add(selectionLabel);
            statusStrip.Items.Add(new ToolStripSeparator());
            statusStrip.Items.Add(lengthLabel);
            statusStrip.Items.Add(new ToolStripStatusLabel
            {
                Spring = true
            });
            statusStrip.Items.Add(modifiedLabel);

            Controls.Add(hexBox);
            Controls.Add(statusStrip);

            hexBox.SelectionStartChanged += HexBox_SelectionChanged;
            hexBox.SelectionLengthChanged += HexBox_SelectionChanged;

            LoadBytes([]);
        }

        public void LoadBytes(byte[] data)
        {
            ArgumentNullException.ThrowIfNull(data);

            loading = true;

            try
            {
                DetachByteProvider();

                // Make our own copy so editing the hex control doesn't
                // unexpectedly modify the caller's array.
                byte[] copy = data.ToArray();

                byteProvider = new DynamicByteProvider(copy);

                byteProvider.Changed += ByteProvider_Changed;
                byteProvider.LengthChanged += ByteProvider_LengthChanged;

                hexBox.ByteProvider = byteProvider;

                SetModified(false);
                UpdateStatus();
            }
            finally
            {
                loading = false;
            }
        }

        public byte[] GetBytes()
        {
            if (hexBox.ByteProvider == null)
                return [];

            long length = hexBox.ByteProvider.Length;

            if (length > int.MaxValue)
            {
                throw new InvalidOperationException(
                    "The data is too large to return as a byte array.");
            }

            byte[] data = new byte[(int)length];

            for (long i = 0; i < length; i++)
                data[i] = hexBox.ByteProvider.ReadByte(i);

            return data;
        }

        public void SetModified(bool modified)
        {
            if (IsModified == modified)
                return;

            IsModified = modified;

            modifiedLabel.Text =
                modified ? "Modified" : "";

            ModifiedChanged?.Invoke(
                this,
                EventArgs.Empty);
        }

        public void GoToOffset(long offset)
        {
            if (hexBox.ByteProvider == null)
                return;

            if (hexBox.ByteProvider.Length == 0)
                return;

            offset = Math.Clamp(
                offset,
                0,
                hexBox.ByteProvider.Length - 1);

            hexBox.SelectionStart = offset;
            hexBox.SelectionLength = 0;

            hexBox.ScrollByteIntoView(offset);
            hexBox.Focus();

            UpdateStatus();
        }

        public void SelectRange(
            long offset,
            long length)
        {
            if (hexBox.ByteProvider == null)
                return;

            if (offset < 0 ||
                length < 0 ||
                offset + length > hexBox.ByteProvider.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            hexBox.SelectionStart = offset;
            hexBox.SelectionLength = length;

            hexBox.ScrollByteIntoView(offset);
            hexBox.Focus();

            UpdateStatus();
        }

        private void ByteProvider_Changed(
            object? sender,
            EventArgs e)
        {
            if (loading)
                return;

            SetModified(true);

            UpdateStatus();

            DataChanged?.Invoke(
                this,
                EventArgs.Empty);
        }

        private void ByteProvider_LengthChanged(
            object? sender,
            EventArgs e)
        {
            if (loading)
                return;

            SetModified(true);

            UpdateStatus();

            DataChanged?.Invoke(
                this,
                EventArgs.Empty);
        }

        private void HexBox_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            long offset = hexBox.SelectionStart;
            long selection = hexBox.SelectionLength;
            long length = hexBox.ByteProvider?.Length ?? 0;

            offsetLabel.Text =
                $"Offset: 0x{offset:X8}";

            selectionLabel.Text =
                $"Selection: {selection}";

            lengthLabel.Text =
                $"Length: {length} (0x{length:X})";
        }

        private void DetachByteProvider()
        {
            if (byteProvider == null)
                return;

            byteProvider.Changed -= ByteProvider_Changed;
            byteProvider.LengthChanged -= ByteProvider_LengthChanged;

            if (ReferenceEquals(
                    hexBox.ByteProvider,
                    byteProvider))
            {
                hexBox.ByteProvider = null;
            }

            byteProvider = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                DetachByteProvider();

            base.Dispose(disposing);
        }
    }
}
