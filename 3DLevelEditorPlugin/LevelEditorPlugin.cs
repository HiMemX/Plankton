
using PluginApi;
using Rendering;

namespace _3DLevelEditorPlugin;

public sealed class LevelEditorPlugin : IPlugin
{
    private LevelEditorProvider provider = null!;
    private LevelEditorPreferences preferences = null;

    public string Id => "plankton.level-editor";
    public string Name => "Level Editor";
    public string Description => "3D level editor.";

    public void Initialize(IHost host)
    {
        preferences = host.Preferences.Register<LevelEditorPreferences>(
            "plankton.level-editor",
            "Level Editor");
        
        provider = new LevelEditorProvider(host, preferences);

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