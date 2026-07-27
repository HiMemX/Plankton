namespace SB09Assets{
    public class __dstEvent__{
        public uint type;
        public uint v;
        public Event.Event eventparams; // NOT IN DWARF

        public __dstEvent__(HoArchive.BinaryReaderEndian file, uint elementOffset){
            type = file.ReadUInt32E();
            v = file.ReadUInt32E();

            uint returnAddr = (uint)file.BaseStream.Position;
            file.BaseStream.Position = elementOffset + v;

            eventparams = Event.EventCaster.ReadEvent(file, type);

            file.BaseStream.Position = returnAddr;
        }
    }
}