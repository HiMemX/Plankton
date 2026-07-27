using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.Rendering
{
    public class TextureSet
    {
        public GenericBuffer diffuseMap = new();
        public GenericBuffer lightMap = new();
        public GenericBuffer environmentMap = new();
        public GenericBuffer blendMap = new();
        public GenericBuffer diffuseMap1 = new();

        public ulong diffuseMapID = 0;
        public ulong lightMapID = 0;
        public ulong environmentMapID = 0;
        public ulong blendMapID = 0;
        public ulong diffuseMap1ID = 0;
    }
}
