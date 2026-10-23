using PluginApi;

namespace HexEditorPlugin
{
    public sealed class HexEditorPlugin : IPlugin
    {
        private HexEditorProvider provider = null!;
        public string Id => "plankton.hex-editor-plugin";
        public string Name => "Hex Editor";
        public string Description => "Gives the user a hex editor for asset ";


        public void Initialize(IHost host)
        {
            provider = new HexEditorProvider(host);
            host.Editors.RegisterProvider(provider);

            host.Commands.Register(new CommandDefinition(
                "plankton.hex-editor.open",
                "Hex Editor",
                _ => { host.Editors.Open(provider); }
                ));

            //host.UI.AddCommand("plankton.main.view", "plankton.hex-editor.open");

        }

    }
}
