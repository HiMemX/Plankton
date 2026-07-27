namespace SB09Assets{
    public class LinkAssetBaseNew{
        public __srcEvent__ srcEvent;
        public __dstEvent__ dstEvent;
        public ulong dstAssetID;
        public ulong chkAssetID;
        public byte chkSourceParams;
        public byte disabled;
        public ushort disabled2;
        public uint chkSourceMask;

        public LinkAssetBaseNew(HoArchive.BinaryReaderEndian file, uint elementOffset){
            srcEvent = new __srcEvent__(file, elementOffset);
            dstEvent = new __dstEvent__(file, elementOffset);
            dstAssetID = file.ReadUInt64E();
            chkAssetID = file.ReadUInt64E();
            chkSourceParams = file.ReadByte();
            disabled = file.ReadByte();
            disabled2 = file.ReadUInt16E();
            chkSourceMask = file.ReadUInt32E();
        }
    }
}