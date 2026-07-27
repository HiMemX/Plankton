using System;
using System.Collections.Generic;
using System.Data;
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
    public class xEntAssetContainer : EditableContainer
    {
        public xEntAsset asset;

        public xEntAssetContainer(TOCEntry entry)
        {
            this.entry = entry;
            isInstanced = true;

            defaultLightKit = enumDefaultLightKitType.Object;

            switch (entry.wmlTypeID)
            {
                case wmlTypeID.BSP:
                    defaultLightKit = enumDefaultLightKitType.Environment;
                    break;

            }
            
            asset = (xEntAsset)entry.entity;
        }

        public override Vector3 GetPosition() // Used for focusing on object
        {
            return asset.GetPosition();
        }
        public override void SetPosition(Vector3 pos)
        {
            asset.Pos = new HoArchive.float3(pos);
            asset.Update(this.entry); // BECAUSE BSPs are shit
        }

        public override Vector3 GetRotation()
        {
            return asset.Orientation.GetVector3();
        }
        public override void SetRotation(Vector3 rot)
        {
            asset.Orientation = new HoArchive.float3(rot);
            asset.Update(this.entry); // BECAUSE BSPs are shit
        }

        public override Vector3 GetScale()
        {
            return asset.Scale.GetVector3();
        }

        public override void SetScale(Vector3 scale)
        {
            asset.Scale = new HoArchive.float3(scale);
            asset.Update(this.entry);
        }

        public override ModelInstance GetModelInstance()
        {
            return new ModelInstance(asset.modelInstance, GetInstanceMatrix());
        }

        public override Matrix4x4 GetInstanceMatrix()
        {
            return asset.GetInstanceMatrix();
        }


    }
}
