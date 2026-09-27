using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PluginApi
{
    public interface IEditor : IDisposable
    {
        string Title { get; }
        Control Control { get; }
        Icon? Icon { get; }

        bool CanReveal(EditorTarget target);

        void Reveal(EditorTarget target);
    }

}
