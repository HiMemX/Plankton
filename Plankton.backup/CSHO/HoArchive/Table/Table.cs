using System.Collections.Generic;
using System;

namespace HoArchive{
    public class Table : ParcelBase{ // Table is a type of Parcel (SECT, P, PD, PTEX, PFST). It will have the same methodes as normal parcels to allow for a complex tree (It also saves me some code).
        public TableHeader TableHeader;
        public List<TableEntry> TableEntries = new List<TableEntry>();
        public StringTable StringTable;
        public List<List<SliceMeta>> MetaTableEntries = new List<List<SliceMeta>>();
        public List<ParcelBase> Parcels = new List<ParcelBase>();


        public Table(string tableTypeTag, string DomainString, uint tableFlags = 0){
            TableHeader = new TableHeader(tableTypeTag, tableFlags);
            StringTable = new StringTable(DomainString);
        }

        public Table(BinaryReaderEndian file, uint sectorSize, string target){
            long baseposition = file.BaseStream.Position;

            // Table Header
            TableHeader = new TableHeader(file);

            // Table Entries
            for (int entry=0; entry<TableHeader.entryCount; entry++){
                TableEntries.Add(new TableEntry(file));
            }

            // String Table
            file.BaseStream.Position = TableHeader.firstString + baseposition;
            long DomainStringPtr = TableEntries[0].namePtr + baseposition;
            StringTable = new StringTable(file, DomainStringPtr);

            // Meta Entries
            List<SliceMeta> tempentries = new List<SliceMeta>();
            foreach (TableEntry entry in TableEntries){
                if (entry.metaRecord != 0xFFFFFFFF){
                    file.BaseStream.Position = entry.metaRecord + baseposition;
                }

                tempentries = new List<SliceMeta>();
                for (int meta=0; meta<entry.metaBlockCount; meta++){
                    string type = file.ReadString(0x04);

                    if (type == "PSL\0"){
                        tempentries.Add(new ParcelSliceMeta(file));
                    }
                    else{
                        tempentries.Add(new ParcelDebugSliceMeta(file));
                    }
                }
                MetaTableEntries.Add(tempentries);
            }

            // Parcels
            for (int i=0; i<TableHeader.entryCount; i++){
                file.BaseStream.Position = TableEntries[i].startSector * sectorSize;
                switch (TableEntries[i].sectionType){
                    case "SECT":
                        Parcels.Add(new Table(file, sectorSize, target));
                        break;
                    
                    case "PD  ":
                        Parcels.Add(new ParcelDebug(file, (ParcelDebugSliceMeta)MetaTableEntries[i][0]));
                        break;

                    default:
                        Parcels.Add(new Parcel(file, (ParcelSliceMeta)MetaTableEntries[i][0], target));
                        break;
                }
            }
        }

