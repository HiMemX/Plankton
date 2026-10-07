using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PluginApi;

namespace Plankton
{
    public partial class DebugWindow
    {
        private List<Brush> itembrushes = new();
        public IHost? host = null;




        public void AddEntry(string origin, string message, DebugEntryType type=DebugEntryType.NORMAL)
        {
            if (host == null) return;

            switch (type) {
                case DebugEntryType.NORMAL:
                    host.Commands.Execute("plankton.console.add-entry", (origin, message));
                    break;

                case DebugEntryType.ERROR:
                    host.Commands.Execute("plankton.console.add-error", (origin, message));
                    break;

                case DebugEntryType.WARNING:
                    host.Commands.Execute("plankton.console.add-warning", (origin, message));
                    break;

                case DebugEntryType.SUCCESS:
                    host.Commands.Execute("plankton.console.add-success", (origin, message));
                    break;
            }
        }


        
    }


    public static class Debug
    {
        public static DebugWindow debugWindow = new DebugWindow();
    }

    static class DebugEntryBrush{
        public static Brush GetBrush(DebugEntryType type)
        {
            switch (type)
            {
                case DebugEntryType.NORMAL: return Brushes.Black;
                case DebugEntryType.ERROR: return Brushes.Red;
                case DebugEntryType.WARNING: return Brushes.Yellow;
                case DebugEntryType.SUCCESS: return Brushes.Green;
                default: return Brushes.Black;
            }
        }
    }

    public enum DebugEntryType
    {
        NORMAL,
        ERROR,
        WARNING,
        SUCCESS
    }

    
}
