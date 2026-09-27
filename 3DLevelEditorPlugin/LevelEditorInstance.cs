using System.Drawing;
using System.Windows.Forms;
using CSHO;
using PluginApi;
using Plankton.Special_Editors.Level_Editor;

namespace _3DLevelEditorPlugin;

internal sealed class LevelEditorInstance : IEditor
{
    private readonly IHost host;
    private readonly LevelEditor control;

    public string Title => "Level Editor";
    public Icon? Icon => new Icon("Resources/LevelEditor.ico");
    public Control Control => control;

    public LevelEditorInstance(
        IHost host,
        EditorTarget? initialTarget = null)
    {
        this.host = host;

        control = new LevelEditor
        {
            Dock = DockStyle.Fill
        };

        control.handler = host.Archive.Current;
        control.InitRenderer();

        host.Archive.Opened += Archive_Opened;
        host.Archive.Closed += Archive_Closed;
        host.Archive.AssetsModified += Archive_AssetsModified;
        host.Archive.AssetsModifiedPreview += Archive_AssetsModifiedPreview;

        // An archive might already be open when this editor is created.
        if (host.Archive.Current.Archive != null)
            LoadArchive(host.Archive.Current);

        // Occasionally we DO open the editor for something specific.
        if (initialTarget != null)
            Reveal(initialTarget);
    }

    private void Archive_Opened(
        object? sender,
        ArchiveEventArgs e)
    {
        LoadArchive(e.Archive);
    }

    private void Archive_Closed(
        object? sender,
        ArchiveEventArgs e)
    {
        control.CollectResourcePools();
    }

    private void Archive_AssetsModified(
        object? sender,
        AssetEventArgs e)
    {
        //control.AssetsModified(e.Assets);
        
    }

    private void Archive_AssetsModifiedPreview(
        object? sender,
        AssetEventArgs e)
    {
        //control.AssetsModifiedPreview(e.Assets);
    }

    private void LoadArchive(Handler archive)
    {
        control.CollectResourcePools();
    }

    public bool CanReveal(EditorTarget target)
    {
        if (host.Archive.Current == null)
            return false;

        return target.Value is AssetInfo asset &&
               CanDisplayAsset(asset);
    }

    public void Reveal(EditorTarget target)
    {
        if (target.Value is AssetInfo asset &&
            CanDisplayAsset(asset))
        {
            control.SetSelectedContainer(asset.Entry);
        }
    }

    private bool CanDisplayAsset(AssetInfo asset)
    {
        // Your actual check.
        return true;
    }

    public void Dispose()
    {
        // Important because ArchiveService outlives the editor.
        host.Archive.Opened -= Archive_Opened;
        host.Archive.Closed -= Archive_Closed;
        host.Archive.AssetsModified -= Archive_AssetsModified;
        host.Archive.AssetsModifiedPreview -= Archive_AssetsModifiedPreview;

        control.Dispose();
    }
}