using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Assimp.Unmanaged;
using CustomControls;
using GeometryExport;
using HoArchive;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Plankton.EditingTools;
using Plankton.Rendering;
using Plankton.Custom_Controls;
using Plankton.Special_Editors.Level_Editor.EditableContainers;


namespace Plankton.Special_Editors.Level_Editor
{



    public partial class LevelEditor : UserControl
    {
        
        
        public CSHO.Handler handler;
        public TreeView archiveView;

        //public GeometryBaseRenderer renderer;

        LocRotScaleEditor lrsEditor = new();

        public Action<TOCEntry> MainEditorNewAssetCallback = (TOCEntry newentry) => { };
        public Action MainEditorFocusCallback = () => { };

        bool shouldUpdateInstances; // General purpose, main purpose was to get the checkedlistbox to update properly

        bool shouldCancelRightClickMenuOpening = false;
        bool shouldHandleMouseUp = true; // Is set to false when mouse click applies/cancels locrotscaleedit.

        List<EditableContainer> selectedContainers = new();
        EditableContainer selectedContainer
        {
            get
            {
                return (selectedContainers.Count == 0) ? null : selectedContainers.Last();
            }
            set
            {
                if (value == null) selectedContainers = new();
                else selectedContainers = new List<EditableContainer>() { value };
            }
        }

        List<EditableContainer> editableContainers = new();
        
        public LevelEditor()
        {
            InitializeComponent();

            lrsEditor.OnStartCallback = () => { SetLocRotScaleFunctions(); };
            lrsEditor.OnUpdateCallback = () => { UpdateLocRotScale(selectedContainers); };
            lrsEditor.OnEditEndCallback += UpdateEasyEditPanelValues;
            InitEasyEditPanel();

            renderer.MouseMove += renderer_MouseMove;
            renderer.MouseClick += renderer_MouseClick;
            renderer.MouseUp += renderer_MouseUp;

            renderer.PreRender += renderer_Paint;
            renderer.Render += RenderEditorGizmos;
            renderer.PostRender += UpdateInfo;

        }



        public void UpdateLocRotScale(List<EditableContainer> containers)
        {
            UpdateSuperInstances(selectedContainers);
            UpdateNonInstancedContainers();

            //UpdateEasyEditPanelValues();
        }

        EditableContainer GetContainer(ulong uid)
        {
            foreach (EditableContainer container in editableContainers)
            {
                if (container.entry.uidSelf == uid) { return container; }
            }

            return null;
        }


        public void OnNewSelection(EditableContainer container)
        {

            UpdateRenderInstances();
            selectedPropertyGrid.SelectedObject = container.entry.entity;
            assetNameLabel.Text = handler.GetName(container.entry.uidSelf);

            lrsEditor.ApplyEdit();
            UpdateEasyEditPanelValues();
        }
        public void AddSelectedContainer(EditableContainer container)
        {
            selectedContainers.Remove(container); // If container already is in list, remove it first
            selectedContainers.Add(container);
            OnNewSelection(container);
        }

        public void AddSelectedContainers(List<EditableContainer> containers)
        {
            foreach(EditableContainer container in containers)
            {
                if (selectedContainers.Contains(container)) continue;
                selectedContainers.Add(container);
            }
            OnNewSelection(selectedContainer);
        }

        public void RemoveSelectedContainer(EditableContainer container)
        {
            selectedContainers.Remove(container);
            OnNewSelection(container);
        }

        public void SetSelectedContainer(EditableContainer container)
        {
            selectedContainer = container;

            OnNewSelection(container);
        }

        public void SetSelectedContainer(TOCEntry entry)
        {
            EditableContainer candidate = GetContainer(entry.uidSelf);

            if (candidate == null) { return; }

            SetSelectedContainer(candidate);

        }
        
        /*public void UpdateFromMainEditor(TOCEntry entry)
        {
            if (entry.entity is aSuperInstance) { UpdateSuperInstanceTree(); }
        }*/

        public void InitRenderer()
        {
            renderer.InitRenderer();
        }


        

        public void UpdateEasyEditPanelValues()
        {
            if (selectedContainers.Count == 0) return;
            EditableContainer selectedContainer = selectedContainers.Last();

            if (lrsEditor.GetMode() == EditMode.NONE)
            {
                positionVectorInputBox.SetVector(ConverterTools.ToOpenTK(selectedContainer.GetPosition()));
                scaleVectorInputBox.SetVector(ConverterTools.ToOpenTK(selectedContainer.GetScale()));
                rotationVectorInputBox.SetVector(ConverterTools.ToOpenTK(ConverterTools.FromRadians(selectedContainer.GetRotation())));
                return;
            }
            /*
            positionVectorInputBox.SetVector(ConverterTools.ToOpenTK(selectedContainer.GetPosition()));
            scaleVectorInputBox.SetVector(ConverterTools.ToOpenTK(selectedContainer.GetScale()));
            rotationVectorInputBox.SetVector(ConverterTools.ToOpenTK(ConverterTools.FromRadians(selectedContainer.GetRotation())));
            */
        }

        private void SetLocRotScaleFunctions() // Makes Locrotscale Fetch and set container transforms + update EasyEditPanel
        {
            lrsEditor.ClearCallbacks();

            foreach (EditableContainer container in selectedContainers)
            {
                lrsEditor.GetPositionCallbacks.Add(() => { return ConverterTools.ToOpenTK(container.GetPosition()); });
                lrsEditor.GetScaleCallbacks.Add(() => { return ConverterTools.ToOpenTK(container.GetScale()); });
                lrsEditor.GetRotationCallbacks.Add(() => { return ConverterTools.ToOpenTK(container.GetRotation()); });

                lrsEditor.SetPositionCallbacks.Add((Vector3 pos) =>
                {
                    container.SetPosition(ConverterTools.FromOpenTK(pos));
                    //positionVectorInputBox.SetVector(pos);
                });
                lrsEditor.SetScaleCallbacks.Add((Vector3 pos) =>
                {
                    container.SetScale(ConverterTools.FromOpenTK(pos));
                    //scaleVectorInputBox.SetVector(pos);
                });
                lrsEditor.SetRotationCallbacks.Add((Vector3 pos) =>
                {
                    container.SetRotation(ConverterTools.FromOpenTK(pos));
                    //rotationVectorInputBox.SetVector(ConverterTools.ToOpenTK(ConverterTools.FromRadians(ConverterTools.FromOpenTK(pos))));
                });
            }

        }


        public void InitEasyEditPanel()
        {
            positionVectorInputBox.SetVector3Callback = (Vector3 position) =>
            {
                if (selectedContainer == null) return;
                selectedContainer.SetPosition(ConverterTools.FromOpenTK(position));
                UpdateRenderInstance(selectedContainer);
            };
            scaleVectorInputBox.SetVector3Callback = (Vector3 scale) =>
            {
                if (selectedContainer == null) return;
                selectedContainer.SetScale(ConverterTools.FromOpenTK(scale));
                UpdateRenderInstance(selectedContainer);
            };
            rotationVectorInputBox.SetVector3Callback = (Vector3 rot) =>
            {
                if (selectedContainer == null) return;
                selectedContainer.SetRotation(ConverterTools.ToRadians(ConverterTools.FromOpenTK(rot)));
                UpdateRenderInstance(selectedContainer);
            };
            positionVectorInputBox.SetMultiplier(0.4f);
            scaleVectorInputBox.SetMultiplier(0.4f);
            rotationVectorInputBox.SetMultiplier(6);

            positionVectorInputBox.SetMouseUpEvent(() => { UpdateEasyEditPanelValues(); });
            rotationVectorInputBox.SetMouseUpEvent(() => { UpdateEasyEditPanelValues(); });
            scaleVectorInputBox.SetMouseUpEvent(() => { UpdateEasyEditPanelValues(); });
        }


        public void UpdateContainerTypesCheckedListbox()
        {
            var baseType = typeof(EditableContainer);
            var derivedTypes = Assembly.GetExecutingAssembly().GetTypes()
                                      .Where(t => t.IsSubclassOf(baseType) && !t.IsAbstract)
                                      .ToList();
            //MessageBox.Show(typeof(TriggerOGContainer).ToString());
            containerTypesCheckedListBox.Items.Clear();
            foreach (var type in derivedTypes)
            {
                containerTypesCheckedListBox.Items.Add(type.Name);
            }

            for (int i = 0; i < containerTypesCheckedListBox.Items.Count; i++)
            {
                containerTypesCheckedListBox.SetItemChecked(i, true);
            }
        }

        public bool ContainerTypeIsChecked(Type type)
        {
            int itemIndex = containerTypesCheckedListBox.Items.IndexOf(type.Name);

            // If the item is found, check if it's checked
            if (itemIndex >= 0)
            {
                return containerTypesCheckedListBox.GetItemChecked(itemIndex);
            }
            return false;
        }


        

        private void DuplicateSelectedContainer()
        {
            selectedContainer.entry.Update();


            TOCEntry entry = handler.DuplicateFirstOccurence(selectedContainer.entry.uidSelf);


            MainEditorNewAssetCallback(entry);
        }

        private void handleUserInput()
        {
            if (renderer.IsPressed(LevelEditorKeybinds.Get("focusOnObject")))
            {
                if (selectedContainer != null)
                {
                    System.Numerics.Vector3 pos = selectedContainer.GetPosition();
                    renderer.camera.orbit = new Vector3(pos.X, pos.Y, pos.Z);
                }
                return;
            }

            if (renderer.IsPressed(Keys.NumPad0)) // Camera Preview
            {
                CameraPreviewer.PreviewCamera(renderer.camera, selectedContainer); // Does checks itself
                return;
            }

            // Duplication
            Keys? duplicatorbind = LevelEditorKeybinds.Get("duplicateObject");
            if (renderer.IsPressed(duplicatorbind) && (selectedContainer != null))
            {

                lrsEditor.ApplyEdit();
                DuplicateSelectedContainer();

                SetSelectedContainer(editableContainers.Last());


                // For immediate editing after duplication (More blender like)
                lrsEditor.StartEdit(EditMode.POSITION);

                renderer.RemovePressedKey(duplicatorbind & (~Control.ModifierKeys));

                return;
            }

            // LocRotScaleEditor
            Keys? moveobjectbind = LevelEditorKeybinds.Get("moveObject");
            Keys? scaleobjectbind = LevelEditorKeybinds.Get("scaleObject");
            Keys? rotateobjectbind = LevelEditorKeybinds.Get("rotateObject");
            if (renderer.IsPressed(moveobjectbind) && (selectedContainer != null))
            {

                lrsEditor.StartEdit(EditMode.POSITION);
                renderer.RemovePressedKey(moveobjectbind);
                return;
            }

            if (renderer.IsPressed(scaleobjectbind) && (selectedContainer != null))
            {

                lrsEditor.StartEdit(EditMode.SCALE);
                renderer.RemovePressedKey(scaleobjectbind);
                return;
            }

            if (renderer.IsPressed(rotateobjectbind) && (selectedContainer != null))
            {

                lrsEditor.StartEdit(EditMode.ROTATION);
                renderer.RemovePressedKey(rotateobjectbind);
                return;
            }

            if (renderer.IsPressed(Keys.X) || renderer.IsPressed(Keys.X | Keys.Shift))
            {
                lrsEditor.SetAxis(renderer.ShiftPressed() ? EditMode.AXIS_YZ : EditMode.AXIS_X);

                renderer.RemovePressedKey(Keys.X);
                return;
            }
            if (renderer.IsPressed(Keys.Y) || renderer.IsPressed(Keys.Y | Keys.Shift))
            {
                lrsEditor.SetAxis(renderer.ShiftPressed() ? EditMode.AXIS_XZ : EditMode.AXIS_Y);

                renderer.RemovePressedKey(Keys.Y);
                return;
            }
            if (renderer.IsPressed(Keys.Z) || renderer.IsPressed(Keys.Z | Keys.Shift))
            {
                lrsEditor.SetAxis(renderer.ShiftPressed() ? EditMode.AXIS_XY : EditMode.AXIS_Z);

                renderer.RemovePressedKey(Keys.Z);
                return;
            }

        }

