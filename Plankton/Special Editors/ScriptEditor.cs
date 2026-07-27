using CSHO;
using HoArchive;
using SB09WiiAsset;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.ComponentModel.Design;
using Be.Windows.Forms;
using System.Threading.Tasks.Dataflow;

namespace Plankton.Special_Editors
{
    public partial class ScriptEditor : Form
    {
        TreeNode currNode;
        Handler handler;

        TOCEntry currEntry;
        Script currScript;
        int currIndex = -1;
        ScriptEntry currScriptEntry;
        List<byte> currArgs;

        HexBox eventArgsHexEditor;

        public ScriptEditor()
        {
            InitializeComponent();
            eventArgsHexEditor = new();
            eventArgsHexEditor.Parent = hexEditorPanel;
            eventArgsHexEditor.Dock = DockStyle.Fill;
            eventArgsHexEditor.Validating += eventArgsTextBox_Validating;
            eventArgsHexEditor.TextChanged += EventArgsHexEditor_TextChanged;
            hexEditorPanel.Controls.Add(eventArgsHexEditor);

            eventTypeComboBox.DataSource = Enum.GetValues(typeof(Asset.Event));
        }

        private void EventArgsHexEditor_TextChanged(object sender, EventArgs e)
        {
            eventArgsHexEditor.ByteProvider.ApplyChanges();
        }

        private void ScriptEditor_Load(object sender, EventArgs e)
        {

        }

        public string GeneratePreviewString(ScriptEntry entry)
        {
            return entry.dstEvent.type.ToString().PadRight(15) + " => " + handler.GetName(entry.dstAsset);
        }


        public void UpdateListBox()
        {
            eventPreviewListBox.Items.Clear();
            foreach (ScriptEntry entry in currScript._ScriptList)
            {
                eventPreviewListBox.Items.Add(GeneratePreviewString(entry));
            }
            if (currIndex != -1 && (currIndex < currScript.eventCount)) { SetEventPreviewIndexNoEvent(currIndex); }
        }
        public void UpdateScriptEntrySettings()
        {
            SetEventTypeSelectedIndexNoEvent(eventTypeComboBox.FindStringExact(currScriptEntry.dstEvent.type.ToString()));
            SetTargetAssetTextNoEvent(currScriptEntry.dstAsset.ToString("X16"));
            SetDelayTextNoEvent(currScriptEntry.delay.ToString().Replace(",", "."));

            eventArgsHexEditor.ByteProvider = new DynamicByteProvider(currArgs);
            //eventArgsTextBox.Text = DataToHexString(GetArgumentData((uint)eventPreviewListBox.SelectedIndex));
            currScript.Update(((assetTreeNode)currNode).asset);
        }

        public void Update(Handler handler, TreeNode node)
        {
            currEntry = ((assetTreeNode)node).asset;

            this.handler = handler;
            currNode = node;
            currScript = (Script)currEntry.entity;

            UpdateListBox();

            Text = "Script Editor - " + node.Text;
        }
        private void ScriptEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void eventTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currIndex == -1) { return; }

            currScriptEntry.dstEvent.type = (Asset.Event)eventTypeComboBox.SelectedItem;

