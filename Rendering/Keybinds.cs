using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Plankton
{
    public static class LevelEditorKeybinds
    {
        public static Keybinds binds { get; set; } = new Keybinds();

        private const string filename = "keybinds.json";

        public static void Load()
        {
            if (File.Exists(filename))
            {
                string json = File.ReadAllText(filename);
                binds = JsonSerializer.Deserialize<Keybinds>(json) ?? new Keybinds();
                Debug.debugWindow.AddEntry("LevelEditorKeybinds", binds.binds.Count().ToString());
            }
        }

        public static void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(binds, options);
            File.WriteAllText(filename, json);
        }

        public static Keys? Get(string tag)
        {
            foreach (Keybind bind in binds.binds)
            {
                if (bind.tag == tag) return bind.key;
            }

            return null;
        }
    }

    [Serializable]
    public class Keybinds
    {
        public List<Keybind> binds { get; set; } = new List<Keybind>();

    }

    [Serializable]
    public class KeybindsOld
    {
        public Keys forward { get; set; } = Keys.W;
        public Keys backward { get; set; } = Keys.S;
        public Keys left { get; set; } = Keys.A;
        public Keys right { get; set; } = Keys.D;
        public Keys up { get; set; } = Keys.E;
        public Keys down { get; set; } = Keys.Q;

        public Keys panLeft { get; set; } = Keys.J;
        public Keys panRight { get; set; } = Keys.L;
        public Keys panUp { get; set; } = Keys.I;
        public Keys panDown { get; set; } = Keys.K;

        public Keys speedUp { get; set; } = Keys.M;
        public Keys speedDown { get; set; } = Keys.N;

        public Keys focusOnObject { get; set; } = Keys.F;
        public Keys moveObject { get; set; } = Keys.G;
        public Keys rotateObject { get; set; } = Keys.R;
        public Keys scaleObject { get; set; } = Keys.B;

        public Keys duplicateObject { get; set; } = Keys.D | Keys.Shift;
    }

    [Serializable]
    public class Keybind
    {
        public string name { get; set; } // To be displayed, e.g. Rotate Object
        public string tag { get; set; } // internal lookup name, e.g. rotateObject
        public Keys key { get; set; }

        public Keybind()
        {
            name = "New Keybind";
            tag = "temp_tag";
            key = Keys.A;
        }

        public Keybind(string name, string tag, Keys key) {
            this.name = name;
            this.tag = tag;
            this.key = key;
        }
    }
}
