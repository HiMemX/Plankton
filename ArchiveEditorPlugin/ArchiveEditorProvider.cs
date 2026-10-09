using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginApi;

namespace ArchiveEditorPlugin
{
    internal class ArchiveEditorProvider : IEditorProvider
    {
        private readonly IHost host = null!;

        public string Id => "plankton.archive-editor.window";
        public string Name => "Archive Editor";
        public int Priority => 1000;

        public ArchiveEditorPreferences preferences = null;

        public ArchiveEditorProvider(IHost host, ArchiveEditorPreferences preferences)
        {
            this.host = host;
            this.preferences = preferences;
        }

        public bool CanOpen(EditorTarget target)
        {
            return target.Value is AssetInfo asset &&
                   CanHandleAsset(asset);
        }

        public IEditor CreateEditor(
            EditorTarget? initialTarget = null)
        {
            return new ArchiveEditorInstance(
                host,
                initialTarget,
                preferences);
        }

        private bool CanHandleAsset(AssetInfo asset)
        {
            return true;
        }
    }
}
