using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;

namespace Plankton.Rendering
{
    public static class GlobalRenderSettings
    {
        // Just a place to store globals like primitive colors and such. Perhaps temporary

        public static Color4 highlightColor = Color4.Gold;

        public static Color4 triggerBoxColor = new Color4(0, 100, 255, 100);
        public static Color4 triggerSphereColor = new Color4(0, 255, 255, 100);
        public static Color4 cameraColor = Color4.Black;
        public static Color4 soundFxColor = Color4.Cyan;
        public static Color4 curveColor = Color4.Black;
        public static Color4 directionColor = Color4.Lime;

        public static int curveRenderResolution = 10;
    }
}
