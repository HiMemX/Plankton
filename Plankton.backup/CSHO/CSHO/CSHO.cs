using System;

namespace CSHO{
    public partial class Handler{
        public string path = "";
        public bool endian;

        public HoArchive.Archive Archive;
        Random RNG = new Random();
    }
}