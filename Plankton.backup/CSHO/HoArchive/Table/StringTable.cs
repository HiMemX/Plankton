using System.Collections.Generic;
using System;

namespace HoArchive{
    public class StringTable{
        public List<string> StringTableEntries = new List<string>();
        public string DomainString;

        public StringTable(string DomainString_in){
            DomainString = DomainString_in;
        }

        public StringTable(BinaryReaderEndian file, long DomainStringPtr){
            while(file.BaseStream.Position < DomainStringPtr){
                file.BaseStream.Position += 0x0C;
                StringTableEntries.Add(file.ReadUntil(0));
                file.Align(0x04);
            }
            file.BaseStream.Position = DomainStringPtr;
            DomainString = file.ReadUntil(0);
        }

        public void Update(){
        }

        public void Save(BinaryWriterEndian file, bool writeDomainString){
            foreach (string entry in StringTableEntries){  
                file.Pad(0x0C, 0xEE);
                file.WriteString(entry + "\0");
                file.PadAlign(0x04, 0x33);
            }
            if(writeDomainString){
                file.WriteString(DomainString + "\0");
            }
        }
    }
}