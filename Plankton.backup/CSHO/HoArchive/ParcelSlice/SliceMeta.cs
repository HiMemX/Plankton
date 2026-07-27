
namespace HoArchive{
    public interface SliceMeta{
        public void Update(ParcelBase parcel, uint memoryAlignment, uint sectorSize);
        public void Save(BinaryWriterEndian file);
    }
}