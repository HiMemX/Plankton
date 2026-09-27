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
    public class PuckReflectorContainer : EditableContainer
    {
        public PuckReflector asset;

        public PuckReflectorContainer(TOCEntry entry)
        {
            this.entry = entry;
            isInstanced = true;

            defaultLightKit = enumDefaultLightKitType.Object;

            
            
            asset = (PuckReflector)entry.entity;
        }

        public override Vector3 GetPosition() // Used for focusing on object
        {
            return asset.pos.GetVector3();
        }
        public override void SetPosition(Vector3 pos)
        {
            asset.pos = new HoArchive.float3(pos);
            asset.Update(this.entry); // BECAUSE BSPs are shit
        }

        public override Vector3 GetRotation()
        {
            return asset.rot.GetVector3();
        }
        public override void SetRotation(Vector3 rot)
        {
            asset.rot = new HoArchive.float3(rot);
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
            return Matrix4x4.CreateTranslation(asset.pos.GetVector3());
        }


    }
}
