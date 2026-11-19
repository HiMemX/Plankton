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

        private Handler handler;
        private List<TOCEntry> entries => GetEntries();
        private OLVColumn NameColumn;

        public event EventHandler? OnBindingChanged;
        public event EventHandler? SelectedObjectsChanged;

        public List<AssetTreeNode> Items => treeListView.Objects.Cast<AssetTreeNode>().ToList();
        public List<AssetTreeNode> SelectedItems => treeListView.SelectedObjects.Cast<AssetTreeNode>().ToList();




        public AssetListView(IHost host, AssetListViewOptions options)
        {
            InitializeComponent();

            handler = host.Archive.Current;

            var openInItem = new OpenInToolStripMenuItem(
                host.Editors,
                () =>
                {
                    AssetInfo? asset = GetSelectedAsset();

                    return asset == null
                        ? null
                        : new EditorTarget(asset);
                });

            contextMenuStrip.Items.Add(openInItem);

            this.host = host;
            this.options = options;

            Setup();

            ScrollWheelBugWorkaround();
        }

        private void ScrollWheelBugWorkaround()
        {
            treeListView.MouseWheel += (sender, e) =>
            {
                if (treeListView.FocusedItem != null)
                    treeListView.FocusedItem.Focused = false;
            };
        }

        private AssetInfo GetSelectedAsset()
        {
            if (treeListView.SelectedItems.Count == 0) return null;
            return new AssetInfo(handler, (TOCEntry)((NodeTag)(treeListView.SelectedObjects[0] as AssetTreeNode).Tag).header);
        }

        public void SetFilter(Func<AssetTreeNode, bool> filter)
        {
            treeListView.ModelFilter = new ModelFilter(obj =>
            {
                var node = (AssetTreeNode)obj;

                return filter(node);
            });
        }

        public void SelectObject(AssetTreeNode node)
        {
            if (treeListView.ShowGroups)
            {
                var group = treeListView.OLVGroups?
                    .FirstOrDefault(g =>
                        Equals(g.Key, node.typeID.ToString()));

                if (group != null)
                    group.Collapsed = false;
            }


            treeListView.SelectedObject = node;
            treeListView.SelectObject(node, true);
            treeListView.EnsureModelVisible(node);
        }

        private bool OptionEnabled(AssetListViewOptions option)
        {
            return (options & option) != 0;
        }

        private void Setup()
        {
            OnBindingChanged += (_, _) => { ReloadList(); };
            treeListView.SelectionChanged += (object? sender, EventArgs e) => { SelectedObjectsChanged?.Invoke(sender, e); };

            SetFilter((_) => { return true; });
            treeListView.HeaderStyle = ColumnHeaderStyle.None;
            AddNameColumn();
            

            if (OptionEnabled(AssetListViewOptions.SHOW_DETAILS)) AddDetailViewColumns();
            if (OptionEnabled(AssetListViewOptions.CATEGORIZE_TYPE)) SetupTypeGrouping();
            if (OptionEnabled(AssetListViewOptions.ARCHIVE_BOUND)) BindArchive();
        }

        private void BindArchive()
        {
            host.Archive.Opened += (s, e) => { SetBinding(host.Archive.Current); };
            host.Archive.Closed += (s, e) => { SetBinding(null); };
        }

        private void ReloadList()
        {

            var items = entries
                .Select(CreateTreeNode)
                .ToList();


            host
                .Commands
                .Execute("plankton.console.add-entry",
                ("ReloadList",
                items.Count.ToString()));

            treeListView.SetObjects(items);


        }

        private void AddNameColumn()
        {

            NameColumn = new OLVColumn("Name", "name") { Width = 400 };
            NameColumn.AspectGetter = obj => ((AssetTreeNode)obj).name;


            treeListView.AllColumns.Add(NameColumn);
            treeListView.Columns.AddRange(new ColumnHeader[] { NameColumn });
        }

        private void AddDetailViewColumns()
        {
            var TypeColumn = new OLVColumn("Type", "type")
            {
                Width = 200,

                // Sort by the actual type ID
                AspectGetter = obj => ((AssetTreeNode)obj).typeID
            };

            var UIDColumn = new OLVColumn("UID", "uid")
            {
                Width = 200,

                // Sort by the actual numeric UID
                AspectGetter = obj => ((AssetTreeNode)obj).entry.uidSelf,

                // Display as 16-digit hexadecimal
                AspectToStringFormat = "{0:X16}"
            };

            var SizeColumn = new OLVColumn("Size", "size")
            {
                Width = 200,

                // Sort by the actual byte count
                AspectGetter = obj => ((AssetTreeNode)obj).entry.blobSize,

                // Display a human-readable size
                AspectToStringConverter = value =>
                    DataProcessing.FormatBytes(Convert.ToInt64(value))
            };

            treeListView.AllColumns.AddRange(new[] { TypeColumn, UIDColumn, SizeColumn});
            treeListView.Columns.AddRange(new ColumnHeader[] { TypeColumn, UIDColumn, SizeColumn });


            treeListView.HeaderStyle = ColumnHeaderStyle.Clickable;
        }

        private void SetupTypeGrouping()
        {
            NameColumn.Groupable = true;

            // Use the enum's string representation as the group key.
            // Groups will now sort alphabetically.
            NameColumn.GroupKeyGetter = obj =>
                ((AssetTreeNode)obj).typeID.ToString();

            NameColumn.GroupFormatter = (group, parameters) =>
            {
                group.Header = $"{group.Key} ({group.Items.Count})";
                group.Collapsed = true;
            };

            // Always group by asset type.
            treeListView.AlwaysGroupByColumn = NameColumn;

            // Keep group ordering alphabetical, even when clicking a column.
            treeListView.AlwaysGroupBySortOrder = SortOrder.Ascending;

            // Sort items within groups by the clicked column,
            // not always by the first column.
            treeListView.SortGroupItemsByPrimaryColumn = false;

            // Default sorting inside groups.
            treeListView.PrimarySortColumn = NameColumn;
            treeListView.PrimarySortOrder = SortOrder.Ascending;

            treeListView.ShowGroups = true;
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
        public object Tag;
        public TOCEntry entry;
        public string name;
        public wmlTypeID typeID;

        public AssetTreeNode(IHost host, TOCEntry entry)
        {
            this.entry = entry;

            typeID = entry.wmlTypeID;
            name = host.Archive.Current.GetName(entry.uidSelf);
            Tag = new NodeTag(entry, entry, entry.entity);
        }
    }

    public enum AssetListViewOptions
    {
        SHOW_DETAILS = 1,
        RENAMABLE = 2,
        CATEGORIZE_TYPE = 4,
        ARCHIVE_BOUND = 8,


        ARCHIVE_VIEWER = 1 + 2,// for testing
        LEVEL_EDITOR = 2 + 4 + 8
    }
}
