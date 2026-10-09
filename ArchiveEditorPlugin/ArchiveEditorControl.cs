using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
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

        private void archiveTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

            object tag = ((NodeTag)e.Node.Tag).obj;
            object header = ((NodeTag)e.Node.Tag).header;
            object data = ((NodeTag)e.Node.Tag).data;



            if (tag is ParcelTOC) ReloadListView((ParcelTOC)tag);

            if ((header is not null) || (data is not null)) SetPropertyGrid((NodeTag)e.Node.Tag);

        }

        private void tocEntryListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<NodeTag> items = new();
            
            foreach(object item in tocEntryListView.SelectedItems)
            {
                items.Add((NodeTag)((ListViewItem)item).Tag);
            }

            SetPropertyGrid(items);
        }

        private void SetPropertyGrid(NodeTag tag)
        {
            headerPropertyGrid.SelectedObject = tag.header;
            dataPropertyGrid.SelectedObject = tag.data; 
        }

        private void SetPropertyGrid(List<NodeTag> tag)
        {
            object[] headers = tag.Select(i => i.header).Where(foo => foo != null).ToArray();
            object[] datas = tag.Select(i => i.data).Where(foo => foo != null).ToArray();

            headerPropertyGrid.SelectedObjects = headers;
            dataPropertyGrid.SelectedObjects = datas;
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
