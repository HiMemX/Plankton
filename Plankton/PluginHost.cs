using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginApi;

namespace Plankton
{
    internal class PluginHost : IHost
    {
        public event EventHandler<FileEventArgs>? OpenedArchive;
        public event EventHandler<FileEventArgs>? ClosedArchive;

        public void InvokeOpenedArchive(object sender, FileEventArgs args)
        {
            OpenedArchive?.Invoke(sender, args);
        }

        public void InvokeClosedArchive(object sender, FileEventArgs args)
        {
            ClosedArchive?.Invoke(sender, args);
        }
    }


}
