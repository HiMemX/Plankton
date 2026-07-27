using System.Collections.Generic;

namespace CSHO{
    public partial class Handler{
        public string New(
            bool endian_in,
            string platform_in,
            string target_in,
            string DomainString, 
            string cMagic_in = "HEL→",
            uint sectorSize_in = 0x800,
            string user_in = "csho_autobuild",
            string creator_in = "",
            string comment_in = "",
            string hash_in = "",
            uint tableFlags = 0
        ){
            Archive = new HoArchive.Archive(cMagic_in, platform_in, target_in, DomainString, sectorSize_in, user_in, creator_in, comment_in, hash_in, tableFlags);
            NewSection((HoArchive.Table)Archive.MasterTable, DomainString);
            endian = endian_in;
            return "";
        }
        
        public string NewFrom(string path_in, bool keepPlayerAssets = true){
            string errorcode = Open(path_in);
            if(errorcode != ""){return errorcode;}

            List<HoArchive.TOCEntry> assets = GetAssets();
            List<HoArchive.TOCEntry> playerAssets = new List<HoArchive.TOCEntry>();
            List<HoArchive.NameTableEntry> playerNameTableEntries = new List<HoArchive.NameTableEntry>();
            foreach(HoArchive.TOCEntry asset in assets){
                if(asset.wmlTypeID == 0xD836DA19){
                    asset.data[0x108] = 0x00; // Hacky but it works :)
                    asset.data[0x109] = 0x00;
                    asset.data[0x10A] = 0x00;
                    asset.data[0x10B] = 0x00;
                    asset.data[0x10C] = 0x00;
                    asset.data[0x10D] = 0x00;
                    asset.data[0x10E] = 0x00;
                    asset.data[0x10F] = 0x00;
                    playerAssets.Add(asset);
                    playerNameTableEntries.Add(GetNameEntry(asset.uidSelf));
                }
            }
            
            Archive.MasterTable = new HoArchive.Table("MAST", Archive.MasterTable.StringTable.DomainString, Archive.MasterTable.TableHeader.tableFlags);
            NewSection((HoArchive.Table)Archive.MasterTable, Archive.MasterTable.StringTable.DomainString);

            if(keepPlayerAssets){
                NewParcel((HoArchive.Table)((HoArchive.Table)Archive.MasterTable).Parcels[0], "P   ");
                NewParcelDebug((HoArchive.Table)((HoArchive.Table)Archive.MasterTable).Parcels[0]);
                NewTable((HoArchive.Parcel)((HoArchive.Table)((HoArchive.Table)Archive.MasterTable).Parcels[0]).Parcels[0]);
                for(int asset = 0; asset<playerAssets.Count; asset++){
                    ((HoArchive.Parcel)((HoArchive.Table)((HoArchive.Table)Archive.MasterTable).Parcels[0]).Parcels[0]).ParcelTOCs[0].Entries.Add(playerAssets[asset]);
                    ((HoArchive.ParcelDebug)((HoArchive.Table)((HoArchive.Table)Archive.MasterTable).Parcels[0]).Parcels[1]).NameTableEntries.Add(playerNameTableEntries[asset]);
                }
            }

            return "";
        }
    }
}