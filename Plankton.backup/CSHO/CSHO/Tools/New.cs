using System.Collections.Generic;

namespace CSHO{
    public partial class Handler{
        public string NewSection(HoArchive.Table table, string DomainString){
            table.TableEntries.Add(new HoArchive.TableEntry("SECT"));
            table.MetaTableEntries.Add(new List<HoArchive.SliceMeta>());
            table.Parcels.Add(new HoArchive.Table("SECT", DomainString));
            return "";
        }


        // ------------ Most important ---------------- //

        public string NewParcel(HoArchive.Table table, 
                        string sectionType_in, 
                        ushort packLangID_in = 0, 
                        byte parcelType_in = 1, 
                        uint userKey_in = 0, 
                        uint fromNameHash_in = 0, 
                        uint fromNamePtr_in = 0xFFFFFFFF,
                        uint attributeFlags_in = 0,
                        uint externName_in = 0xFFFFFFFF,
                        bool Reversed = false){
            table.TableEntries.Add(new HoArchive.TableEntry(sectionType_in, packLangID_in, parcelType_in, userKey_in, fromNameHash_in, fromNamePtr_in, attributeFlags_in, externName_in));
            table.MetaTableEntries.Add(new List<HoArchive.SliceMeta>(){new HoArchive.ParcelSliceMeta(Reversed)});
            table.Parcels.Add(new HoArchive.Parcel());

            return "";
        }

        public string NewParcelDebug(HoArchive.Table table,
                        ushort packLangID_in = 0, 
                        byte parcelType_in = 1, 
                        uint userKey_in = 0, 
                        uint fromNameHash_in = 0, 
                        uint fromNamePtr_in = 0xFFFFFFFF,
                        uint attributeFlags_in = 0,
                        uint externName_in = 0xFFFFFFFF,
                        bool Reversed = false){
            table.TableEntries.Add(new HoArchive.TableEntry("PD  ", packLangID_in, parcelType_in, userKey_in, fromNameHash_in, fromNamePtr_in, attributeFlags_in, externName_in));
            table.MetaTableEntries.Add(new List<HoArchive.SliceMeta>(){new HoArchive.ParcelDebugSliceMeta()});
            table.Parcels.Add(new HoArchive.ParcelDebug());

            return "";
        }

        public string NewTable(HoArchive.Parcel parcel){
            parcel.ParcelTOCs.Add(new HoArchive.ParcelTOC());
        
            return "";
        }

        public string NewAsset(HoArchive.ParcelTOC toc, ulong uidSelf_in, uint wmlTypeID_in, uint blobAlign_in = 0x04, ushort subType_in = 0, ushort blobFlags_in = 1, List<byte> data_in = null, Asset.AssetEntity entity_in = null){
            toc.Entries.Add(new HoArchive.TOCEntry(uidSelf_in, wmlTypeID_in, blobAlign_in, subType_in, blobFlags_in, data_in, entity_in));
            
            return "";
        }

        public string NewAsset(HoArchive.ParcelTOC toc, ulong uidSelf_in, uint wmlTypeID_in, string path, uint blobAlign_in = 0x04, ushort subType_in = 0, ushort blobFlags_in = 1){
            toc.Entries.Add(new HoArchive.TOCEntry(uidSelf_in, wmlTypeID_in, data_in: ReadFile(path)));
            
            return "";
        }

        public string NewNameTableEntry(HoArchive.ParcelDebug parcel, ulong uidAsset_in, string name_in, uint typeID_in = 0xFFFFFFFF){
            parcel.NameTableEntries.Add(new HoArchive.NameTableEntry(uidAsset_in, name_in, typeID_in));

            return "";
        }
    }
}