        private EditableContainer DuplicateContainer(EditableContainer container)
        {
            TOCEntry entry = container.entry;

            return ContainerCreator.CreateContainer(entry.Copy());
        }

        

        

        private Ray GetCursorRay()
        {
            return renderer.GetCursorRay();
        }


        private void LevelEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void selectedPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            selectedContainer.entry.Update(0x40, true);
            renderer.Focus();
        }

        private void openInMainEditorButton_Click(object sender, EventArgs e)
        {
            if (selectedContainer == null) { return; }

            assetTreeNode node = ((tableTreeNode)archiveView.Nodes[0]).getAssetNode(selectedContainer.entry.uidSelf);


            if (node == null) { return; }

            archiveView.SelectedNode = node;
            MainEditorFocusCallback();
        }



        public void UpdateInfo()
        {
            UpdateInfoDrawCalls();
            UpdateInfoFPS();
            UpdateInfoCamera();

        }

        public void UpdateInfoDrawCalls()
        {
            //drawCallsLabelValue.Text = RenderHelper.drawCallsThisFrame.ToString();
        }

        public void UpdateInfoFPS()
        {
            fpsLabelValue.Text = Math.Round(renderer.fps, 2).ToString();
        }

        public void UpdateInfoCamera()
        {
            OpenTK.Vector3 pos = renderer.camera.GetPosition();
            cameraLabelValue.Text = Math.Round(pos.X, 3).ToString() + ", " + Math.Round(pos.Y, 3).ToString() + ", " + Math.Round(pos.Z, 3).ToString();
        }






        private void containerTypesCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            shouldUpdateInstances = true;

            Debug.debugWindow.AddEntry("containerTypesCheckedListBox_ItemCheck", e.CurrentValue.ToString() + e.NewValue.ToString());

