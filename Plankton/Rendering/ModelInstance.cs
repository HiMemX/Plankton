using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Plankton.Rendering
{
    public struct ModelInstance
    {
        public ulong modelPrototypeID;
        public ulong lightKitID;
        public Matrix4x4 matrix;
        
        public ModelInstance(ulong modelPrototypeID, ulong lightKitID, Matrix4x4 matrix) {
            this.modelPrototypeID = modelPrototypeID;
            this.lightKitID = lightKitID;
            this.matrix = matrix;
        }

        public ModelInstance(SB09WiiAsset.ModelInstanceAsset sb09wiiinstance, Matrix4x4 matrix)
        {
            modelPrototypeID = sb09wiiinstance.modelPrototypeID;
            lightKitID = sb09wiiinstance.lightKitID;
            this.matrix = matrix;
        }
    }
}
