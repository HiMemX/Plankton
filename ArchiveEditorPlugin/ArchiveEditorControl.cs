using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSHO;
using HoArchive;
using Plankton.ArchiveEditorPlugin.Helpers;
using PluginApi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArchiveEditorPlugin
{
    public partial class ArchiveEditorControl : UserControl
    {
        private readonly IHost host;
        private readonly ArchiveEditorPreferences preferences;
        private Handler handler => host.Archive.Current;

        private List<ListViewItem> tocEntryListViewItems = new();

        public ArchiveEditorControl(IHost host, ArchiveEditorPreferences preferences)
        {
            InitializeComponent();
            uidSearchTextBox.PlaceholderText = "UID...";


            SetupSearchComboboxes();

            this.host = host;
            this.preferences = preferences;


            var openInItem = new OpenInToolStripMenuItem(
                host.Editors,
                () =>
                {
                    AssetInfo? asset = GetSelectedAsset();

                    return asset == null
                        ? null
                        : new EditorTarget(asset);
                });

            assetListViewContextMenu.Items.Add(openInItem);

            host.Archive.AssetsRenamed += IngoingAssetsRenamed;
        }

        private void IngoingAssetsRenamed(object? sender, AssetEventArgs e)
        {
            foreach(AssetInfo info in e.Assets)
            {
                ListViewItem? entry = FindListViewItem(info.Entry);

                if (entry == null) continue;

                entry.Text = handler.GetName(info.Entry.uidSelf);
            }

            //host.Commands.Execute("plankton.console.add-entry", ("ArchiveEditorControl.IngoingAssetsRenamed", sender.ToString() + ", " + handler.GetName(e.Assets[0].Entry.uidSelf)));
        }

        private void NotifyAssetsRenamed(IEnumerable<TOCEntry> entries)
        {
            host.Archive.NotifyAssetsRenamed(this, GetInfos(entries));
        }

        private IEnumerable<AssetInfo> GetInfos(IEnumerable<TOCEntry> entries)
        {
            return entries.Select(k => new AssetInfo(handler, k));
        }

        public void SetupSearchComboboxes()
        {

            wmlTypeIDComboBox.Items.Clear();
            wmlTypeIDComboBox.Sorted = false; // Preserve the order we specify

            // Add "All" as the first item
            wmlTypeIDComboBox.Items.Add("All");

            // Add all enum values

            foreach (wmlTypeID status in Enum.GetValues(typeof(wmlTypeID))
                             .Cast<wmlTypeID>()
                             .OrderBy(x => x.ToString()))
            {
                wmlTypeIDComboBox.Items.Add(status);
            }

            // Select "All" by default
            wmlTypeIDComboBox.SelectedIndex = 0;
        }

        public void Reveal(EditorTarget target)
        {
            if (archiveTreeView.Nodes.Count == 0) return;

            TOCEntry entry = ((AssetInfo)target.Value).Entry;

            Reveal(entry);
        }

        public async void Reveal(TOCEntry entry)
        {
            TreeNode? node = FindTreeNodeWithAsset(archiveTreeView.Nodes[0], entry);

            if (node == null) return;

            archiveTreeView.SelectedNode = node;

            await ArchiveTreeViewOnSelect(node);

            ListViewItem? item = FindListViewItem(entry);

            if (item == null)
            {
                host.Commands.Execute("plankton.console.add-error", ("ArchiveEditorControl.Reveal", "Couldn't find ListViewItem " + tocEntryListView.Items.Count.ToString()));
                return;
            }
            host.Commands.Execute("plankton.console.add-entry", ("ArchiveEditorControl.Reveal", "Found ListViewItem"));

            item.Focused = true;
            item.Selected = true;

            item.EnsureVisible();
        }

        private ListViewItem? FindListViewItem(TOCEntry entry)
        {
            foreach (ListViewItem item in tocEntryListView.Items)
            {
                if (entry == ((NodeTag)item.Tag).obj) return item;
            }
            return null;
        }

        private TreeNode? FindTreeNodeWithAsset(TreeNode parent, TOCEntry entry)
        {
            object obj = ((NodeTag)parent.Tag).obj;
            if (obj is ParcelTOC)
            {
                if (((ParcelTOC)obj).Entries.Contains(entry)) return parent;
                return null;
            }

            foreach (TreeNode node in parent.Nodes)
            {
                TreeNode? result = FindTreeNodeWithAsset(node, entry);
                if (result != null) return result;
            }

            return null;
        }


        private AssetInfo GetSelectedAsset()
        {
            if (tocEntryListView.SelectedItems.Count == 0) return null;
            return new AssetInfo(handler, (TOCEntry)((NodeTag)tocEntryListView.SelectedItems[0].Tag).header);
        }

        public void ReloadData()
        {
            ClearListView();
            ReloadTreeView();

        }

        private void ClearListView()
        {
            tocEntryListView.BeginUpdate();
            tocEntryListView.Items.Clear();
            tocEntryListView.EndUpdate();
        }

        private void ReloadTreeView()
        {
            archiveTreeView.Nodes.Clear();

            archiveTreeView.Nodes.Add(
                CreateNode(handler.Archive.MasterTable)
            );

        }

        private async Task ReloadListView(ParcelTOC toc)
        {
            tocEntryListView.BeginUpdate();
            tocEntryListView.Items.Clear();
            tocEntryListViewItems.Clear();

            foreach (TOCEntry entry in toc.Entries)
            {
                tocEntryListViewItems.Add(CreateListViewItem(entry));
            }
            await UpdateVisibleTOCEntries();
            tocEntryListView.EndUpdate();
        }

        private ListViewItem CreateListViewItem(TOCEntry entry)
        {
            ListViewItem item = new ListViewItem(handler.GetName(entry.uidSelf));
            item.Tag = new NodeTag(entry, entry, entry.entity);
            item.SubItems.Add(entry.wmlTypeID.ToString());
            item.SubItems.Add(entry.uidSelf.ToString("X16"));
            item.SubItems.Add(DataProcessing.FormatBytes(entry.blobSize));

            return item;
        }


        private TreeNode CreateNode(ParcelBase parcel, TableEntry? entry = null)
        {
            if (parcel is Table)
            {
                return CreateTableNode((Table)parcel, entry);
            }

            if (parcel is Parcel)
            {
                return CreateParcelNode((Parcel)parcel, entry);
            }

            return null;
        }

        private TreeNode CreateParcelNode(Parcel parcel, TableEntry? entry = null)
        {
            TreeNode node = new TreeNode(entry.sectionType);
            node.Tag = new NodeTag(parcel, null, entry);

            foreach (ParcelTOC toc in parcel.ParcelTOCs)
            {
                node.Nodes.Add(CreateTOCNode(toc));
            }

            return node;
        }

        private TreeNode CreateTOCNode(ParcelTOC toc, string tag = "(ParcelTOC)")
        {
            TreeNode node = new TreeNode(tag);
            node.Tag = new NodeTag(toc);
            return node;
        }


        private TreeNode CreateTableNode(Table table, TableEntry ownentry = null)
        {
            TreeNode node = new TreeNode(table.TableHeader.tableTypeTag);
            node.Tag = new NodeTag(table, table.TableHeader, ownentry);
            for (int i = 0; i < table.Parcels.Count; i++)
            {
                ParcelBase parcel = table.Parcels[i];
                TableEntry entry = table.TableEntries[i];

                TreeNode child = CreateNode(parcel, entry);
                if (child == null) continue;
                node.Nodes.Add(child);
            }
            return node;
        }


        private void tocEntryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<NodeTag> items = new();

            foreach (object item in tocEntryListView.SelectedItems)
            {
                items.Add((NodeTag)((ListViewItem)item).Tag);
            }

            SetPropertyGrid(items);
        }

        private void SetPropertyGrid(NodeTag tag)
        {
            headerPropertyGrid.SelectedObject = tag.header;
            dataPropertyGrid.SelectedObject = tag.data;

            ResetLabels();


            if (tag.header != null) headerLabel.Text = tag.header.ToString();
            if (tag.data != null) dataLabel.Text = tag.data.ToString();
        }

        private void ResetLabels()
        {
            headerLabel.Text = "";
            dataLabel.Text = "";
        }

        private void SetPropertyGrid(List<NodeTag> tag)
        {
            object[] headers = tag.Select(i => i.header).Where(foo => foo != null).ToArray();
            object[] datas = tag.Select(i => i.data).Where(foo => foo != null).ToArray();

            headerPropertyGrid.SelectedObjects = headers;
            dataPropertyGrid.SelectedObjects = datas;

            ResetLabels();

            if (headers.Length != 0) headerLabel.Text = headers[0].ToString();
            if (datas.Length != 0) dataLabel.Text = datas[0].ToString();

        }

        private void archiveTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown)
                return;

            ArchiveTreeViewOnSelect(e.Node);

        }

        private async Task ArchiveTreeViewOnSelect(TreeNode node)
        {
            object tag = ((NodeTag)node.Tag).obj;
            object header = ((NodeTag)node.Tag).header;
            object data = ((NodeTag)node.Tag).data;


            if (tag is ParcelTOC) await ReloadListView((ParcelTOC)tag);

            if ((header is not null) || (data is not null)) SetPropertyGrid((NodeTag)node.Tag);


            host.Commands.Execute("plankton.console.add-entry", ("ArchiveEditorControl.BeforeSelect", "Set List View"));
        }



        private bool ShouldBeVisible(
            ListViewItem item,
            wmlTypeID? typeFilter,
            string match)
        {
            TOCEntry entry = (item.Tag as NodeTag).obj as TOCEntry;

            // Check type first
            if (typeFilter.HasValue && entry.wmlTypeID != typeFilter.Value)
                return false;

            // No search text? We're done!
            if (match.Length == 0)
                return true;

            string candidate = "";


            candidate = DataProcessing.ByteArrayToString(entry.data.ToArray());


            string candidate2 = host.Archive.Current.GetName(entry.uidSelf);



            return candidate.Replace(" ", "").Contains(match) || candidate2.Replace(" ", "").Contains(match);
        }

        /*private void UpdateVisibleTOCEntries()
        {
            tocEntryListView.Items.Clear();
            foreach (ListViewItem item in tocEntryListViewItems)
            {
                if (ShouldBeVisible(item)) tocEntryListView.Items.Add(item);
            }
        }*/

        private async void wmlTypeIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            await UpdateVisibleTOCEntries();
        }

        private async void searchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            await UpdateVisibleTOCEntries();
        }

        private async void searchTextBox_TextChanged(object sender, EventArgs e)
        {

            await UpdateVisibleTOCEntries();
        }

        private CancellationTokenSource _filterCts;
        private async Task UpdateVisibleTOCEntries()
        {
            // Cancel the previous filtering operation
            _filterCts?.Cancel();

            var cts = new CancellationTokenSource();
            _filterCts = cts;

            string searchText = searchTextBox.Text.Trim();

            try
            {
                // Wait until the user stops typing for 250 ms
                await Task.Delay(250, cts.Token);

                wmlTypeID? typeFilter =
                    wmlTypeIDComboBox.SelectedItem is wmlTypeID type
                        ? type
                        : (wmlTypeID?)null;

                string match = searchTextBox.Text.Replace(" ", "");

                // Filter on a background thread
                List<ListViewItem> filtered = await Task.Run(() =>
                {
                    return tocEntryListViewItems
                        .Where(item => ShouldBeVisible(item, typeFilter, match)).ToList();
                }, cts.Token);


                // Don't display results from an outdated search
                cts.Token.ThrowIfCancellationRequested();

                // Back on the UI thread
                var listViewItems = filtered.ToArray();

                tocEntryListView.BeginUpdate();

                try
                {
                    tocEntryListView.Items.Clear();
                    tocEntryListView.Items.AddRange(listViewItems);
                }
                finally
                {
                    tocEntryListView.EndUpdate();
                }
            }
            catch (OperationCanceledException)
            {
                // Another search replaced this one
            }
            finally
            {
                if (ReferenceEquals(_filterCts, cts))
                    _filterCts = null;

                cts.Dispose();
            }
        }


        private void dataPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //host.Commands.Execute("plankton.console.add-entry", ("PropertyValueChanged", "Test!"));
            List<AssetInfo> infos = headerPropertyGrid.SelectedObjects.Where(k => k is TOCEntry)
                .Select(k => new AssetInfo(host.Archive.Current, (TOCEntry)k)).ToList();

            host.Archive.NotifyAssetsModified(this, infos);
        }

        private void uidSearchButton_Click(object sender, EventArgs e)
        {
            string text = uidSearchTextBox.Text;

            text = text.Split("x").Last();
            text = text.Replace(" ", "");

            if (!ulong.TryParse(text, System.Globalization.NumberStyles.HexNumber, CultureInfo.GetCultureInfo("en-US"), out ulong id)) return;

            TOCEntry asset = handler.GetAsset(id);
            if (asset == null) return;

            Reveal(asset);
        }

        private TOCEntry GetTOCEntryFromItem(ListViewItem item)
        {
            return (item.Tag as NodeTag).obj as TOCEntry;
        }

        private void tocEntryListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null) return;

            TOCEntry entry = GetTOCEntryFromItem(tocEntryListView.Items[e.Item]);

            NameTableEntry nameentry = handler.GetNameEntry(entry.uidSelf);
            nameentry.name = e.Label;
            nameentry.Update();

            host.Commands.Execute("plankton.console.add-entry", ("AfterLabelEdit", nameentry.name));
            host.Commands.Execute("plankton.console.add-entry", ("AfterLabelEdit", handler.GetName(entry.uidSelf)));

            NotifyAssetsRenamed(new List<TOCEntry>() { entry });
        }

        private void tocEntryListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get the item that was double-clicked
            ListViewItem clickedItem = tocEntryListView.GetItemAt(e.X, e.Y);

            if (clickedItem != null)
            {
                // Force the first column into edit mode instantly
                clickedItem.BeginEdit();
            }
        }
    }

    internal class NodeTag
    {
        public object obj;
        public object? header;
        public object? data;

        public NodeTag(object obj, object? header = null, object? data = null)
        {
            this.obj = obj;
            this.header = header;
            this.data = data;
        }
    }

    internal class TableInfo
    {
        public Table table;

        [DisplayName("Table Header")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public TableHeader? tableHeader { get; set; } = null;
        [DisplayName("Table Entry")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public TableEntry? tableEntry { get; set; } = null;

        public TableInfo(Table table, TableEntry? tableEntry = null) {
            this.table = table;
            this.tableHeader = table.TableHeader;
            this.tableEntry = tableEntry;
        }
    }
}
