using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB09WiiAsset;
using static Assimp.Metadata;

namespace Plankton.Rendering
{
    public class MaterialSettings
    {
        public int diffuseMapIndex;
        public int lightMapIndex;
        public int environmentMapIndex;
        public int blendMapIndex;
        public int diffuseMap1Index;

        public int ambientScaleIndex;
        public int environmentScaleIndex;

        public MaterialSettings() { }

        public MaterialSettings(Wii_MaterialSettings mat)
        {
            diffuseMapIndex = mat.diffuseMapParamIndex;
            lightMapIndex = mat.lightMapParamIndex;
            diffuseMap1Index = mat.diffuseMap1ParamIndex;
            ambientScaleIndex = mat.ambientScaleParamIndex;
            blendMapIndex = mat.blendMapParamIndex;
            environmentMapIndex = mat.envMapParamIndex;
            environmentScaleIndex = mat.envMapScaleParamIndex;
        }
    }
}
