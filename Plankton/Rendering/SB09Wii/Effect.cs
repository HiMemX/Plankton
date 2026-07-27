using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using Plankton.Rendering.Base;
using SB09WiiAsset;

namespace Plankton.Rendering.SB09Wii
{
    internal class Effect : Rendering.Base.EffectBase
    {
        public SB09WiiAsset.Effect entity;

        public Effect(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool)
        {
            entity = (SB09WiiAsset.Effect)asset.entity;
        }

        public override ulong GetRenderModeID()
        {
            return entity.GetRenderModeID();
        }

        public override ulong GetShaderID()
        {
            foreach(Technique tech in entity.techniques)
            {
                if (tech.name.str != "") continue;

                int lodindex = tech.stages.element3.lodIndex;

                int featureindex = entity.lods[lodindex].featureIndex;

                int passindex = entity.features[featureindex].passIndex;
                int passcount = entity.features[featureindex].passCount;

                int shaderindex = entity.passes[passindex + passcount - 1].shaderIndex;

                return entity.shaders[shaderindex].shaderID;
            }

            return entity.GetShaderID(); // Old algo, fallback
        }

        public override void Init()
        {
            
        }

        public override void UpdateAssociates()
        {
            resourcePool.DoActionOnPool(resourcePool.aMaterialPool, (entry) => {
                if (((MaterialBase)entry).GetEffectID() == asset.uidSelf) { entry.UpdateAssociates(); }
            });
        }
    }
}
