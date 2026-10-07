using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
namespace Keybinds;

public sealed class KeybindCaptureDialog : Form
{
    private readonly Label instructionLabel;
    private readonly Label keybindLabel;
    private readonly Button clearButton;
    private readonly Button okButton;
    private readonly Button cancelButton;

    private Keybind keybind;

    public Keybind Keybind => keybind;

    public KeybindCaptureDialog(Keybind current)
    {
        keybind = current;

        Text = "Set Keybind";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;
        KeyPreview = true;

        ClientSize = new Size(360, 150);

        instructionLabel = new Label
        {
            Text = "Press the key combination you want to use.",
            AutoSize = true,
            Location = new Point(16, 16)
        };

        keybindLabel = new Label
        {
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new System.Drawing.Font(Font, FontStyle.Bold),
            Location = new Point(16, 48),
            Size = new Size(328, 34)
        };

        clearButton = new Button
        {
            Text = "Clear",
            AutoSize = true,
            Location = new Point(16, 101)
        };

        okButton = new Button
        {
            Text = "OK",
            AutoSize = true,
            Location = new Point(188, 101)
        };

        cancelButton = new Button
        {
            Text = "Cancel",
            AutoSize = true,
            Location = new Point(269, 101)
        };

        Controls.Add(instructionLabel);
        Controls.Add(keybindLabel);
        Controls.Add(clearButton);
        Controls.Add(okButton);
        Controls.Add(cancelButton);

        AcceptButton = okButton;
        CancelButton = cancelButton;

        clearButton.Click += (_, _) =>
        {
            keybind = new Keybind(Keys.None);
            UpdateDisplay();
        };

        okButton.Click += (_, _) =>
        {
            DialogResult = DialogResult.OK;
            Close();
        };

        cancelButton.Click += (_, _) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        KeyDown += CaptureKeyDown;

        UpdateDisplay();
    }

    private void CaptureKeyDown(object? sender, KeyEventArgs e)
    {
        // Don't bind a modifier by itself.
        if (IsModifierKey(e.KeyCode))
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            return;
        }

        Keys modifier = GetSingleModifier(e);

        // Your Keybind model supports exactly one modifier.
        // Reject combinations such as Ctrl+Shift+S rather than silently
        // throwing one of them away.
        if (HasMultipleModifiers(e))
        {
            keybindLabel.Text = "Only one modifier is supported.";
            e.Handled = true;
            e.SuppressKeyPress = true;
            return;
        }

        keybind = new Keybind(
            e.KeyCode,
            modifier);

        UpdateDisplay();

        e.Handled = true;
        e.SuppressKeyPress = true;
    }

    private void UpdateDisplay()
    {
        keybindLabel.Text =
            KeybindTypeConverter.GetDisplayText(keybind);
    }

    private static Keys GetSingleModifier(KeyEventArgs e)
    {
        if (e.Control)
            return Keys.Control;

        if (e.Shift)
            return Keys.Shift;

        if (e.Alt)
            return Keys.Alt;

        return Keys.None;
    }

    private static bool HasMultipleModifiers(KeyEventArgs e)
    {
        int count = 0;

        if (e.Control)
            count++;

        if (e.Shift)
            count++;

        if (e.Alt)
            count++;

        return count > 1;
    }

    private static bool IsModifierKey(Keys key)
    {
        return key is
            Keys.ControlKey or
            Keys.LControlKey or
            Keys.RControlKey or
            Keys.ShiftKey or
            Keys.LShiftKey or
            Keys.RShiftKey or
            Keys.Menu or
            Keys.LMenu or
            Keys.RMenu;
    }
}