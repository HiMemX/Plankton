using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plankton.Core.CustomUIEditors;

namespace Plankton.Core
{
    [Serializable]
    public class CorePreferences
    {
        [Category("Startup")]
        [DisplayName("Editors to open on startup")]
        [Editor(typeof(StartupEditorsEditor), typeof(UITypeEditor))]
        public List<string> StartupEditors { get; set; } = new();
    }
}
