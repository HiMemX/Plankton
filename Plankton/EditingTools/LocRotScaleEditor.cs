using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using Plankton.Rendering;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Plankton.EditingTools
{
    public class LocRotScaleEditor
    {

        // 2.10.2025
        // Task: Update this to support editing multiple Objects
        //
        // idea: When starting to edit we save all of the value differences to the main object (which is by definition the last object in the list)
        // -> Keep this difference constant during the editing process
        // -> Individual Origins editing mode
        //
        // We'll need a slightly different approach for median center/bounding center modes or 3D cursor or active element, since we'll be rotating and scaling objects with respect
        // to a center point which isn't their own origin. Which means scaling and rotating will also include translating.

        public List<Vector3> OriginalPositions = new();
        public List<Vector3> OriginalRotations = new();
        public List<Vector3> OriginalScales = new();

        // Values to keep constant in individual origins
        public List<Vector3> PositionDifferences;
        //public static List<Vector3> ScaleQuotients; // Scaling is special ( like you and me :) )

        public List<Func<Vector3>> GetPositionCallbacks = new(); // Is necessary to get location of object when doing scaling and rotating, isn't used elsewhere
        public List<Func<Vector3>> GetRotationCallbacks = new();
        public List<Func<Vector3>> GetScaleCallbacks = new();

        public List<Action<Vector3>> SetPositionCallbacks = new();
        public List<Action<Vector3>> SetRotationCallbacks = new();
        public List<Action<Vector3>> SetScaleCallbacks = new();

        private int count { get => GetPositionCallbacks.Count; }

        public EditMode currentEditMode = EditMode.NONE;


        public Action OnUpdateCallback;
        public Action OnCancelCallback;
        public Action OnStartCallback = () => { };
        public event Action? OnEditEndCallback;

        // Used for relative scaling (Scaling doesn't change at cursors starting position)
        bool scalingFirstUpdate = true;
        float initialScalingDistance = 0;

        // Relative Rotation (rotate relative to cursor starting position)
        bool rotatingFirstUpdate = true;
        Vector3 initialRotationVector = Vector3.UnitX;

        public Vector3 GetMedianPoint()
        {
            Vector3 sum = new Vector3();

            foreach(Vector3 pos in OriginalPositions)
            {
                sum += pos / count;
            }

            return sum;
        }

        public Vector3? GetUpdatedPosition(Ray cursorray, Vector3 cameranormal, int i){
            Vector3 normal = Vector3.UnitY;
            if (GetAxis() == EditMode.AXIS_ALL) normal = cameranormal;
            else if (GetAxis() == EditMode.AXIS_XZ) normal = Vector3.UnitY;
            else if (GetAxis() == EditMode.AXIS_XY) normal = Vector3.UnitZ;
            else if (GetAxis() == EditMode.AXIS_YZ) normal = Vector3.UnitX;

            else
            {

                // Algorithm:
                // We want to calculate a plane and calculate the intersection point of the cursorray with it.
                // We then project that intersection point onto the projection axis (relative to the OriginalValue)
                // The plane should have the current editing axis inside of it. It should also face the camera (cursorray.origin)
                // The normal vector is thus the vector from the camera to the original value, of which it's component from the editing axis is subtracted.

                Vector3 projectionaxis = Vector3.UnitX;
                if (GetAxis() == EditMode.AXIS_Y) { projectionaxis = Vector3.UnitY; }
                if (GetAxis() == EditMode.AXIS_Z) { projectionaxis = Vector3.UnitZ; }

                normal = cursorray.origin - OriginalPositions[i];
                normal -= Vector3.Dot(normal, projectionaxis) * projectionaxis;
                normal.Normalize();

                Vector3? closest = VectorMath.RayPlaneIntersection(cursorray, normal, OriginalPositions[i]);
                if (closest == null) { return null; }


                return Vector3.Dot((Vector3)closest - OriginalPositions[i], projectionaxis) * projectionaxis + OriginalPositions[i];

                
            }

            // Code is only run when multiple axis are selected
            normal.Normalize();

            Vector3? intersect = VectorMath.RayPlaneIntersection(cursorray, normal, OriginalPositions[i]);
            if (intersect == null) { return null; }

            return (Vector3) intersect;
        }

        public void UpdatePositions(Ray cursorray, Vector3 cameranormal)
        {
            /*for (int i = 0; i < count; i++){
                Vector3? newpos = GetUpdatedPosition(cursorray, cameranormal, i);
                if (newpos == null) continue;


            }*/
            int origin_index = count - 1; // Last object is per defitinion the origin for now

            Vector3? newpos = GetUpdatedPosition(cursorray, cameranormal, origin_index);
            if (newpos == null) return;

            //SetPositionCallbacks[origin_index]((Vector3)newpos);
            for (int i = 0; i < count; i++)
            {
                SetPositionCallbacks[i]((Vector3)newpos + OriginalPositions[i] - OriginalPositions[origin_index]);
            }
        }

        public void UpdateScales(Ray cursorray, Vector3 cameranormal)
        {
            Vector3 median = GetMedianPoint();

            for (int i = 0; i < count; i++)
            {
                Vector3 referencepoint = GetReferencePoint();

                Matrix4 axismat = ConverterTools.ToOpenTK(System.Numerics.Matrix4x4.CreateFromYawPitchRoll(OriginalRotations[i].X, OriginalRotations[i].Y, OriginalRotations[i].Z));

                Vector3 axisx = ConverterTools.GetAxisX(axismat);
                Vector3 axisy = ConverterTools.GetAxisY(axismat);
                Vector3 axisz = ConverterTools.GetAxisZ(axismat);


                Vector3 normal = Vector3.UnitY;
                Vector3? intersect;
                if (GetAxis() == EditMode.AXIS_ALL)
                {
                    normal = cameranormal;
                    intersect = VectorMath.RayPlaneIntersection(cursorray, normal, median);
                }
                else if (GetAxis() == EditMode.AXIS_XZ)
                {
                    normal = axisy;
                    intersect = VectorMath.RayPlaneIntersection(cursorray, normal, median);
                }
                else if (GetAxis() == EditMode.AXIS_XY)
                {
                    normal = axisz;
                    intersect = VectorMath.RayPlaneIntersection(cursorray, normal, median);
                }
                else if (GetAxis() == EditMode.AXIS_YZ)
                {
                    normal = axisx;
                    intersect = VectorMath.RayPlaneIntersection(cursorray, normal, median);
                }

                else
                {

                    Vector3 projectionaxis = axisx;
                    if (GetAxis() == EditMode.AXIS_Y) { projectionaxis = axisy; }
                    if (GetAxis() == EditMode.AXIS_Z) { projectionaxis = axisz; }

                    normal = cursorray.origin - median;
                    normal -= Vector3.Dot(normal, projectionaxis) * projectionaxis;
                    normal.Normalize();

                    Vector3? closest = VectorMath.RayPlaneIntersection(cursorray, normal, median);
                    if (closest == null) { return; }


                    intersect = Vector3.Dot((Vector3)closest - median, projectionaxis) * projectionaxis + median;

                }

                if (intersect == null) { return; }


                float dist = Vector3.Distance((Vector3)intersect, median);

                if (scalingFirstUpdate)
                {
                    initialScalingDistance = dist;
                    scalingFirstUpdate = false;
                }

                float adjusteddist = dist / initialScalingDistance;

                Vector3 newscale = Vector3.One;
                if ((GetAxis() & EditMode.AXIS_X) != 0) { newscale.X = adjusteddist; }
                if ((GetAxis() & EditMode.AXIS_Y) != 0) { newscale.Y = adjusteddist; }
                if ((GetAxis() & EditMode.AXIS_Z) != 0) { newscale.Z = adjusteddist; }

                //SetScaleCallbacks[i]((new Vector4(newscale, 1) * axismat).Xyz * OriginalScales[i]);
                SetScaleCallbacks[i](newscale * OriginalScales[i]);

                if (GetPivotMode() == EditMode.PIVOT_INDIVIDUAL_ORIGINS) continue;

                SetPositionCallbacks[i](newscale * (OriginalPositions[i] - referencepoint) + referencepoint);
            }
        }


        public Vector3 GetReferencePoint()
        {
            switch (GetPivotMode())
            {
                case EditMode.PIVOT_MEDIAN_POINT:
                    return GetMedianPoint();
                    
                case EditMode.PIVOT_INDIVIDUAL_ORIGINS:
                    return OriginalPositions.Last();
                    
                default:
                    Debug.debugWindow.AddEntry("GetReferencePoint", "Error: Invalid Pivot mode");
                    return OriginalPositions.Last();
            }
        }

        public void UpdateRotations(Ray cursorray, Vector3 cameranormal)
        {
            // Buncha fancy math that mimics blenders fancy rotation edit
            // (Global rotation, rotates strictly around global axis (axes?)
            
            for (int i = 0; i < count; i++)
            {
                Vector3 referencepoint = GetReferencePoint();
                
                Vector3 CameraToObject = Vector3.Normalize(referencepoint - cursorray.origin);

                Vector3 rotationvector = Vector3.Normalize(cursorray.direction - CameraToObject * Vector3.Dot(CameraToObject, cursorray.direction));

                if (rotatingFirstUpdate)
                {
                    initialRotationVector = rotationvector;
                    rotatingFirstUpdate = false;
                }

                float angle = ConverterTools.CalculateAngle(rotationvector, initialRotationVector, -cameranormal);//(float)Math.Acos(Vector3.Dot(rotationvector, initialRotationVector));

                EditMode axis = GetAxis();
                Vector3 rotateaxis = cameranormal; // EditMode.AXIS_XYZ

                if (axis == EditMode.AXIS_X || axis == EditMode.AXIS_YZ) { rotateaxis = Vector3.UnitX; }
                if (axis == EditMode.AXIS_Y || axis == EditMode.AXIS_XZ) { rotateaxis = Vector3.UnitY; }
                if (axis == EditMode.AXIS_Z || axis == EditMode.AXIS_XY) { rotateaxis = Vector3.UnitZ; }

                if (Vector3.Dot(rotateaxis, cameranormal) < 0) { angle *= -1; }

                Matrix4 originalrotationmatrix = ConverterTools.ToOpenTK(System.Numerics.Matrix4x4.CreateFromYawPitchRoll(OriginalRotations[i].X, OriginalRotations[i].Y, OriginalRotations[i].Z));
                Matrix4 rotate = Matrix4.CreateFromAxisAngle(rotateaxis, angle);
                Matrix4 newrotationmatrix = originalrotationmatrix * rotate;

                var (yaw, pitch, roll) = ConverterTools.MatrixToYawPitchRoll(newrotationmatrix);
                //var (yaw, pitch, roll) = ConverterTools.MatrixToYawPitchRoll(originalrotationmatrix);

                //Debug.debugWindow.AddEntry("LocRotScaleEditor", yaw, pitch, roll, angle, rotateaxis.X, rotateaxis.Y, rotateaxis.Z);

                SetRotationCallbacks[i](new Vector3(yaw, pitch, roll));

                if (GetMode() == EditMode.PIVOT_INDIVIDUAL_ORIGINS) continue;

                rotate.Transpose(); // I'm not sure why I have to do this but oh well, it works :)))
                SetPositionCallbacks[i]((rotate * new Vector4(OriginalPositions[i] - referencepoint, 1)).Xyz + referencepoint);
            }
        }

        private void SetPositionCallback(List<Vector3> newpos)
        {
            for (int i = 0; i < SetPositionCallbacks.Count; i++)
            {
                SetPositionCallbacks[i](newpos[i]);
                
            }
        }

        private void SetRotationCallback(List<Vector3> newrot)
        {
            for (int i = 0; i < SetPositionCallbacks.Count; i++)
            {
                SetRotationCallbacks[i](newrot[i]);

            }
        }
        private void SetScaleCallback(List<Vector3> newscale)
        {
            for (int i = 0; i < SetPositionCallbacks.Count; i++)
            {
                SetScaleCallbacks[i](newscale[i]);
            }
        }

        public void Update(Ray cursorray, Vector3 cameranormal) // Update editing process.
        {
            if(GetMode() == EditMode.NONE) { return; }


            if(GetMode() == EditMode.POSITION) { UpdatePositions(cursorray, cameranormal); }
            if(GetMode() == EditMode.SCALE) { UpdateScales(cursorray, cameranormal); }
            if(GetMode() == EditMode.ROTATION) { UpdateRotations(cursorray, cameranormal); }

            OnUpdateCallback();
        }


        public void ClearCallbacks()
        {
            GetPositionCallbacks.Clear();
            GetRotationCallbacks.Clear();
            GetScaleCallbacks.Clear();

            SetPositionCallbacks.Clear();
            SetRotationCallbacks.Clear();
            SetScaleCallbacks.Clear();
        }
        /*
        private static void SetDifferences()
        {
            PositionDifferences = new();
            RotationDifferences = new();
            ScaleQuotients = new();

            for(int i=0; i<SetPositionCallbacks.Count; i++)
            {
                PositionDifferences.Add(GetPositionCallbacks[i]() - OriginalPosition);
                RotationDifferences.Add(GetRotationCallbacks[i]() - OriginalRotation);

                Vector3 scl = GetScaleCallbacks[i]();

                ScaleQuotients.Add(new Vector3(scl.X / OriginalScale.X, scl.Y / OriginalScale.Y, scl.Z / OriginalScale.Z));
            }
        }*/

        public void StartEdit(EditMode mode) // Starts setting edited values for preview
        {
            if(currentEditMode != EditMode.NONE) { CancelEdit(); }

            Debug.debugWindow.AddEntry("LocRotScaleEditor", "Started edit");

            OnStartCallback();

            OriginalPositions.Clear();
            OriginalRotations.Clear();
            OriginalScales.Clear();
            for (int i = 0; i < count; i++)
            {
                OriginalPositions.Add(GetPositionCallbacks[i]());
                OriginalRotations.Add(GetRotationCallbacks[i]());
                OriginalScales.Add(GetScaleCallbacks[i]());
            }
            //SetDifferences();

            scalingFirstUpdate = true;
            rotatingFirstUpdate = true;

            currentEditMode = mode | EditMode.AXIS_ALL | EditMode.PIVOT_MEDIAN_POINT;
            
            OnUpdateCallback();

        }

        public void CancelEdit() // Reset edited values to original values and end edit
        {
            SetPositionCallback(OriginalPositions);
            SetRotationCallback(OriginalRotations);
            SetScaleCallback(OriginalScales);


            currentEditMode = EditMode.NONE;
            OnUpdateCallback();
            OnEditEndCallback?.Invoke();
        }


        public void ApplyEdit() // Apply edited values and end edit
        {
            currentEditMode = EditMode.NONE;

            OnUpdateCallback();
            OnEditEndCallback?.Invoke();
        }
        
        public bool IsEditing()
        {
            return (currentEditMode & EditMode.MASK_TYPE) != EditMode.NONE;
        }

        public EditMode GetPivotMode()
        {
            return currentEditMode & EditMode.MASK_PIVOT;
        }

        public EditMode GetAxis()
        {
            return currentEditMode & EditMode.MASK_AXIS;
        }
        public void SetAxis(EditMode axis)
        {
            currentEditMode = currentEditMode & ~EditMode.MASK_AXIS | axis;
        }
        public EditMode GetMode()
        {
            return currentEditMode & EditMode.MASK_TYPE;
        }
        public void SetMode(EditMode mode)
        {
            currentEditMode = currentEditMode & ~EditMode.MASK_TYPE | mode;
        }


        // Temporary
        public static Vector3 TransformPosition(Vector3 startpos, EditMode editmode, Ray cursorray)
        {
            // Temporary: Intersection of the XZ Plane

            //return cursorray.origin + cursorray.direction * 5;

            float h = cursorray.origin.Y - startpos.Y;

            float denom = -cursorray.direction.Y;

            if(Math.Abs(denom) < 0.01f) { return Vector3.Zero; }
            if(h * denom < 0) { return Vector3.Zero; }

            return cursorray.origin + cursorray.direction * h / denom;
        }
    }
}
