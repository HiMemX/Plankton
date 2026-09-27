using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Plankton.Rendering
{
    public struct InstanceInfo
    {
        public Matrix4x4 matrix;
        public uint flags;

        public uint parentAttr;
        public uint childAttr;

        public int lightkitIndex;


        public InstanceInfo Clone()
        {
            InstanceInfo info = new InstanceInfo();
            info.matrix = matrix;
            info.flags = flags;
            info.parentAttr = parentAttr;
            info.childAttr = childAttr;
            info.lightkitIndex = lightkitIndex;

            return info;
        }
    }
}
