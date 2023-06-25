using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HoArchive;

namespace Plankton
{
    public partial class ExportAllWindow : Form
    {
        public CSHO.Handler handler;
        public FolderBrowserDialog dialog;
        public ExportAllWindow()
        {
            dialog = new FolderBrowserDialog();
            InitializeComponent();
        }

        private void exportAssets(string path, List<HoArchive.TOCEntry> assets)
        {
            foreach(HoArchive.TOCEntry asset in assets)
            {
                Directory.CreateDirectory(path + "/" + asset.wmlTypeID.ToString());
          

                asset.Update(0x40);

                HoArchive.BinaryWriterEndian writer = new HoArchive.BinaryWriterEndian(path + "/" + asset.wmlTypeID.ToString() + "/" + handler.GetName(asset.uidSelf) + " [" + asset.uidSelf.ToString("X8") + "].dat", false);

                writer.WriteE(asset.data.ToArray());
                writer.Dispose();
            }

        }
        private void exportParcel(string path, HoArchive.Parcel parcel)
        {
            if (differentiateTablesCheck.Checked)
            {
                for(int t=0; t<parcel.ParcelTOCs.Count; t++)
                {
                    HoArchive.ParcelTOC toc = parcel.ParcelTOCs[t];
                    exportAssets(path + "/[" + t + "]", toc.Entries);
                }
            }
            else
            {
                for (int t = 0; t < parcel.ParcelTOCs.Count; t++)
                {
                    HoArchive.ParcelTOC toc = parcel.ParcelTOCs[t];
                    exportAssets(path, toc.Entries);
                }
            }
        }

        private void exportTable(string path, HoArchive.Table table)
        {
            for (int p = 0; p < table.Parcels.Count; p++)
            {
                if (table.Parcels[p] is HoArchive.Table)
                {
                    HoArchive.Table table2 = (HoArchive.Table)table.Parcels[p];
                    exportTable(path + "/" + table2.TableHeader.tableTypeTag + " [" + p + "]", table2);
                }
                if (table.Parcels[p] is HoArchive.Parcel)
                {
                    HoArchive.Parcel parcel = (HoArchive.Parcel)table.Parcels[p];
                    exportParcel(path + "/" + table.TableEntries[p].sectionType + " [" + p + "]", parcel);
                }
            }
        }

        private void exportButton_Click_1(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.SelectedPath)) { return; }

            string path = dialog.SelectedPath;


            handler.Update();

            if (differentiateParcelsCheck.Checked)
            {
                exportTable(path + "/" + Path.GetFileName(handler.path), handler.Archive.MasterTable);
            }

            else if (!differentiateTablesCheck.Checked)
            {
                exportAssets(path + "/" + Path.GetFileName(handler.path), handler.GetAssets());
            }
            else {
                List<HoArchive.ParcelBase> parcels = handler.GetParcels((HoArchive.Table)handler.Archive.MasterTable); 
                for(int p=0; p<parcels.Count; p++)
                {
                    if (parcels[p] is HoArchive.ParcelDebug) { continue; }
                    HoArchive.Parcel parcel = (HoArchive.Parcel)parcels[p];
                    exportParcel(path + "/" + Path.GetFileName(handler.path), parcel);
                }
            }

            Hide();
        }
    }
}
