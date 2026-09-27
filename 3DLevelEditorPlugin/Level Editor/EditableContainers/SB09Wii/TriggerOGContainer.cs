using System;
using System.Collections.Generic;
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
    public class TriggerOGContainer : EditableContainer
    {
        public TriggerOG asset;

        private Vector3 scale;
        private Vector3 rotation; // Used for editing purposes, not actually part of TriggerOG



        public TriggerOGContainer(TOCEntry entry)
        {
            this.entry = entry;
            isInstanced = false;

            asset = (TriggerOG)entry.entity;
        }

        public override Vector3 GetPosition() // Used for focusing on object
        {
            switch (asset.subtype)
            {
                case enTriggerSubtype.BOX: 
                    return ((TriggerBox)asset.triggerSubtype).Transform.GetSystemMatrix().Translation;

                case enTriggerSubtype.SPHERE:
                    return ((TriggerSphere)asset.triggerSubtype).Center.GetVector3();
            }

            return new Vector3();
        }
        public override void SetPosition(Vector3 pos)
        {
            switch (asset.subtype)
            {
                case enTriggerSubtype.BOX:
                    ((TriggerBox)asset.triggerSubtype).Transform.SetTranslation(pos);
                    break;

                case enTriggerSubtype.SPHERE:
                    ((TriggerSphere)asset.triggerSubtype).Center = new float3(pos);
                    break;
            }

        }

        public void SetBoxRotation(Vector3 rot)
        {
            // Hacky algorithm

            Matrix4x4 inv;
            Matrix4x4.Invert(Matrix4x4.CreateFromYawPitchRoll(rotation.X, rotation.Y, rotation.Z), out inv);
            Vector3 translation = GetPosition();


            rotation = rot;

            Matrix4x3 mat43 = ((TriggerBox)asset.triggerSubtype).Transform;
            // Remove translation
            mat43.SetTranslation(Vector3.Zero);

            Matrix4x4 mat = mat43.GetSystemMatrix();

            // Remove previous rotation (inverse matrix of the old rotation value)
            // Apply new rotation
            mat = (mat * inv) * Matrix4x4.CreateFromYawPitchRoll(rot.X, rot.Y, rot.Z);
            ((TriggerBox)asset.triggerSubtype).Transform = new Matrix4x3(mat);

            // Re-add translation
            ((TriggerBox)asset.triggerSubtype).Transform.SetTranslation(translation);
        }

        public override void SetRotation(Vector3 rot)
        {
            switch (asset.subtype)
            {
                case enTriggerSubtype.BOX:
                    SetBoxRotation(rot);
                    break;
            }
        }

        public override Vector3 GetRotation()
        {
            rotation = Vector3.Zero;
            return rotation;
            
        }

        public override Vector3 GetScale()
        {
            scale = Vector3.One;
            return scale;
        }

        public void SetBoxScale(Vector3 scale)
        {
            Matrix4x4 inv;
            Matrix4x4.Invert(Matrix4x4.CreateScale(this.scale.X, this.scale.Y, this.scale.Z), out inv);
            Vector3 translation = GetPosition();


            Matrix4x3 mat43 = ((TriggerBox)asset.triggerSubtype).Transform;
            mat43.SetTranslation(Vector3.Zero);

            Matrix4x4 mat = mat43.GetSystemMatrix();


            mat = (mat * inv) * Matrix4x4.CreateScale(scale.X, scale.Y, scale.Z);
            ((TriggerBox)asset.triggerSubtype).Transform = new Matrix4x3(mat);
            ((TriggerBox)asset.triggerSubtype).Transform.SetTranslation(translation);
        }


        public override void SetScale(Vector3 scale)
        {
            switch (asset.subtype)
            {
                case enTriggerSubtype.BOX:
                    SetBoxScale(scale);
                    break;

                case enTriggerSubtype.SPHERE:
                    ((TriggerSphere)asset.triggerSubtype).Radius *= (scale.X + scale.Y + scale.Z) / (this.scale.X + this.scale.Y + this.scale.Z);
                    break;
            }

            this.scale = scale;
        }

        public override Matrix4x4 GetInstanceMatrix()
        {
            switch (asset.subtype)
            {
                case enTriggerSubtype.BOX:
                    return ((TriggerBox)asset.triggerSubtype).Transform.GetSystemMatrix();

                case enTriggerSubtype.SPHERE:
                    TriggerSphere sphere = ((TriggerSphere)asset.triggerSubtype);
                    return Matrix4x4.CreateScale(sphere.Radius) * Matrix4x4.CreateTranslation(sphere.Center.GetVector3());
            }

            return Matrix4x4.Identity;


        }

        public override void AddRenderInstances(PrimitiveInstance baseinstance, RenderHelper helper) {
            //RenderHelper.SetupShader(GetInstanceMatrix(), new OpenTK.Graphics.Color4(0f, 0f, 1f, 0.1f));
            //SimpleShapeRender();

            switch (asset.subtype)
            {
                case enTriggerSubtype.BOX:
                    baseinstance.matrix = ((TriggerBox)asset.triggerSubtype).Transform.GetSystemMatrix();
                    baseinstance.color = GlobalRenderSettings.triggerBoxColor;
                    helper.AddCube(baseinstance);
                    break;

                case enTriggerSubtype.SPHERE:
                    baseinstance.matrix = GetInstanceMatrix();
                    baseinstance.color = GlobalRenderSettings.triggerSphereColor;
                    helper.AddSphere(baseinstance);
                    break;
            }

            
            
                
        }

        private void SimpleShapeRender()
        {
            //if (asset.subtype == enTriggerSubtype.BOX) { RenderHelper.RenderSimpleCube(); }
        }
    }
}
