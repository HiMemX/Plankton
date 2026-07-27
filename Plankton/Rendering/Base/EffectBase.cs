using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;

namespace Plankton.Rendering.Base
{
    internal abstract class EffectBase : BaseClass
    {
        public EffectBase(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool) { }
        public abstract ulong GetShaderID();
        public abstract ulong GetRenderModeID();
    }
}
