namespace SB09Assets{
    public class SoundBankSource{
        public byte streamed;
        public byte pad1;
        public byte pad2;
        public byte pad3;
        public uint sourceStringPtr; // NOT DIRECTLY IN DWARF
        public uint indicesPtr; // NOT DIRECTLY IN DWARF

        public string sourceString;
        public SoundIndices indices; // CLASS NOT IN DWARF

        public SoundBankSource(HoArchive.BinaryReaderEndian file, uint elementOffset){
            streamed = file.ReadByte();
            pad1 = file.ReadByte();
            pad2 = file.ReadByte();
            pad3 = file.ReadByte();
            sourceStringPtr = file.ReadUInt32E();
            indicesPtr = file.ReadUInt32E();
            
            uint returnAddr = (uint)file.BaseStream.Position;
            file.BaseStream.Position = elementOffset + sourceStringPtr;
            sourceString = file.ReadUntil(0x00);
            file.Align(0x04);
            file.BaseStream.Position = elementOffset + indicesPtr;
            indices = new SoundIndices(file);

            file.BaseStream.Position = returnAddr;

        }

        public void Save1(HoArchive.BinaryWriterEndian file){
            file.WriteE(streamed);
            file.WriteE(pad1);
            file.WriteE(pad2);
            file.WriteE(pad3);
            file.WriteE(sourceStringPtr);
            file.WriteE(indicesPtr);
        }

        public void Save2(HoArchive.BinaryWriterEndian file){
            file.WriteString(sourceString + "\0");
            file.PadAlign(0x04, 0x00);
            indices.Save(file);
        }
    }
}