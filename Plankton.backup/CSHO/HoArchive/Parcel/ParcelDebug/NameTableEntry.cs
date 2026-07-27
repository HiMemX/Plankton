
namespace HoArchive{
    public class NameTableEntry{
        public ulong uidAsset;
        public uint nameOffset;
        public uint typeID;
        public uint[] unknown = new uint[4];
        
        public string name;

        public NameTableEntry(ulong uidAsset_in, string name_in, uint typeID_in = 0xFFFFFFFF){
            uidAsset = uidAsset_in;
            name = name_in;
            typeID = typeID_in;
        }

        public NameTableEntry(BinaryReaderEndian file){
            long baseposition = file.BaseStream.Position;
            uidAsset = file.ReadUInt64E();
            nameOffset = file.ReadUInt32E();
            typeID = file.ReadUInt32E();

            for (int i=0; i<4; i++){
                unknown[i] = file.ReadUInt32E();
            }

            file.BaseStream.Position = baseposition + nameOffset;
            name = file.ReadUntil(0);
        }

        public void Update(){
            nameOffset = 0x20;
        }

        public void Save(BinaryWriterEndian file){
            file.WriteE(uidAsset);
            file.WriteE(nameOffset);
            file.WriteE(typeID);
            foreach (uint unknwn in unknown){
                file.WriteE(unknwn);
            }
            file.WriteString(name + "\0");
            file.PadAlign(0x40, 0x33);
        }
    }
}