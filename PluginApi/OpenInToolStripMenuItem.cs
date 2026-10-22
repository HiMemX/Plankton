using System.Windows.Forms;

namespace PluginApi;

public sealed class OpenInToolStripMenuItem : ToolStripMenuItem
{
    private readonly IEditorService editors;
    private readonly Func<EditorTarget?> getTarget;

    public OpenInToolStripMenuItem(
        IEditorService editors,
        Func<EditorTarget?> getTarget)
        : base("Open in...")
    {
        this.editors = editors;
        this.getTarget = getTarget;

        DropDownOpening += OnDropDownOpening;
    }

    private void OnDropDownOpening(object? sender, EventArgs e)
    {
        DropDownItems.Clear();

        EditorTarget? target = getTarget();

        if (target == null)
        {
            DropDownItems.Add(new ToolStripMenuItem("No asset selected")
            {
                Enabled = false
            });

            return;
        }

        AddNewEditors(target);
        AddOpenEditors(target);

        if (DropDownItems.Count == 0)
        {
            DropDownItems.Add(new ToolStripMenuItem("No compatible editors")
            {
                Enabled = false
            });
        }
    }


    private void AddNewEditors(EditorTarget target)
    {
        IReadOnlyList<IEditorProvider> providers =
            editors.GetProvidersFor(target);

        var newItem = new ToolStripMenuItem("New")
        {
            Enabled = providers.Count > 0
        };

        foreach (IEditorProvider provider in providers)
        {
            var providerItem = new ToolStripMenuItem(provider.Name);

            providerItem.Click += (_, _) =>
            {
                editors.OpenWith(provider, target);
            };

            newItem.DropDownItems.Add(providerItem);
        }

        DropDownItems.Add(newItem);
    }

    private void AddOpenEditors(EditorTarget target)
    {
        IReadOnlyList<IEditor> openEditors =
            editors.GetOpenEditorsFor(target);

        if (openEditors.Count == 0)
            return;

        DropDownItems.Add(new ToolStripSeparator());

        foreach (IEditor editor in openEditors)
        {
            var editorItem = new ToolStripMenuItem(editor.Title);

            if (editor.Icon != null)
                editorItem.Image = editor.Icon.ToBitmap();

            editorItem.Click += (_, _) =>
            {
                editor.Reveal(target);
                editors.Activate(editor);
            };

            DropDownItems.Add(editorItem);
        }
    }
}