
namespace SB09Assets{
    public class SoundSourcesPhysics{
        public SoundBankSource SoundHit;
        public SoundBankSource SoundScrape;
        public SoundBankSource SoundRoll;
        public SoundPhysicsData PhysicsData;

        public SoundSourcesPhysics(HoArchive.BinaryReaderEndian file, uint elementOffset){
            SoundHit = new SoundBankSource(file, elementOffset);
            SoundScrape = new SoundBankSource(file, elementOffset);
            SoundRoll = new SoundBankSource(file, elementOffset);
            PhysicsData = new SoundPhysicsData(file);
        }

        public void Save1(HoArchive.BinaryWriterEndian file){
            SoundHit.Save1(file);
            SoundScrape.Save1(file);
            SoundRoll.Save1(file);
            PhysicsData.Save(file);
        }

        public void Save2(HoArchive.BinaryWriterEndian file){
            SoundHit.Save2(file);
            SoundScrape.Save2(file);
            SoundRoll.Save2(file);
        }
    }
}