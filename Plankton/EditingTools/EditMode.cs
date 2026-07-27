using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.EditingTools
{
    public enum EditMode
    {
        NONE = 0,
        POSITION = 1,
        ROTATION = 2,
        SCALE = 3,

        AXIS_X = 4,
        AXIS_Y = 8,
        AXIS_Z = 16,
        AXIS_XY = 12,
        AXIS_XZ = 20,
        AXIS_YZ = 24,
        AXIS_ALL = 28,

        PIVOT_INDIVIDUAL_ORIGINS = 32,
        PIVOT_MEDIAN_POINT = 64,

        MASK_PIVOT = 0b1100000,
        MASK_AXIS  = 0b0011100,
        MASK_TYPE  = 0b0000011,
    }
}
