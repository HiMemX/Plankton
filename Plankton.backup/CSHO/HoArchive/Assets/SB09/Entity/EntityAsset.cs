
namespace SB09Assets{
    public class EntityAsset : BaseAsset{
        public byte flags;
        public byte subtype;
        public byte pad;
        public byte Targettable;
        public byte Pad0;
        public byte MoreFlags;// INT24
        public ulong surfaceID;
        public HoArchive.float3 Orientation;
        public HoArchive.float3 Pos;
        public HoArchive.float3 Scale;
        public ModelInstanceAsset modelInstance;
        public ulong animListID;
        public PhysicsDataStruct physicsData;
        public uint Pad1;
        public uint Pad2;
        public uint Pad3;

        public EntityAsset(HoArchive.BinaryReaderEndian file, uint elementOffset) : base(file, elementOffset){
            flags = file.ReadByte();
            subtype = file.ReadByte();
            pad = file.ReadByte();
            Targettable = file.ReadByte();
            Pad0 = file.ReadByte();
            MoreFlags = file.ReadByte();
            file.BaseStream.Position += 0x02;
            surfaceID = file.ReadUInt64E();
            Orientation = new HoArchive.float3(file);
            Pos = new HoArchive.float3(file);
            Scale = new HoArchive.float3(file);
            file.BaseStream.Position += 0x0C;
            modelInstance = new ModelInstanceAsset(file);
            animListID = file.ReadUInt64E();
            physicsData = new PhysicsDataStruct(file, elementOffset);
            Pad1 = file.ReadUInt32E();
            Pad2 = file.ReadUInt32E();
            Pad3 = file.ReadUInt32E();
        }

        public void Save1(HoArchive.BinaryWriterEndian file){
            base.Save(file);
            file.WriteE(flags);
            file.WriteE(subtype);
            file.WriteE(pad);
            file.WriteE(Targettable);
            file.WriteE(Pad0);
            file.WriteE(MoreFlags);
            file.Pad(0x02, 0x00);
            file.WriteE(surfaceID);
            Orientation.Save(file);
            Pos.Save(file);
            Scale.Save(file);
            file.Pad(0x0C, 0x00);
            modelInstance.Save(file);
            file.WriteE(animListID);
            physicsData.Save1(file);
            file.WriteE(Pad1);
            file.WriteE(Pad2);
            file.WriteE(Pad3);
        }

        public void Save2(HoArchive.BinaryWriterEndian file){
            physicsData.Save2(file);
        }

        //public override void Update(HoArchive.TOCEntry entry, uint rawlength){
        //    modelInstance.Update(entry, rawlength);
        //    physicsData.Update(entry);
        //    base.Update(entry);
        //}
    }
}