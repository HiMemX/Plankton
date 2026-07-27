using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;


namespace Plankton.Rendering.Base
{
    internal abstract class TextureBase : Base.BaseClass
    {
        public TextureBase(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool) { }

        public GenericBuffer buffer = new();
        public bool hasAlpha = false;

        public abstract ulong GetTextureBufferID();
    }
}
