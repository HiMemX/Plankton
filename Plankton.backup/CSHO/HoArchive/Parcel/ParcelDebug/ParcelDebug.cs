using System.Collections.Generic;

namespace HoArchive{
    public class ParcelDebug : ParcelBase{
        public List<NameTableEntry> NameTableEntries = new List<NameTableEntry>();

        public ParcelDebug(){
            
        }

        public ParcelDebug(BinaryReaderEndian file, ParcelDebugSliceMeta SliceMeta){
            long baseposition = file.BaseStream.Position;
            uint curposition;
            List<uint> namelengths = new List<uint>();

            for (int i=0; i<SliceMeta.Entry.count; i++){
                namelengths.Add(file.ReadUInt32E());
            }

            file.BaseStream.Position = baseposition + SliceMeta.Entry.offset;

            foreach (uint length in namelengths){
                curposition = (uint)file.BaseStream.Position;
                NameTableEntries.Add(new NameTableEntry(file));
                file.BaseStream.Position = curposition + length;
            }
        }
        public void Update(uint Align, uint sectorSize, bool Reversed){
            foreach (NameTableEntry entry in NameTableEntries){
                entry.Update();
            }
        }
        public void updateSectors(uint sectorSize, uint startSector){}

        public uint getSize(List<SliceMeta> meta){
            uint size = ((ParcelDebugSliceMeta)meta[0]).Entry.offset;//((ParcelDebugSliceMeta)meta[0]).Entry.count*0x04;
            foreach (NameTableEntry entry in NameTableEntries){
                size += MathTools.RoundUpTo((uint)entry.name.Length + entry.nameOffset + 1, 0x40);
            }
            return size;
        }

        public uint getTotalSize(List<SliceMeta> meta, uint sectorSize){
            uint size = MathTools.RoundUpTo(getSize(meta), sectorSize);
            return size;
        }

        public void Save(BinaryWriterEndian file, uint sectorSize, List<SliceMeta> metas){
            foreach(NameTableEntry entry in NameTableEntries){
                file.WriteE(MathTools.RoundUpTo(entry.nameOffset + (uint)entry.name.Length + 1, 0x40));
            }
            file.PadAlign(0x40, 0x33);
            foreach(NameTableEntry entry in NameTableEntries){
                entry.Save(file);
            }
            file.PadAlign(sectorSize, 0x33);
        }
    }
}