using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.Rendering.Shaders
{
    public class ShaderCollection
    {
        public Shader regular;
        public Shader highlight;
        public Shader selection;

        List<Shader> allshaders;

        public ShaderCollection(string basepath)
        {
            regular   = new Shader(basepath + "regular.vert",   basepath + "regular.frag");
            selection = new Shader(basepath + "selection.vert", basepath + "selection.frag");
            highlight = new Shader(basepath + "highlight.vert", basepath + "highlight.frag");

            allshaders = new List<Shader>() { regular, selection, highlight };
        }

    }
}
