using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using SB09WiiAsset;
using OpenTK.Mathematics;

namespace Plankton.Rendering
{
    public struct PrimitiveInstance
    {
        public Matrix4x4 matrix;
        public Color4 color;
        public int containerIndex; // Used for the selection buffer pass
        public enumInstanceFlags instanceFlags; // These are the same instance flags the inbuilt geometry uses
        public enumDrawStageMask drawMaskUnselected;
        public enumDrawStageMask drawMaskSelected;
        

        public PrimitiveInstance(Matrix4x4 matrix, Color4 color, int containerIndex, enumInstanceFlags instanceFlags, enumDrawStageMask drawMaskUnselected, enumDrawStageMask drawMaskSelected) {
            this.matrix = matrix;
            this.color = color;
            this.containerIndex = containerIndex;
            this.instanceFlags = instanceFlags;
            this.drawMaskSelected = drawMaskSelected;
            this.drawMaskUnselected = drawMaskUnselected;
        }

        public PrimitiveInstance Clone(Matrix4x4 matrix)
        {
            return new PrimitiveInstance(matrix, color, containerIndex, instanceFlags, drawMaskUnselected, drawMaskSelected);
        }

        public PrimitiveInstance Clone()
        {
            return new PrimitiveInstance(matrix, color, containerIndex, instanceFlags, drawMaskUnselected, drawMaskSelected);
        }

        public PrimitiveInstance Clone(Matrix4x4 matrix, enumDrawStageMask drawMaskUnselected, enumDrawStageMask drawMaskSelected)
        {
            return new PrimitiveInstance(matrix, color, containerIndex, instanceFlags, drawMaskUnselected, drawMaskSelected);
        }
    }

    public enum enumDrawStageMask
    {
        DRAW_REGULAR = 1, // Wether or not primitiveinstance draws regularly on screen
        DRAW_SELECTION = 2, // Wether or not primitiveinstance draws into the selection buffer
        DRAW_HIGHLIGHT = 4, // Wether or not priminstance effects highlighting
    
        DRAW_ALL = 1 + 2 + 4,
        DRAW_NONE = 0
    }
    // Example:
    // Triggerbox (Wireframe unselected, can only select wireframe ever, when selected is box)
    // wireframe prim, unselected: DRAW_REGULAR | DRAW_SELECTION | DRAW_HIGHLIGHT
}
