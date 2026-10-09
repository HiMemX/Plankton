using PluginApi;

namespace ArchiveEditorPlugin
{
    public class ArchiveEditorPlugin : IPlugin
    {
        public string Id => "plankton.archive-editor";
        public string Name => "Archive Editor";
        public string Description => "Gives the user an Archive Editor with full control over the currently opened .ho Archive.";

        private ArchiveEditorProvider provider = null!;
        private ArchiveEditorPreferences preferences = null!;

        public void Initialize(IHost host)
        {
            preferences = host.Preferences.Register<ArchiveEditorPreferences>(
                "plankton.archive-editor",
                "Archive Editor");

            provider = new ArchiveEditorProvider(host, preferences);
            host.Editors.RegisterProvider(provider);

            host.Commands.Register(
            new CommandDefinition(
                "plankton.archive-editor.open",
                "Archive Editor",
                _ => host.Editors.Open(provider)));

            host.UI.AddCommand(
                "plankton.main.view",
                "plankton.archive-editor.open");
        }

    }
}
