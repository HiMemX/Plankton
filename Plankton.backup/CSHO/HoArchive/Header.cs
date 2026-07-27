using System;
using System.Globalization;

namespace HoArchive{
    public class Header{
        public string cMagic; // Necessary
        public uint verPackFile;
        public uint verWMlSchema;
        public uint geBuildNum;
        public ulong timeValue;
        public string timeString;
        public uint sectorSize;
        public uint startSector;
        public uint tableSize;
        public uint pad01;
        public string platform;
        public string user;
        public string target;
        public string creator;
        public string comment;
        public string hash;
        public uint libVersion;
        public uint pad02;

        public Header(
            string cMagic_in,
            string platform_in,
            string target_in,
            uint sectorSize_in = 0x800,
            string user_in = "csho_autobuild",
            string creator_in = "",
            string comment_in = "",
            string hash_in = ""
        ){
            cMagic = cMagic_in;
            verPackFile = 1;
            verWMlSchema = 0;
            geBuildNum = 1;
            timeValue = 0;
            timeString = "";
            sectorSize = sectorSize_in;
            startSector = 1;
            tableSize = 0;
            pad01 = 0;
            platform = platform_in;
            user = user_in;
            target = target_in;
            creator = creator_in;
            comment = comment_in;
            hash = hash_in;
            libVersion = 1;
            pad02 = 0;

        }

        public Header(BinaryReaderEndian file){
            cMagic       = file.ReadString(0x04);
            verPackFile  = file.ReadUInt32E();
            verWMlSchema = file.ReadUInt32E();
            geBuildNum   = file.ReadUInt32E();
            timeValue    = file.ReadUInt64E();
            timeString   = file.ReadString(0x19);

            file.BaseStream.Position = 0x40;

            sectorSize   = file.ReadUInt32E();
            startSector  = file.ReadUInt32E();
            tableSize    = file.ReadUInt32E();
            pad01        = file.ReadUInt32E();

            file.BaseStream.Position = 0x400;
            platform = file.ReadWideStringE(0x06, 0);
            file.BaseStream.Position = 0x43C;
            user     = file.ReadWideStringE(0x40, 0);
            file.BaseStream.Position = 0x47C;
            target   = file.ReadWideStringE(0x40, 0);
            file.BaseStream.Position = 0x4BC;
            creator  = file.ReadWideStringE(0x40, 0);
            file.BaseStream.Position = 0x4FC;
            comment  = file.ReadWideStringE(0x40, 0);
            file.BaseStream.Position = 0x5FC;
            hash     = file.ReadWideStringE(0x40, 0);
            file.BaseStream.Position = 0x63C;

            libVersion = file.ReadUInt32E();
            pad02      = file.ReadUInt32E();
        }

        public void Update(Table MasterTable){
            cMagic = StringTools.ConditionalTrim(cMagic, 0x04);
            timeValue = (ulong)DateTimeOffset.Now.ToUnixTimeSeconds();
            timeString = DateTime.Now.ToString("ddd MMM dd HH:mm:ss yyyy", CultureInfo.GetCultureInfo("en-US")) + "\n";

            tableSize = MathTools.RoundUpTo((uint)MasterTable.TableEntries.Count*0x40 + 0x20 + MasterTable.TableHeader.stringTableSize, 0x20);

            platform = StringTools.ConditionalTrim(platform, 0x20);
            user     = StringTools.ConditionalTrim(user, 0x20);
            target   = StringTools.ConditionalTrim(target, 0x20);
            creator  = StringTools.ConditionalTrim(creator, 0x20);
            comment  = StringTools.ConditionalTrim(comment, 0x20);
            hash     = StringTools.ConditionalTrim(hash, 0x20);
        }

        public void Save(BinaryWriterEndian file){
            file.WriteString(cMagic);
            file.WriteE(verPackFile);
            file.WriteE(verWMlSchema);
            file.WriteE(geBuildNum);
            file.WriteE(timeValue);
            file.WriteString(timeString);
            file.PadTo(0x40, 0x00);
            file.WriteE(sectorSize);
            file.WriteE(startSector);
            file.WriteE(tableSize);
            file.WriteE(pad01);
            file.PadTo(0x400, 0x00);

            file.WriteWideStringE(platform);
            file.PadTo(0x43C, 0x00);
            file.WriteWideStringE(user);
            file.PadTo(0x47C, 0x00);
            file.WriteWideStringE(target);
            file.PadTo(0x4BC, 0x00);
            file.WriteWideStringE(creator);
            file.PadTo(0x4FC, 0x00);
            file.WriteWideStringE(comment);
            file.PadTo(0x5FC, 0x00);
            file.WriteWideStringE(hash);
            file.PadTo(0x63C, 0x00);

            file.WriteE(libVersion);
            file.WriteE(pad02);
        }
    }
}