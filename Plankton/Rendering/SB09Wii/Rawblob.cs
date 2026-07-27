using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using Plankton.Rendering.Base;

namespace Plankton.Rendering.SB09Wii
{
    internal class Rawblob : RawblobBase
    {
        public Rawblob(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool)
        {
        }

        public override void Update(TOCEntry asset = null)
        {
            base.Update(asset);
            UpdateAssociates();
        }

        public override void Init()
        {
            
        }

        public override void UpdateAssociates()
        {
            resourcePool.DoActionOnPool(resourcePool.aTexturePool, (entry) => {
                if (asset.uidSelf == ((TextureBase)entry).GetTextureBufferID()) { entry.Init(); } // If texture asset has this id, init it again
            });

            // If it's a vertex buffer blob
            resourcePool.DoActionOnPool(resourcePool.aGeometryPool, (entry) => {
                if (((GeometryBase)entry).GetBufferIDs().Contains(asset.uidSelf)) { entry.Init(); } // If geometry has this buffer, init it again
            });

            // If it's an index buffer blob
            resourcePool.DoActionOnPool(resourcePool.aGeometryPool, (entry) => {
                if (((GeometryBase)entry).GetIndexBufferID() == asset.uidSelf) { entry.Init(); } // If geometry has this buffer, init it again
            });
        }
    }
}
