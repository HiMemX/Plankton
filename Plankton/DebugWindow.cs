using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plankton
{
    public partial class DebugWindow : Form
    {
        private List<Brush> itembrushes = new();

        public DebugWindow()
        {
            InitializeComponent();
        }

        public void AddEntry(string origin, DebugEntryType type = DebugEntryType.NORMAL, params double[] numbers)
        {

            string numbersString = string.Join(", ", numbers);

            AddEntry(origin, numbersString, type);
        }

        public void AddEntry(string origin, DebugEntryType type = DebugEntryType.NORMAL, params float[] numbers)
        {

            string numbersString = string.Join(", ", numbers);

            AddEntry(origin, numbersString, type);
        }

        public void AddEntry(string origin, DebugEntryType type = DebugEntryType.NORMAL, params int[] numbers)
        {

            string numbersString = string.Join(", ", numbers);

            AddEntry(origin, numbersString, type);
        }

        public void AddEntry(string origin, string message, DebugEntryType type=DebugEntryType.NORMAL)
        {
            string formattedMessage = $"[{DateTime.Now:HH:mm:ss:fff}] [{origin}] {message}";

            itembrushes.Add(DebugEntryBrush.GetBrush(type));

            debugListBox.Items.Add(formattedMessage);
            debugListBox.TopIndex = debugListBox.Items.Count - 1;
        }

        private void DebugWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void debugListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();


            if (e.Index >= 0)
            {
                e.Graphics.DrawString(debugListBox.Items[e.Index].ToString(),
                    e.Font, itembrushes[e.Index], e.Bounds);
            }

            e.DrawFocusRectangle();
        }
    }


    public static class Debug
    {
        public static DebugWindow debugWindow = new DebugWindow();
    }

    static class DebugEntryBrush{
        public static Brush GetBrush(DebugEntryType type)
        {
            switch (type)
            {
                case DebugEntryType.NORMAL: return Brushes.Black;
                case DebugEntryType.ERROR: return Brushes.Red;
                case DebugEntryType.WARNING: return Brushes.Yellow;
                case DebugEntryType.SUCCESS: return Brushes.Green;
                default: return Brushes.Black;
            }
        }
    }

    public enum DebugEntryType
    {
        NORMAL,
        ERROR,
        WARNING,
        SUCCESS
    }

    
}
