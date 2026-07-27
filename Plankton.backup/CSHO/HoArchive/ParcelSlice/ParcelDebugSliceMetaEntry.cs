
namespace HoArchive{
    public class ParcelDebugSliceMetaEntry{
        public uint count;
        public uint offset;

        public uint unknown1;
        public uint unknown2;
        public uint unknown3;
        public uint unknown4;

        public ParcelDebugSliceMetaEntry(){
        }

        public ParcelDebugSliceMetaEntry(BinaryReaderEndian file){
            count    = file.ReadUInt32E();
            offset   = file.ReadUInt32E();

            unknown1 = file.ReadUInt32E();
            unknown2 = file.ReadUInt32E();
            unknown3 = file.ReadUInt32E();
            unknown4 = file.ReadUInt32E();
        }

        public void Update(ParcelBase parcel){
            count  = (uint)((ParcelDebug)parcel).NameTableEntries.Count;
            offset = MathTools.RoundUpTo(count * 0x04, 0x40);
        }

        public void Save(BinaryWriterEndian file){
            file.WriteE(count);
            file.WriteE(offset);
            file.WriteE(unknown1);
            file.WriteE(unknown2);
            file.WriteE(unknown3);
            file.WriteE(unknown4);
        }
    }
}