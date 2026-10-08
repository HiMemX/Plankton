using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginApi;

namespace ConsolePlugin
{
    internal sealed class ConsoleInstance : IEditor
    {
        private readonly IHost host;
        private readonly ConsoleForm control;

        public string Title => "Console";
        public Icon? Icon => new Icon("Resources/Console.ico");
        public Control Control => control;

        public ConsoleInstance(IHost host)
        {
            this.host = host;
            this.control = new ConsoleForm();
            this.control.host = host;
        }

        public bool CanReveal(EditorTarget target) => false;

        public void Reveal(EditorTarget target) { }

        public void Dispose()
        {
            control.Dispose();
        }

        public void AddEntry(string origin, string message, DebugEntryType type)
        {
            if (control == null) return;
            control.AddEntry(origin, message, type);
        }
    }
}
