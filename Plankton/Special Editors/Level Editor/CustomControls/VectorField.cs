using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CustomControls;

public class VectorField : UserControl
{
    private Button button;
    private TextBox textBox;
    private bool isDragging;
    private bool mouseDown;
    private Point mouseDownPos;
    private float dragStartValue;

    public float multiplier;

    public Action ValueChanged = () => { };
    public Action MouseUpCallback = () => { };


    private float _value;
    private Timer updateTimer;

    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            // don’t update the UI immediately, timer will handle it
        }
    }

    public VectorField(string label)
    {
        button = new Button()
        {
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };
        textBox = new TextBox()
        {
            Dock = DockStyle.Fill,
            Visible = false
        };
        button.Dock = DockStyle.Fill;
        button.Margin = Padding.Empty;      // no margin
        button.Padding = Padding.Empty;     // no padding
        

        textBox.Dock = DockStyle.Fill;
        textBox.Margin = Padding.Empty;

        this.Margin = Padding.Empty;
        this.Padding = Padding.Empty;

        //this.Height = button.Height;

        button.MouseDown += Button_MouseDown;
        button.MouseMove += Button_MouseMove;
        button.MouseUp += Button_MouseUp;
        button.Click += Button_Click;

        textBox.KeyDown += TextBox_KeyDown;
        textBox.LostFocus += TextBox_LostFocus;

        this.Controls.Add(textBox);
        this.Controls.Add(button);

        updateTimer = new Timer();
        updateTimer.Interval = 100; // ms
        updateTimer.Tick += (s, e) =>
        {
            if (!textBox.Visible) // don’t overwrite while editing
            {
                UpdateText();
            }
        };
        updateTimer.Start();
    }

    public void UpdateText()
    {
        button.Text = _value.ToString("0.#####");
    }

    private void Button_Click(object sender, EventArgs e)
    {
        if (!isDragging)
        {
            textBox.Text = Value.ToString();
            button.Visible = false;
            textBox.Visible = true;
            textBox.Focus();
            textBox.SelectAll();
        }
    }

    private void Button_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            mouseDown = true;
            isDragging = false;
            mouseDownPos = e.Location;
            dragStartValue = Value;
            button.Capture = true;
        }
    }

    private void Button_MouseMove(object sender, MouseEventArgs e)
    {
        if (mouseDown)
        {
            
            int dx = e.X - mouseDownPos.X;
            if (Math.Abs(dx) > -1) // threshold in pixels
            {
                isDragging = true;
                Value = dragStartValue + dx * 0.1f * multiplier; // sensitivity factor
                ValueChanged();

            }
        }
    }

    private void Button_MouseUp(object sender, MouseEventArgs e)
    {
        if (!isDragging && mouseDown)
        {
            // Treat as click -> enter edit mode
            textBox.Text = Value.ToString();
            button.Visible = false;
            textBox.Visible = true;
            textBox.Focus();
            textBox.SelectAll();
        }

        mouseDown = false;
        isDragging = false;
        button.Capture = false;
        MouseUpCallback();
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            ApplyEdit();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Escape)
        {
            CancelEdit();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    private void TextBox_LostFocus(object sender, EventArgs e)
    {
        CancelEdit();
    }

    private void ApplyEdit()
    {
        if (float.TryParse(textBox.Text, out float v))
        {
            Value = v;
            ValueChanged();
        }
        textBox.Visible = false;
        button.Visible = true;
    }

    private void CancelEdit()
    {
        textBox.Visible = false;
        button.Visible = true;
    }
}