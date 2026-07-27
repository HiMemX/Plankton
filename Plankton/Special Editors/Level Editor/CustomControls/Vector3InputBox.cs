using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows.Forms;
using OpenTK;

namespace CustomControls;

public class Vector3InputBox : UserControl
{
    private VectorField fieldX;
    private VectorField fieldY;
    private VectorField fieldZ;
    private Label xLabel;
    private Label yLabel;
    private Label zLabel;
    private TableLayoutPanel tableLayoutPanel;

    public event EventHandler ValueChanged;

    public Vector3InputBox()
    {

        this.tableLayoutPanel = new TableLayoutPanel();
        fieldX = new VectorField("X");
        fieldY = new VectorField("Y");
        fieldZ = new VectorField("Z");
        this.xLabel = new Label();
        this.yLabel = new Label();
        this.zLabel = new Label();


        // Set up TableLayoutPanel
        this.tableLayoutPanel.Dock = DockStyle.Fill;
        this.tableLayoutPanel.RowCount = 3;
        this.tableLayoutPanel.ColumnCount = 2;
        this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20));
        this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
        this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
        this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));

        
        //fieldX.Height = fieldY.Height = fieldZ.Height = 32;
        //this.Height = fieldX.Height * 3;


        this.xLabel.Text = "X";
        this.yLabel.Text = "Y";
        this.zLabel.Text = "Z";


        // Add labels and input boxes to the table layout
        this.tableLayoutPanel.Controls.Add(this.fieldX, 1, 0);
        this.tableLayoutPanel.Controls.Add(this.fieldY, 1, 1);
        this.tableLayoutPanel.Controls.Add(this.fieldZ, 1, 2);
        this.tableLayoutPanel.Controls.Add(this.xLabel, 0, 0);
        this.tableLayoutPanel.Controls.Add(this.yLabel, 0, 1);
        this.tableLayoutPanel.Controls.Add(this.zLabel, 0, 2);

        this.fieldX.Dock = DockStyle.Top;
        this.fieldY.Dock = DockStyle.Top;
        this.fieldZ.Dock = DockStyle.Top;
        //this.fieldX.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        //this.fieldY.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        //this.fieldZ.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        this.xLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.yLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.zLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        /*
        fieldX.Dock = DockStyle.Fill;
        fieldY.Dock = DockStyle.Fill;
        fieldZ.Dock = DockStyle.Fill;
        */
        this.Controls.Add(this.tableLayoutPanel);


        this.fieldX.ValueChanged = OnValueChanged;
        this.fieldY.ValueChanged = OnValueChanged;
        this.fieldZ.ValueChanged = OnValueChanged;
        /*
        drawTimer = new System.Windows.Forms.Timer();
        drawTimer.Interval = 100;
        drawTimer.Tick += UpdateInputBoxes;
        drawTimer.Start();*/
    
    }

    public float X { get => fieldX.Value; set => fieldX.Value = value; }
    public float Y { get => fieldY.Value; set => fieldY.Value = value; }
    public float Z { get => fieldZ.Value; set => fieldZ.Value = value; }

    public (float X, float Y, float Z) Value
    {
        get => (X, Y, Z);
        set { X = value.X; Y = value.Y; Z = value.Z; }
    }

    /*
    public VectorField xBox;
    public VectorField yBox;
    public VectorField zBox;
    private Label xLabel;
    private Label yLabel;
    private Label zLabel;
    private TableLayoutPanel tableLayoutPanel;
    */
    System.Windows.Forms.Timer drawTimer;

    public Action<Vector3> SetVector3Callback { get; set; }
    /*
    public Vector3InputBox()
    {
        InitializeComponent();
        drawTimer = new System.Windows.Forms.Timer();
        drawTimer.Interval = 100;
        drawTimer.Tick += UpdateInputBoxes;
        drawTimer.Start();
    }*/

    public void SetMultiplier(float mult)
    {
        fieldX.multiplier = mult;
        fieldY.multiplier = mult;
        fieldZ.multiplier = mult;
    }

    public void SetMouseUpEvent(Action evt)
    {
        fieldX.MouseUpCallback = evt;
        fieldY.MouseUpCallback = evt;
        fieldZ.MouseUpCallback = evt;
    }
    /*
    private void InitializeComponent()
    {
        // Create the TableLayoutPanel
        this.tableLayoutPanel = new TableLayoutPanel();
        this.xBox = new FloatInputBox();
        this.yBox = new FloatInputBox();
        this.zBox = new FloatInputBox();
        this.xLabel = new Label();
        this.yLabel = new Label();
        this.zLabel = new Label();

        // Set up TableLayoutPanel
        this.tableLayoutPanel.Dock = DockStyle.Fill;
        this.tableLayoutPanel.RowCount = 3;
        this.tableLayoutPanel.ColumnCount = 2;
        this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
        this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
        this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
        this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
        this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));

        // Set up labels
        this.xLabel.Text = "X";
        this.yLabel.Text = "Y";
        this.zLabel.Text = "Z";

        // Set up input boxes
        this.xBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        this.yBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        this.zBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        this.xLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight; // Right-align labels
        this.yLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.zLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;


        // Add labels and input boxes to the table layout
        this.tableLayoutPanel.Controls.Add(this.xLabel, 0, 0);
        this.tableLayoutPanel.Controls.Add(this.xBox, 1, 0);
        this.tableLayoutPanel.Controls.Add(this.yLabel, 0, 1);
        this.tableLayoutPanel.Controls.Add(this.yBox, 1, 1);
        this.tableLayoutPanel.Controls.Add(this.zLabel, 0, 2);
        this.tableLayoutPanel.Controls.Add(this.zBox, 1, 2);

        // Add the TableLayoutPanel to the UserControl
        this.Controls.Add(this.tableLayoutPanel);

        // Set size of the user control
        this.Size = new System.Drawing.Size(200, 100);

        this.xBox.ValueChanged = OnValueChanged;
        this.yBox.ValueChanged = OnValueChanged;
        this.zBox.ValueChanged = OnValueChanged;

    }*/

    private void UpdateInputBoxes(object sender, EventArgs e)
    {
        fieldX.UpdateText();
        fieldY.UpdateText();
        fieldZ.UpdateText();
    }

    private void OnValueChanged()
    {
        if (SetVector3Callback != null)
        {
            var vector = new Vector3(fieldX.Value, fieldY.Value, fieldZ.Value);
            SetVector3Callback(vector);
        }
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }


    public void SetVector(Vector3 newvec)
    {
        fieldX.Value = newvec.X;
        fieldY.Value = newvec.Y;
        fieldZ.Value = newvec.Z;


    }
}
