using System.Collections.Generic;

namespace HoArchive{
    public class Parcel : ParcelBase{
        public List<ParcelTOC> ParcelTOCs = new List<ParcelTOC>();

        public Parcel(){}

        public Parcel(BinaryReaderEndian file, ParcelSliceMeta SliceMeta, string target){
            uint baseposition = (uint)file.BaseStream.Position;
            uint DataPtr;
            uint MetaPtr;
            for (int i=0; i<(SliceMeta.Header.numSlices-1)/3; i++){
                DataPtr = SliceMeta.EntriesParcelData[i].sliceStart + baseposition;
                MetaPtr = SliceMeta.EntriesParcelTOC[i].sliceStart  + baseposition;

                file.BaseStream.Position = MetaPtr;
                ParcelTOCs.Add(new ParcelTOC(file, DataPtr, target));



            }
        }
        public void Update(uint Align, uint sectorSize, bool Reversed){
            foreach (ParcelTOC toc in ParcelTOCs){
                toc.Update(Align); // If Table offsets break, change this
            }
            uint DataPtr = 0;
            if (Reversed){
                foreach (ParcelTOC toc in ParcelTOCs){
                    DataPtr += MathTools.RoundUpTo(toc.Header.elementCount * 0x20 + 0x20, Align);
                }
                DataPtr = MathTools.RoundUpTo(DataPtr, sectorSize);
            }
            foreach (ParcelTOC toc in ParcelTOCs){
                foreach (TOCEntry entry in toc.Entries){
                    entry.elementOffset = DataPtr;
                    DataPtr += entry.elementSize;
                }
            }
        }
        public void updateSectors(uint sectorSize, uint startSector){}

        public uint getSize(List<SliceMeta> meta){
            uint size = 0;
            foreach (ParcelSliceMetaEntry entry in ((ParcelSliceMeta)meta[0]).EntriesParcelTOC){
                size += entry.sliceSize;
            }
            foreach (ParcelSliceMetaEntry entry in ((ParcelSliceMeta)meta[0]).EntriesParcelData){
                size += entry.sliceSize;
            }
            foreach (ParcelSliceMetaEntry entry in ((ParcelSliceMeta)meta[0]).EntriesPadding){
                size += entry.sliceSize;
            }
            return size;
        }

        public uint getTotalSize(List<SliceMeta> meta, uint sectorSize){
            uint size = MathTools.RoundUpTo(getSize(meta), sectorSize);
            return size;
        }

        public void Save(BinaryWriterEndian file, uint sectorSize, List<SliceMeta> metas){// uint memoryAlignment){
            // First write 0x33 buffer to file large enough to fit everything (Order doesn't matter cause 3+1 = 1+3 :D)
            long baseposition = file.BaseStream.Position;
            foreach (ParcelSliceMetaEntry entry in ((ParcelSliceMeta)metas[0]).EntriesParcelData){
                file.Pad(entry.sliceSize, 0x33);
            }
            foreach (ParcelSliceMetaEntry entry in ((ParcelSliceMeta)metas[0]).EntriesParcelTOC){
                file.Pad(entry.sliceSize, 0x33);
            }
            foreach (ParcelSliceMetaEntry entry in ((ParcelSliceMeta)metas[0]).EntriesPadding){
                file.Pad(entry.sliceSize, 0x33);
            }
            file.PadAlign(sectorSize, 0x33);
            long endposition = file.BaseStream.Position;

            // Now write actual data
            for (int slice=0; slice<((ParcelSliceMeta)metas[0]).EntriesParcelData.Count; slice++){
                file.BaseStream.Position = baseposition + ((ParcelSliceMeta)metas[0]).EntriesParcelData[slice].sliceStart;
                ParcelTOCs[slice].SaveData(file);
            }
            for (int slice=0; slice<((ParcelSliceMeta)metas[0]).EntriesParcelTOC.Count; slice++){
                file.BaseStream.Position = baseposition + ((ParcelSliceMeta)metas[0]).EntriesParcelTOC[slice].sliceStart;
                ParcelTOCs[slice].Header.Save(file);
                ParcelTOCs[slice].SaveMeta(file);
            }
            file.BaseStream.Position = endposition;
        }
    }
}