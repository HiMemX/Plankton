using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSHO;
using HoArchive;
using PluginApi;

namespace ArchiveEditorPlugin
{
    public partial class ArchiveEditorControl : UserControl
    {
        private readonly IHost host;
        private readonly ArchiveEditorPreferences preferences;
        private Handler handler => host.Archive.Current;

        public ArchiveEditorControl(IHost host, ArchiveEditorPreferences preferences)
        {
            InitializeComponent();

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

        }

        public void Reveal(EditorTarget target)
        {
            if (archiveTreeView.Nodes.Count == 0) return;

            TOCEntry entry = (TOCEntry)((AssetInfo)target.Value).Entry;
            TreeNode? node = FindTreeNodeWithAsset(archiveTreeView.Nodes[0], entry);

            if (node == null) return;

            archiveTreeView.SelectedNode = node;

            ArchiveTreeViewOnSelect(node);

            ListViewItem? item = FindListViewItem(entry);

            if (item == null)
            {
                host.Commands.Execute("plankton.console.add-error", ("ArchiveEditorControl.Reveal", "Couldn't find ListViewItem " + tocEntryListView.Items.Count.ToString()));
                return;
            }
            host.Commands.Execute("plankton.console.add-entry", ("ArchiveEditorControl.Reveal", "Found ListViewItem"));

            item.Focused = true;
            item.Selected = true;

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

        private void ReloadListView(ParcelTOC toc)
        {
            tocEntryListView.BeginUpdate();
            tocEntryListView.Items.Clear();

            foreach (TOCEntry entry in toc.Entries)
            {
                tocEntryListView.Items.Add(CreateListViewItem(entry));
            }
            tocEntryListView.EndUpdate();
        }

        private ListViewItem CreateListViewItem(TOCEntry entry)
        {
            ListViewItem item = new ListViewItem(handler.GetName(entry.uidSelf));
            item.Tag = new NodeTag(entry, entry, entry.entity);
            item.SubItems.Add(entry.wmlTypeID.ToString());
            item.SubItems.Add(entry.uidSelf.ToString("X16"));

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

        private void ArchiveTreeViewOnSelect(TreeNode node)
        {
            object tag = ((NodeTag)node.Tag).obj;
            object header = ((NodeTag)node.Tag).header;
            object data = ((NodeTag)node.Tag).data;


            if (tag is ParcelTOC) ReloadListView((ParcelTOC)tag);

            if ((header is not null) || (data is not null)) SetPropertyGrid((NodeTag)node.Tag);


            host.Commands.Execute("plankton.console.add-entry", ("ArchiveEditorControl.BeforeSelect", "Set List View"));
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
