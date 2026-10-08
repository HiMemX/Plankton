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
using Rendering.Rendering;

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

        [DisplayName("Default Render Settings")]
        public RenderSettings defaultRenderSettings { get; set; } = new();

    }

    
}
