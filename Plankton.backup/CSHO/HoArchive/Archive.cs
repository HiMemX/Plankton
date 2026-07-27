using System.Collections.Generic;

namespace HoArchive{
    public class Archive{
        public Header Header;
        public Table MasterTable;

        public Archive(
            string cMagic_in,
            string platform_in,
            string target_in,
            string DomainString, 
            uint sectorSize_in = 0x800,
            string user_in = "csho_autobuild",
            string creator_in = "",
            string comment_in = "",
            string hash_in = "",
            uint tableFlags = 0,
            string tableTypeTag = "MAST"
        ){
            Header = new Header(cMagic_in, platform_in, target_in, sectorSize_in, user_in, creator_in, comment_in, hash_in);
            MasterTable = new Table(tableTypeTag, DomainString, tableFlags);
        }

        public Archive(BinaryReaderEndian file){
            Header = new Header(file);

            file.BaseStream.Position = Header.startSector * Header.sectorSize;
            MasterTable = new Table(file, Header.sectorSize, Header.target);
        }

        public void Update(){
            uint startSector  = MathTools.CeilDiv(0x800, Header.sectorSize);
            Header.startSector = startSector;

            MasterTable.Update(0x40, Header.sectorSize, false); // 0x40 is default alignment
            MasterTable.updateSectors(Header.sectorSize, startSector);
            Header.Update(MasterTable);
        }

        public void Save(BinaryWriterEndian file){
            Header.Save(file);
            
            file.PadTo(Header.sectorSize*Header.startSector, 0x00);

            MasterTable.Save(file, Header.sectorSize, new List<SliceMeta>());
        }
    }
}