using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.Rendering
{
    public class ChannelInfo
    { // Most important information
        public int vtype;
        public int vindex;
        public int vfrac;
        public int voffset;
        public int vstride;

        public int GetTupleCount()
        {
            switch (vtype)
            {
                case 1:
                    return 3;
                case 3:
                    return 2;
                case 4:
                    return 3;
                case 5:
                    return 4;
                default:
                    return 0;
            }
        }

        public ChannelInfo(SB09WiiAsset.ChannelInfo info)
        {
            
            this.vfrac = info.vfrac;
            this.vindex = info.vindex;
            this.voffset = info.voffset;
            this.vstride = info.vstride;
            this.vtype = info.vtype;

        }
    }
}
