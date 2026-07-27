using System.Collections.Generic;

namespace Event{
    public static class EventKey{
        public static Dictionary<uint, string> key = new Dictionary<uint, string>{
            {2488081165, "SetCount"}
        };

        public static bool Contains(uint type){
            return key.ContainsKey(type);
        }
    }
}