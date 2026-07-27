using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plankton.Rendering;
using Plankton.Special_Editors.Level_Editor.EditableContainers;

namespace Plankton.Special_Editors.Level_Editor
{
    internal static class CameraPreviewer
    {
        public static void PreviewSB09WiiCamera(Camera camera, EditableContainers.SB09Wii.CameraContainer container)
        {
            if (container.asset.TargetMode == SB09WiiAsset.TargetMode.Rotation)
            {
                // FOV calculation has a quirk in SB09 apparently
                Func<float> fov = () => { return container.GetFixedFOVY() + 20; };
                camera.PreviewCamera(
                    () => {return ConverterTools.ToOpenTK(container.asset.pos.GetVector3()); },
                    () => { return ConverterTools.ToOpenTK(container.asset.uTargetMode.rotation.GetVector3()); },
                    fov);
            }
            
        }

        public static void PreviewCamera(Camera camera, EditableContainer container) {
            if(container is EditableContainers.SB09Wii.CameraContainer)
            {
                PreviewSB09WiiCamera(camera, (EditableContainers.SB09Wii.CameraContainer)container);
            }
        }
    }
}
