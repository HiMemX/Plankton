using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB09WiiAsset;

namespace Plankton.Special_Editors.LinkAssetEditor
{
    public class GlobalLink
    {
        public Event sourceEvent;
        public Event targetEvent;

        public ulong destinationAssetID;
        public ulong checkAssetID;
        public bool checkSourceParams;
        public bool disabled;
        public uint checkSourceMask;

        public GlobalLink(Event sourceEvent, Event targetEvent, ulong destinationAssetID, ulong checkAssetID, bool checkSourceParams, bool disabled, uint checkSourceMask)
        {   
            this.sourceEvent = sourceEvent;
            this.targetEvent = targetEvent;
            this.destinationAssetID = destinationAssetID;
            this.checkAssetID = checkAssetID;
            this.checkSourceParams = checkSourceParams;
            this.disabled = disabled;
            this.checkSourceMask = checkSourceMask;
            
        }
    }
}
