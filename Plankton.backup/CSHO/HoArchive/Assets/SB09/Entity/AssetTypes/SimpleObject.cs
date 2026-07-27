
namespace SB09Assets{
    public class SimpleObject : EntityAsset{
        public ulong Destructible;
        public float AnimationSpeed;
        public uint InitialState; // ??
        public byte Collision; // HAVOK_COLLISION_ENUMERATOR
        public byte CameraCollisionOff;
        public byte GoTransparent;
        public byte SimpleFlags;
        public uint ForceUpdateDistance; // ??
        public uint ForceUpdateDistanceValue; // ??
        public uint orientRef; // ??
        public LinkAsset EventLinksNew;

        public SimpleObject(HoArchive.BinaryReaderEndian file, uint elementOffset, uint blobSize) : base(file, elementOffset){
            Destructible = file.ReadUInt64E();
            AnimationSpeed = file.ReadFloat32E();
            InitialState = file.ReadUInt32E();
            Collision = file.ReadByte();
            CameraCollisionOff = file.ReadByte();
            GoTransparent = file.ReadByte();
            SimpleFlags = file.ReadByte();
            ForceUpdateDistance = file.ReadUInt32E();
            ForceUpdateDistanceValue = file.ReadUInt32E();
            orientRef = file.ReadUInt32E();
            EventLinksNew = new LinkAsset(file, elementOffset, blobSize);
        }

        public override void Save(HoArchive.BinaryWriterEndian file){
            base.Save1(file);
            file.WriteE(Destructible);
            file.WriteE(AnimationSpeed);
            file.WriteE(InitialState);
            file.WriteE(Collision);
            file.WriteE(CameraCollisionOff);
            file.WriteE(GoTransparent);
            file.WriteE(SimpleFlags);
            file.WriteE(ForceUpdateDistance);
            file.WriteE(ForceUpdateDistanceValue);
            file.WriteE(orientRef);
            EventLinksNew.Save1(file);
            base.Save2(file);
            EventLinksNew.Save2(file);
        }
    }
}