using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginApi
{
    public interface IEditorProvider
    {
        string Id { get; }
        string Name { get; }
        int Priority { get; }

        bool CanOpen(EditorTarget target);

        IEditor CreateEditor(EditorTarget? initialTarget = null);
    }
}