        public void Update(uint Align, uint sectorSize, bool Reversed){
            // Parcel (Parcel Content)
            for (int parcel=0; parcel<Parcels.Count; parcel++){
                if (TableEntries[parcel].sectionType != "PD  " && TableEntries[parcel].sectionType != "SECT"){ // This weird stuff cause .hoes don't like being reversed
                    Parcels[parcel].Update(TableEntries[parcel].memoryAlignment, sectorSize, ((ParcelSliceMeta)MetaTableEntries[parcel][0]).Reversed);
                    continue;
                }
                Parcels[parcel].Update(TableEntries[parcel].memoryAlignment, sectorSize, false);
            }
            // ParcelMeta (Parcel Content description)
            for (int parcel=0; parcel<Parcels.Count; parcel++){
                foreach (SliceMeta meta in MetaTableEntries[parcel]){
                    meta.Update(Parcels[parcel], TableEntries[parcel].memoryAlignment, sectorSize);
                }
            }
            // StringTable (We have no clue what it does, it's just there. We do know how it works doe)
            StringTable.Update();
            
            // TableEntry (Parcel Registry)
            uint StringTableLength = 0;
            foreach (string entry in StringTable.StringTableEntries){StringTableLength += MathTools.RoundUpTo(0x0D + (uint)entry.Length, 0x04);}
            uint namePtr = (uint)(TableEntries.Count * 0x40 + 0x20 + StringTableLength);

            uint size;
            uint currMetaRecord = MathTools.RoundUpTo((uint)(TableEntries.Count * 0x40 + 0x20 + StringTableLength + StringTable.DomainString.Length + 0x01), 0x20);
            uint nameHash = MathTools.LowerCaseBKDR(StringTable.DomainString);
            for (int parcel=0; parcel<Parcels.Count; parcel++){
                size = Parcels[parcel].getSize(MetaTableEntries[parcel]);
                
                TableEntries[parcel].sectionType    = StringTools.ConditionalTrim(TableEntries[parcel].sectionType, 0x04);
                TableEntries[parcel].namePtr        = namePtr;
                TableEntries[parcel].sizeOnDisk     = size;
                TableEntries[parcel].sizeInMem      = size;
                TableEntries[parcel].metaBlockCount = (uint)MetaTableEntries[parcel].Count;
                TableEntries[parcel].nameHash       = nameHash;

                if (TableEntries[parcel].metaBlockCount == 0){
                    TableEntries[parcel].metaRecord = 0xFFFFFFFF;
                    continue;
                }

                TableEntries[parcel].metaRecord     = currMetaRecord;
                foreach(SliceMeta selfmeta in MetaTableEntries[parcel]){
                    if (selfmeta is ParcelSliceMeta){currMetaRecord += ((ParcelSliceMeta)selfmeta).Header.metaSize;}
                    else {currMetaRecord += ((ParcelDebugSliceMeta)selfmeta).Header.metaSize;}
                }
            }
            
            // TableHeader (Table description)
            uint MetaTableLength = 0;
            foreach (List<SliceMeta> metas in MetaTableEntries){
                foreach(SliceMeta selfmeta in metas){
                    if (selfmeta is ParcelSliceMeta){MetaTableLength += ((ParcelSliceMeta)selfmeta).Header.metaSize;}
                    else {MetaTableLength += ((ParcelDebugSliceMeta)selfmeta).Header.metaSize;}
                }
            }

            TableHeader.tableTypeTag    = StringTools.ConditionalTrim(TableHeader.tableTypeTag, 0x04);
            TableHeader.entryCount      = (uint)TableEntries.Count;
            TableHeader.firstString     = (uint)(TableEntries.Count * 0x40 + 0x20);
            TableHeader.stringTableSize = (uint)(StringTableLength + (StringTable.DomainString.Length + 0x01)*Convert.ToInt32(TableEntries.Count > 0));
            TableHeader.firstMetaRec    = MathTools.RoundUpTo((uint)(TableEntries.Count * 0x40 + 0x20 + StringTableLength + StringTable.DomainString.Length + 0x01), 0x20);
            TableHeader.metaDataSize    = MetaTableLength;
            
            if (TableHeader.metaDataSize == 0){
                TableHeader.firstMetaRec = 0xFFFFFFFF;
            }
        }

        public void updateSectors(uint sectorSize, uint startSector){
            uint currSector = startSector + MathTools.CeilDiv(MathTools.RoundUpTo(TableHeader.firstString + TableHeader.stringTableSize, 0x20) + TableHeader.metaDataSize, sectorSize);
            

            for (int parcel=0; parcel<TableHeader.entryCount; parcel++){
                Parcels[parcel].updateSectors(sectorSize, currSector);
                TableEntries[parcel].startSector = currSector;
                currSector += Parcels[parcel].getTotalSize(MetaTableEntries[parcel], sectorSize) / sectorSize;
            }
        }

        public uint getSize(List<SliceMeta> meta){
            uint StringTableLength = 0;
            uint MetaTableLength = 0;
            foreach (string entry in StringTable.StringTableEntries){StringTableLength += 0x0D + (uint)entry.Length;}
            foreach (List<SliceMeta> metas in MetaTableEntries){
                foreach(SliceMeta selfmeta in metas){
                    if (selfmeta is ParcelSliceMeta){MetaTableLength += ((ParcelSliceMeta)selfmeta).Header.metaSize;}
                    else {MetaTableLength += ((ParcelDebugSliceMeta)selfmeta).Header.metaSize;}
                }
            }
            uint size = MathTools.RoundUpTo((uint)(TableEntries.Count * 0x40 + 0x20 + StringTableLength + (StringTable.DomainString.Length + 0x01)*Convert.ToInt32(TableEntries.Count > 0)), 0x20) + MetaTableLength;
            return size;
        }

        public uint getTotalSize(List<SliceMeta> meta, uint sectorSize){
            uint size = getSize(meta);
            size = MathTools.RoundUpTo(size, sectorSize);
            for (int parcel=0; parcel<Parcels.Count; parcel++){
                size += Parcels[parcel].getTotalSize(MetaTableEntries[parcel], sectorSize);
            }
            return size;
        }


        public void Save(BinaryWriterEndian file, uint sectorSize, List<SliceMeta> selfmetas){
            TableHeader.Save(file);

            foreach (TableEntry entry in TableEntries){
                entry.Save(file);
            }

            StringTable.Save(file, TableEntries.Count > 0);
            file.PadAlign(0x20, 0x33);

            foreach (List<SliceMeta> metas in MetaTableEntries){
                foreach (SliceMeta meta in metas){
                    meta.Save(file);
                }
            }

            file.PadAlign(sectorSize, 0x33);

            for (int i=0; i<TableHeader.entryCount; i++){
                file.PadTo(TableEntries[i].startSector*sectorSize, 0x33);
                Parcels[i].Save(file, sectorSize, MetaTableEntries[i]);
            }
        }
    }
}