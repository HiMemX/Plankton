using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginApi;
using WeifenLuo.WinFormsUI.Docking;

namespace Plankton.Core
{
    internal sealed class EditorDockContent : DockContent
    {
        public IEditor Editor { get; }

        public EditorDockContent(IEditor editor)
        {
            Editor = editor;

            Text = editor.Title;

            if (editor.Icon != null)
                Icon = editor.Icon;

            editor.Control.Dock = DockStyle.Fill;

            Controls.Add(editor.Control);
        }

        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            Editor.Dispose();

            base.OnFormClosed(e);
        }
    }
}
