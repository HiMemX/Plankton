using System.Collections.Generic;
using System.IO;

namespace CSHO{
    public partial class Handler{
        public List<byte> ReadFile(string path){
            HoArchive.BinaryReaderEndian file;
            try{file = new HoArchive.BinaryReaderEndian(path, false);} // Using false as default endian, will change on FetchEndian
            catch(FileNotFoundException){ return null; }
            catch(DirectoryNotFoundException) { return null;}
            catch(IOException)          { return null; }

            List<byte> bytes = new List<byte>(file.ReadBytes((int)file.BaseStream.Length));

            file.Dispose();

            return bytes;
        }
    }
}