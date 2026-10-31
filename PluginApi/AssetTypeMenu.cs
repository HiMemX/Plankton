using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HoArchive;

namespace PluginApi
{
    public static class AssetTypeMenu
    {
        public static void Populate(
            ToolStripItemCollection items,
            IEnumerable<wmlTypeID> types,
            Action<wmlTypeID> selected)
        {
            items.Clear();

            foreach (wmlTypeID type in types)
            {
                var item = new ToolStripMenuItem(
                    GetDisplayName(type));

                item.Click += (_, _) =>
                    selected(type);

                items.Add(item);
            }
        }

        private static string GetDisplayName(
            wmlTypeID type)
        {
            return type.ToString();
        }
    }
}
