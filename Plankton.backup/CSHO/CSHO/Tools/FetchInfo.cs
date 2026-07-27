
namespace CSHO{
    public partial class Handler{
        bool FetchEndian(HoArchive.BinaryReaderEndian file){
            file.BaseStream.Position = 0x06;
            bool endian = (file.ReadUInt16E() == 0);
            file.BaseStream.Position = 0x00;    
            return endian;
        }

        bool FetchValid(HoArchive.BinaryReaderEndian file){
            file.BaseStream.Position = 0x04;
            bool valid = (file.ReadUInt32E() != 0);
            file.BaseStream.Position = 0x00;
            return valid;
        }



        


        



        
    }
}