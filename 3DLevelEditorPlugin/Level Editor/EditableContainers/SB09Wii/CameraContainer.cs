using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using OpenTK.Graphics;
using Plankton.Rendering;
using SB09WiiAsset;

namespace Plankton.Special_Editors.Level_Editor.EditableContainers.SB09Wii
{
    public class CameraContainer : EditableContainer
    {
        public SB09WiiAsset.Camera asset;

        public CameraContainer(TOCEntry entry)
        {
            this.entry = entry;
            isInstanced = false;

            asset = (SB09WiiAsset.Camera)entry.entity;
        }

        public override Vector3 GetPosition() // Used for focusing on object
        {
            return asset.pos.GetVector3();
        }
        public override void SetPosition(Vector3 pos)
        {
            asset.pos = new HoArchive.float3(pos);
        }

        public override Vector3 GetRotation()
        {
            if(asset.TargetMode == TargetMode.Rotation) return asset.uTargetMode.rotation.GetVector3();
            return Vector3.Zero;
        }

        public override void SetRotation(Vector3 rot)
        {
            if (asset.TargetMode == TargetMode.Rotation) asset.uTargetMode.rotation = new float3(rot);

            asset.Update(this.entry); // BECAUSE BSPs are shit
        }
        /*
        public override Vector3 GetScale()
        {
            return new Vector3(1 / asset.fov);
        }

        public override void SetScale(Vector3 scale)
        {
            asset.fov = 3.0f / (scale.X + scale.Y + scale.Z);
        }*/

        public override Matrix4x4 GetInstanceMatrix()
        {
            Matrix4x4 mat = Matrix4x4.Identity;

            if(asset.TargetMode == TargetMode.Rotation)
            {
                Vector3 ypr = asset.uTargetMode.rotation.GetVector3();
                mat = Matrix4x4.CreateFromYawPitchRoll(ypr.X, ypr.Y, ypr.Z);
            }

            return mat * Matrix4x4.CreateTranslation(GetPosition());
        }

        public override void AddRenderInstances(PrimitiveInstance baseinstance, RenderHelper helper) {
            //if (isHaloMask) { return; } // Custom drawing code for halo

            baseinstance.matrix = GetInstanceMatrix();
            baseinstance.color = GlobalRenderSettings.cameraColor;

            helper.AddCamera(baseinstance, GetFixedFOVY());
        }
        

        public float GetFixedFOVY() // In degrees
        {
            return (float)Math.Tan(asset.fov * (float)Math.PI / 360f) * 180f / (float)Math.PI;
        }

    }
}
