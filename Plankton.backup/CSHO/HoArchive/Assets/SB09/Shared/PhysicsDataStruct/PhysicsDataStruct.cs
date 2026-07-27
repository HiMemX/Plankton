
namespace SB09Assets{
    public class PhysicsDataStruct{
        public float mass;
        public float friction;
        public float elasticity;
        public float linearDamping;
        public float angularDamping;
        public float settleTimer;
        public byte magnetic_Charge;
        public byte pad1;
        public byte pad2;
        public byte pad3;
        public SoundSourcesPhysics SoundsPhysics;
        
        public PhysicsDataStruct(HoArchive.BinaryReaderEndian file, uint elementOffset){
            mass = file.ReadFloat32E();
            friction = file.ReadFloat32E();
            elasticity = file.ReadFloat32E();
            linearDamping = file.ReadFloat32E();
            angularDamping = file.ReadFloat32E();
            settleTimer = file.ReadFloat32E();
            magnetic_Charge = file.ReadByte();
            pad1 = file.ReadByte();
            pad2 = file.ReadByte();
            pad3 = file.ReadByte();
            SoundsPhysics = new SoundSourcesPhysics(file, elementOffset);
        }

        public void Save1(HoArchive.BinaryWriterEndian file){
            file.WriteE(mass);
            file.WriteE(friction);
            file.WriteE(elasticity);
            file.WriteE(linearDamping);
            file.WriteE(angularDamping);
            file.WriteE(settleTimer);
            file.WriteE(magnetic_Charge);
            file.WriteE(pad1);
            file.WriteE(pad2);
            file.WriteE(pad3);
            SoundsPhysics.Save1(file);
        }

        public void Save2(HoArchive.BinaryWriterEndian file){
            SoundsPhysics.Save2(file);
        }
    }
}