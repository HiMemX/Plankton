
namespace HoArchive{
    public class TOCHeader{
        public uint elementCount;
        public uint brickDataOffset;
        public uint[] reserved = new uint[6];

        public TOCHeader(){
            elementCount = 0;
            brickDataOffset = 0xFFFFFFFF;
            reserved = new uint[6]{0x74747474, 0x74747474, 0x74747474, 0x74747474, 0x74747474, 0x74747474};
        }
        public TOCHeader(BinaryReaderEndian file){
            elementCount    = file.ReadUInt32E();
            brickDataOffset = file.ReadUInt32E();

            for (int i=0; i<6; i++){
                reserved[i] = file.ReadUInt32E();
            }
        }

        public void Save(BinaryWriterEndian file){
            file.WriteE(elementCount);
            file.WriteE(brickDataOffset);
            foreach (uint reserve in reserved){
                file.WriteE(reserve);
            }
        }
    }
}