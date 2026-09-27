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
    public class SoundFXContainer : EditableContainer
    {
        public SoundFX asset;

        public SoundFXContainer(TOCEntry entry)
        {
            this.entry = entry;
            isInstanced = false;

            asset = (SoundFX)entry.entity;
        }

        public override Vector3 GetPosition() // Used for focusing on object
        {
            return asset.pos.GetVector3();
        }
        public override void SetPosition(Vector3 pos)
        {
            asset.pos = new HoArchive.float3(pos);
        }

        public override Matrix4x4 GetInstanceMatrix()
        {
            return Matrix4x4.CreateTranslation(GetPosition());
        }

        public override void AddRenderInstances(PrimitiveInstance baseinstance, RenderHelper helper)
        {
            baseinstance.matrix = GetInstanceMatrix();
            baseinstance.color = GlobalRenderSettings.soundFxColor;

            helper.AddEmpty(baseinstance);
        }


    }
}
