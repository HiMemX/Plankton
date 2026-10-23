using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginApi;

namespace Plankton.Core.CustomUIEditors
{
    public sealed class StartupEditorsEditor : UITypeEditor
    {
        internal static Func<IReadOnlyList<IEditorProvider>>? GetProviders;

        public override UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext? context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object? EditValue(
            ITypeDescriptorContext? context,
            IServiceProvider provider,
            object? value)
        {
            if (GetProviders == null)
                return value;

            var selectedIds =
                value as List<string> ?? new List<string>();

            using var dialog = new StartupEditorsDialog(
                GetProviders(),
                selectedIds);

            return dialog.ShowDialog() == DialogResult.OK
                ? dialog.SelectedProviderIds
                : value;
        }
    }



    internal sealed class StartupEditorsDialog : Form
    {
        private readonly CheckedListBox editorList;
        private readonly Button okButton;
        private readonly Button cancelButton;

        private readonly IReadOnlyList<IEditorProvider> providers;

        public List<string> SelectedProviderIds { get; private set; } = new();

        public StartupEditorsDialog(
            IReadOnlyList<IEditorProvider> providers,
            IEnumerable<string> selectedProviderIds)
        {
            this.providers = providers;

            Text = "Startup Editors";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;

            ClientSize = new Size(360, 420);

            editorList = new CheckedListBox
            {
                Dock = DockStyle.Fill,
                CheckOnClick = true,
                IntegralHeight = false
            };

            okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                AutoSize = true
            };

            cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                AutoSize = true
            };

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                Padding = new Padding(8)
            };

            buttonPanel.Controls.Add(cancelButton);
            buttonPanel.Controls.Add(okButton);

            Controls.Add(editorList);
            Controls.Add(buttonPanel);

            AcceptButton = okButton;
            CancelButton = cancelButton;

            var selected = new HashSet<string>(
                selectedProviderIds,
                StringComparer.Ordinal);

            for (int i = 0; i < providers.Count; i++)
            {
                IEditorProvider provider = providers[i];

                editorList.Items.Add(
                    provider.Name,
                    selected.Contains(provider.Id));
            }

            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(
            object? sender,
            EventArgs e)
        {
            SelectedProviderIds = new List<string>();

            foreach (int index in editorList.CheckedIndices)
            {
                SelectedProviderIds.Add(
                    providers[index].Id);
            }
        }
    }
}
