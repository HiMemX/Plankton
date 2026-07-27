using System.Collections.Generic;
using System.Linq;

namespace CSHO{
    public partial class Handler{
        public List<HoArchive.TOCEntry> GetAssets(){
            List<HoArchive.TOCEntry> assets = new List<HoArchive.TOCEntry>();
            foreach (HoArchive.ParcelBase table in Archive.MasterTable.Parcels){
                foreach (HoArchive.ParcelBase parcel in ((HoArchive.Table)table).Parcels){
                    if (parcel is HoArchive.Parcel == false){continue;}

                    foreach (HoArchive.ParcelTOC toc in ((HoArchive.Parcel)parcel).ParcelTOCs){
                        foreach (HoArchive.TOCEntry entry in toc.Entries){
                            assets.Add(entry);
                        }
                    }
                }
            }
            return assets;
        }

        public List<byte> GetAssetData(ulong AssetID){
            HoArchive.TOCEntry asset = GetAsset(AssetID);
            if (asset == null){return new List<byte>();}
            return asset.data;
        }

        public HoArchive.TOCEntry GetAsset(ulong AssetID){
            List<HoArchive.TOCEntry> assets = GetAssets();
            foreach (HoArchive.TOCEntry entry in assets){
                if (entry.uidSelf == AssetID){return entry;}
            }
            return null;
        }

        public List<HoArchive.NameTableEntry> GetNameEntries(){
            List<HoArchive.NameTableEntry> entries = new List<HoArchive.NameTableEntry>();
            foreach (HoArchive.ParcelBase table in Archive.MasterTable.Parcels){
                foreach (HoArchive.ParcelBase parcel in ((HoArchive.Table)table).Parcels){
                    if (parcel is HoArchive.ParcelDebug == false){continue;}

                    foreach (HoArchive.NameTableEntry entry in ((HoArchive.ParcelDebug)parcel).NameTableEntries){
                        entries.Add(entry);
                    }
                }
            }
            return entries;
        }

        public HoArchive.NameTableEntry GetNameEntry(ulong AssetID){
            List<HoArchive.NameTableEntry> entries = GetNameEntries();
            foreach (HoArchive.NameTableEntry entry in entries){
                if (entry.uidAsset == AssetID){return entry;}
            }
            return null;
        }

        public string GetName(ulong AssetID){
            HoArchive.NameTableEntry entry = GetNameEntry(AssetID);
            if (entry == null){return null;}
            return entry.name;
        }

    }
}