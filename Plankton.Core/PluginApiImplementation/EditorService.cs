using PluginApi;

namespace Plankton.PluginApiImplementation
{
    internal sealed class EditorService : IEditorService
    {
        private readonly List<IEditorProvider> providers = new();
        private readonly List<IEditor> openEditors = new();

        private readonly Action<IEditor> showEditor;
        private readonly Action<IEditor> activateEditor;

        private IEditor? activeEditor;

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

        // Open an editor without an initial target.
        public IEditor? Open(IEditorProvider provider)
        {
            ValidateProvider(provider);

            return CreateAndShowEditor(
                provider,
                null);
        }

        public IEditor? OpenWith(
            IEditorProvider provider,
            EditorTarget target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            ValidateProvider(provider);

            if (!provider.CanOpen(target))
                return null;

            return CreateAndShowEditor(
                provider,
                target);
        }

        private IEditor CreateAndShowEditor(
            IEditorProvider provider,
            EditorTarget? initialTarget)
        {
            IEditor editor =
                provider.CreateEditor(initialTarget);

            openEditors.Add(editor);

            editor.Control.Disposed += EditorControlDisposed;

            try
            {
                showEditor(editor);
                Activate(editor);

                return editor;
            }
            catch
            {
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

            // Prefer the active editor if it understands the target.
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

        private void ValidateProvider(
            IEditorProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            if (!providers.Contains(provider))
            {
                throw new InvalidOperationException(
                    $"Editor provider '{provider.Id}' is not registered.");
            }
        }

        private void EditorControlDisposed(
            object? sender,
            EventArgs e)
        {
            IEditor? editor = openEditors
                .FirstOrDefault(
                    x => ReferenceEquals(x.Control, sender));

            if (editor == null)
                return;

            editor.Control.Disposed -= EditorControlDisposed;

            openEditors.Remove(editor);

            if (ReferenceEquals(activeEditor, editor))
                activeEditor = null;

            // Important: editor instances may have subscribed to
            // ArchiveService events, so Dispose() must run when the
            // editor is actually closed.
            editor.Dispose();
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