using ArchiveEditorPlugin.Helpers;
using HoArchive;
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

        private IHost host;

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

            host.Commands.Register(
                new CommandDefinition(
                    "plankton.assets.create-asset",
                    "Create Asset",
                    CreateAsset
                    ));

            host.Commands.Register(new CommandDefinition(
                    "plankton.assets.open-toc-dialog",
                    "Open ParcelTOC Dialog",
                    OpenParcelTOCDialog
                ));

            host.Commands.Register(new CommandDefinition(
                    "plankton.assets.create-asset-dialog",
                    "Create Asset Dialog",
                    CreateAssetDialog
                ));

            this.host = host;
        }

        private TOCEntry? CreateAssetDialog(object? parameters)
        {
            if (host.Archive.Current.Archive == null) return null;

            var dialog = new CreateAssetDialog(host.Archive.Current);
            if (dialog.ShowDialog() == DialogResult.OK) return dialog.entry;

            return null;
        }

        private ParcelTOC? OpenParcelTOCDialog(object? parameters)
        {
            if (host.Archive.Current.Archive == null) return null;

            var dialog = new SelectParcelTOCDialog(host.Archive.Current);
            if (dialog.ShowDialog() == DialogResult.OK) return dialog.GetSelectedTOC();

            return null;
        }

        private TOCEntry? CreateAsset(object? parameters) { // Everywhere nulls...
            ParcelTOC? parcelTOC = null;
            TOCEntry? entry = null;
            if (parameters != null) (parcelTOC, entry) = ((ParcelTOC?, TOCEntry?))parameters;

            if (parcelTOC == null)
            {
                parcelTOC = OpenParcelTOCDialog(null);
                if (parcelTOC == null) return null;
            }

            if(entry == null)
            {
                entry = CreateAssetDialog(null);
                if (entry == null) return null;
            }

            parcelTOC.Entries.Add(entry);

            host.Archive.NotifyAssetsCreated(this, new List<AssetInfo>() {new AssetInfo(host.Archive.Current, entry)});

            return entry;
        }

    }
}
