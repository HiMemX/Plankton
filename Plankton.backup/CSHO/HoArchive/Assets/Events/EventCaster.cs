
namespace Event{
    public static class EventCaster{

        public static Event ReadEvent(HoArchive.BinaryReaderEndian file, uint type){
            if (!EventKey.Contains(type)){
                return null;
            }
            string wmlTypeName = EventKey.key[type];

            switch (wmlTypeName){
                case "SetCount":
                    return new SetCount(file);

                default:
                    return null;
            }
        }
    }
}