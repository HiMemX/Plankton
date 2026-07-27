using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using Plankton.Rendering;
using SB09WiiAsset;

namespace Plankton.Special_Editors.Level_Editor.EditableContainers.SB09Wii
{
    public class FloatingCollectibleContainer : EditableContainer
    {
        public FloatingCollectible asset;

        public FloatingCollectibleContainer(TOCEntry entry)
        {
            this.entry = entry;
            isInstanced = true;

            defaultLightKit = enumDefaultLightKitType.Object;

            asset = (FloatingCollectible)entry.entity;

        }

        public override Vector3 GetPosition() // Used for focusing on object
        {
            return asset.Position.GetVector3();
        }
        public override void SetPosition(Vector3 pos)
        {
            asset.Position = new HoArchive.float3(pos);
        }

        public override ModelInstance GetModelInstance()
        {
            return new ModelInstance(asset.ModelInstance, GetInstanceMatrix());
        }

        public override Matrix4x4 GetInstanceMatrix()
        {
            return asset.GetInstanceMatrix();
        }

    }
}
