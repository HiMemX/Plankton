namespace Event{
    public class SetCount : Event{
        public ushort param0; // 0x00
        public ulong paramWidgetAssetID; // 0x08

        public SetCount(HoArchive.BinaryReaderEndian file){
            param0 = file.ReadUInt16E();
            file.BaseStream.Position += 0x06;
            paramWidgetAssetID = file.ReadUInt64E();
        }
    }
}