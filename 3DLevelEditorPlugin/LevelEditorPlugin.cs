
using PluginApi;

namespace _3DLevelEditorPlugin;

public sealed class LevelEditorPlugin : IPlugin
{
    private LevelEditorProvider provider = null!;

    public string Id => "plankton.level-editor";
    public string Name => "Level Editor";
    public string Description => "3D level editor.";

    public void Initialize(IHost host)
    {
        provider = new LevelEditorProvider(host);

        host.Editors.RegisterProvider(provider);

        host.Commands.Register(
            new CommandDefinition(
                "plankton.level-editor.open",
                "Level Editor",
                _ => host.Editors.Open(provider)));

        host.UI.AddCommand(
            "plankton.main.view",
            "plankton.level-editor.open");
    }
}