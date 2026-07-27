using System.Collections.Generic;

namespace HoArchive{
    public class ParcelSliceMeta : SliceMeta{
        public ParcelSliceMetaHeader Header;
        public List<ParcelSliceMetaEntry> EntriesParcelTOC  = new List<ParcelSliceMetaEntry>();
        public List<ParcelSliceMetaEntry> EntriesParcelData = new List<ParcelSliceMetaEntry>();
        public List<ParcelSliceMetaEntry> EntriesPadding    = new List<ParcelSliceMetaEntry>();

        public bool Reversed;

        public ParcelSliceMeta(bool Reversed_in = false){
            Header = new ParcelSliceMetaHeader();
            Reversed = Reversed_in;
        }

        public ParcelSliceMeta(BinaryReaderEndian file){
            Header = new ParcelSliceMetaHeader(file);
            uint curroffset = 0;

            for (int entry=0; entry<Header.numSlices; entry++){
                ParcelSliceMetaEntry Entry = new ParcelSliceMetaEntry(file);
                Entry.sliceStart = curroffset;
                curroffset += Entry.sliceSize;
                switch(Entry.sliceType){
                    case 0:
                        EntriesParcelTOC.Add(Entry);
                        break;
                    case 1:
                        EntriesParcelData.Add(Entry);
                        break;
                    case 2:
                        EntriesPadding.Add(Entry);
                        break;
                }
            }

            Reversed = EntriesParcelTOC[0].sliceStart < EntriesParcelData[0].sliceStart;
        }
        public void Update(ParcelBase parcel, uint memoryAlignment, uint sectorSize){
            EntriesParcelTOC  = new List<ParcelSliceMetaEntry>();
            EntriesParcelData = new List<ParcelSliceMetaEntry>();
            EntriesPadding    = new List<ParcelSliceMetaEntry>();

            ParcelSliceMetaEntry newpad;
            ParcelSliceMetaEntry newdata;
            ParcelSliceMetaEntry newtoc;

            uint curroffset = 0;
            uint entrystart;
            uint entrysize;
            uint entryalign = 0xFFFFFFFF;
            
            uint totalsize;

            //Padding And Data (Also if else cause I can't be bothered doing it otherwise)
            if (Reversed == false){
                foreach(ParcelTOC parceltoc in ((Parcel)parcel).ParcelTOCs){
                    entrystart = curroffset;
                    totalsize = 0;
                    foreach (TOCEntry tocentry in parceltoc.Entries){
                        totalsize += tocentry.elementSize;
                    }
                    entrysize = MathTools.RoundUpTo(totalsize, memoryAlignment);
                    curroffset += entrysize;
                    newpad  = new ParcelSliceMetaEntry(2, entrystart, 0, entryalign);
                    newdata = new ParcelSliceMetaEntry(1, entrystart, entrysize, entryalign);

                    EntriesPadding.Add(newpad);
                    EntriesParcelData.Add(newdata);
                }
            }
            else{
                foreach (ParcelTOC parceltoc in ((Parcel)parcel).ParcelTOCs){
                    entrystart = curroffset;
                    entrysize = MathTools.RoundUpTo((uint)parceltoc.Entries.Count * 0x20 + 0x20, memoryAlignment);
                    curroffset += entrysize;
                    newtoc = new ParcelSliceMetaEntry(0, entrystart, entrysize, entryalign);

                    EntriesParcelTOC.Add(newtoc);
                }
            }

            // Middle Padding
            entrystart = curroffset;
            entrysize = MathTools.RoundUpTo(curroffset, sectorSize) - curroffset;
            curroffset = MathTools.RoundUpTo(curroffset, sectorSize);
            newpad = new ParcelSliceMetaEntry(2, entrystart, entrysize, entryalign);

            EntriesPadding.Add(newpad);

            // TOCMetaEntries
            if (Reversed == false){
                foreach (ParcelTOC parceltoc in ((Parcel)parcel).ParcelTOCs){
                    entrystart = curroffset;
                    entrysize = MathTools.RoundUpTo((uint)parceltoc.Entries.Count * 0x20 + 0x20, memoryAlignment);
                    curroffset += entrysize;
                    newtoc = new ParcelSliceMetaEntry(0, entrystart, entrysize, entryalign);

                    EntriesParcelTOC.Add(newtoc);
                }
            }
            else{
                foreach(ParcelTOC parceltoc in ((Parcel)parcel).ParcelTOCs){
                    entrystart = curroffset;
                    totalsize = 0;
                    foreach (TOCEntry tocentry in parceltoc.Entries){
                        totalsize += tocentry.elementSize;
                    }
                    entrysize = MathTools.RoundUpTo(totalsize, memoryAlignment);
                    curroffset += entrysize;
                    newpad  = new ParcelSliceMetaEntry(2, entrystart, 0, entryalign);
                    newdata = new ParcelSliceMetaEntry(1, entrystart, entrysize, entryalign);

                    EntriesPadding.Add(newpad);
                    EntriesParcelData.Add(newdata);
                }
            }

            Header.Update(EntriesParcelTOC, EntriesParcelData, EntriesPadding);
        }

        public void Save(BinaryWriterEndian file){
            Header.Save(file);

            if (Reversed){
                for (int i=0; i<(Header.numSlices-1)/3; i++){
                    EntriesParcelTOC[i].Save(file);
                }   
                EntriesPadding[0].Save(file);
            }
            else{
                for (int i=0; i<(Header.numSlices-1)/3; i++){
                    EntriesPadding[i].Save(file);
                    EntriesParcelData[i].Save(file);
                }
                EntriesPadding[EntriesPadding.Count-1].Save(file);
            }
            

            if (Reversed){
                for (int i=0; i<(Header.numSlices-1)/3; i++){
                    EntriesPadding[i+1].Save(file);
                    EntriesParcelData[i].Save(file);
                }
            }
            else{
                for (int i=0; i<(Header.numSlices-1)/3; i++){
                    EntriesParcelTOC[i].Save(file);
                }
            }
        }
    }
}