using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;

namespace Plankton.Rendering.Base
{
    internal abstract class MaterialBase : BaseClass
    {
        public MaterialBase(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool) { }
        public abstract List<ulong> GetTextureIDs();
        public abstract ulong GetEffectID();
        public abstract ulong GetRenderModeID();
    }
}
