using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArchiveUI.Helpers;
using BrightIdeasSoftware;
using CSHO;
using HoArchive;
using PluginApi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace ArchiveUI
{
    public partial class AssetListView : UserControl
    {
        private IHost host;
        private AssetListViewOptions options;
        private object? dataSource = null;

        private List<TOCEntry> entries => GetEntries();

        public event EventHandler? OnBindingChanged;
        public event EventHandler? SelectedObjectsChanged;

        public ListView.ListViewItemCollection Items => treeListView.Items;
        public ListView.SelectedListViewItemCollection SelectedItems => treeListView.SelectedItems;

        public Func<TOCEntry, bool> Filter = (_) => { return true; };

        public AssetListView(IHost host, AssetListViewOptions options)
        {
            InitializeComponent();


            this.host = host;
            this.options = options;

            Setup();
        }

        private bool OptionEnabled(AssetListViewOptions option)
        {
            return (options & option) != 0;
        }

        private void Setup()
        {
            OnBindingChanged += (_, _) => { ReloadList(); };
            treeListView.SelectedIndexChanged += (object? sender, EventArgs e) => { SelectedObjectsChanged?.Invoke(sender, e); };

            if (OptionEnabled(AssetListViewOptions.SHOW_DETAILS)) AddDetailViewColumns();
        }

        private void ReloadList()
        {

            List<AssetTreeNode> items = new();
            foreach(TOCEntry entry in entries)
            {
                if (!Filter(entry)) continue;

                items.Add(CreateTreeNode(entry));
            }

            
            host
                .Commands
                .Execute("plankton.console.add-entry",
                ("ReloadList",
                items.Count.ToString()));

            treeListView.ClearObjects();
            treeListView.Roots = items;

        }

        private void AddDetailViewColumns()
        {
            var NameColumn = new OLVColumn("Name", "name") { Width = 400 };
            var TypeColumn = new OLVColumn("Type", "type") { Width = 200 };
            var UIDColumn = new OLVColumn("UID", "uid") { Width = 200 };
            var SizeColumn = new OLVColumn("Size", "size") { Width = 200 };

            treeListView.AllColumns.AddRange(new[] { NameColumn, TypeColumn, UIDColumn, SizeColumn});
            treeListView.Columns.AddRange(new ColumnHeader[] { NameColumn, TypeColumn, UIDColumn, SizeColumn });

            // 3. Tell TreeListView how to expand nodes
            treeListView.CanExpandGetter = model => (false);
            treeListView.ChildrenGetter = model => new List<AssetTreeNode>();

        }

        private List<TOCEntry> GetEntries()
        {
            if (dataSource is ParcelTOC toc) { return toc.Entries; }
            if (dataSource is Handler handler) return handler.GetAssets();

            return new();
        }

        public void SetBinding(object? source)
        {
            dataSource = source;

            OnBindingChanged?.Invoke(this, EventArgs.Empty);
        }

        public ListViewItem? GetItemAt(int x, int y)
        {
            return treeListView.GetItemAt(x, y);
        }

        private AssetTreeNode CreateTreeNode(TOCEntry entry)
        {
            return new AssetTreeNode(host, entry);
        }
    }

    public class AssetTreeNode
    {
        public object Tag; // For compatibility reasons
        public TOCEntry entry;
        public string name;
        public string type;
        public string uid;
        public string size;

        public AssetTreeNode(IHost host, TOCEntry entry)
        {
            this.entry = entry;

            name = host.Archive.Current.GetName(entry.uidSelf);
            Tag = new NodeTag(entry, entry, entry.entity);
            type = entry.wmlTypeID.ToString();
            uid = entry.uidSelf.ToString("X16");
            size = DataProcessing.FormatBytes(entry.blobSize);
        }

    }

    public enum AssetListViewOptions
    {
        SHOW_DETAILS = 1,
        RENAMABLE = 2,
        CATEGORIZE_TYPE = 4,


        ARCHIVE_VIEWER = 1 + 2
    }
}
