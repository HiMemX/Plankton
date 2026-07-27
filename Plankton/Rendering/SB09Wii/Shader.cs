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
    internal class Shader : Base.ShaderBase
    {
        public SB09WiiAsset.GenericShader entity;

        public Shader(TOCEntry asset, ResourcePool resourcePool) : base(asset, resourcePool)
        {
            this.entity = (SB09WiiAsset.GenericShader)asset.entity;
        }


        public override ShaderInfo GetShaderInfo()
        {
            ShaderInfo output = new ShaderInfo();


            output.position = new ChannelInfo(entity.geomOps.geometryStateOp.pos);
            output.normal = new ChannelInfo(entity.geomOps.geometryStateOp.norm);
            output.color = new ChannelInfo(entity.geomOps.geometryStateOp.color);
            output.uvs[0] = new ChannelInfo(entity.geomOps.geometryStateOp.uv[0]);
            output.uvs[1] = new ChannelInfo(entity.geomOps.geometryStateOp.uv[1]);
            output.uvs[2] = new ChannelInfo(entity.geomOps.geometryStateOp.uv[2]);

            output.materialSettings = new MaterialSettings(entity.materialOps.materialStateOp.mat);
            output.rendParamSettings = new MaterialSettings(entity.rendOps.materialStateOp.mat);

            return output;
        }

        public override void Init()
        {
            
        }

        public override void UpdateAssociates()
        {
            resourcePool.DoActionOnPool(resourcePool.aEffectPool, (entry) => {
                if (((EffectBase)entry).GetShaderID() == asset.uidSelf) { entry.UpdateAssociates(); }
            });
        }
    }
}
