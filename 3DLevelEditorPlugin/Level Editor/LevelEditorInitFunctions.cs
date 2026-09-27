using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HoArchive;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Plankton.Rendering;
using Plankton.Rendering.Base;
using Plankton.Special_Editors.Level_Editor.EditableContainers;
using SB09WiiAsset;

namespace Plankton.Special_Editors.Level_Editor
{
    public partial class LevelEditor
    {
        public void RecollectResourcePools()
        {

            selectedContainer = null;
            renderer.ClearResourcePool();
            editableContainers.Clear();

            foreach (TOCEntry entry in handler.GetAssets())
            {
                if (entry.delete) { continue; }

                AddToResources(entry);



            }

            UpdateRenderInstances();
            UpdateContainerTypesCheckedListbox();
        }

        public void CollectResourcePools()
        {
            selectedContainer = null;
            renderer.ClearResourcePool();
            editableContainers.Clear();

            foreach (TOCEntry entry in handler.GetAssets())
            {
                if (entry.delete) { continue; }

                AddToResources(entry, false);
            }


            // Inits all assets in the pool (Super neat way of writing it)
            renderer.InitPools();

            UpdateRenderInstances();
            UpdateContainerTypesCheckedListbox();

        }


        public void AddToResources(TOCEntry entry, bool doEntryInit = true)
        {
            EditableContainer container = ContainerCreator.CreateContainer(entry);

            if (container == null)
            {
                BaseClass o = renderer.AddToResources(entry);
                if (o == null) return;

                InitOnUpdate(o);
                if (doEntryInit) InitEntry(o);

            }
            else
            {
                editableContainers.Add(container);

            }
        }



        public void AddAndInit(TOCEntry entry)
        {
            AddToResources(entry);

            UpdateRenderInstances();
        }

        void InitOnUpdate(BaseClass entry)
        {
            entry.OnUpdate.Clear();

            entry.OnUpdate.Add((entry) => { UpdateRenderInstances(); }); 
            entry.OnUpdate.Add((entry) => { UpdateEasyEditPanelValues(); });
            
        }

        void InitEntry(BaseClass entry)
        {
            entry.Init();
        }

        public void InitSuperInstance(EditableContainer container, uint attr, bool onlyupdate)
        {
            if (container.entry.delete) { return; }

            ModelInstance instance = container.GetModelInstance();
            
            uint flags = 0;
            if (selectedContainer != null)
            {
                if (selectedContainers.Contains(container)) { flags |= (int)enumInstanceFlags.OUTLINE; } // Marks the super instance for highlight
            }

            TOCEntry lightkitscene = renderer.GetLightKitScene();
            ulong id = 0;
            if (lightkitscene != null && ((int)container.defaultLightKit != -1))
            {
                LightKitScene scene = (LightKitScene)lightkitscene.entity;
                id = new List<ulong> { scene.NPC, scene.Player, scene.Object, scene.Environment }[(int)container.defaultLightKit];
            }

            renderer.InitModelInstance(instance, attr, flags, onlyupdate, id);
        }



        public void UpdateSuperInstance(EditableContainer targetcontainer)
        {
            //Debug.debugWindow.AddEntry("LevelEditorRendering", targetcontainer.entry.uidSelf.ToString("X16"));
            uint attr = 0;

            foreach (EditableContainer container in editableContainers)
            {
                attr++;
                if (container.entry.uidSelf != targetcontainer.entry.uidSelf) { continue; }
                if (!container.isInstanced) { continue; }
                if (!ContainerTypeIsChecked(container.GetType())) { continue; }

                InitSuperInstance(container, attr, true);
            }

            //BufferAllGeometryInstances();
        }

        public void UpdateSuperInstances(List<EditableContainer> targetcontainers)
        {
            //Debug.debugWindow.AddEntry("LevelEditorRendering", targetcontainer.entry.uidSelf.ToString("X16"));
            uint attr = 0;

            foreach (EditableContainer container in editableContainers)
            {
                attr++;
                if (!targetcontainers.Contains(container)) { continue; }
                if (!container.isInstanced) { continue; }
                if (!ContainerTypeIsChecked(container.GetType())) { continue; }

                InitSuperInstance(container, attr, true);
            }

            //BufferAllGeometryInstances();
        }

        public void UpdateRenderInstance(EditableContainer container)
        {
            if (container.isInstanced) UpdateSuperInstance(container);
            else UpdateNonInstancedContainers();
        }


        public void UpdateRenderInstances()
        {
            // Updates the superinstance tree aswell as the non-instanced containers (Which are actually instanced just not with models in game)
            UpdateSuperInstanceTree();
            UpdateNonInstancedContainers();
        }

        public void UpdateSuperInstanceTree()
        {

            renderer.ResetGeometryInstanceLists();
            InitSuperInstances();
            renderer.PrepareAllGeometryBuffers();

        }

        public void InitSuperInstances()
        {
            uint attr = 0;

            foreach (EditableContainer container in editableContainers)
            {
                attr++;
                if (!container.isInstanced) { continue; }
                if (!ContainerTypeIsChecked(container.GetType())) { continue; }

                InitSuperInstance(container, attr, false);
            }
        }
    }
}
