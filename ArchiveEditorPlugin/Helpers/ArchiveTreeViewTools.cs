using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;

namespace ArchiveEditorPlugin.Helpers
{
    internal static class ArchiveTreeViewTools
    {

        public static TreeNode CreateNode(ParcelBase parcel, TableEntry? entry = null)
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

        private static TreeNode CreateParcelNode(Parcel parcel, TableEntry? entry = null)
        {
            TreeNode node = new TreeNode(entry.sectionType);
            node.Tag = new NodeTag(parcel, null, entry);

            foreach (ParcelTOC toc in parcel.ParcelTOCs)
            {
                node.Nodes.Add(CreateTOCNode(toc));
            }

            return node;
        }

        private static TreeNode CreateTOCNode(ParcelTOC toc, string tag = "(ParcelTOC)")
        {
            TreeNode node = new TreeNode(tag);
            node.Tag = new NodeTag(toc);
            return node;
        }


        private static TreeNode CreateTableNode(Table table, TableEntry ownentry = null)
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
}
