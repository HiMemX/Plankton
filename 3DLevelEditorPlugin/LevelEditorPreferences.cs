using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rendering;
using Keybinds;

namespace _3DLevelEditorPlugin
{
    [Serializable]
    public class LevelEditorPreferences
    {
        [DisplayName("Base editor preferences")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Preferences renderingPreferences { get; set; } = new();


        [DisplayName("Focus on Object")]
        [TypeConverter(typeof(KeybindTypeConverter))]
        [Editor(typeof(KeybindEditor), typeof(UITypeEditor))]
        public Keybind focusOnObject { get; set; } = new Keybind(Keys.Decimal);
        public Keybind moveObject { get; set; } = new Keybind(Keys.G);
        public Keybind rotateObject { get; set; } = new Keybind(Keys.R);
        public Keybind scaleObject { get; set; } = new Keybind(Keys.B);

        public Keybind duplicateObject { get; set; } = new Keybind(Keys.D, Keys.Shift);
        public Keybind cameraPreview { get; set; } = new Keybind(Keys.NumPad0);
    }
}
