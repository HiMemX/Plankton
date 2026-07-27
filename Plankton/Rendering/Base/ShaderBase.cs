using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;

namespace Plankton.Rendering.Base
{
    internal abstract class ShaderBase : BaseClass
    {
        public ShaderBase(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool) { }
        public abstract ShaderInfo GetShaderInfo();
    }
}
