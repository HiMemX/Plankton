using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.Special_Editors.Level_Editor
{
    internal enum enumGeometryStatus
    {
        SUCCESS = 0,
        ERR_BUFFER_MISMATCH = 1,
        ERR_BUFFER_NOT_FOUND = 2,
        ERR_MATERIAL_NOT_FOUND = 3,
        ERR_TEXTURE_NOT_FOUND = 4,
        ERR_EFFECT_NOT_FOUND = 5,
        ERR_SHADER_NOT_FOUND = 6,
    }
}
