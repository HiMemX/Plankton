using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using SB09WiiAsset;

namespace Plankton.Rendering
{
    public struct PrimitiveInstance
    {
        public Matrix4x4 matrix;
        public Color4 color;
        public int containerIndex; // Used for the selection buffer pass
        public enumInstanceFlags instanceFlags; // These are the same instance flags the inbuilt geometry uses
        

        public PrimitiveInstance(Matrix4x4 matrix, Color4 color, int containerIndex, enumInstanceFlags instanceFlags) {
            this.matrix = matrix;
            this.color = color;
            this.containerIndex = containerIndex;
            this.instanceFlags = instanceFlags;
        }

        public PrimitiveInstance Clone(Matrix4x4 matrix)
        {
            return new PrimitiveInstance(matrix, color, containerIndex, instanceFlags);
        }
    }

    
}
