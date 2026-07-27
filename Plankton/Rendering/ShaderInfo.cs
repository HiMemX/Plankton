using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Plankton.Rendering
{
    public class ShaderInfo
    {
        // Buffer 
        public ChannelInfo position;
        public ChannelInfo normal;
        public ChannelInfo color;
        public ChannelInfo[] uvs = new ChannelInfo[3];


        public MaterialSettings materialSettings = new();
        public MaterialSettings rendParamSettings = new();




        public ChannelInfo GetChannelInfo(int bufferindex)
        {
            if (bufferindex == position.vindex) { return position; }
            if (bufferindex == normal.vindex) { return normal; }
            if (bufferindex == color.vindex) { return color; }
            for (int i = 0; i < 3; i++)
            {
                if (bufferindex == uvs[i].vindex) { return uvs[i]; }
            }

            return null;
        }

        public List<ChannelInfo> GetChannelInfos()
        {
            return new List<ChannelInfo>() { position, normal, color, uvs[0], uvs[1], uvs[2] };
        }

        public List<ChannelInfo> GetUsedChannelInfos()
        {

            List<ChannelInfo> temp = GetChannelInfos();
            List<ChannelInfo> output = new();

            foreach (ChannelInfo c in temp)
            {
                if (c.vtype != 255) { output.Add(c); }
            }

            return output;
        }
    }
}
