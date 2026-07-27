
namespace SB09Assets{
    public class BaseAsset : Asset.AssetEntity{
        public ulong id;
        public uint baseType;
        public ushort linkCount;
        public ushort baseFlags;
        
        public BaseAsset(HoArchive.BinaryReaderEndian file, uint elementOffset){
            id = file.ReadUInt64E();
            baseType = file.ReadUInt32E();
            linkCount = file.ReadUInt16E();
            baseFlags = file.ReadUInt16E();
        }

        public override void Save(HoArchive.BinaryWriterEndian file){
            file.WriteE(id);
            file.WriteE(baseType);
            file.WriteE(linkCount);
            file.WriteE(baseFlags);
        }

        //public override void Update(HoArchive.TOCEntry entry, uint rawlength){ // rawlength is the length before any data from pointers are added, so the asset without events or sounddata, for example.
        //    id = entry.uidSelf;
        //    baseType = entry.wmlTypeID;
        //    linkCount = 0;
        //}
    }
}