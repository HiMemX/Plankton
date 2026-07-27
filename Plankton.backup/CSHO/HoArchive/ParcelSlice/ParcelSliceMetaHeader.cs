using System.Collections.Generic;

namespace HoArchive{
    public class ParcelSliceMetaHeader{
        public uint metaType = 1347636224;
        public uint metaSize;
        public uint numSlices;
        public uint reserved;

        public ParcelSliceMetaHeader(){
            reserved = 0;
        }

        public ParcelSliceMetaHeader(BinaryReaderEndian file){
            metaSize  = file.ReadUInt32E();
            numSlices = file.ReadUInt32E();
            reserved  = file.ReadUInt32E();
        }
        
        public void Update(List<ParcelSliceMetaEntry> TOCs, List<ParcelSliceMetaEntry> DATAs, List<ParcelSliceMetaEntry> PADs){
            metaSize  = (uint)(TOCs.Count + DATAs.Count + PADs.Count + 0x01)*0x10;
            numSlices = (uint)(TOCs.Count + DATAs.Count + PADs.Count);
        }

        public void Save(BinaryWriterEndian file){
            file.WriteString("PSL\0");
            file.WriteE(metaSize);
            file.WriteE(numSlices);
            file.WriteE(reserved);
        }
    }
}