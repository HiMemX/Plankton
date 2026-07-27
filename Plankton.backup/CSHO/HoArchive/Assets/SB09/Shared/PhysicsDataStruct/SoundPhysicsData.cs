namespace SB09Assets{
    public class SoundPhysicsData{
        public float MinVelocity;
        public float MaxVelocity;
        public float MinRun;
        public float MaxRun;
        public float MaxRise;

        public SoundPhysicsData(HoArchive.BinaryReaderEndian file){
            MinVelocity = file.ReadFloat32E();
            MaxVelocity = file.ReadFloat32E();
            MinRun = file.ReadFloat32E();
            MaxRun = file.ReadFloat32E();
            MaxRise = file.ReadFloat32E();
        }

        public void Save(HoArchive.BinaryWriterEndian file){
            file.WriteE(MinVelocity);
            file.WriteE(MaxVelocity);
            file.WriteE(MinRun);
            file.WriteE(MaxRun);
            file.WriteE(MaxRise);
        }
    }
}