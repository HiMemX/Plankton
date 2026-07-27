using System.IO;

namespace CSHO{
    public partial class Handler{
        public string Save(bool update = true){
            if(path == ""){return "PATH_NOT_SET";}
            if(Archive == null){return "ARCHIVE_NOT_SET";}

            HoArchive.BinaryWriterEndian file;
            
            try{file = new HoArchive.BinaryWriterEndian(path, endian);}
            catch(FileNotFoundException){ return "ERR_FILE_NOT_FOUND"; }
            catch(DirectoryNotFoundException) { return "ERR_DIRECTORY_NOT_FOUND";}
            catch(IOException)          { return "ERR_IO_EXCEPTION"; }

            if(update){Archive.Update();}
            Archive.Save(file);

            file.Dispose();

            return "";
        }

        public string Update(){
            if(Archive == null){return "ARCHIVE_NOT_SET";}

            Archive.Update();
            return "";
        }
    }
}