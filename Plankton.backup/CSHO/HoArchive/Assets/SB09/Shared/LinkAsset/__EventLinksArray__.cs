using System.Collections.Generic;
using System.Linq;

namespace SB09Assets{
    public class __EventLinksArray__{
        public uint count;
        public uint data; // Pointer to LinkAssets
        //public List<LinkAssetBaseNew> events; // NOT IN DWARF (UNCOMMENT ONCE EVENTS ARE FINISHED)
        public List<byte> events; // REMOVE ONCE EVENTS ARE FINISHED
    
        public __EventLinksArray__(HoArchive.BinaryReaderEndian file, uint elementOffset, uint blobSize){
            count = file.ReadUInt32E();
            data = file.ReadUInt32E();
            uint returnAddr = (uint)file.BaseStream.Position;
            file.BaseStream.Position = elementOffset + data;

            events = file.ReadBytes((int)(blobSize - data)).ToList(); // REMOVE ONCE EVENTS ARE FINISHED
            // UNCOMMENT ONCE EVENTS ARE FINISHED
            /* 
            for (uint evnt=0; evnt<count; evnt++){
                events.Add(new LinkAssetBaseNew(file, elementOffset));
            }
            */
            file.BaseStream.Position = returnAddr;
        }

        public void Save1(HoArchive.BinaryWriterEndian file){
            file.WriteE(count);
            file.WriteE(data);
        }

        public void Save2(HoArchive.BinaryWriterEndian file){
            file.WriteE(events.ToArray());
        }
    }
}