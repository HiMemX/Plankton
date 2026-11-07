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

        private List<object> backlog = new();

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

            instance.disposing += InstanceDisposing;

            foreach(object p in backlog)
            {
                if (p is not (string origin, string message, DebugEntryType target)) continue;
                instance.AddEntry(origin, message, target);
            }

            return instance;
        }

        private void InstanceDisposing(object? sender, EventArgs e)
        {
            instance = null;
        }


        public void AddEntry(string origin, string message, DebugEntryType target)
        {
            AddToBacklog(origin, message, target);

            if (instance == null)
            {
                return;
            }

            instance.AddEntry(origin, message, target);
        }

        private void AddToBacklog(string origin, string message, DebugEntryType target)
        {
            backlog.Add((origin, message, target));
        }
    }
}
