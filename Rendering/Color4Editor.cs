using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using OpenTK.Mathematics;

public sealed class Color4Editor : UITypeEditor
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
        if (value is not Color4 color)
            return value;

        using var dialog = new ColorDialog
        {
            FullOpen = true,
            Color = Color.FromArgb(
                255,
                ToByte(color.R),
                ToByte(color.G),
                ToByte(color.B))
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return value;

        Color selected = dialog.Color;

        return new Color4(
            selected.R / 255f,
            selected.G / 255f,
            selected.B / 255f,
            color.A); // preserve alpha
    }

    private static int ToByte(float value)
    {
        return (int)(Math.Clamp(value, 0f, 1f) * 255f);
    }
}