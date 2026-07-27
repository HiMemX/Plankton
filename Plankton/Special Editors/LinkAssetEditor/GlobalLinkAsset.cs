using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.Special_Editors.LinkAssetEditor
{
    public abstract class GlobalLinkAsset
    {
        ///
        /// 
        ///

        public List<GlobalLink> events;

        public GlobalLinkAsset() {
            this.events = new List<GlobalLink>();
        }

        public abstract List<GlobalLink> GetLinks();


    }
}
