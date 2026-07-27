using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plankton.Custom_Controls;

namespace Plankton
{
    internal static class UserConfigurationApplicator
    {
        public static void Apply(UserConfiguration config, GeometryBaseRenderer renderer)
        {
            RenderingConfiguration rendconfig = UserConfigurationManager.userConfig.renderingConfiguration;
            renderer.alphaTransparency = rendconfig.alphaTransparency;
            System.Drawing.Color bg = rendconfig.backgroundColor;
            renderer.backgroundColor = new OpenTK.Graphics.Color4(bg.R, bg.G, bg.B, bg.A);
            renderer.movementSpeed = rendconfig.movementSpeed;
            renderer.rotationSpeed = rendconfig.rotationSpeed;
            renderer.renderFog = rendconfig.enableFog;
        }
    }
}
