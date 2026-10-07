namespace _3DLevelEditorPlugin;
using PluginApi;

internal sealed class LevelEditorProvider : IEditorProvider
{
    private readonly IHost host;

    public string Id => "plankton.level-editor.editor";
    public string Name => "Level Editor";
    public int Priority => 100;

    public LevelEditorPreferences preferences = null;

    public LevelEditorProvider(IHost host, LevelEditorPreferences preferences)
    {
        this.host = host;
        this.preferences = preferences;
    }

    public bool CanOpen(EditorTarget target)
    {
        return target.Value is AssetInfo asset &&
               CanHandleAsset(asset);
    }

    public IEditor CreateEditor(
        EditorTarget? initialTarget = null)
    {
        return new LevelEditorInstance(
            host,
            initialTarget,
            preferences);
    }

    private bool CanHandleAsset(AssetInfo asset)
    {
        // Actual level-editor-supported assets.
        return true;
    }
}