using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;

namespace Plankton.Rendering.Base
{
    internal abstract class RawblobBase : BaseClass
    {        
        public RawblobBase(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool) { }

    }
}
