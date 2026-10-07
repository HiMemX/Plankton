using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginApi;

namespace ConsolePlugin
{
    internal class ConsoleForm : Form
    {
        private ListBox messageBox;
        private TextBox textBox;
        private List<Brush> itembrushes = new();

        public IHost? host = null;

        public ConsoleForm()
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

        public void AddEntry(string origin, string message, DebugEntryType type = DebugEntryType.NORMAL)
        {
            string formattedMessage = $"[{DateTime.Now:HH:mm:ss:fff}] [{origin}] {message}";

            AddEntry(formattedMessage, type);
        }

        public void AddEntry(string entry, DebugEntryType type = DebugEntryType.NORMAL)
        {
            itembrushes.Add(DebugEntryBrush.GetBrush(type));

            messageBox.Items.Add(entry);
            messageBox.TopIndex = messageBox.Items.Count - 1;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsoleForm));
            messageBox = new ListBox();
            textBox = new TextBox();
            SuspendLayout();
            // 
            // messageBox
            // 
            messageBox.Dock = DockStyle.Fill;
            messageBox.DrawMode = DrawMode.OwnerDrawFixed;
            messageBox.FormattingEnabled = true;
            messageBox.IntegralHeight = false;
            messageBox.ItemHeight = 15;
            messageBox.Location = new Point(0, 0);
            messageBox.Name = "messageBox";
            messageBox.Size = new Size(811, 413);
            messageBox.TabIndex = 0;
            messageBox.DrawItem += messageBox_DrawItem;
            // 
            // textBox
            // 
            textBox.Dock = DockStyle.Bottom;
            textBox.Location = new Point(0, 413);
            textBox.Name = "textBox";
            textBox.Size = new Size(811, 23);
            textBox.TabIndex = 1;
            textBox.KeyDown += textBox_KeyDown;
            // 
            // ConsoleForm
            // 
            ClientSize = new Size(811, 436);
            Controls.Add(messageBox);
            Controls.Add(textBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ConsoleForm";
            Text = "Console";
            FormClosing += ConsoleForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();

        }

        private void ConsoleForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private List<string> AvailableCommandIds()
        {
            List<string> output = new();

            foreach (CommandDefinition cmd in host.Commands.Commands)
            {
                output.Add(cmd.Id);
            }
            return output;
        }

        private List<string> InbuiltCommands()
        {
            return new List<string>() { "help" };
        }

        private string GetCommandDescriptorString(CommandDefinition cmd)
        {
            return "/" + cmd.Id + "\t\t\t" + cmd.Name + (host.Commands.CanExecute(cmd.Id) ? "" : " [Unavailable]");
        }

        private void HelpCommand()
        {
            foreach(CommandDefinition cmd in host.Commands.Commands)
            {
                AddEntry(GetCommandDescriptorString(cmd), host.Commands.CanExecute(cmd.Id) ? DebugEntryType.NORMAL : DebugEntryType.WARNING);
            }
        }

        private void ExecuteInbuiltCommand(string command) {
            if (command == "help") HelpCommand();
        }

        private void HandleCommand(string text)
        {
            if(host == null)
            {
                AddEntry("Console", "Host is null", DebugEntryType.ERROR);
                return;
            }

            string command = text.Substring(1);

            bool inids = AvailableCommandIds().Contains(command);
            bool inbuilt = InbuiltCommands().Contains(command);

            if (!(inids || inbuilt)) {
                AddEntry("Console", "Unknown Command", DebugEntryType.ERROR);
                return;
            }

            if (inids)
            {
                if (!host.Commands.CanExecute(command)) {
                    AddEntry("Console", "Wrong parameter set", DebugEntryType.ERROR);
                    return;
                }
                host.Commands.Execute(command);
            }

            if (inbuilt) ExecuteInbuiltCommand(command);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !String.IsNullOrEmpty(textBox.Text))
            {
                AddEntry("User", textBox.Text, DebugEntryType.NORMAL);
                if (textBox.Text[0] == '/') HandleCommand(textBox.Text);

                textBox.Clear();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

        }

        private void messageBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();


            if (e.Index >= 0)
            {
                e.Graphics.DrawString(messageBox.Items[e.Index].ToString(),
                    e.Font, itembrushes[e.Index], e.Bounds);
            }

            e.DrawFocusRectangle();
        }


    }
    static class DebugEntryBrush
    {
        public static Brush GetBrush(DebugEntryType type)
        {
            switch (type)
            {
                case DebugEntryType.NORMAL: return Brushes.Black;
                case DebugEntryType.ERROR: return Brushes.Red;
                case DebugEntryType.WARNING: return Brushes.DarkOrange;
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
