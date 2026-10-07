using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Keybinds;

public sealed class KeybindEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(
        ITypeDescriptorContext? context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object? EditValue(
        ITypeDescriptorContext? context,
        IServiceProvider provider,
        object? value)
    {
        Keybind current = value as Keybind
            ?? new Keybind(Keys.None);

        using var dialog = new KeybindCaptureDialog(current);

        return dialog.ShowDialog(Form.ActiveForm) == DialogResult.OK
            ? dialog.Keybind
            : value;
    }
}