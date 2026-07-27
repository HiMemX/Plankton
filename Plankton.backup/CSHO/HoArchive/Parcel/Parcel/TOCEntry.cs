using System.Collections.Generic;
using System;

namespace HoArchive{
    public class TOCEntry{
        public uint elementSize;
        public uint elementOffset;
        public uint blobSize;
        public uint blobAlign;
        public ulong uidSelf;
        public uint wmlTypeID;
        public ushort subType;
        public ushort blobFlags;

        public Asset.AssetEntity entity;

        public List<byte> data = new List<byte>();

        public TOCEntry(ulong uidSelf_in, uint wmlTypeID_in, uint blobAlign_in = 0x04, ushort subType_in = 0, ushort blobFlags_in = 1, List<byte> data_in = null, Asset.AssetEntity entity_in = null){
            if(data_in == null && entity == null){throw new Exception();}
            
            uidSelf = uidSelf_in;
            wmlTypeID = wmlTypeID_in;
            blobAlign = blobAlign_in;
            subType = subType_in;
            blobFlags = blobFlags_in;

            if(data_in != null){
                data = data_in;
            }
            if(entity_in != null){
                entity = entity_in;
            }
        }

        public TOCEntry(BinaryReaderEndian file, uint DataPtr, string target){
            elementSize   = file.ReadUInt32E();
            elementOffset = file.ReadUInt32E();
            blobSize      = file.ReadUInt32E();
            blobAlign     = file.ReadUInt32E();
            uidSelf       = file.ReadUInt64E();
            wmlTypeID     = file.ReadUInt32E();
            subType       = file.ReadUInt16E();
            blobFlags     = file.ReadUInt16E();

            uint returnaddr = (uint)file.BaseStream.Position;
            file.BaseStream.Position = DataPtr;
            data = new List<byte>(file.ReadBytes((int)blobSize));
            file.BaseStream.Position = DataPtr;
            entity = null;//Asset.AssetCaster.ReadAsset(file, DataPtr, blobSize, wmlTypeID, target);

            file.BaseStream.Position = returnaddr;
        }

        public void Update(uint Align){
            elementSize   = MathTools.RoundUpTo((uint)data.Count, Align);
            blobSize      = (uint)data.Count;
        }

        public void SaveData(BinaryWriterEndian file){
            if (entity == null){file.Write(data.ToArray());}
            else{entity.Save(file);}
            file.Pad(elementSize - blobSize, 0x33);
        }

        public void SaveMeta(BinaryWriterEndian file){
            file.WriteE(elementSize);
            file.WriteE(elementOffset);
            file.WriteE(blobSize);
            file.WriteE(blobAlign);
            file.WriteE(uidSelf);
            file.WriteE(wmlTypeID);
            file.WriteE(subType);
            file.WriteE(blobFlags);
        }
    }
}