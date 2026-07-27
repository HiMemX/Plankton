
namespace HoArchive{
    public class ParcelDebugSliceMeta : SliceMeta{
        public ParcelDebugSliceMetaHeader Header;
        public ParcelDebugSliceMetaEntry Entry;

        
        public ParcelDebugSliceMeta(){
            Header = new ParcelDebugSliceMetaHeader();
            Entry  = new ParcelDebugSliceMetaEntry();    
        }

        public ParcelDebugSliceMeta(BinaryReaderEndian file){
            Header = new ParcelDebugSliceMetaHeader(file);
            Entry  = new ParcelDebugSliceMetaEntry(file);    
        }

        public void Update(ParcelBase parcel, uint memoryAlignment, uint sectorSize){
            Entry.Update(parcel);
            Header.Update();
        }

        public void Save(BinaryWriterEndian file){
            Header.Save(file);
            Entry.Save(file);
        }
    }
}