            UpdateScriptEntrySettings();
            SetCurrentEntryPreviewText(GeneratePreviewString(currScriptEntry));
        }

        private List<byte> GetArgumentData(int index)
        {
            ScriptEntry currentry = currScript._ScriptList[index];

            return currentry.dstEvent.args;

            /*ScriptEntry nextentry = null;
            if (index < currScript.eventCount - 1)
            {
                nextentry = currScript._ScriptList[index + 1];
            }

            uint pointer = currentry.dstEvent.v - currScript.ScriptList._p - currScript.eventCount * 0x20;
            uint end = (uint)currScript.eventArgs.Count;
            if (nextentry != null)
            {
                end = nextentry.dstEvent.v - currScript.ScriptList._p - currScript.eventCount * 0x20;
            }
            return currScript.eventArgs.Skip((int)pointer).Take((int)(end - pointer)).ToList();*/
        }

        private string DataToHexString(List<byte> data)
        {

            string output = "";

            for (int i = 0; i < data.Count; i += 0x10)
            {
                for (int b = 0; b < Math.Min(data.Count - i, 0x10); b++)
                {
                    output += data[i + b].ToString("X2") + " ";
                }
                output += "\r\n";
            }

            return output;
        }

        private void eventPreviewListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currIndex = eventPreviewListBox.SelectedIndex;

            if (currIndex != -1)
            {
                currScriptEntry = currScript._ScriptList[eventPreviewListBox.SelectedIndex];
                currArgs = GetArgumentData(eventPreviewListBox.SelectedIndex);
                UpdateScriptEntrySettings();
            }

        }

        private void targetAssetTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (currIndex == -1) { return; }

            ulong newid = 0;
            if (!UInt64.TryParse(targetAssetTextBox.Text, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out newid)) { e.Cancel = true; return; }
            currScriptEntry.dstAsset = newid;

            UpdateScriptEntrySettings();
            SetCurrentEntryPreviewText(GeneratePreviewString(currScriptEntry));
        }
        private void delayTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (currIndex == -1) { return; }

            float newvalue = 0;
            if (!float.TryParse(delayTextBox.Text, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out newvalue)) { e.Cancel = true; return; }
            currScriptEntry.delay = newvalue;

            UpdateScriptEntrySettings();
        }
        private void eventArgsTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (currIndex == -1) { return; }
            /*List<byte> newargs = new List<byte>();
            string data = eventArgsTextBox.Text.Trim().Replace(" ", "").Replace("\r\n", "");

            byte currbyte;
            for(int i=0; i<data.Length; i+=2)
            {
                if(!byte.TryParse(String.Join("", data.Skip(i).Take(2)), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out currbyte)) { e.Cancel = true; return; }
                newargs.Add(currbyte);
            }*/
            eventArgsHexEditor.ByteProvider.ApplyChanges();
            List<byte> newargs = currArgs;

            currScriptEntry.dstEvent.args = newargs;

            /*
            List<byte> oldargs = GetArgumentData(currIndex);
            for (int i = currIndex + 1; i < currScript.eventCount; i++)
            {
                currScript._ScriptList[i].dstEvent.v = (uint)(currScript._ScriptList[i].dstEvent.v - oldargs.Count + newargs.Count);
            }*/


            //currScript.eventArgs = currScript.eventArgs.Take(AbsoluteToRelativeArgOffset(currScriptEntry.dstEvent.v)).Concat(newargs).Concat(currScript.eventArgs.Skip(AbsoluteToRelativeArgOffset(currScriptEntry.dstEvent.v) + (int)oldargs.Count)).ToList();

            UpdateScriptEntrySettings();
        }

        private void targetAssetTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void delayTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private int AbsoluteToRelativeArgOffset(uint v)
        {
            return (int)(v - currScript.ScriptList._p - currScript.eventCount * 0x20);
        }


        private void SetEventTypeSelectedIndexNoEvent(int index)
        {
            eventTypeComboBox.SelectedIndexChanged -= eventTypeComboBox_SelectedIndexChanged;
            eventTypeComboBox.SelectedIndex = index;
            eventTypeComboBox.SelectedIndexChanged += eventTypeComboBox_SelectedIndexChanged;
        }
        private void SetTargetAssetTextNoEvent(string str)
        {
            targetAssetTextBox.TextChanged -= targetAssetTextBox_TextChanged;
            targetAssetTextBox.Text = str;
            targetAssetTextBox.TextChanged += targetAssetTextBox_TextChanged;
        }
        private void SetDelayTextNoEvent(string str)
        {
            delayTextBox.TextChanged -= delayTextBox_TextChanged;
            delayTextBox.Text = str;
            delayTextBox.TextChanged += delayTextBox_TextChanged;
        }
        private void SetEventPreviewIndexNoEvent(int index)
        {
            eventPreviewListBox.SelectedIndexChanged -= eventPreviewListBox_SelectedIndexChanged;
            eventPreviewListBox.SelectedIndex = index;
            eventPreviewListBox.SelectedIndexChanged += eventPreviewListBox_SelectedIndexChanged;
        }
        public void SetCurrentEntryPreviewText(string str)
        {
            eventPreviewListBox.SelectedIndexChanged -= eventPreviewListBox_SelectedIndexChanged;
            eventPreviewListBox.Items[currIndex] = str;
            eventPreviewListBox.SelectedIndexChanged += eventPreviewListBox_SelectedIndexChanged;
        }



        private void newButton_Click(object sender, EventArgs e)
        {
            ScriptEntry newentry = new ScriptEntry();
            newentry.unknown1 = true;

            currScript._ScriptList.Add(newentry);
            //currScript._ScriptList.Last().dstEvent.v = (uint)currScript.eventArgs.Count + currScript.ScriptList._p + currScript.eventCount * 0x20;
            currScript.eventCount++;

            for (int i = 0; i < currScript.eventCount; i++)
            {
                currScript._ScriptList[i].dstEvent.v += 0x20;
            }

            UpdateListBox();
            eventPreviewListBox.SelectedIndex = (int)(currScript.eventCount - 1);
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (currIndex == -1) { return; }

            currScript._ScriptList.RemoveAt(currIndex);
            currScript.eventCount--;

            for (int i = 0; i < currScript.eventCount; i++)
            {
                currScript._ScriptList[i].dstEvent.v -= 0x20;
            }

            if (currIndex >= currScript.eventCount)
            {
                eventPreviewListBox.SelectedIndex--;
            }

            UpdateListBox();
            eventPreviewListBox_SelectedIndexChanged(null, null);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (currIndex <= 0) { return; } // Top selected or nothing selected, do nothing

            //currScript.eventArgs = currScript.eventArgs.Take(AbsoluteToRelativeArgOffset(currScript._ScriptList[currIndex - 1].dstEvent.v)).Concat(GetArgumentData(currIndex)).Concat(GetArgumentData(currIndex - 1)).Concat(currScript.eventArgs.Skip(AbsoluteToRelativeArgOffset(currScript._ScriptList[currIndex].dstEvent.v) + (int)GetArgumentData(currIndex).Count)).ToList();

            uint temp = currScript._ScriptList[currIndex - 1].dstEvent.v;
            currScript._ScriptList[currIndex - 1].dstEvent.v += (uint)GetArgumentData(currIndex).Count;
            currScript._ScriptList[currIndex].dstEvent.v = temp;

            currScript._ScriptList.Insert(currIndex - 1, currScriptEntry);
            currScript._ScriptList.RemoveAt(currIndex + 1);


            //uint tempv = currScript._ScriptList[currIndex].dstEvent.v;

            eventPreviewListBox.SelectedIndex--;
            UpdateListBox();
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if (currIndex == -1 || (currIndex >= currScript.eventCount - 1)) { return; } // last selected or nothing selected, do nothing

            currIndex++;

            //currScript.eventArgs = currScript.eventArgs.Take(AbsoluteToRelativeArgOffset(currScript._ScriptList[currIndex - 1].dstEvent.v)).Concat(GetArgumentData(currIndex)).Concat(GetArgumentData(currIndex - 1)).Concat(currScript.eventArgs.Skip(AbsoluteToRelativeArgOffset(currScript._ScriptList[currIndex].dstEvent.v) + (int)GetArgumentData(currIndex).Count)).ToList();

            uint temp = currScript._ScriptList[currIndex - 1].dstEvent.v;
            currScript._ScriptList[currIndex - 1].dstEvent.v += (uint)GetArgumentData(currIndex).Count;
            currScript._ScriptList[currIndex].dstEvent.v = temp;

            currScript._ScriptList.Insert(currIndex - 1, currScript._ScriptList[currIndex]);
            currScript._ScriptList.RemoveAt(currIndex + 1);

            eventPreviewListBox.SelectedIndex++;
            UpdateListBox();
        }

        private void ScriptEditor_Leave(object sender, EventArgs e)
        {
            delayTextBox_Validating(null, null);
            targetAssetTextBox_Validating(null, null);
            
        }
    }
}
