using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;

namespace Keybinds;

[TypeConverter("Keybinds.KeybindTypeConverter")]
[Editor("Keybinds.KeybindEditor", typeof(UITypeEditor))]
public sealed class Keybind
{
    public Keys key { get; set; } = Keys.None;
    public Keys modifier { get; set; } = Keys.None;

    // Used by System.Text.Json
    public Keybind()
    {
    }

    // Convenient for defaults in preference classes
    public Keybind(Keys key, Keys modifier = Keys.None)
    {
        this.key = key;
        this.modifier = modifier;
    }
}

public sealed class KeybindTypeConverter : TypeConverter
{
    public override bool CanConvertTo(
        ITypeDescriptorContext? context,
        Type? destinationType)
    {
        if (destinationType == typeof(string))
            return true;

        return base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertTo(
        ITypeDescriptorContext? context,
        CultureInfo? culture,
        object? value,
        Type destinationType)
    {
        if (destinationType == typeof(string) &&
            value is Keybind keybind)
        {
            return GetDisplayText(keybind);
        }

        return base.ConvertTo(
            context,
            culture,
            value,
            destinationType);
    }

    public static string GetDisplayText(Keybind keybind)
    {
        if (keybind.key == Keys.None)
            return "(None)";

        string key = GetKeyName(keybind.key);

        if (keybind.modifier == Keys.None)
            return key;

        return $"{GetModifierName(keybind.modifier)}+{key}";
    }

    private static string GetModifierName(Keys modifier)
    {
        return modifier switch
        {
            Keys.Control => "Ctrl",
            Keys.ControlKey => "Ctrl",
            Keys.LControlKey => "Ctrl",
            Keys.RControlKey => "Ctrl",

            Keys.Shift => "Shift",
            Keys.ShiftKey => "Shift",
            Keys.LShiftKey => "Shift",
            Keys.RShiftKey => "Shift",

            Keys.Alt => "Alt",
            Keys.Menu => "Alt",
            Keys.LMenu => "Alt",
            Keys.RMenu => "Alt",

            _ => modifier.ToString()
        };
    }

    private static string GetKeyName(Keys key)
    {
        return key switch
        {
            Keys.Return => "Enter",
            Keys.Escape => "Esc",
            Keys.Space => "Space",
            Keys.Back => "Backspace",
            Keys.Delete => "Delete",

            Keys.Oemcomma => ",",
            Keys.OemPeriod => ".",
            Keys.OemMinus => "-",
            Keys.Oemplus => "+",

            _ => key.ToString()
        };
    }
}