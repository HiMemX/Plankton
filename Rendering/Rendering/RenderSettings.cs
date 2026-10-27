using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Rendering.Rendering
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RenderSettings
    {
        [DisplayName("Background Color")]
        [Editor(typeof(Color4Editor), typeof(UITypeEditor))]
        public Color4 backgroundColor { get; set; } = new Color4(0, 0, 0, 255);

        [DisplayName("Highlight Color")]
        [Editor(typeof(Color4Editor), typeof(UITypeEditor))]
        public Color4 highlightColor { get; set; } = new Color4(0.92941177f, 0.34117648f, 0, 1);

        [DisplayName("Active Color")]
        [Editor(typeof(Color4Editor), typeof(UITypeEditor))]
        public Color4 activeColor { get; set; } = new Color4(1f, 0.627451f, 0.15686275f, 1);

        [DisplayName("Alpha Transparency")]
        public bool alphaTransparency { get; set; } = true;
        [DisplayName("Render Fog")]
        public bool renderFog { get; set; } = true;

        private float _fov = 70f;
        [DisplayName("Field of View")]
        public float fov { get => _fov; set { _fov = Math.Max(Math.Min(value, 179.99f), 0.01f); } }

        [DisplayName("Show grid")]
        public bool showGrid { get; set; } = true;
    }
}
