using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginApi;

namespace HexEditorPlugin
{
    internal class HexEditorProvider : IEditorProvider
    {
        private readonly IHost host;

        public string Id => "plankton.hex-editor.window";
        public string Name => "Hex Editor";
        public int Priority => 100;

        
        public HexEditorProvider(IHost host)
        {
            this.host = host;
        }
        
        public bool CanOpen(EditorTarget target)
        {
            return target.Value is AssetInfo;
        }

        public IEditor CreateEditor(
        EditorTarget? initialTarget = null)
        {
            return new HexEditorInstance(host, initialTarget);
        }

    }
}
