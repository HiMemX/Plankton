using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asset;
using CSHO;
using HoArchive;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Plankton.EditingTools;
using Plankton.Rendering;
using Plankton.Rendering.Base;
using Plankton.Special_Editors.Level_Editor.EditableContainers;
using SB09WiiAsset;

namespace Plankton.Special_Editors.Level_Editor
{ 
    public partial class LevelEditor
    {
        public void renderer_Paint(object sender, EventArgs e) {
            if (shouldUpdateInstances)
            {

                if (selectedContainer != null)
                {
                    if (!ContainerTypeIsChecked(selectedContainer.GetType())) selectedContainer = null;
                }

                UpdateRenderInstances();
                shouldUpdateInstances = false;
            }

            handleUserInput();
            renderer.handleCameraMovement = !lrsEditor.IsEditing();
            UserConfigurationApplicator.Apply(UserConfigurationManager.userConfig, renderer);
        }

        public void StartRenderLoop()
        {
            renderer.StartRenderLoop();
        }

        public void UpdateInfo(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        public void RenderEditorGizmos(object sender, EventArgs e)
        {
            // Draw Editing Gizmos and whatnot
            renderer.DrawGrid();
            DrawEditingAxis();
            DrawOriginPoint();
        }

        

        public void UpdateNonInstancedContainers()
        {
            renderer.MakeCurrent();
            renderer.renderhelper.Clear();
            Parallel.For(0, editableContainers.Count, i =>
            {
                EditableContainer container = editableContainers[i];
                if (container.isInstanced) { return; }
                if (!ContainerTypeIsChecked(container.GetType())) { return; }


                enumInstanceFlags flags = 0;
                if (selectedContainer != null) flags = (selectedContainers.Contains(container)) ? enumInstanceFlags.OUTLINE : 0;

                container.AddRenderInstances(new PrimitiveInstance(Matrix4x4.Identity, Color4.White, i + 1, flags), renderer.renderhelper);
            });
            renderer.renderhelper.Buffer();

        }

        private void DrawOriginPoint()
        {
            if (selectedContainer == null) { return; }

            renderer.renderhelper.DisableDepthTest();
            renderer.renderhelper.RenderPoint(selectedContainer.GetPosition(), new Color4(1, 0.7f, 0, 1), 4);

            for (int i = 0; i < selectedContainers.Count - 1; i++)
            {
                renderer.renderhelper.RenderPoint(selectedContainers[i].GetPosition(), new Color4(1, 0.4f, 0, 1), 4);
            }

            renderer.renderhelper.EnableDepthTest();
        }

        private void DrawEditingAxis()
        {

            if (lrsEditor.GetMode() == EditMode.NONE) { return; }

            renderer.renderhelper.DisableDepthTest();

            System.Numerics.Vector3 pos = ConverterTools.FromOpenTK(lrsEditor.OriginalPositions.Last());

            Matrix4x4 mat;
            if (lrsEditor.GetMode() != EditMode.SCALE) { mat = Matrix4x4.Identity; }
            else
            {

                mat = System.Numerics.Matrix4x4.CreateFromYawPitchRoll(lrsEditor.OriginalRotations.Last().X, lrsEditor.OriginalRotations.Last().Y, lrsEditor.OriginalRotations.Last().Z);
            }

            System.Numerics.Vector3 axisx = ConverterTools.GetAxisX(mat);
            System.Numerics.Vector3 axisy = ConverterTools.GetAxisY(mat);
            System.Numerics.Vector3 axisz = ConverterTools.GetAxisZ(mat);




            if ((lrsEditor.GetAxis() & EditMode.AXIS_X) != 0)
                renderer.renderhelper.RenderLine(axisx * -1000f + pos, axisx * 1000f + pos, Color4.Red, 1);

            if ((lrsEditor.GetAxis() & EditMode.AXIS_Y) != 0)
                renderer.renderhelper.RenderLine(axisy * -1000f + pos, axisy * 1000f + pos, Color4.Lime, 1);

            if ((lrsEditor.GetAxis() & EditMode.AXIS_Z) != 0)
                renderer.renderhelper.RenderLine(axisz * -1000f + pos, axisz * 1000f + pos, Color4.Blue, 1);

            renderer.renderhelper.EnableDepthTest();
        }



    }


    
}
