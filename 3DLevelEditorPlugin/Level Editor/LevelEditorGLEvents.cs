using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HoArchive;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Plankton.EditingTools;
using Plankton.Custom_Controls;
using System.Drawing;
using Plankton.Special_Editors.Level_Editor.EditableContainers;

namespace Plankton.Special_Editors.Level_Editor
{
    public partial class LevelEditor
    {
        private void renderer_MouseUp(object sender, MouseEventArgs e)
        {
            if (!shouldHandleMouseUp)
            {
                shouldHandleMouseUp = true; // Mouse up (selection) shouldn't be handled if mouse click already did something.
                return;
            }
            if (e.Button != MouseButtons.Left) { return; }

            
            if (renderer.mouseInfo.isBoxSelecting)
            {
                Rectangle selectionRectangle = renderer.CreateRectangle(renderer.mouseInfo.mouseDownPoint, e.Location);

                MultiSelect(selectionRectangle);
            }
            else
            {
                SingleSelect(e.X, e.Y);
            }
            
        }

        private void MultiSelect(Rectangle rectangle)
        {

            HashSet<uint> sels = renderer.ReadSelectionIds(rectangle);

            if (sels.Count() == 0) return;

            if (!renderer.ShiftPressed())
            {
                selectedContainers.Clear();
            }
            List<EditableContainer> containerstoadd = new();


            foreach (uint sel in sels)
            {
                if (sel == -1) { continue; }
                if (sel >= editableContainers.Count)
                {
                    // Indicates a more serious error in the render pipeline
                    MessageBox.Show("Selection index higher than superInstance count! " + sel.ToString());
                    continue;
                }


                containerstoadd.Add(editableContainers[(int)sel]);
                
            }

            AddSelectedContainers(containerstoadd);
        }

        private void SingleSelect(int x, int y)
        {
            int sel = renderer.ReadSelectionIndex(x, y);

            if (sel == -1) { return; }
            if (sel >= editableContainers.Count)
            {
                // Indicates a more serious error in the render pipeline
                MessageBox.Show("Selection index higher than superInstance count! " + sel.ToString());
                return;
            }

            if (renderer.ShiftPressed())
            {
                if ((selectedContainers.Count > 0) && editableContainers[sel] == selectedContainers.Last()) RemoveSelectedContainer(selectedContainers.Last());

                else AddSelectedContainer(editableContainers[sel]); // Handles if already contains itself
            }

            else SetSelectedContainer(editableContainers[sel]);
        }

        private void renderer_MouseMove(object sender, MouseEventArgs e)
        {
            lrsEditor.Update(GetCursorRay(), renderer.camera.GetNormal());
            
        }


        private void renderer_MouseClick(object sender, MouseEventArgs e)
        {
            if (lrsEditor.IsEditing())
            {
                shouldHandleMouseUp = false;
                if (e.Button == MouseButtons.Left) { lrsEditor.ApplyEdit(); }
                if (e.Button == MouseButtons.Right) { lrsEditor.CancelEdit(); shouldCancelRightClickMenuOpening = true; }
                return;
            }

        }




    }
}
