using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.Special_Editors.Level_Editor
{
    internal enum ViewMode
    {
        All = 0b11111111, // Temporarily no vertex colors
        DiffuseOnly     = 0b10011110,
        LightMapOnly    = 0b10011101,
        VertexColorOnly = 0b10111100,
        NormalOnly      = 0b11011100
    }
}
