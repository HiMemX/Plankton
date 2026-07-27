using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using Plankton.Rendering.Base;

namespace Plankton.Rendering.SB09Wii
{
    internal class Material : Rendering.Base.MaterialBase
    {
        public SB09WiiAsset.Material entity;

        public Material(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool)
        {
            entity = (SB09WiiAsset.Material)asset.entity;
        }

        public override void Update(TOCEntry asset = null)
        {
            base.Update(asset);
            UpdateAssociates();
        }

        public override ulong GetEffectID()
        {
            return entity.GetEffectID();
        }

        public override ulong GetRenderModeID()
        {
            return entity.GetRenderModeID();
        }

        public override List<ulong> GetTextureIDs()
        {
            return entity.GetTextureIDs();
        }

        public override void Init()
        {
            
        }

        public override void UpdateAssociates()
        {
            resourcePool.DoActionOnPool(resourcePool.aGeometryPool, (entry) => {
                if (asset.uidSelf == ((GeometryBase)entry).GetMaterialID()) { entry.Init(); } // If geometry asset has this material, init it again
            });
        }
    }
}
