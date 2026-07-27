using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB09WiiAsset;

namespace Plankton.Special_Editors.LinkAssetEditor
{
    public class SB09WiiLinkAsset : GlobalLinkAsset
    {
        LinkAsset linkasset;

        public SB09WiiLinkAsset(LinkAsset linkasset) : base() {
            this.linkasset = linkasset;
        }

        public override List<GlobalLink> GetLinks()
        {
            List<GlobalLink> links = new();




            return links;
        }
        

    }
}
