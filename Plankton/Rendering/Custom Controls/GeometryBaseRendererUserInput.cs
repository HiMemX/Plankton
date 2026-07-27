using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using Plankton.EditingTools;
using Plankton.Rendering;

namespace Plankton.Custom_Controls
{
    public partial class GeometryBaseRenderer
    {
        private void HandleMouseMovement()
        {
            if (!handleCameraMovement) return;

            float mouseMoveFactor = camera.dist * 1 / 5f * 3.0f * dt * movementSpeed;
            if (mouseInfo.middleMouseDown && ((Control.ModifierKeys & Keys.Shift) != 0))
            {
                camera.RelativeMove(Vector3.UnitY * mouseInfo.speedy * mouseMoveFactor + Vector3.UnitZ * mouseInfo.speedx * mouseMoveFactor);
            }
            float mouseRotateFactor = 4f * dt * rotationSpeed;
            if (mouseInfo.middleMouseDown && ((Control.ModifierKeys & Keys.Shift) == 0))
            {
                camera.RotZ += mouseRotateFactor * mouseInfo.speedy;
                camera.RotY -= mouseRotateFactor * mouseInfo.speedx;

                if (camera.RotZ < -Math.PI / 2.0f + 0.0001f)
                {
                    camera.RotZ = -(float)Math.PI / 2.0f + 0.0001f;
                }
                if (camera.RotZ > Math.PI / 2.0f - 0.0001f)
                {
                    camera.RotZ = (float)Math.PI / 2.0f - 0.0001f;
                }
            }
        }

        private float GetAdjustedMovementSpeed()
        {
            return camera.dist / 3f * 40.0f * dt * movementSpeed;
        }

        private float GetAdjustedRotationSpeed()
        {
            return 40.0f * dt * rotationSpeed;
        }
        public bool ShiftPressed()
        {
            return (Control.ModifierKeys & Keys.Shift) != 0;
        }
        public bool CtrlPressed()
        {
            return (Control.ModifierKeys & Keys.Control) != 0;
        }

        public bool IsPressed(Keys? key_nullable)
        {
            if (key_nullable == null) return false;
            Keys key = (Keys)key_nullable;
            bool pressed = pressedKeys.Contains(key & ~Keys.Shift) || (key & ~Keys.Shift) == 0;// && (pressedKeys.Contains(Keys.ShiftKey) || ((key & Keys.ShiftKey) == 0));
            pressed &= ShiftPressed() == key.HasFlag(Keys.Shift);


            return pressed;
        }

        public void RemovePressedKey(Keys? key)
        {
            if (key == null) return;

            pressedKeys.Remove((Keys)key);
        }

