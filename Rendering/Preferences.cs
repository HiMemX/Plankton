using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Keybinds;
using OpenTK.Mathematics;

namespace Rendering
{
    [Serializable]
    public class Preferences
    {
        [Category("Camera")]
        [DisplayName("Move forward")]
        public Keybind forward { get; set; } = new Keybind(Keys.W);

        [DisplayName("Move backward")]
        public Keybind backward { get; set; } = new Keybind(Keys.S);

        [DisplayName("Move left")]
        public Keybind left { get; set; } = new Keybind(Keys.A);

        [DisplayName("Move right")]
        public Keybind right { get; set; } = new Keybind(Keys.D);

        [DisplayName("Move up")]
        public Keybind up { get; set; } = new Keybind(Keys.E);

        [DisplayName("Move down")]
        public Keybind down { get; set; } = new Keybind(Keys.Q);

        [DisplayName("Pan left")]
        public Keybind panLeft { get; set; } = new Keybind(Keys.J);

        [DisplayName("Pan right")]
        public Keybind panRight { get; set; } = new Keybind(Keys.L);

        [DisplayName("Pan up")]
        public Keybind panUp { get; set; } = new Keybind(Keys.I);

        [DisplayName("Pan down")]
        public Keybind panDown { get; set; } = new Keybind(Keys.K);

        public Keybind speedUp { get; set; } = new Keybind(Keys.M);
        public Keybind speedDown { get; set; } = new Keybind(Keys.N);

        [Category("Settings")]
        [DisplayName("Background Color")]
        [Editor(typeof(Color4Editor), typeof(UITypeEditor))]
        public Color4 backgroundColor { get; set; } = new Color4(0, 0, 0, 0);//new Color4(0.2f, 0.2f, 0.2f, 1f); // TEMP, absorb into user preferences
        [DisplayName("Render Fog")]
        public bool renderFog { get; set; } = true;
        [DisplayName("Alpha Transparency")]
        public bool alphaTransparency { get; set; } = true;

    }

    
}
