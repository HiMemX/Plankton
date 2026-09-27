using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoArchive;
using Plankton.Special_Editors.Level_Editor.EditableContainers.SB09Wii;
using SB09WiiAsset;

namespace Plankton.Special_Editors.Level_Editor.EditableContainers
{
    internal static class ContainerCreator
    {
        public static EditableContainer CreateContainer(TOCEntry entry) // Todo: Add Version specific casting
        {
            if(entry.entity is xEntAsset) { return new xEntAssetContainer(entry); }

            if (entry.entity is TriggerOG) {
                return new TriggerOGContainer(entry);
            }

            if (entry.entity is Direction) return new DirectionContainer(entry);
            if(entry.entity is Tiki) { return new TikiContainer(entry); }
            if(entry.entity is FloatingCollectible) { return new FloatingCollectibleContainer(entry); }
            if(entry.entity is SoundFX) { return new SoundFXContainer(entry); }
            if(entry.entity is SB09WiiAsset.Camera) { return new CameraContainer(entry); }
            if (entry.entity is Curve) return new CurveContainer(entry);
            if (entry.entity is NPCGeneric) return new NPCGenericContainer(entry);
            if (entry.entity is PuckReflector) return new PuckReflectorContainer(entry);

            return null;
        }
    }
}
