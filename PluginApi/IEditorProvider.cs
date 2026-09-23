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

        /// <summary>
        /// Used when Plankton needs to choose a default editor.
        /// Higher values are preferred.
        /// </summary>
        int Priority { get; }

        bool CanOpen(EditorTarget target);

        IEditor CreateEditor(EditorTarget target);
    }
}
