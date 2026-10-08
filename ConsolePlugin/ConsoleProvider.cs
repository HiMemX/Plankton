using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginApi;

namespace ConsolePlugin
{
    internal sealed class ConsoleProvider : IEditorProvider
    {
        private readonly IHost host;

        public string Id => "plankton.console.window";
        public string Name => "Console";
        public int Priority => 100;

        internal ConsoleInstance? instance = null;

        public ConsoleProvider(IHost host) {
            this.host = host;
        }
        public bool CanOpen(EditorTarget target)
        {
            return false;
        }
        public IEditor CreateEditor(
        EditorTarget? initialTarget = null)
        {

            instance = new ConsoleInstance(
                host);

            return instance;
        }

        public void AddEntry(string origin, string message, DebugEntryType target)
        {
            if (instance == null) return;

            instance.AddEntry(origin, message, target);
        }
    }
}
