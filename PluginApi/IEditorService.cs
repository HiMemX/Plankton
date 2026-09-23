using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginApi
{
    public interface IEditorService
    {
        /// <summary>
        /// Registers a type of editor that this plugin can create.
        /// </summary>
        IDisposable RegisterProvider(IEditorProvider provider);

        /// <summary>
        /// Finds every installed editor capable of opening this target.
        ///
        /// Useful for "Open With...".
        /// </summary>
        IReadOnlyList<IEditorProvider> GetProvidersFor(
            EditorTarget target);

        /// <summary>
        /// Finds already-open editors capable of revealing this target.
        ///
        /// Useful for "Reveal In...".
        /// </summary>
        IReadOnlyList<IEditor> GetOpenEditorsFor(
            EditorTarget target);

        /// <summary>
        /// Opens the target using the preferred provider.
        /// </summary>
        IEditor? Open(EditorTarget target);

        /// <summary>
        /// Opens using a particular provider.
        /// Useful for an "Open With..." menu.
        /// </summary>
        IEditor? OpenWith(
            IEditorProvider provider,
            EditorTarget target);

        /// <summary>
        /// Tries to reveal the target in an already-open editor.
        /// </summary>
        bool Reveal(EditorTarget target);

        /// <summary>
        /// Brings an editor to the front.
        /// </summary>
        void Activate(IEditor editor);
    }
}
