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
    public class CurveContainer : EditableContainer
    {
        public Curve asset;

        public CurveContainer(TOCEntry entry)
        {
            this.entry = entry;
            isInstanced = false;

            asset = (Curve)entry.entity;
        }
        

        public override void AddRenderInstances(PrimitiveInstance baseinstance, RenderHelper helper) // Optimize this lol
        {
            baseinstance.matrix = GetInstanceMatrix();
            baseinstance.color = GlobalRenderSettings.curveColor;

            Vector3 prev = asset._Coefficients[0].GetVector3();
            for (int i = 0; i < asset._Coefficients.Count; i += 4)
            {
                for (float t = 0; t <= 1; t += 1.0f / (float)GlobalRenderSettings.curveRenderResolution)
                {
                    Vector3 next = asset._Coefficients[i].GetVector3() + t * asset._Coefficients[i + 1].GetVector3() + t*t * asset._Coefficients[i + 2].GetVector3() + t*t*t * asset._Coefficients[i + 3].GetVector3();

                    helper.AddLine(baseinstance.Clone(
                        helper.GetLineMatrix(prev / 2.0f, next / 2.0f)
                        ));

                    prev = next;
                }

            }
        }


    }
}
