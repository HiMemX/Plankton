

namespace HoArchive{
    public class TableHeader{ // Update gets handeled by Table.cs
        public string tableTypeTag; // Necessary
        public uint entryCount;
        public uint tableFlags; // Optional
        public uint firstString;
        public uint stringTableSize;
        public uint firstMetaRec;
        public uint metaDataSize;
        public uint reserved;

        public TableHeader(string tableTypeTag_in, uint tableFlags_in = 0){
            tableTypeTag = tableTypeTag_in;
            entryCount = 0;
            tableFlags = tableFlags_in;
            firstString = 0;
            stringTableSize = 0;
            firstMetaRec = 0;
            metaDataSize = 0;
            reserved = 0;
        }

        public TableHeader(BinaryReaderEndian file){
            tableTypeTag    = file.ReadString(0x04); // Update
            entryCount      = file.ReadUInt32E(); // Update
            tableFlags      = file.ReadUInt32E(); 
            firstString     = file.ReadUInt32E(); // Update
            stringTableSize = file.ReadUInt32E(); // Update
            firstMetaRec    = file.ReadUInt32E(); // Update
            metaDataSize    = file.ReadUInt32E(); // Update
            reserved        = file.ReadUInt32E();
        }

        public void Save(BinaryWriterEndian file){
            file.WriteString(tableTypeTag);
            file.WriteE(entryCount);
            file.WriteE(tableFlags);
            file.WriteE(firstString);
            file.WriteE(stringTableSize);
            file.WriteE(firstMetaRec);
            file.WriteE(metaDataSize);
            file.WriteE(reserved);
        }
    }
}