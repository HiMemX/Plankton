using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginApi;
using CSHO;
using HoArchive;

namespace ArchiveEditorPlugin
{
    internal class ArchiveEditorInstance : IEditor
    {

        private readonly IHost host;
        private readonly ArchiveEditorControl control;

        public ArchiveEditorPreferences preferences = new();

        public string Title => "Archive Editor";
        public Icon? Icon => new Icon("Resources/ArchiveEditor.ico");
        public Control Control => control;

        

        public ArchiveEditorInstance(IHost host, EditorTarget initialTarget, ArchiveEditorPreferences preferences) {
            this.host = host;
            this.preferences = preferences;

            control = new ArchiveEditorControl(host, preferences) { Dock = DockStyle.Fill };

            host.Archive.Opened += Archive_Opened;



            if (host.Archive.Current.Archive != null)
                Archive_Opened(null, null);
        }

        public void Archive_Opened(object? sender, ArchiveEventArgs e)
        {
            control.ReloadData();
        }

        public bool CanReveal(EditorTarget target)
        {
            if (target.Value is TOCEntry) return true;
            return false;
        }

        public void Reveal(EditorTarget target)
        {
            // Do nothing for now
        }

        public void Dispose()
        {
            control.Dispose();
        }
    }
}
