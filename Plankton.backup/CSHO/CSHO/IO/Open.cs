using System.IO;

namespace CSHO{
    public partial class Handler{
        public string Open(string path_in){
            HoArchive.BinaryReaderEndian file;
            HoArchive.Archive temparchive;

            try{file = new HoArchive.BinaryReaderEndian(path_in, false);} // Using false as default endian, will change on FetchEndian
            catch(FileNotFoundException){ return "ERR_FILE_NOT_FOUND"; }
            catch(DirectoryNotFoundException) { return "ERR_DIRECTORY_NOT_FOUND";}
            catch(IOException)          { return "ERR_IO_EXCEPTION"; }

            if (FetchValid(file) == false){ return "ERR_INVALID_FILE"; }
            
            endian = FetchEndian(file);
            file.endianness = endian;

            try{temparchive = new HoArchive.Archive(file);}
            catch(EndOfStreamException){ return "ERR_END_OF_STREAM"; }
            
            Archive = temparchive;
            path = path_in;

            file.Dispose();
            return "";
        }
        
    }
}