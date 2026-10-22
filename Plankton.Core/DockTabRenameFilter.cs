using Plankton.PluginApiImplementation;
using WeifenLuo.WinFormsUI.Docking;

namespace Plankton.Core;

internal sealed class DockTabRenameFilter : IMessageFilter, IDisposable
{
    private const int WM_LBUTTONDBLCLK = 0x0203;

    private readonly DockPanel dockPanel;

    private TextBox? textBox;
    private DockContent? editingContent;
    private bool finishing;

    public DockTabRenameFilter(
        DockPanel dockPanel)
    {
        this.dockPanel = dockPanel;
    }

    public bool PreFilterMessage(ref Message m)
    {
        if (m.Msg != WM_LBUTTONDBLCLK)
            return false;

        if (Control.FromHandle(m.HWnd) is not DockPaneStripBase strip)
            return false;

        if (strip.Parent is not DockPane pane)
            return false;

        if (pane.DockPanel != dockPanel)
            return false;

        Point mousePosition = Control.MousePosition;

        AccessibleObject? hit = strip.AccessibilityObject.HitTest(
            mousePosition.X,
            mousePosition.Y);

        // Make sure the user actually double-clicked a tab,
        // rather than empty space in the tab strip.
        if (hit?.Role != AccessibleRole.PageTab)
            return false;

        if (pane.ActiveContent is not DockContent content)
            return false;

        // Optional: only allow actual editor windows to be renamed.
        if (content is not EditorDockContent)
            return false;

        BeginRename(
            strip,
            content,
            hit.Bounds);

        // Important: consume the double-click so DockPanel Suite
        // doesn't perform its normal dock/float toggle.
        return true;
    }

    private void BeginRename(
        DockPaneStripBase strip,
        DockContent content,
        Rectangle screenBounds)
    {
        FinishRename(commit: true);

        editingContent = content;

        Rectangle bounds =
            strip.RectangleToClient(screenBounds);

        bounds.Inflate(-2, -2);

        textBox = new TextBox
        {
            Parent = strip,
            Text = content.DockHandler.TabText,
            Font = strip.Font,
            BorderStyle = BorderStyle.FixedSingle,
            Bounds = bounds
        };

        textBox.KeyDown += TextBox_KeyDown;
        textBox.LostFocus += TextBox_LostFocus;

        textBox.BringToFront();
        textBox.Focus();
        textBox.SelectAll();
    }

    private void TextBox_KeyDown(
        object? sender,
        KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            FinishRename(commit: true);
        }
        else if (e.KeyCode == Keys.Escape)
        {
            e.SuppressKeyPress = true;
            FinishRename(commit: false);
        }
    }

    private void TextBox_LostFocus(
        object? sender,
        EventArgs e)
    {
        FinishRename(commit: true);
    }

    private void FinishRename(bool commit)
    {
        if (textBox == null || finishing)
            return;

        finishing = true;

        TextBox currentTextBox = textBox;
        DockContent? content = editingContent;

        textBox = null;
        editingContent = null;

        if (commit && content is EditorDockContent editorWindow)
        {
            string name = currentTextBox.Text.Trim();

            if (name.Length > 0)
            {
                editorWindow.Editor.Title = name;
                editorWindow.Text = name;
                editorWindow.TabText = name;
            }
        }

        currentTextBox.KeyDown -= TextBox_KeyDown;
        currentTextBox.LostFocus -= TextBox_LostFocus;
        currentTextBox.Dispose();

        finishing = false;
    }

    public void Dispose()
    {
        FinishRename(commit: false);
    }
}