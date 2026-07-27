
namespace HoArchive{
    public class ParcelSliceMetaEntry{
        public uint sliceType;
        public uint sliceStart;
        public uint sliceSize;
        public uint sliceAlign;

        public ParcelSliceMetaEntry(BinaryReaderEndian file){
            sliceType   = file.ReadUInt32E();
            sliceStart  = file.ReadUInt32E();
            sliceSize   = file.ReadUInt32E();
            sliceAlign  = file.ReadUInt32E();
        }
        public ParcelSliceMetaEntry(uint sliceType_in, uint sliceStart_in, uint sliceSize_in, uint sliceAlign_in){
            sliceType   = sliceType_in;
            sliceStart  = sliceStart_in;
            sliceSize   = sliceSize_in;
            sliceAlign  = sliceAlign_in;
        }

        public void Save(BinaryWriterEndian file){
            file.WriteE(sliceType);
            file.WriteE(sliceStart);
            file.WriteE(sliceSize);
            file.WriteE(sliceAlign);
        }
    }
}