            if (selectedContainer == null) return;
            if (!ContainerTypeIsChecked(selectedContainer.GetType())) selectedContainer = null;

        }

        private void screenshotButton_Click(object sender, EventArgs e) // Debug, remove before release
        {
            int width = renderer.Width;
            int height = renderer.Height;

            // Allocate byte array for RGBA pixels
            byte[] pixels = new byte[width * height * 4];

            // Read pixels from the framebuffer
            GL.ReadPixels(0, 0, width, height, PixelFormat.Bgra, PixelType.UnsignedByte, pixels);

            // Create a Bitmap with alpha channel
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Lock bits so we can copy into it
            var bmpData = bmp.LockBits(
                new Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // OpenGL’s origin is bottom-left, Bitmap’s is top-left — need to flip vertically
            int stride = bmpData.Stride;
            for (int y = 0; y < height; y++)
            {
                int srcIndex = (height - y - 1) * width * 4;
                IntPtr destPtr = bmpData.Scan0 + y * stride;
                System.Runtime.InteropServices.Marshal.Copy(pixels, srcIndex, destPtr, width * 4);
            }

            bmp.UnlockBits(bmpData);
            bmp.Save("C:\\Users\\felix\\Desktop\\Random_stuff\\DebugScreenshots\\test.png");
        }

        private void levelViewRightClickMenu_Opening(object sender, CancelEventArgs e)
        {
            if (shouldCancelRightClickMenuOpening)
            {
                e.Cancel = true;
                shouldCancelRightClickMenuOpening = false;
                return;
            }
        }

        private void openLinkAssetButton_Click(object sender, EventArgs e)
        {

        }

        private void tempButtonRemove_Click(object sender, EventArgs e)
        {
            //List<Vector3> coords = new List<Vector3>() { new Vector3(-39.5f, 0, -39.5f), new Vector3(-38.5f, 0, -39.5f), new Vector3(-37.5f, 0, -39.5f), new Vector3(-36.5f, 0, -39.5f), new Vector3(-35.5f, 0, -39.5f), new Vector3(-34.5f, 0, -39.5f), new Vector3(-33.5f, 0, -39.5f), new Vector3(-32.5f, 0, -39.5f), new Vector3(-31.5f, 0, -39.5f), new Vector3(-30.5f, 0, -39.5f), new Vector3(-29.5f, 0, -39.5f), new Vector3(-28.5f, 0, -39.5f), new Vector3(-27.5f, 0, -39.5f), new Vector3(-26.5f, 0, -39.5f), new Vector3(-25.5f, 0, -39.5f), new Vector3(-24.5f, 0, -39.5f), new Vector3(-23.5f, 0, -39.5f), new Vector3(-22.5f, 0, -39.5f), new Vector3(-21.5f, 0, -39.5f), new Vector3(-20.5f, 0, -39.5f), new Vector3(-19.5f, 0, -39.5f), new Vector3(-18.5f, 0, -39.5f), new Vector3(-17.5f, 0, -39.5f), new Vector3(-16.5f, 0, -39.5f), new Vector3(-15.5f, 0, -39.5f), new Vector3(-14.5f, 0, -39.5f), new Vector3(-13.5f, 0, -39.5f), new Vector3(-12.5f, 0, -39.5f), new Vector3(-11.5f, 0, -39.5f), new Vector3(-10.5f, 0, -39.5f), new Vector3(-9.5f, 0, -39.5f), new Vector3(-8.5f, 0, -39.5f), new Vector3(-7.5f, 0, -39.5f), new Vector3(-6.5f, 0, -39.5f), new Vector3(-5.5f, 0, -39.5f), new Vector3(-4.5f, 0, -39.5f), new Vector3(-3.5f, 0, -39.5f), new Vector3(-2.5f, 0, -39.5f), new Vector3(-1.5f, 0, -39.5f), new Vector3(-0.5f, 0, -39.5f), new Vector3(0.5f, 0, -39.5f), new Vector3(1.5f, 0, -39.5f), new Vector3(2.5f, 0, -39.5f), new Vector3(3.5f, 0, -39.5f), new Vector3(4.5f, 0, -39.5f), new Vector3(5.5f, 0, -39.5f), new Vector3(6.5f, 0, -39.5f), new Vector3(7.5f, 0, -39.5f), new Vector3(8.5f, 0, -39.5f), new Vector3(9.5f, 0, -39.5f), new Vector3(10.5f, 0, -39.5f), new Vector3(11.5f, 0, -39.5f), new Vector3(12.5f, 0, -39.5f), new Vector3(13.5f, 0, -39.5f), new Vector3(14.5f, 0, -39.5f), new Vector3(15.5f, 0, -39.5f), new Vector3(16.5f, 0, -39.5f), new Vector3(17.5f, 0, -39.5f), new Vector3(18.5f, 0, -39.5f), new Vector3(19.5f, 0, -39.5f), new Vector3(20.5f, 0, -39.5f), new Vector3(21.5f, 0, -39.5f), new Vector3(22.5f, 0, -39.5f), new Vector3(23.5f, 0, -39.5f), new Vector3(24.5f, 0, -39.5f), new Vector3(25.5f, 0, -39.5f), new Vector3(26.5f, 0, -39.5f), new Vector3(27.5f, 0, -39.5f), new Vector3(28.5f, 0, -39.5f), new Vector3(29.5f, 0, -39.5f), new Vector3(30.5f, 0, -39.5f), new Vector3(31.5f, 0, -39.5f), new Vector3(32.5f, 0, -39.5f), new Vector3(33.5f, 0, -39.5f), new Vector3(34.5f, 0, -39.5f), new Vector3(35.5f, 0, -39.5f), new Vector3(36.5f, 0, -39.5f), new Vector3(37.5f, 0, -39.5f), new Vector3(38.5f, 0, -39.5f), new Vector3(39.5f, 0, -39.5f), new Vector3(40.5f, 0, -39.5f), new Vector3(-39.5f, 0, -38.5f), new Vector3(-19.5f, 0, -38.5f), new Vector3(-14.5f, 0, -38.5f), new Vector3(0.5f, 0, -38.5f), new Vector3(20.5f, 0, -38.5f), new Vector3(30.5f, 0, -38.5f), new Vector3(35.5f, 0, -38.5f), new Vector3(40.5f, 0, -38.5f), new Vector3(-39.5f, 0, -37.5f), new Vector3(-19.5f, 0, -37.5f), new Vector3(-14.5f, 0, -37.5f), new Vector3(0.5f, 0, -37.5f), new Vector3(20.5f, 0, -37.5f), new Vector3(30.5f, 0, -37.5f), new Vector3(35.5f, 0, -37.5f), new Vector3(40.5f, 0, -37.5f), new Vector3(-39.5f, 0, -36.5f), new Vector3(-19.5f, 0, -36.5f), new Vector3(-14.5f, 0, -36.5f), new Vector3(0.5f, 0, -36.5f), new Vector3(20.5f, 0, -36.5f), new Vector3(30.5f, 0, -36.5f), new Vector3(35.5f, 0, -36.5f), new Vector3(40.5f, 0, -36.5f), new Vector3(-39.5f, 0, -35.5f), new Vector3(-19.5f, 0, -35.5f), new Vector3(-14.5f, 0, -35.5f), new Vector3(0.5f, 0, -35.5f), new Vector3(20.5f, 0, -35.5f), new Vector3(30.5f, 0, -35.5f), new Vector3(35.5f, 0, -35.5f), new Vector3(40.5f, 0, -35.5f), new Vector3(-39.5f, 0, -34.5f), new Vector3(-38.5f, 0, -34.5f), new Vector3(-37.5f, 0, -34.5f), new Vector3(-36.5f, 0, -34.5f), new Vector3(-35.5f, 0, -34.5f), new Vector3(-34.5f, 0, -34.5f), new Vector3(-33.5f, 0, -34.5f), new Vector3(-32.5f, 0, -34.5f), new Vector3(-31.5f, 0, -34.5f), new Vector3(-30.5f, 0, -34.5f), new Vector3(-29.5f, 0, -34.5f), new Vector3(-24.5f, 0, -34.5f), new Vector3(-23.5f, 0, -34.5f), new Vector3(-22.5f, 0, -34.5f), new Vector3(-21.5f, 0, -34.5f), new Vector3(-20.5f, 0, -34.5f), new Vector3(-19.5f, 0, -34.5f), new Vector3(-14.5f, 0, -34.5f), new Vector3(-13.5f, 0, -34.5f), new Vector3(-12.5f, 0, -34.5f), new Vector3(-11.5f, 0, -34.5f), new Vector3(-10.5f, 0, -34.5f), new Vector3(-9.5f, 0, -34.5f), new Vector3(-4.5f, 0, -34.5f), new Vector3(0.5f, 0, -34.5f), new Vector3(5.5f, 0, -34.5f), new Vector3(6.5f, 0, -34.5f), new Vector3(7.5f, 0, -34.5f), new Vector3(8.5f, 0, -34.5f), new Vector3(9.5f, 0, -34.5f), new Vector3(10.5f, 0, -34.5f), new Vector3(11.5f, 0, -34.5f), new Vector3(12.5f, 0, -34.5f), new Vector3(13.5f, 0, -34.5f), new Vector3(14.5f, 0, -34.5f), new Vector3(15.5f, 0, -34.5f), new Vector3(16.5f, 0, -34.5f), new Vector3(17.5f, 0, -34.5f), new Vector3(18.5f, 0, -34.5f), new Vector3(19.5f, 0, -34.5f), new Vector3(20.5f, 0, -34.5f), new Vector3(25.5f, 0, -34.5f), new Vector3(30.5f, 0, -34.5f), new Vector3(35.5f, 0, -34.5f), new Vector3(40.5f, 0, -34.5f), new Vector3(-39.5f, 0, -33.5f), new Vector3(-34.5f, 0, -33.5f), new Vector3(-9.5f, 0, -33.5f), new Vector3(-4.5f, 0, -33.5f), new Vector3(5.5f, 0, -33.5f), new Vector3(10.5f, 0, -33.5f), new Vector3(15.5f, 0, -33.5f), new Vector3(25.5f, 0, -33.5f), new Vector3(30.5f, 0, -33.5f), new Vector3(40.5f, 0, -33.5f), new Vector3(-39.5f, 0, -32.5f), new Vector3(-34.5f, 0, -32.5f), new Vector3(-9.5f, 0, -32.5f), new Vector3(-4.5f, 0, -32.5f), new Vector3(5.5f, 0, -32.5f), new Vector3(10.5f, 0, -32.5f), new Vector3(15.5f, 0, -32.5f), new Vector3(25.5f, 0, -32.5f), new Vector3(30.5f, 0, -32.5f), new Vector3(40.5f, 0, -32.5f), new Vector3(-39.5f, 0, -31.5f), new Vector3(-34.5f, 0, -31.5f), new Vector3(-9.5f, 0, -31.5f), new Vector3(-4.5f, 0, -31.5f), new Vector3(5.5f, 0, -31.5f), new Vector3(10.5f, 0, -31.5f), new Vector3(15.5f, 0, -31.5f), new Vector3(25.5f, 0, -31.5f), new Vector3(30.5f, 0, -31.5f), new Vector3(40.5f, 0, -31.5f), new Vector3(-39.5f, 0, -30.5f), new Vector3(-34.5f, 0, -30.5f), new Vector3(-9.5f, 0, -30.5f), new Vector3(-4.5f, 0, -30.5f), new Vector3(5.5f, 0, -30.5f), new Vector3(10.5f, 0, -30.5f), new Vector3(15.5f, 0, -30.5f), new Vector3(25.5f, 0, -30.5f), new Vector3(30.5f, 0, -30.5f), new Vector3(40.5f, 0, -30.5f), new Vector3(-39.5f, 0, -29.5f), new Vector3(-34.5f, 0, -29.5f), new Vector3(-29.5f, 0, -29.5f), new Vector3(-28.5f, 0, -29.5f), new Vector3(-27.5f, 0, -29.5f), new Vector3(-26.5f, 0, -29.5f), new Vector3(-25.5f, 0, -29.5f), new Vector3(-24.5f, 0, -29.5f), new Vector3(-19.5f, 0, -29.5f), new Vector3(-14.5f, 0, -29.5f), new Vector3(-13.5f, 0, -29.5f), new Vector3(-12.5f, 0, -29.5f), new Vector3(-11.5f, 0, -29.5f), new Vector3(-10.5f, 0, -29.5f), new Vector3(-9.5f, 0, -29.5f), new Vector3(-8.5f, 0, -29.5f), new Vector3(-7.5f, 0, -29.5f), new Vector3(-6.5f, 0, -29.5f), new Vector3(-5.5f, 0, -29.5f), new Vector3(-4.5f, 0, -29.5f), new Vector3(0.5f, 0, -29.5f), new Vector3(1.5f, 0, -29.5f), new Vector3(2.5f, 0, -29.5f), new Vector3(3.5f, 0, -29.5f), new Vector3(4.5f, 0, -29.5f), new Vector3(5.5f, 0, -29.5f), new Vector3(10.5f, 0, -29.5f), new Vector3(15.5f, 0, -29.5f), new Vector3(20.5f, 0, -29.5f), new Vector3(25.5f, 0, -29.5f), new Vector3(30.5f, 0, -29.5f), new Vector3(31.5f, 0, -29.5f), new Vector3(32.5f, 0, -29.5f), new Vector3(33.5f, 0, -29.5f), new Vector3(34.5f, 0, -29.5f), new Vector3(35.5f, 0, -29.5f), new Vector3(40.5f, 0, -29.5f), new Vector3(-39.5f, 0, -28.5f), new Vector3(-34.5f, 0, -28.5f), new Vector3(-24.5f, 0, -28.5f), new Vector3(-19.5f, 0, -28.5f), new Vector3(-4.5f, 0, -28.5f), new Vector3(5.5f, 0, -28.5f), new Vector3(15.5f, 0, -28.5f), new Vector3(20.5f, 0, -28.5f), new Vector3(25.5f, 0, -28.5f), new Vector3(40.5f, 0, -28.5f), new Vector3(-39.5f, 0, -27.5f), new Vector3(-34.5f, 0, -27.5f), new Vector3(-24.5f, 0, -27.5f), new Vector3(-19.5f, 0, -27.5f), new Vector3(-4.5f, 0, -27.5f), new Vector3(5.5f, 0, -27.5f), new Vector3(15.5f, 0, -27.5f), new Vector3(20.5f, 0, -27.5f), new Vector3(25.5f, 0, -27.5f), new Vector3(40.5f, 0, -27.5f), new Vector3(-39.5f, 0, -26.5f), new Vector3(-34.5f, 0, -26.5f), new Vector3(-24.5f, 0, -26.5f), new Vector3(-19.5f, 0, -26.5f), new Vector3(-4.5f, 0, -26.5f), new Vector3(5.5f, 0, -26.5f), new Vector3(15.5f, 0, -26.5f), new Vector3(20.5f, 0, -26.5f), new Vector3(25.5f, 0, -26.5f), new Vector3(40.5f, 0, -26.5f), new Vector3(-39.5f, 0, -25.5f), new Vector3(-34.5f, 0, -25.5f), new Vector3(-24.5f, 0, -25.5f), new Vector3(-19.5f, 0, -25.5f), new Vector3(-4.5f, 0, -25.5f), new Vector3(5.5f, 0, -25.5f), new Vector3(15.5f, 0, -25.5f), new Vector3(20.5f, 0, -25.5f), new Vector3(25.5f, 0, -25.5f), new Vector3(40.5f, 0, -25.5f), new Vector3(-39.5f, 0, -24.5f), new Vector3(-34.5f, 0, -24.5f), new Vector3(-33.5f, 0, -24.5f), new Vector3(-32.5f, 0, -24.5f), new Vector3(-31.5f, 0, -24.5f), new Vector3(-30.5f, 0, -24.5f), new Vector3(-29.5f, 0, -24.5f), new Vector3(-24.5f, 0, -24.5f), new Vector3(-23.5f, 0, -24.5f), new Vector3(-22.5f, 0, -24.5f), new Vector3(-21.5f, 0, -24.5f), new Vector3(-20.5f, 0, -24.5f), new Vector3(-19.5f, 0, -24.5f), new Vector3(-14.5f, 0, -24.5f), new Vector3(-13.5f, 0, -24.5f), new Vector3(-12.5f, 0, -24.5f), new Vector3(-11.5f, 0, -24.5f), new Vector3(-10.5f, 0, -24.5f), new Vector3(-9.5f, 0, -24.5f), new Vector3(-4.5f, 0, -24.5f), new Vector3(-3.5f, 0, -24.5f), new Vector3(-2.5f, 0, -24.5f), new Vector3(-1.5f, 0, -24.5f), new Vector3(-0.5f, 0, -24.5f), new Vector3(0.5f, 0, -24.5f), new Vector3(5.5f, 0, -24.5f), new Vector3(10.5f, 0, -24.5f), new Vector3(11.5f, 0, -24.5f), new Vector3(12.5f, 0, -24.5f), new Vector3(13.5f, 0, -24.5f), new Vector3(14.5f, 0, -24.5f), new Vector3(15.5f, 0, -24.5f), new Vector3(20.5f, 0, -24.5f), new Vector3(21.5f, 0, -24.5f), new Vector3(22.5f, 0, -24.5f), new Vector3(23.5f, 0, -24.5f), new Vector3(24.5f, 0, -24.5f), new Vector3(25.5f, 0, -24.5f), new Vector3(26.5f, 0, -24.5f), new Vector3(27.5f, 0, -24.5f), new Vector3(28.5f, 0, -24.5f), new Vector3(29.5f, 0, -24.5f), new Vector3(30.5f, 0, -24.5f), new Vector3(35.5f, 0, -24.5f), new Vector3(36.5f, 0, -24.5f), new Vector3(37.5f, 0, -24.5f), new Vector3(38.5f, 0, -24.5f), new Vector3(39.5f, 0, -24.5f), new Vector3(40.5f, 0, -24.5f), new Vector3(-39.5f, 0, -23.5f), new Vector3(-19.5f, 0, -23.5f), new Vector3(-9.5f, 0, -23.5f), new Vector3(5.5f, 0, -23.5f), new Vector3(10.5f, 0, -23.5f), new Vector3(20.5f, 0, -23.5f), new Vector3(35.5f, 0, -23.5f), new Vector3(40.5f, 0, -23.5f), new Vector3(-39.5f, 0, -22.5f), new Vector3(-19.5f, 0, -22.5f), new Vector3(-9.5f, 0, -22.5f), new Vector3(5.5f, 0, -22.5f), new Vector3(10.5f, 0, -22.5f), new Vector3(20.5f, 0, -22.5f), new Vector3(35.5f, 0, -22.5f), new Vector3(40.5f, 0, -22.5f), new Vector3(-39.5f, 0, -21.5f), new Vector3(-19.5f, 0, -21.5f), new Vector3(-9.5f, 0, -21.5f), new Vector3(5.5f, 0, -21.5f), new Vector3(10.5f, 0, -21.5f), new Vector3(20.5f, 0, -21.5f), new Vector3(35.5f, 0, -21.5f), new Vector3(40.5f, 0, -21.5f), new Vector3(-39.5f, 0, -20.5f), new Vector3(-19.5f, 0, -20.5f), new Vector3(-9.5f, 0, -20.5f), new Vector3(5.5f, 0, -20.5f), new Vector3(10.5f, 0, -20.5f), new Vector3(20.5f, 0, -20.5f), new Vector3(35.5f, 0, -20.5f), new Vector3(40.5f, 0, -20.5f), new Vector3(-39.5f, 0, -19.5f), new Vector3(-38.5f, 0, -19.5f), new Vector3(-37.5f, 0, -19.5f), new Vector3(-36.5f, 0, -19.5f), new Vector3(-35.5f, 0, -19.5f), new Vector3(-34.5f, 0, -19.5f), new Vector3(-29.5f, 0, -19.5f), new Vector3(-28.5f, 0, -19.5f), new Vector3(-27.5f, 0, -19.5f), new Vector3(-26.5f, 0, -19.5f), new Vector3(-25.5f, 0, -19.5f), new Vector3(-24.5f, 0, -19.5f), new Vector3(-19.5f, 0, -19.5f), new Vector3(-18.5f, 0, -19.5f), new Vector3(-17.5f, 0, -19.5f), new Vector3(-16.5f, 0, -19.5f), new Vector3(-15.5f, 0, -19.5f), new Vector3(-14.5f, 0, -19.5f), new Vector3(-9.5f, 0, -19.5f), new Vector3(-8.5f, 0, -19.5f), new Vector3(-7.5f, 0, -19.5f), new Vector3(-6.5f, 0, -19.5f), new Vector3(-5.5f, 0, -19.5f), new Vector3(-4.5f, 0, -19.5f), new Vector3(-3.5f, 0, -19.5f), new Vector3(-2.5f, 0, -19.5f), new Vector3(-1.5f, 0, -19.5f), new Vector3(-0.5f, 0, -19.5f), new Vector3(0.5f, 0, -19.5f), new Vector3(5.5f, 0, -19.5f), new Vector3(10.5f, 0, -19.5f), new Vector3(15.5f, 0, -19.5f), new Vector3(20.5f, 0, -19.5f), new Vector3(25.5f, 0, -19.5f), new Vector3(26.5f, 0, -19.5f), new Vector3(27.5f, 0, -19.5f), new Vector3(28.5f, 0, -19.5f), new Vector3(29.5f, 0, -19.5f), new Vector3(30.5f, 0, -19.5f), new Vector3(35.5f, 0, -19.5f), new Vector3(40.5f, 0, -19.5f), new Vector3(-39.5f, 0, -18.5f), new Vector3(-24.5f, 0, -18.5f), new Vector3(-19.5f, 0, -18.5f), new Vector3(-14.5f, 0, -18.5f), new Vector3(-4.5f, 0, -18.5f), new Vector3(15.5f, 0, -18.5f), new Vector3(20.5f, 0, -18.5f), new Vector3(25.5f, 0, -18.5f), new Vector3(40.5f, 0, -18.5f), new Vector3(-39.5f, 0, -17.5f), new Vector3(-24.5f, 0, -17.5f), new Vector3(-19.5f, 0, -17.5f), new Vector3(-14.5f, 0, -17.5f), new Vector3(-4.5f, 0, -17.5f), new Vector3(15.5f, 0, -17.5f), new Vector3(20.5f, 0, -17.5f), new Vector3(25.5f, 0, -17.5f), new Vector3(40.5f, 0, -17.5f), new Vector3(-39.5f, 0, -16.5f), new Vector3(-24.5f, 0, -16.5f), new Vector3(-19.5f, 0, -16.5f), new Vector3(-14.5f, 0, -16.5f), new Vector3(-4.5f, 0, -16.5f), new Vector3(15.5f, 0, -16.5f), new Vector3(20.5f, 0, -16.5f), new Vector3(25.5f, 0, -16.5f), new Vector3(40.5f, 0, -16.5f), new Vector3(-39.5f, 0, -15.5f), new Vector3(-24.5f, 0, -15.5f), new Vector3(-19.5f, 0, -15.5f), new Vector3(-14.5f, 0, -15.5f), new Vector3(-4.5f, 0, -15.5f), new Vector3(15.5f, 0, -15.5f), new Vector3(20.5f, 0, -15.5f), new Vector3(25.5f, 0, -15.5f), new Vector3(40.5f, 0, -15.5f), new Vector3(-39.5f, 0, -14.5f), new Vector3(-34.5f, 0, -14.5f), new Vector3(-33.5f, 0, -14.5f), new Vector3(-32.5f, 0, -14.5f), new Vector3(-31.5f, 0, -14.5f), new Vector3(-30.5f, 0, -14.5f), new Vector3(-29.5f, 0, -14.5f), new Vector3(-28.5f, 0, -14.5f), new Vector3(-27.5f, 0, -14.5f), new Vector3(-26.5f, 0, -14.5f), new Vector3(-25.5f, 0, -14.5f), new Vector3(-24.5f, 0, -14.5f), new Vector3(-23.5f, 0, -14.5f), new Vector3(-22.5f, 0, -14.5f), new Vector3(-21.5f, 0, -14.5f), new Vector3(-20.5f, 0, -14.5f), new Vector3(-19.5f, 0, -14.5f), new Vector3(-14.5f, 0, -14.5f), new Vector3(-9.5f, 0, -14.5f), new Vector3(-8.5f, 0, -14.5f), new Vector3(-7.5f, 0, -14.5f), new Vector3(-6.5f, 0, -14.5f), new Vector3(-5.5f, 0, -14.5f), new Vector3(-4.5f, 0, -14.5f), new Vector3(0.5f, 0, -14.5f), new Vector3(5.5f, 0, -14.5f), new Vector3(6.5f, 0, -14.5f), new Vector3(7.5f, 0, -14.5f), new Vector3(8.5f, 0, -14.5f), new Vector3(9.5f, 0, -14.5f), new Vector3(10.5f, 0, -14.5f), new Vector3(11.5f, 0, -14.5f), new Vector3(12.5f, 0, -14.5f), new Vector3(13.5f, 0, -14.5f), new Vector3(14.5f, 0, -14.5f), new Vector3(15.5f, 0, -14.5f), new Vector3(20.5f, 0, -14.5f), new Vector3(25.5f, 0, -14.5f), new Vector3(26.5f, 0, -14.5f), new Vector3(27.5f, 0, -14.5f), new Vector3(28.5f, 0, -14.5f), new Vector3(29.5f, 0, -14.5f), new Vector3(30.5f, 0, -14.5f), new Vector3(35.5f, 0, -14.5f), new Vector3(36.5f, 0, -14.5f), new Vector3(37.5f, 0, -14.5f), new Vector3(38.5f, 0, -14.5f), new Vector3(39.5f, 0, -14.5f), new Vector3(40.5f, 0, -14.5f), new Vector3(-39.5f, 0, -13.5f), new Vector3(-29.5f, 0, -13.5f), new Vector3(-14.5f, 0, -13.5f), new Vector3(-9.5f, 0, -13.5f), new Vector3(0.5f, 0, -13.5f), new Vector3(10.5f, 0, -13.5f), new Vector3(20.5f, 0, -13.5f), new Vector3(25.5f, 0, -13.5f), new Vector3(30.5f, 0, -13.5f), new Vector3(40.5f, 0, -13.5f), new Vector3(-39.5f, 0, -12.5f), new Vector3(-29.5f, 0, -12.5f), new Vector3(-14.5f, 0, -12.5f), new Vector3(-9.5f, 0, -12.5f), new Vector3(0.5f, 0, -12.5f), new Vector3(10.5f, 0, -12.5f), new Vector3(20.5f, 0, -12.5f), new Vector3(25.5f, 0, -12.5f), new Vector3(30.5f, 0, -12.5f), new Vector3(40.5f, 0, -12.5f), new Vector3(-39.5f, 0, -11.5f), new Vector3(-29.5f, 0, -11.5f), new Vector3(-14.5f, 0, -11.5f), new Vector3(-9.5f, 0, -11.5f), new Vector3(0.5f, 0, -11.5f), new Vector3(10.5f, 0, -11.5f), new Vector3(20.5f, 0, -11.5f), new Vector3(25.5f, 0, -11.5f), new Vector3(30.5f, 0, -11.5f), new Vector3(40.5f, 0, -11.5f), new Vector3(-39.5f, 0, -10.5f), new Vector3(-29.5f, 0, -10.5f), new Vector3(-14.5f, 0, -10.5f), new Vector3(-9.5f, 0, -10.5f), new Vector3(0.5f, 0, -10.5f), new Vector3(10.5f, 0, -10.5f), new Vector3(20.5f, 0, -10.5f), new Vector3(25.5f, 0, -10.5f), new Vector3(30.5f, 0, -10.5f), new Vector3(40.5f, 0, -10.5f), new Vector3(-39.5f, 0, -9.5f), new Vector3(-34.5f, 0, -9.5f), new Vector3(-29.5f, 0, -9.5f), new Vector3(-28.5f, 0, -9.5f), new Vector3(-27.5f, 0, -9.5f), new Vector3(-26.5f, 0, -9.5f), new Vector3(-25.5f, 0, -9.5f), new Vector3(-24.5f, 0, -9.5f), new Vector3(-19.5f, 0, -9.5f), new Vector3(-18.5f, 0, -9.5f), new Vector3(-17.5f, 0, -9.5f), new Vector3(-16.5f, 0, -9.5f), new Vector3(-15.5f, 0, -9.5f), new Vector3(-14.5f, 0, -9.5f), new Vector3(-13.5f, 0, -9.5f), new Vector3(-12.5f, 0, -9.5f), new Vector3(-11.5f, 0, -9.5f), new Vector3(-10.5f, 0, -9.5f), new Vector3(-9.5f, 0, -9.5f), new Vector3(-4.5f, 0, -9.5f), new Vector3(0.5f, 0, -9.5f), new Vector3(1.5f, 0, -9.5f), new Vector3(2.5f, 0, -9.5f), new Vector3(3.5f, 0, -9.5f), new Vector3(4.5f, 0, -9.5f), new Vector3(5.5f, 0, -9.5f), new Vector3(6.5f, 0, -9.5f), new Vector3(7.5f, 0, -9.5f), new Vector3(8.5f, 0, -9.5f), new Vector3(9.5f, 0, -9.5f), new Vector3(10.5f, 0, -9.5f), new Vector3(11.5f, 0, -9.5f), new Vector3(12.5f, 0, -9.5f), new Vector3(13.5f, 0, -9.5f), new Vector3(14.5f, 0, -9.5f), new Vector3(15.5f, 0, -9.5f), new Vector3(16.5f, 0, -9.5f), new Vector3(17.5f, 0, -9.5f), new Vector3(18.5f, 0, -9.5f), new Vector3(19.5f, 0, -9.5f), new Vector3(20.5f, 0, -9.5f), new Vector3(21.5f, 0, -9.5f), new Vector3(22.5f, 0, -9.5f), new Vector3(23.5f, 0, -9.5f), new Vector3(24.5f, 0, -9.5f), new Vector3(25.5f, 0, -9.5f), new Vector3(30.5f, 0, -9.5f), new Vector3(31.5f, 0, -9.5f), new Vector3(32.5f, 0, -9.5f), new Vector3(33.5f, 0, -9.5f), new Vector3(34.5f, 0, -9.5f), new Vector3(35.5f, 0, -9.5f), new Vector3(40.5f, 0, -9.5f), new Vector3(-39.5f, 0, -8.5f), new Vector3(-34.5f, 0, -8.5f), new Vector3(-29.5f, 0, -8.5f), new Vector3(-19.5f, 0, -8.5f), new Vector3(-4.5f, 0, -8.5f), new Vector3(10.5f, 0, -8.5f), new Vector3(15.5f, 0, -8.5f), new Vector3(30.5f, 0, -8.5f), new Vector3(35.5f, 0, -8.5f), new Vector3(40.5f, 0, -8.5f), new Vector3(-39.5f, 0, -7.5f), new Vector3(-34.5f, 0, -7.5f), new Vector3(-29.5f, 0, -7.5f), new Vector3(-19.5f, 0, -7.5f), new Vector3(-4.5f, 0, -7.5f), new Vector3(10.5f, 0, -7.5f), new Vector3(15.5f, 0, -7.5f), new Vector3(30.5f, 0, -7.5f), new Vector3(35.5f, 0, -7.5f), new Vector3(40.5f, 0, -7.5f), new Vector3(-39.5f, 0, -6.5f), new Vector3(-34.5f, 0, -6.5f), new Vector3(-29.5f, 0, -6.5f), new Vector3(-19.5f, 0, -6.5f), new Vector3(-4.5f, 0, -6.5f), new Vector3(10.5f, 0, -6.5f), new Vector3(15.5f, 0, -6.5f), new Vector3(30.5f, 0, -6.5f), new Vector3(35.5f, 0, -6.5f), new Vector3(40.5f, 0, -6.5f), new Vector3(-39.5f, 0, -5.5f), new Vector3(-34.5f, 0, -5.5f), new Vector3(-29.5f, 0, -5.5f), new Vector3(-19.5f, 0, -5.5f), new Vector3(-4.5f, 0, -5.5f), new Vector3(10.5f, 0, -5.5f), new Vector3(15.5f, 0, -5.5f), new Vector3(30.5f, 0, -5.5f), new Vector3(35.5f, 0, -5.5f), new Vector3(40.5f, 0, -5.5f), new Vector3(-39.5f, 0, -4.5f), new Vector3(-38.5f, 0, -4.5f), new Vector3(-37.5f, 0, -4.5f), new Vector3(-36.5f, 0, -4.5f), new Vector3(-35.5f, 0, -4.5f), new Vector3(-34.5f, 0, -4.5f), new Vector3(-33.5f, 0, -4.5f), new Vector3(-32.5f, 0, -4.5f), new Vector3(-31.5f, 0, -4.5f), new Vector3(-30.5f, 0, -4.5f), new Vector3(-29.5f, 0, -4.5f), new Vector3(-24.5f, 0, -4.5f), new Vector3(-23.5f, 0, -4.5f), new Vector3(-22.5f, 0, -4.5f), new Vector3(-21.5f, 0, -4.5f), new Vector3(-20.5f, 0, -4.5f), new Vector3(-19.5f, 0, -4.5f), new Vector3(-18.5f, 0, -4.5f), new Vector3(-17.5f, 0, -4.5f), new Vector3(-16.5f, 0, -4.5f), new Vector3(-15.5f, 0, -4.5f), new Vector3(-14.5f, 0, -4.5f), new Vector3(-13.5f, 0, -4.5f), new Vector3(-12.5f, 0, -4.5f), new Vector3(-11.5f, 0, -4.5f), new Vector3(-10.5f, 0, -4.5f), new Vector3(-9.5f, 0, -4.5f), new Vector3(-4.5f, 0, -4.5f), new Vector3(-3.5f, 0, -4.5f), new Vector3(-2.5f, 0, -4.5f), new Vector3(-1.5f, 0, -4.5f), new Vector3(-0.5f, 0, -4.5f), new Vector3(0.5f, 0, -4.5f), new Vector3(1.5f, 0, -4.5f), new Vector3(2.5f, 0, -4.5f), new Vector3(3.5f, 0, -4.5f), new Vector3(4.5f, 0, -4.5f), new Vector3(5.5f, 0, -4.5f), new Vector3(6.5f, 0, -4.5f), new Vector3(7.5f, 0, -4.5f), new Vector3(8.5f, 0, -4.5f), new Vector3(9.5f, 0, -4.5f), new Vector3(10.5f, 0, -4.5f), new Vector3(15.5f, 0, -4.5f), new Vector3(20.5f, 0, -4.5f), new Vector3(25.5f, 0, -4.5f), new Vector3(30.5f, 0, -4.5f), new Vector3(35.5f, 0, -4.5f), new Vector3(36.5f, 0, -4.5f), new Vector3(37.5f, 0, -4.5f), new Vector3(38.5f, 0, -4.5f), new Vector3(39.5f, 0, -4.5f), new Vector3(40.5f, 0, -4.5f), new Vector3(-39.5f, 0, -3.5f), new Vector3(-29.5f, 0, -3.5f), new Vector3(-24.5f, 0, -3.5f), new Vector3(0.5f, 0, -3.5f), new Vector3(5.5f, 0, -3.5f), new Vector3(20.5f, 0, -3.5f), new Vector3(25.5f, 0, -3.5f), new Vector3(40.5f, 0, -3.5f), new Vector3(-39.5f, 0, -2.5f), new Vector3(-29.5f, 0, -2.5f), new Vector3(-24.5f, 0, -2.5f), new Vector3(0.5f, 0, -2.5f), new Vector3(5.5f, 0, -2.5f), new Vector3(20.5f, 0, -2.5f), new Vector3(25.5f, 0, -2.5f), new Vector3(40.5f, 0, -2.5f), new Vector3(-39.5f, 0, -1.5f), new Vector3(-29.5f, 0, -1.5f), new Vector3(-24.5f, 0, -1.5f), new Vector3(0.5f, 0, -1.5f), new Vector3(5.5f, 0, -1.5f), new Vector3(20.5f, 0, -1.5f), new Vector3(25.5f, 0, -1.5f), new Vector3(40.5f, 0, -1.5f), new Vector3(-39.5f, 0, -0.5f), new Vector3(-29.5f, 0, -0.5f), new Vector3(-24.5f, 0, -0.5f), new Vector3(0.5f, 0, -0.5f), new Vector3(5.5f, 0, -0.5f), new Vector3(20.5f, 0, -0.5f), new Vector3(25.5f, 0, -0.5f), new Vector3(40.5f, 0, -0.5f), new Vector3(-39.5f, 0, 0.5f), new Vector3(-38.5f, 0, 0.5f), new Vector3(-37.5f, 0, 0.5f), new Vector3(-36.5f, 0, 0.5f), new Vector3(-35.5f, 0, 0.5f), new Vector3(-34.5f, 0, 0.5f), new Vector3(-29.5f, 0, 0.5f), new Vector3(-24.5f, 0, 0.5f), new Vector3(-23.5f, 0, 0.5f), new Vector3(-22.5f, 0, 0.5f), new Vector3(-21.5f, 0, 0.5f), new Vector3(-20.5f, 0, 0.5f), new Vector3(-19.5f, 0, 0.5f), new Vector3(-14.5f, 0, 0.5f), new Vector3(-9.5f, 0, 0.5f), new Vector3(-8.5f, 0, 0.5f), new Vector3(-7.5f, 0, 0.5f), new Vector3(-6.5f, 0, 0.5f), new Vector3(-5.5f, 0, 0.5f), new Vector3(-4.5f, 0, 0.5f), new Vector3(-3.5f, 0, 0.5f), new Vector3(-2.5f, 0, 0.5f), new Vector3(-1.5f, 0, 0.5f), new Vector3(-0.5f, 0, 0.5f), new Vector3(0.5f, 0, 0.5f), new Vector3(5.5f, 0, 0.5f), new Vector3(10.5f, 0, 0.5f), new Vector3(15.5f, 0, 0.5f), new Vector3(16.5f, 0, 0.5f), new Vector3(17.5f, 0, 0.5f), new Vector3(18.5f, 0, 0.5f), new Vector3(19.5f, 0, 0.5f), new Vector3(20.5f, 0, 0.5f), new Vector3(25.5f, 0, 0.5f), new Vector3(30.5f, 0, 0.5f), new Vector3(31.5f, 0, 0.5f), new Vector3(32.5f, 0, 0.5f), new Vector3(33.5f, 0, 0.5f), new Vector3(34.5f, 0, 0.5f), new Vector3(35.5f, 0, 0.5f), new Vector3(36.5f, 0, 0.5f), new Vector3(37.5f, 0, 0.5f), new Vector3(38.5f, 0, 0.5f), new Vector3(39.5f, 0, 0.5f), new Vector3(40.5f, 0, 0.5f), new Vector3(-39.5f, 0, 1.5f), new Vector3(-19.5f, 0, 1.5f), new Vector3(-14.5f, 0, 1.5f), new Vector3(-9.5f, 0, 1.5f), new Vector3(0.5f, 0, 1.5f), new Vector3(5.5f, 0, 1.5f), new Vector3(10.5f, 0, 1.5f), new Vector3(15.5f, 0, 1.5f), new Vector3(25.5f, 0, 1.5f), new Vector3(40.5f, 0, 1.5f), new Vector3(-39.5f, 0, 2.5f), new Vector3(-19.5f, 0, 2.5f), new Vector3(-14.5f, 0, 2.5f), new Vector3(-9.5f, 0, 2.5f), new Vector3(0.5f, 0, 2.5f), new Vector3(5.5f, 0, 2.5f), new Vector3(10.5f, 0, 2.5f), new Vector3(15.5f, 0, 2.5f), new Vector3(25.5f, 0, 2.5f), new Vector3(40.5f, 0, 2.5f), new Vector3(-39.5f, 0, 3.5f), new Vector3(-19.5f, 0, 3.5f), new Vector3(-14.5f, 0, 3.5f), new Vector3(-9.5f, 0, 3.5f), new Vector3(0.5f, 0, 3.5f), new Vector3(5.5f, 0, 3.5f), new Vector3(10.5f, 0, 3.5f), new Vector3(15.5f, 0, 3.5f), new Vector3(25.5f, 0, 3.5f), new Vector3(40.5f, 0, 3.5f), new Vector3(-39.5f, 0, 4.5f), new Vector3(-19.5f, 0, 4.5f), new Vector3(-14.5f, 0, 4.5f), new Vector3(-9.5f, 0, 4.5f), new Vector3(0.5f, 0, 4.5f), new Vector3(5.5f, 0, 4.5f), new Vector3(10.5f, 0, 4.5f), new Vector3(15.5f, 0, 4.5f), new Vector3(25.5f, 0, 4.5f), new Vector3(40.5f, 0, 4.5f), new Vector3(-39.5f, 0, 5.5f), new Vector3(-38.5f, 0, 5.5f), new Vector3(-37.5f, 0, 5.5f), new Vector3(-36.5f, 0, 5.5f), new Vector3(-35.5f, 0, 5.5f), new Vector3(-34.5f, 0, 5.5f), new Vector3(-33.5f, 0, 5.5f), new Vector3(-32.5f, 0, 5.5f), new Vector3(-31.5f, 0, 5.5f), new Vector3(-30.5f, 0, 5.5f), new Vector3(-29.5f, 0, 5.5f), new Vector3(-24.5f, 0, 5.5f), new Vector3(-19.5f, 0, 5.5f), new Vector3(-18.5f, 0, 5.5f), new Vector3(-17.5f, 0, 5.5f), new Vector3(-16.5f, 0, 5.5f), new Vector3(-15.5f, 0, 5.5f), new Vector3(-14.5f, 0, 5.5f), new Vector3(-9.5f, 0, 5.5f), new Vector3(-8.5f, 0, 5.5f), new Vector3(-7.5f, 0, 5.5f), new Vector3(-6.5f, 0, 5.5f), new Vector3(-5.5f, 0, 5.5f), new Vector3(-4.5f, 0, 5.5f), new Vector3(0.5f, 0, 5.5f), new Vector3(5.5f, 0, 5.5f), new Vector3(6.5f, 0, 5.5f), new Vector3(7.5f, 0, 5.5f), new Vector3(8.5f, 0, 5.5f), new Vector3(9.5f, 0, 5.5f), new Vector3(10.5f, 0, 5.5f), new Vector3(15.5f, 0, 5.5f), new Vector3(16.5f, 0, 5.5f), new Vector3(17.5f, 0, 5.5f), new Vector3(18.5f, 0, 5.5f), new Vector3(19.5f, 0, 5.5f), new Vector3(20.5f, 0, 5.5f), new Vector3(21.5f, 0, 5.5f), new Vector3(22.5f, 0, 5.5f), new Vector3(23.5f, 0, 5.5f), new Vector3(24.5f, 0, 5.5f), new Vector3(25.5f, 0, 5.5f), new Vector3(26.5f, 0, 5.5f), new Vector3(27.5f, 0, 5.5f), new Vector3(28.5f, 0, 5.5f), new Vector3(29.5f, 0, 5.5f), new Vector3(30.5f, 0, 5.5f), new Vector3(35.5f, 0, 5.5f), new Vector3(36.5f, 0, 5.5f), new Vector3(37.5f, 0, 5.5f), new Vector3(38.5f, 0, 5.5f), new Vector3(39.5f, 0, 5.5f), new Vector3(40.5f, 0, 5.5f), new Vector3(-39.5f, 0, 6.5f), new Vector3(-24.5f, 0, 6.5f), new Vector3(-19.5f, 0, 6.5f), new Vector3(-14.5f, 0, 6.5f), new Vector3(10.5f, 0, 6.5f), new Vector3(15.5f, 0, 6.5f), new Vector3(35.5f, 0, 6.5f), new Vector3(40.5f, 0, 6.5f), new Vector3(-39.5f, 0, 7.5f), new Vector3(-24.5f, 0, 7.5f), new Vector3(-19.5f, 0, 7.5f), new Vector3(-14.5f, 0, 7.5f), new Vector3(10.5f, 0, 7.5f), new Vector3(15.5f, 0, 7.5f), new Vector3(35.5f, 0, 7.5f), new Vector3(40.5f, 0, 7.5f), new Vector3(-39.5f, 0, 8.5f), new Vector3(-24.5f, 0, 8.5f), new Vector3(-19.5f, 0, 8.5f), new Vector3(-14.5f, 0, 8.5f), new Vector3(10.5f, 0, 8.5f), new Vector3(15.5f, 0, 8.5f), new Vector3(35.5f, 0, 8.5f), new Vector3(40.5f, 0, 8.5f), new Vector3(-39.5f, 0, 9.5f), new Vector3(-24.5f, 0, 9.5f), new Vector3(-19.5f, 0, 9.5f), new Vector3(-14.5f, 0, 9.5f), new Vector3(10.5f, 0, 9.5f), new Vector3(15.5f, 0, 9.5f), new Vector3(35.5f, 0, 9.5f), new Vector3(40.5f, 0, 9.5f), new Vector3(-39.5f, 0, 10.5f), new Vector3(-38.5f, 0, 10.5f), new Vector3(-37.5f, 0, 10.5f), new Vector3(-36.5f, 0, 10.5f), new Vector3(-35.5f, 0, 10.5f), new Vector3(-34.5f, 0, 10.5f), new Vector3(-33.5f, 0, 10.5f), new Vector3(-32.5f, 0, 10.5f), new Vector3(-31.5f, 0, 10.5f), new Vector3(-30.5f, 0, 10.5f), new Vector3(-29.5f, 0, 10.5f), new Vector3(-24.5f, 0, 10.5f), new Vector3(-23.5f, 0, 10.5f), new Vector3(-22.5f, 0, 10.5f), new Vector3(-21.5f, 0, 10.5f), new Vector3(-20.5f, 0, 10.5f), new Vector3(-19.5f, 0, 10.5f), new Vector3(-14.5f, 0, 10.5f), new Vector3(-9.5f, 0, 10.5f), new Vector3(-8.5f, 0, 10.5f), new Vector3(-7.5f, 0, 10.5f), new Vector3(-6.5f, 0, 10.5f), new Vector3(-5.5f, 0, 10.5f), new Vector3(-4.5f, 0, 10.5f), new Vector3(0.5f, 0, 10.5f), new Vector3(5.5f, 0, 10.5f), new Vector3(10.5f, 0, 10.5f), new Vector3(11.5f, 0, 10.5f), new Vector3(12.5f, 0, 10.5f), new Vector3(13.5f, 0, 10.5f), new Vector3(14.5f, 0, 10.5f), new Vector3(15.5f, 0, 10.5f), new Vector3(16.5f, 0, 10.5f), new Vector3(17.5f, 0, 10.5f), new Vector3(18.5f, 0, 10.5f), new Vector3(19.5f, 0, 10.5f), new Vector3(20.5f, 0, 10.5f), new Vector3(25.5f, 0, 10.5f), new Vector3(30.5f, 0, 10.5f), new Vector3(31.5f, 0, 10.5f), new Vector3(32.5f, 0, 10.5f), new Vector3(33.5f, 0, 10.5f), new Vector3(34.5f, 0, 10.5f), new Vector3(35.5f, 0, 10.5f), new Vector3(40.5f, 0, 10.5f), new Vector3(-39.5f, 0, 11.5f), new Vector3(-29.5f, 0, 11.5f), new Vector3(-4.5f, 0, 11.5f), new Vector3(0.5f, 0, 11.5f), new Vector3(5.5f, 0, 11.5f), new Vector3(20.5f, 0, 11.5f), new Vector3(25.5f, 0, 11.5f), new Vector3(40.5f, 0, 11.5f), new Vector3(-39.5f, 0, 12.5f), new Vector3(-29.5f, 0, 12.5f), new Vector3(-4.5f, 0, 12.5f), new Vector3(0.5f, 0, 12.5f), new Vector3(5.5f, 0, 12.5f), new Vector3(20.5f, 0, 12.5f), new Vector3(25.5f, 0, 12.5f), new Vector3(40.5f, 0, 12.5f), new Vector3(-39.5f, 0, 13.5f), new Vector3(-29.5f, 0, 13.5f), new Vector3(-4.5f, 0, 13.5f), new Vector3(0.5f, 0, 13.5f), new Vector3(5.5f, 0, 13.5f), new Vector3(20.5f, 0, 13.5f), new Vector3(25.5f, 0, 13.5f), new Vector3(40.5f, 0, 13.5f), new Vector3(-39.5f, 0, 14.5f), new Vector3(-29.5f, 0, 14.5f), new Vector3(-4.5f, 0, 14.5f), new Vector3(0.5f, 0, 14.5f), new Vector3(5.5f, 0, 14.5f), new Vector3(20.5f, 0, 14.5f), new Vector3(25.5f, 0, 14.5f), new Vector3(40.5f, 0, 14.5f), new Vector3(-39.5f, 0, 15.5f), new Vector3(-38.5f, 0, 15.5f), new Vector3(-37.5f, 0, 15.5f), new Vector3(-36.5f, 0, 15.5f), new Vector3(-35.5f, 0, 15.5f), new Vector3(-34.5f, 0, 15.5f), new Vector3(-29.5f, 0, 15.5f), new Vector3(-28.5f, 0, 15.5f), new Vector3(-27.5f, 0, 15.5f), new Vector3(-26.5f, 0, 15.5f), new Vector3(-25.5f, 0, 15.5f), new Vector3(-24.5f, 0, 15.5f), new Vector3(-19.5f, 0, 15.5f), new Vector3(-14.5f, 0, 15.5f), new Vector3(-13.5f, 0, 15.5f), new Vector3(-12.5f, 0, 15.5f), new Vector3(-11.5f, 0, 15.5f), new Vector3(-10.5f, 0, 15.5f), new Vector3(-9.5f, 0, 15.5f), new Vector3(-8.5f, 0, 15.5f), new Vector3(-7.5f, 0, 15.5f), new Vector3(-6.5f, 0, 15.5f), new Vector3(-5.5f, 0, 15.5f), new Vector3(-4.5f, 0, 15.5f), new Vector3(-3.5f, 0, 15.5f), new Vector3(-2.5f, 0, 15.5f), new Vector3(-1.5f, 0, 15.5f), new Vector3(-0.5f, 0, 15.5f), new Vector3(0.5f, 0, 15.5f), new Vector3(1.5f, 0, 15.5f), new Vector3(2.5f, 0, 15.5f), new Vector3(3.5f, 0, 15.5f), new Vector3(4.5f, 0, 15.5f), new Vector3(5.5f, 0, 15.5f), new Vector3(10.5f, 0, 15.5f), new Vector3(15.5f, 0, 15.5f), new Vector3(20.5f, 0, 15.5f), new Vector3(21.5f, 0, 15.5f), new Vector3(22.5f, 0, 15.5f), new Vector3(23.5f, 0, 15.5f), new Vector3(24.5f, 0, 15.5f), new Vector3(25.5f, 0, 15.5f), new Vector3(30.5f, 0, 15.5f), new Vector3(35.5f, 0, 15.5f), new Vector3(40.5f, 0, 15.5f), new Vector3(-39.5f, 0, 16.5f), new Vector3(-19.5f, 0, 16.5f), new Vector3(-9.5f, 0, 16.5f), new Vector3(-4.5f, 0, 16.5f), new Vector3(0.5f, 0, 16.5f), new Vector3(10.5f, 0, 16.5f), new Vector3(15.5f, 0, 16.5f), new Vector3(20.5f, 0, 16.5f), new Vector3(30.5f, 0, 16.5f), new Vector3(35.5f, 0, 16.5f), new Vector3(40.5f, 0, 16.5f), new Vector3(-39.5f, 0, 17.5f), new Vector3(-19.5f, 0, 17.5f), new Vector3(-9.5f, 0, 17.5f), new Vector3(-4.5f, 0, 17.5f), new Vector3(0.5f, 0, 17.5f), new Vector3(10.5f, 0, 17.5f), new Vector3(15.5f, 0, 17.5f), new Vector3(20.5f, 0, 17.5f), new Vector3(30.5f, 0, 17.5f), new Vector3(35.5f, 0, 17.5f), new Vector3(40.5f, 0, 17.5f), new Vector3(-39.5f, 0, 18.5f), new Vector3(-19.5f, 0, 18.5f), new Vector3(-9.5f, 0, 18.5f), new Vector3(-4.5f, 0, 18.5f), new Vector3(0.5f, 0, 18.5f), new Vector3(10.5f, 0, 18.5f), new Vector3(15.5f, 0, 18.5f), new Vector3(20.5f, 0, 18.5f), new Vector3(30.5f, 0, 18.5f), new Vector3(35.5f, 0, 18.5f), new Vector3(40.5f, 0, 18.5f), new Vector3(-39.5f, 0, 19.5f), new Vector3(-19.5f, 0, 19.5f), new Vector3(-9.5f, 0, 19.5f), new Vector3(-4.5f, 0, 19.5f), new Vector3(0.5f, 0, 19.5f), new Vector3(10.5f, 0, 19.5f), new Vector3(15.5f, 0, 19.5f), new Vector3(20.5f, 0, 19.5f), new Vector3(30.5f, 0, 19.5f), new Vector3(35.5f, 0, 19.5f), new Vector3(40.5f, 0, 19.5f), new Vector3(-39.5f, 0, 20.5f), new Vector3(-34.5f, 0, 20.5f), new Vector3(-29.5f, 0, 20.5f), new Vector3(-28.5f, 0, 20.5f), new Vector3(-27.5f, 0, 20.5f), new Vector3(-26.5f, 0, 20.5f), new Vector3(-25.5f, 0, 20.5f), new Vector3(-24.5f, 0, 20.5f), new Vector3(-23.5f, 0, 20.5f), new Vector3(-22.5f, 0, 20.5f), new Vector3(-21.5f, 0, 20.5f), new Vector3(-20.5f, 0, 20.5f), new Vector3(-19.5f, 0, 20.5f), new Vector3(-18.5f, 0, 20.5f), new Vector3(-17.5f, 0, 20.5f), new Vector3(-16.5f, 0, 20.5f), new Vector3(-15.5f, 0, 20.5f), new Vector3(-14.5f, 0, 20.5f), new Vector3(-13.5f, 0, 20.5f), new Vector3(-12.5f, 0, 20.5f), new Vector3(-11.5f, 0, 20.5f), new Vector3(-10.5f, 0, 20.5f), new Vector3(-9.5f, 0, 20.5f), new Vector3(-4.5f, 0, 20.5f), new Vector3(0.5f, 0, 20.5f), new Vector3(5.5f, 0, 20.5f), new Vector3(6.5f, 0, 20.5f), new Vector3(7.5f, 0, 20.5f), new Vector3(8.5f, 0, 20.5f), new Vector3(9.5f, 0, 20.5f), new Vector3(10.5f, 0, 20.5f), new Vector3(11.5f, 0, 20.5f), new Vector3(12.5f, 0, 20.5f), new Vector3(13.5f, 0, 20.5f), new Vector3(14.5f, 0, 20.5f), new Vector3(15.5f, 0, 20.5f), new Vector3(16.5f, 0, 20.5f), new Vector3(17.5f, 0, 20.5f), new Vector3(18.5f, 0, 20.5f), new Vector3(19.5f, 0, 20.5f), new Vector3(20.5f, 0, 20.5f), new Vector3(21.5f, 0, 20.5f), new Vector3(22.5f, 0, 20.5f), new Vector3(23.5f, 0, 20.5f), new Vector3(24.5f, 0, 20.5f), new Vector3(25.5f, 0, 20.5f), new Vector3(26.5f, 0, 20.5f), new Vector3(27.5f, 0, 20.5f), new Vector3(28.5f, 0, 20.5f), new Vector3(29.5f, 0, 20.5f), new Vector3(30.5f, 0, 20.5f), new Vector3(31.5f, 0, 20.5f), new Vector3(32.5f, 0, 20.5f), new Vector3(33.5f, 0, 20.5f), new Vector3(34.5f, 0, 20.5f), new Vector3(35.5f, 0, 20.5f), new Vector3(40.5f, 0, 20.5f), new Vector3(-39.5f, 0, 21.5f), new Vector3(-34.5f, 0, 21.5f), new Vector3(-19.5f, 0, 21.5f), new Vector3(-14.5f, 0, 21.5f), new Vector3(0.5f, 0, 21.5f), new Vector3(10.5f, 0, 21.5f), new Vector3(30.5f, 0, 21.5f), new Vector3(40.5f, 0, 21.5f), new Vector3(-39.5f, 0, 22.5f), new Vector3(-34.5f, 0, 22.5f), new Vector3(-19.5f, 0, 22.5f), new Vector3(-14.5f, 0, 22.5f), new Vector3(0.5f, 0, 22.5f), new Vector3(10.5f, 0, 22.5f), new Vector3(30.5f, 0, 22.5f), new Vector3(40.5f, 0, 22.5f), new Vector3(-39.5f, 0, 23.5f), new Vector3(-34.5f, 0, 23.5f), new Vector3(-19.5f, 0, 23.5f), new Vector3(-14.5f, 0, 23.5f), new Vector3(0.5f, 0, 23.5f), new Vector3(10.5f, 0, 23.5f), new Vector3(30.5f, 0, 23.5f), new Vector3(40.5f, 0, 23.5f), new Vector3(-39.5f, 0, 24.5f), new Vector3(-34.5f, 0, 24.5f), new Vector3(-19.5f, 0, 24.5f), new Vector3(-14.5f, 0, 24.5f), new Vector3(0.5f, 0, 24.5f), new Vector3(10.5f, 0, 24.5f), new Vector3(30.5f, 0, 24.5f), new Vector3(40.5f, 0, 24.5f), new Vector3(-39.5f, 0, 25.5f), new Vector3(-38.5f, 0, 25.5f), new Vector3(-37.5f, 0, 25.5f), new Vector3(-36.5f, 0, 25.5f), new Vector3(-35.5f, 0, 25.5f), new Vector3(-34.5f, 0, 25.5f), new Vector3(-33.5f, 0, 25.5f), new Vector3(-32.5f, 0, 25.5f), new Vector3(-31.5f, 0, 25.5f), new Vector3(-30.5f, 0, 25.5f), new Vector3(-29.5f, 0, 25.5f), new Vector3(-24.5f, 0, 25.5f), new Vector3(-23.5f, 0, 25.5f), new Vector3(-22.5f, 0, 25.5f), new Vector3(-21.5f, 0, 25.5f), new Vector3(-20.5f, 0, 25.5f), new Vector3(-19.5f, 0, 25.5f), new Vector3(-14.5f, 0, 25.5f), new Vector3(-9.5f, 0, 25.5f), new Vector3(-4.5f, 0, 25.5f), new Vector3(0.5f, 0, 25.5f), new Vector3(5.5f, 0, 25.5f), new Vector3(10.5f, 0, 25.5f), new Vector3(15.5f, 0, 25.5f), new Vector3(20.5f, 0, 25.5f), new Vector3(21.5f, 0, 25.5f), new Vector3(22.5f, 0, 25.5f), new Vector3(23.5f, 0, 25.5f), new Vector3(24.5f, 0, 25.5f), new Vector3(25.5f, 0, 25.5f), new Vector3(26.5f, 0, 25.5f), new Vector3(27.5f, 0, 25.5f), new Vector3(28.5f, 0, 25.5f), new Vector3(29.5f, 0, 25.5f), new Vector3(30.5f, 0, 25.5f), new Vector3(35.5f, 0, 25.5f), new Vector3(40.5f, 0, 25.5f), new Vector3(-39.5f, 0, 26.5f), new Vector3(-34.5f, 0, 26.5f), new Vector3(-19.5f, 0, 26.5f), new Vector3(-9.5f, 0, 26.5f), new Vector3(-4.5f, 0, 26.5f), new Vector3(0.5f, 0, 26.5f), new Vector3(5.5f, 0, 26.5f), new Vector3(15.5f, 0, 26.5f), new Vector3(20.5f, 0, 26.5f), new Vector3(25.5f, 0, 26.5f), new Vector3(35.5f, 0, 26.5f), new Vector3(40.5f, 0, 26.5f), new Vector3(-39.5f, 0, 27.5f), new Vector3(-34.5f, 0, 27.5f), new Vector3(-19.5f, 0, 27.5f), new Vector3(-9.5f, 0, 27.5f), new Vector3(-4.5f, 0, 27.5f), new Vector3(0.5f, 0, 27.5f), new Vector3(5.5f, 0, 27.5f), new Vector3(15.5f, 0, 27.5f), new Vector3(20.5f, 0, 27.5f), new Vector3(25.5f, 0, 27.5f), new Vector3(35.5f, 0, 27.5f), new Vector3(40.5f, 0, 27.5f), new Vector3(-39.5f, 0, 28.5f), new Vector3(-34.5f, 0, 28.5f), new Vector3(-19.5f, 0, 28.5f), new Vector3(-9.5f, 0, 28.5f), new Vector3(-4.5f, 0, 28.5f), new Vector3(0.5f, 0, 28.5f), new Vector3(5.5f, 0, 28.5f), new Vector3(15.5f, 0, 28.5f), new Vector3(20.5f, 0, 28.5f), new Vector3(25.5f, 0, 28.5f), new Vector3(35.5f, 0, 28.5f), new Vector3(40.5f, 0, 28.5f), new Vector3(-39.5f, 0, 29.5f), new Vector3(-34.5f, 0, 29.5f), new Vector3(-19.5f, 0, 29.5f), new Vector3(-9.5f, 0, 29.5f), new Vector3(-4.5f, 0, 29.5f), new Vector3(0.5f, 0, 29.5f), new Vector3(5.5f, 0, 29.5f), new Vector3(15.5f, 0, 29.5f), new Vector3(20.5f, 0, 29.5f), new Vector3(25.5f, 0, 29.5f), new Vector3(35.5f, 0, 29.5f), new Vector3(40.5f, 0, 29.5f), new Vector3(-39.5f, 0, 30.5f), new Vector3(-34.5f, 0, 30.5f), new Vector3(-33.5f, 0, 30.5f), new Vector3(-32.5f, 0, 30.5f), new Vector3(-31.5f, 0, 30.5f), new Vector3(-30.5f, 0, 30.5f), new Vector3(-29.5f, 0, 30.5f), new Vector3(-28.5f, 0, 30.5f), new Vector3(-27.5f, 0, 30.5f), new Vector3(-26.5f, 0, 30.5f), new Vector3(-25.5f, 0, 30.5f), new Vector3(-24.5f, 0, 30.5f), new Vector3(-23.5f, 0, 30.5f), new Vector3(-22.5f, 0, 30.5f), new Vector3(-21.5f, 0, 30.5f), new Vector3(-20.5f, 0, 30.5f), new Vector3(-19.5f, 0, 30.5f), new Vector3(-18.5f, 0, 30.5f), new Vector3(-17.5f, 0, 30.5f), new Vector3(-16.5f, 0, 30.5f), new Vector3(-15.5f, 0, 30.5f), new Vector3(-14.5f, 0, 30.5f), new Vector3(-13.5f, 0, 30.5f), new Vector3(-12.5f, 0, 30.5f), new Vector3(-11.5f, 0, 30.5f), new Vector3(-10.5f, 0, 30.5f), new Vector3(-9.5f, 0, 30.5f), new Vector3(-8.5f, 0, 30.5f), new Vector3(-7.5f, 0, 30.5f), new Vector3(-6.5f, 0, 30.5f), new Vector3(-5.5f, 0, 30.5f), new Vector3(-4.5f, 0, 30.5f), new Vector3(0.5f, 0, 30.5f), new Vector3(1.5f, 0, 30.5f), new Vector3(2.5f, 0, 30.5f), new Vector3(3.5f, 0, 30.5f), new Vector3(4.5f, 0, 30.5f), new Vector3(5.5f, 0, 30.5f), new Vector3(10.5f, 0, 30.5f), new Vector3(11.5f, 0, 30.5f), new Vector3(12.5f, 0, 30.5f), new Vector3(13.5f, 0, 30.5f), new Vector3(14.5f, 0, 30.5f), new Vector3(15.5f, 0, 30.5f), new Vector3(16.5f, 0, 30.5f), new Vector3(17.5f, 0, 30.5f), new Vector3(18.5f, 0, 30.5f), new Vector3(19.5f, 0, 30.5f), new Vector3(20.5f, 0, 30.5f), new Vector3(25.5f, 0, 30.5f), new Vector3(26.5f, 0, 30.5f), new Vector3(27.5f, 0, 30.5f), new Vector3(28.5f, 0, 30.5f), new Vector3(29.5f, 0, 30.5f), new Vector3(30.5f, 0, 30.5f), new Vector3(35.5f, 0, 30.5f), new Vector3(36.5f, 0, 30.5f), new Vector3(37.5f, 0, 30.5f), new Vector3(38.5f, 0, 30.5f), new Vector3(39.5f, 0, 30.5f), new Vector3(40.5f, 0, 30.5f), new Vector3(-39.5f, 0, 31.5f), new Vector3(-19.5f, 0, 31.5f), new Vector3(-9.5f, 0, 31.5f), new Vector3(20.5f, 0, 31.5f), new Vector3(40.5f, 0, 31.5f), new Vector3(-39.5f, 0, 32.5f), new Vector3(-19.5f, 0, 32.5f), new Vector3(-9.5f, 0, 32.5f), new Vector3(20.5f, 0, 32.5f), new Vector3(40.5f, 0, 32.5f), new Vector3(-39.5f, 0, 33.5f), new Vector3(-19.5f, 0, 33.5f), new Vector3(-9.5f, 0, 33.5f), new Vector3(20.5f, 0, 33.5f), new Vector3(40.5f, 0, 33.5f), new Vector3(-39.5f, 0, 34.5f), new Vector3(-19.5f, 0, 34.5f), new Vector3(-9.5f, 0, 34.5f), new Vector3(20.5f, 0, 34.5f), new Vector3(40.5f, 0, 34.5f), new Vector3(-39.5f, 0, 35.5f), new Vector3(-38.5f, 0, 35.5f), new Vector3(-37.5f, 0, 35.5f), new Vector3(-36.5f, 0, 35.5f), new Vector3(-35.5f, 0, 35.5f), new Vector3(-34.5f, 0, 35.5f), new Vector3(-29.5f, 0, 35.5f), new Vector3(-28.5f, 0, 35.5f), new Vector3(-27.5f, 0, 35.5f), new Vector3(-26.5f, 0, 35.5f), new Vector3(-25.5f, 0, 35.5f), new Vector3(-24.5f, 0, 35.5f), new Vector3(-19.5f, 0, 35.5f), new Vector3(-14.5f, 0, 35.5f), new Vector3(-13.5f, 0, 35.5f), new Vector3(-12.5f, 0, 35.5f), new Vector3(-11.5f, 0, 35.5f), new Vector3(-10.5f, 0, 35.5f), new Vector3(-9.5f, 0, 35.5f), new Vector3(-4.5f, 0, 35.5f), new Vector3(0.5f, 0, 35.5f), new Vector3(1.5f, 0, 35.5f), new Vector3(2.5f, 0, 35.5f), new Vector3(3.5f, 0, 35.5f), new Vector3(4.5f, 0, 35.5f), new Vector3(5.5f, 0, 35.5f), new Vector3(6.5f, 0, 35.5f), new Vector3(7.5f, 0, 35.5f), new Vector3(8.5f, 0, 35.5f), new Vector3(9.5f, 0, 35.5f), new Vector3(10.5f, 0, 35.5f), new Vector3(15.5f, 0, 35.5f), new Vector3(16.5f, 0, 35.5f), new Vector3(17.5f, 0, 35.5f), new Vector3(18.5f, 0, 35.5f), new Vector3(19.5f, 0, 35.5f), new Vector3(20.5f, 0, 35.5f), new Vector3(21.5f, 0, 35.5f), new Vector3(22.5f, 0, 35.5f), new Vector3(23.5f, 0, 35.5f), new Vector3(24.5f, 0, 35.5f), new Vector3(25.5f, 0, 35.5f), new Vector3(26.5f, 0, 35.5f), new Vector3(27.5f, 0, 35.5f), new Vector3(28.5f, 0, 35.5f), new Vector3(29.5f, 0, 35.5f), new Vector3(30.5f, 0, 35.5f), new Vector3(35.5f, 0, 35.5f), new Vector3(36.5f, 0, 35.5f), new Vector3(37.5f, 0, 35.5f), new Vector3(38.5f, 0, 35.5f), new Vector3(39.5f, 0, 35.5f), new Vector3(40.5f, 0, 35.5f), new Vector3(-39.5f, 0, 36.5f), new Vector3(-24.5f, 0, 36.5f), new Vector3(-4.5f, 0, 36.5f), new Vector3(10.5f, 0, 36.5f), new Vector3(40.5f, 0, 36.5f), new Vector3(-39.5f, 0, 37.5f), new Vector3(-24.5f, 0, 37.5f), new Vector3(-4.5f, 0, 37.5f), new Vector3(10.5f, 0, 37.5f), new Vector3(40.5f, 0, 37.5f), new Vector3(-39.5f, 0, 38.5f), new Vector3(-24.5f, 0, 38.5f), new Vector3(-4.5f, 0, 38.5f), new Vector3(10.5f, 0, 38.5f), new Vector3(40.5f, 0, 38.5f), new Vector3(-39.5f, 0, 39.5f), new Vector3(-24.5f, 0, 39.5f), new Vector3(-4.5f, 0, 39.5f), new Vector3(10.5f, 0, 39.5f), new Vector3(40.5f, 0, 39.5f), new Vector3(-39.5f, 0, 40.5f), new Vector3(-38.5f, 0, 40.5f), new Vector3(-37.5f, 0, 40.5f), new Vector3(-36.5f, 0, 40.5f), new Vector3(-35.5f, 0, 40.5f), new Vector3(-34.5f, 0, 40.5f), new Vector3(-33.5f, 0, 40.5f), new Vector3(-32.5f, 0, 40.5f), new Vector3(-31.5f, 0, 40.5f), new Vector3(-30.5f, 0, 40.5f), new Vector3(-29.5f, 0, 40.5f), new Vector3(-28.5f, 0, 40.5f), new Vector3(-27.5f, 0, 40.5f), new Vector3(-26.5f, 0, 40.5f), new Vector3(-25.5f, 0, 40.5f), new Vector3(-24.5f, 0, 40.5f), new Vector3(-23.5f, 0, 40.5f), new Vector3(-22.5f, 0, 40.5f), new Vector3(-21.5f, 0, 40.5f), new Vector3(-20.5f, 0, 40.5f), new Vector3(-19.5f, 0, 40.5f), new Vector3(-18.5f, 0, 40.5f), new Vector3(-17.5f, 0, 40.5f), new Vector3(-16.5f, 0, 40.5f), new Vector3(-15.5f, 0, 40.5f), new Vector3(-14.5f, 0, 40.5f), new Vector3(-13.5f, 0, 40.5f), new Vector3(-12.5f, 0, 40.5f), new Vector3(-11.5f, 0, 40.5f), new Vector3(-10.5f, 0, 40.5f), new Vector3(-9.5f, 0, 40.5f), new Vector3(-8.5f, 0, 40.5f), new Vector3(-7.5f, 0, 40.5f), new Vector3(-6.5f, 0, 40.5f), new Vector3(-5.5f, 0, 40.5f), new Vector3(-4.5f, 0, 40.5f), new Vector3(-3.5f, 0, 40.5f), new Vector3(-2.5f, 0, 40.5f), new Vector3(-1.5f, 0, 40.5f), new Vector3(-0.5f, 0, 40.5f), new Vector3(0.5f, 0, 40.5f), new Vector3(1.5f, 0, 40.5f), new Vector3(2.5f, 0, 40.5f), new Vector3(3.5f, 0, 40.5f), new Vector3(4.5f, 0, 40.5f), new Vector3(5.5f, 0, 40.5f), new Vector3(6.5f, 0, 40.5f), new Vector3(7.5f, 0, 40.5f), new Vector3(8.5f, 0, 40.5f), new Vector3(9.5f, 0, 40.5f), new Vector3(10.5f, 0, 40.5f), new Vector3(11.5f, 0, 40.5f), new Vector3(12.5f, 0, 40.5f), new Vector3(13.5f, 0, 40.5f), new Vector3(14.5f, 0, 40.5f), new Vector3(15.5f, 0, 40.5f), new Vector3(16.5f, 0, 40.5f), new Vector3(17.5f, 0, 40.5f), new Vector3(18.5f, 0, 40.5f), new Vector3(19.5f, 0, 40.5f), new Vector3(20.5f, 0, 40.5f), new Vector3(21.5f, 0, 40.5f), new Vector3(22.5f, 0, 40.5f), new Vector3(23.5f, 0, 40.5f), new Vector3(24.5f, 0, 40.5f), new Vector3(25.5f, 0, 40.5f), new Vector3(26.5f, 0, 40.5f), new Vector3(27.5f, 0, 40.5f), new Vector3(28.5f, 0, 40.5f), new Vector3(29.5f, 0, 40.5f), new Vector3(30.5f, 0, 40.5f), new Vector3(31.5f, 0, 40.5f), new Vector3(32.5f, 0, 40.5f), new Vector3(33.5f, 0, 40.5f), new Vector3(34.5f, 0, 40.5f), new Vector3(35.5f, 0, 40.5f), new Vector3(36.5f, 0, 40.5f), new Vector3(37.5f, 0, 40.5f), new Vector3(38.5f, 0, 40.5f), new Vector3(39.5f, 0, 40.5f), new Vector3(40.5f, 0, 40.5f) };

            float step = 0.3f;
            int amt = 20;

            System.Numerics.Vector3 pos = selectedContainer.GetPosition();
            for (int i = 0; i < amt; i++)
            {
                pos.Z += step;

                DuplicateSelectedContainer();
                editableContainers.Last().SetPosition(pos);
            }
        }

        private void ExportTexture(ulong blobid, string outpath) // Currently SB09Wii only but who cares I guess
        {
            Rendering.Base.BaseClass blob = renderer.GetAssetFromRawblobPool(blobid);
            if (blob == null) return;

            Bitmap map = SB09WiiTPL.BitmapsFromRawblob(blob.asset.data.ToArray()).Last();
            map.Save(outpath);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportWindow export = new();
            DialogResult result = export.ShowDialog(this);

            if (result != DialogResult.OK) return;
            
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = AssimpFilterBuilder.BuildSaveFileDialogFilter();

            DialogResult fileresult = dialog.ShowDialog();

            if (fileresult != DialogResult.OK) return;

            List<GeometryObject> objects = new();

            foreach(EditableContainer container in selectedContainers)
            {
                if (!container.isInstanced) continue;

                ulong modelid = container.GetModelInstance().modelPrototypeID;
                Rendering.Base.BaseClass model = renderer.GetAssetFromModelPool(modelid);

                if (model == null) continue;

                foreach(ulong aGeomID in ((Rendering.Base.ModelBase)model).GetGeometryIDs())
                {
                    Rendering.Base.BaseClass geom = renderer.GetAssetFromGeometryPool(aGeomID);
                    if (geom == null) continue;

                    ulong diffuseID = ((Rendering.Base.GeometryBase)geom).textureSet.diffuseMapID;
                    string diffusetexturepath = "";
                    if (diffuseID != 0)
                    {
                        diffusetexturepath = Path.GetDirectoryName(dialog.FileName) + "/diffuse " +
                            geom.asset.uidSelf.ToString("X16") + ".png";
                        ExportTexture(diffuseID, diffusetexturepath);
                    }

                    ulong lightMapID = ((Rendering.Base.GeometryBase)geom).textureSet.lightMapID;
                    string lightmappath = "";
                    if (diffuseID != 0)
                    {
                        lightmappath = Path.GetDirectoryName(dialog.FileName) + "/lightmap " +
                            geom.asset.uidSelf.ToString("X16") + ".png";
                        ExportTexture(lightMapID, lightmappath);
                    }

                    objects.Add(new GeometryObject(((Rendering.Base.GeometryBase)geom).vertexdata,
                        ((Rendering.Base.GeometryBase)geom).indexdata,
                        handler.GetName(geom.asset.uidSelf) + " " + geom.asset.uidSelf.ToString("X16"),
                        diffusetexturepath, lightmappath));
                }
            }

            aGeometryExporter.Export(objects, dialog.FileName);
        }
    }

}
