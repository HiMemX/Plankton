using System.Collections.Generic;

namespace SB09Assets{
    public class SoundIndices{
        public uint indexCount;
        public List<uint> indices = new List<uint>();

        public SoundIndices(HoArchive.BinaryReaderEndian file){
            indexCount = file.ReadUInt32E();
            
            for (uint index=0; index<indexCount; index++){
                indices.Add(file.ReadUInt32E());
            }
        }

        public void Save(HoArchive.BinaryWriterEndian file){
            file.WriteE(indexCount);
            
            foreach(uint index in indices){
                file.WriteE(index);
            }
        }
    }
}