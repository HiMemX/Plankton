using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginApi;

namespace Plankton.PluginApiImplementation
{
    internal sealed class Host : IHost
    {
        public IArchiveService Archive { get; }
        public IEditorService Editors { get; }
        public ICommandService Commands { get; }
        public IUIService UI { get; }

        public Host(
            IArchiveService archives,
            IEditorService editors,
            ICommandService commands,
            IUIService ui)
        {
            Archive = archives;
            Editors = editors;
            Commands = commands;
            UI = ui;
        }
    }
}