        private void HandleUserInput()
        {
            if (!handleCameraMovement) return;

            if (IsPressed(LevelEditorKeybinds.Get("speedUp")))
            {
                movementSpeed *= 1.05f;
            }
            if (IsPressed(LevelEditorKeybinds.Get("speedDown")))
            {
                movementSpeed /= 1.05f;
            }
            movementSpeed = Math.Min(Math.Max(movementSpeed, movementSpeedMin), movementSpeedMax);

            float factor = GetAdjustedMovementSpeed();
            float rotatefactor = GetAdjustedRotationSpeed();

            
            if (IsPressed(LevelEditorKeybinds.Get("forward"))) // Forward
            {
                camera.RelativeMove(-factor * Vector3.UnitX);
            }
            if (IsPressed(LevelEditorKeybinds.Get("backward"))) // Backward
            {
                camera.RelativeMove(factor * Vector3.UnitX);
            }
            if (IsPressed(LevelEditorKeybinds.Get("left"))) // Left
            {
                camera.RelativeMove(factor * Vector3.UnitZ);
            }
            if (IsPressed(LevelEditorKeybinds.Get("right"))) // Right
            {
                camera.RelativeMove(-factor * Vector3.UnitZ);
            }
            if (IsPressed(LevelEditorKeybinds.Get("up"))) // Up
            {
                camera.RelativeMove(factor * Vector3.UnitY);
            }
            if (IsPressed(LevelEditorKeybinds.Get("down"))) // Down
            {
                camera.RelativeMove(-factor * Vector3.UnitY);
            }





            // Rotation
            if (IsPressed(LevelEditorKeybinds.Get("panRight"))) // Rotate Right
            {
                camera.RotY += rotatefactor;
            }
            if (IsPressed(LevelEditorKeybinds.Get("panLeft"))) // Rotate Left
            {
                camera.RotY -= rotatefactor;
            }
            if (IsPressed(LevelEditorKeybinds.Get("panUp"))) // Rotate Up
            {
                camera.RotZ += rotatefactor;

                if (camera.RotZ > Math.PI / 2.0f - 0.0001f) // Fudge factor (love it)
                {
                    camera.RotZ = (float)Math.PI / 2.0f - 0.0001f;
                }
            }
            if (IsPressed(LevelEditorKeybinds.Get("panDown"))) // Rotate Down
            {
                camera.RotZ -= rotatefactor;

                if (camera.RotZ < -Math.PI / 2.0f + 0.0001f)
                {
                    camera.RotZ = -(float)Math.PI / 2.0f + 0.0001f;
                }
            }
        }


        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pressedKeys.Contains(e.KeyCode))
            {
                //MessageBox.Show(e.KeyCode.ToString());
                pressedKeys.Add(e.KeyCode);
            }
        }
        private void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Remove(e.KeyCode);
            }
        }

        private void glControl_LostFocus(object sender, EventArgs e)
        {
            pressedKeys = new();
        }

        private void HandleMouseWheel()
        {
            camera.dist *= 1 - 0.0005f * mouseInfo.scrollSpeed;
            if (camera.dist < 1f) { camera.dist = 1f; }
        }

        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            mouseInfo.scrollSpeed = e.Delta;

            HandleMouseWheel();

        }

        

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle) { mouseInfo.middleMouseDown = false; }
            if (e.Button == MouseButtons.Left) { mouseInfo.leftMouseDown = false; }
            if (e.Button == MouseButtons.Right) { mouseInfo.rightMouseDown = false; }

            EraseSelectionRectangle();
            glControl.Capture = false;
            pauseRendering = false;

            this.MouseUp?.Invoke(this, e);

            if (e.Button == MouseButtons.Left) { mouseInfo.isBoxSelecting = false; }
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            mouseInfo.SetPosition(e.X, e.Y); 
            mouseInfo.currentPoint = e.Location;

            HandleMouseMovement();
            HandleBoxSelect(e);

            this.MouseMove?.Invoke(this, e);
        }

        public Rectangle CreateRectangle(Point first, Point second)
        {
            int left = Math.Min(first.X, second.X);
            int top = Math.Min(first.Y, second.Y);
            int right = Math.Max(first.X, second.X);
            int bottom = Math.Max(first.Y, second.Y);

            return Rectangle.FromLTRB(
                left,
                top,
                right,
                bottom);
        }

        private Rectangle ClampToControl(Rectangle rectangle)
        {
            Rectangle controlBounds = new(
                0,
                0,
                glControl.ClientSize.Width,
                glControl.ClientSize.Height);

            return Rectangle.Intersect(rectangle, controlBounds);
        }

        private void DrawBoxSelect()
        {
            EraseSelectionRectangle();

            Rectangle clientRectangle =
                CreateRectangle(mouseInfo.mouseDownPoint, mouseInfo.currentPoint);

            clientRectangle = ClampToControl(clientRectangle);

            mouseInfo.lastScreenRectangle =
                glControl.RectangleToScreen(clientRectangle);

            ControlPaint.DrawReversibleFrame(
                mouseInfo.lastScreenRectangle,
                Color.White,
                FrameStyle.Dashed);
        }

        private void EraseSelectionRectangle()
        {
            if (mouseInfo.lastScreenRectangle.IsEmpty)
                return;

            ControlPaint.DrawReversibleFrame(
                mouseInfo.lastScreenRectangle,
                Color.White,
                FrameStyle.Dashed);

            mouseInfo.lastScreenRectangle = Rectangle.Empty;
        }

        private void HandleBoxSelect(MouseEventArgs e)
        {
            if (!mouseInfo.leftMouseDown)
                return;


            if (!mouseInfo.isBoxSelecting)
            {
                Rectangle dragThreshold = new(
                    mouseInfo.mouseDownPoint.X - SystemInformation.DragSize.Width / 2,
                    mouseInfo.mouseDownPoint.Y - SystemInformation.DragSize.Height / 2,
                    SystemInformation.DragSize.Width,
                    SystemInformation.DragSize.Height);

                if (dragThreshold.Contains(e.Location))
                    return;

                mouseInfo.isBoxSelecting = true;
            }

            pauseRendering = true;
            DrawBoxSelect();

        }

        private void glControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle) { mouseInfo.middleMouseDown = true; }
            if (e.Button == MouseButtons.Left) {
                mouseInfo.leftMouseDown = true;
                mouseInfo.mouseDownPoint = e.Location;
                glControl.Capture = true;
            }
            if (e.Button == MouseButtons.Right) { mouseInfo.rightMouseDown = true; }



            this.MouseClick?.Invoke(this, e);
        }

        public Ray GetCursorRay()
        {
            int mouseX = mouseInfo.x;
            int mouseY = mouseInfo.y;

            float x = (2.0f * mouseX) / (float)Width - 1.0f;
            float y = 1.0f - (2.0f * mouseY) / (float)Height;

            return camera.NDCToWorldRay(x, y);

        }
    }
}
