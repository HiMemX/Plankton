using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.PluginApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using global::PluginApi;

    namespace PluginApi
    {
        internal sealed class EditorService : IEditorService
        {
            private readonly List<IEditorProvider> providers = new();
            private readonly List<IEditor> openEditors = new();

            private readonly Action<IEditor> showEditor;
            private readonly Action<IEditor> activateEditor;

            private IEditor? activeEditor;

            /// <summary>
            /// showEditor:
            ///     Called when a newly-created editor needs to be added
            ///     to Plankton's UI.
            ///
            /// activateEditor:
            ///     Called when an existing editor needs to be brought
            ///     to the front.
            /// </summary>
            public EditorService(
                Action<IEditor> showEditor,
                Action<IEditor> activateEditor)
            {
                this.showEditor =
                    showEditor ?? throw new ArgumentNullException(nameof(showEditor));

                this.activateEditor =
                    activateEditor ?? throw new ArgumentNullException(nameof(activateEditor));
            }

            public IDisposable RegisterProvider(IEditorProvider provider)
            {
                if (provider == null)
                    throw new ArgumentNullException(nameof(provider));

                if (providers.Any(x => x.Id == provider.Id))
                {
                    throw new InvalidOperationException(
                        $"An editor provider with ID '{provider.Id}' is already registered.");
                }

                providers.Add(provider);

                return new Registration(() =>
                {
                    providers.Remove(provider);
                });
            }

            public IReadOnlyList<IEditorProvider> GetProvidersFor(
                EditorTarget target)
            {
                if (target == null)
                    throw new ArgumentNullException(nameof(target));

                return providers
                    .Where(x => x.CanOpen(target))
                    .OrderByDescending(x => x.Priority)
                    .ToArray();
            }

            public IReadOnlyList<IEditor> GetOpenEditorsFor(
                EditorTarget target)
            {
                if (target == null)
                    throw new ArgumentNullException(nameof(target));

                return openEditors
                    .Where(x => x.CanReveal(target))
                    .ToArray();
            }

            public IEditor? Open(EditorTarget target)
            {
                if (target == null)
                    throw new ArgumentNullException(nameof(target));

                IEditorProvider? provider = providers
                    .Where(x => x.CanOpen(target))
                    .OrderByDescending(x => x.Priority)
                    .FirstOrDefault();

                if (provider == null)
                    return null;

                return OpenWith(provider, target);
            }

            public IEditor? OpenWith(
                IEditorProvider provider,
                EditorTarget target)
            {
                if (provider == null)
                    throw new ArgumentNullException(nameof(provider));

                if (target == null)
                    throw new ArgumentNullException(nameof(target));

                // Don't allow arbitrary provider objects that were never
                // registered with this service.
                if (!providers.Contains(provider))
                {
                    throw new InvalidOperationException(
                        $"Editor provider '{provider.Id}' is not registered.");
                }

                if (!provider.CanOpen(target))
                    return null;

                IEditor editor = provider.CreateEditor(target);

                openEditors.Add(editor);

                // Since IEditorService currently has no Close() method,
                // notice when the WinForms control is disposed and remove
                // the editor from our list automatically.
                editor.Control.Disposed += EditorControlDisposed;

                try
                {
                    showEditor(editor);
                    Activate(editor);

                    return editor;
                }
                catch
                {
                    // Don't leave a half-open editor registered if putting
                    // it into Plankton's UI failed.
                    editor.Control.Disposed -= EditorControlDisposed;
                    openEditors.Remove(editor);
                    editor.Dispose();

                    throw;
                }
            }

            public bool Reveal(EditorTarget target)
            {
                if (target == null)
                    throw new ArgumentNullException(nameof(target));

                // Prefer the currently-active editor if it can reveal it.
                if (activeEditor != null &&
                    openEditors.Contains(activeEditor) &&
                    activeEditor.CanReveal(target))
                {
                    activeEditor.Reveal(target);
                    return true;
                }

                IEditor? editor = openEditors
                    .FirstOrDefault(x => x.CanReveal(target));

                if (editor == null)
                    return false;

                Activate(editor);
                editor.Reveal(target);

                return true;
            }

            public void Activate(IEditor editor)
            {
                if (editor == null)
                    throw new ArgumentNullException(nameof(editor));

                if (!openEditors.Contains(editor))
                {
                    throw new InvalidOperationException(
                        "Cannot activate an editor that is not open.");
                }

                activateEditor(editor);
                activeEditor = editor;
            }

            private void EditorControlDisposed(
                object? sender,
                EventArgs e)
            {
                IEditor? editor = openEditors
                    .FirstOrDefault(x => ReferenceEquals(x.Control, sender));

                if (editor == null)
                    return;

                editor.Control.Disposed -= EditorControlDisposed;

                openEditors.Remove(editor);

                if (ReferenceEquals(activeEditor, editor))
                    activeEditor = null;
            }

            private sealed class Registration : IDisposable
            {
                private Action? disposeAction;

                public Registration(Action disposeAction)
                {
                    this.disposeAction = disposeAction;
                }

                public void Dispose()
                {
                    disposeAction?.Invoke();
                    disposeAction = null;
                }
            }
        }
    }
}
