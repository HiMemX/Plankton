using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Plankton.EditingTools
{
    public class LocRotScaleEditor
    {
        // 2.10.2025
        // Task: Update this to support editing multiple Objects
        //
        // idea: When starting to edit we save all of the value differences to the main object
        // (which is by definition the last object in the list)
        // -> Keep this difference constant during the editing process
        // -> Individual Origins editing mode
        //
        // We'll need a slightly different approach for median center/bounding center modes or
        // 3D cursor or active element, since we'll be rotating and scaling objects with respect
        // to a center point which isn't their own origin. Which means scaling and rotating will
        // also include translating.

        public List<Vector3> OriginalPositions = new();
        public List<Vector3> OriginalRotations = new();
        public List<Vector3> OriginalScales = new();

        // Values to keep constant in individual origins
        public List<Vector3> PositionDifferences = new();

        public List<Func<Vector3>> GetPositionCallbacks = new();
        public List<Func<Vector3>> GetRotationCallbacks = new();
        public List<Func<Vector3>> GetScaleCallbacks = new();

        public List<Action<Vector3>> SetPositionCallbacks = new();
        public List<Action<Vector3>> SetRotationCallbacks = new();
        public List<Action<Vector3>> SetScaleCallbacks = new();

        private int count => GetPositionCallbacks.Count;

        public EditMode currentEditMode = EditMode.NONE;

        public Action OnUpdateCallback;
        public Action OnCancelCallback;
        public Action OnStartCallback = () => { };
        public event Action? OnEditEndCallback;

        // Used for relative scaling (Scaling doesn't change at cursor's starting position)
        private bool scalingFirstUpdate = true;
        private float initialScalingDistance = 0;

        // Relative Rotation (rotate relative to cursor starting position)
        private bool rotatingFirstUpdate = true;
        private Vector3 initialRotationVector = Vector3.UnitX;

        public Vector3 GetMedianPoint()
        {
            Vector3 sum = Vector3.Zero;

            foreach (Vector3 pos in OriginalPositions)
            {
                sum += pos / count;
            }

            return sum;
        }

        public Vector3? GetUpdatedPosition(Ray cursorray, Vector3 cameranormal, int i)
        {
            Vector3 normal = Vector3.UnitY;

            if (GetAxis() == EditMode.AXIS_ALL)
                normal = cameranormal;
            else if (GetAxis() == EditMode.AXIS_XZ)
                normal = Vector3.UnitY;
            else if (GetAxis() == EditMode.AXIS_XY)
                normal = Vector3.UnitZ;
            else if (GetAxis() == EditMode.AXIS_YZ)
                normal = Vector3.UnitX;
            else
            {
                // Algorithm:
                // We want to calculate a plane and calculate the intersection point of the
                // cursorray with it.
                // We then project that intersection point onto the projection axis
                // (relative to the OriginalValue).
                // The plane should have the current editing axis inside of it. It should
                // also face the camera (cursorray.origin).
                // The normal vector is thus the vector from the camera to the original value,
                // of which its component from the editing axis is subtracted.

                Vector3 projectionaxis = Vector3.UnitX;

                if (GetAxis() == EditMode.AXIS_Y)
                    projectionaxis = Vector3.UnitY;

                if (GetAxis() == EditMode.AXIS_Z)
                    projectionaxis = Vector3.UnitZ;

                normal = cursorray.origin - OriginalPositions[i];
                normal -= Vector3.Dot(normal, projectionaxis) * projectionaxis;
                normal = Vector3.Normalize(normal);

                Vector3? closest =
                    VectorMath.RayPlaneIntersection(
                        cursorray,
                        normal,
                        OriginalPositions[i]);

                if (closest == null)
                    return null;

                return Vector3.Dot(
                           closest.Value - OriginalPositions[i],
                           projectionaxis)
                       * projectionaxis
                       + OriginalPositions[i];
            }

            // Code is only run when multiple axes are selected
            normal = Vector3.Normalize(normal);

            Vector3? intersect =
                VectorMath.RayPlaneIntersection(
                    cursorray,
                    normal,
                    OriginalPositions[i]);

            if (intersect == null)
                return null;

            return intersect.Value;
        }

        public void UpdatePositions(Ray cursorray, Vector3 cameranormal)
        {
            int origin_index = count - 1; // Last object is per definition the origin for now

            Vector3? newpos =
                GetUpdatedPosition(
                    cursorray,
                    cameranormal,
                    origin_index);

            if (newpos == null)
                return;

            for (int i = 0; i < count; i++)
            {
                SetPositionCallbacks[i](
                    newpos.Value
                    + OriginalPositions[i]
                    - OriginalPositions[origin_index]);
            }
        }

        public void UpdateScales(Ray cursorray, Vector3 cameranormal)
        {
            Vector3 median = GetMedianPoint();

            for (int i = 0; i < count; i++)
            {
                Vector3 referencepoint = GetReferencePoint();

                Matrix4x4 axismat =
                    Matrix4x4.CreateFromYawPitchRoll(
                        OriginalRotations[i].X,
                        OriginalRotations[i].Y,
                        OriginalRotations[i].Z);

                Vector3 axisx = GetAxisX(axismat);
                Vector3 axisy = GetAxisY(axismat);
                Vector3 axisz = GetAxisZ(axismat);

                Vector3 normal = Vector3.UnitY;
                Vector3? intersect;

                if (GetAxis() == EditMode.AXIS_ALL)
                {
                    normal = cameranormal;
                    intersect =
                        VectorMath.RayPlaneIntersection(
                            cursorray,
                            normal,
                            median);
                }
                else if (GetAxis() == EditMode.AXIS_XZ)
                {
                    normal = axisy;
                    intersect =
                        VectorMath.RayPlaneIntersection(
                            cursorray,
                            normal,
                            median);
                }
                else if (GetAxis() == EditMode.AXIS_XY)
                {
                    normal = axisz;
                    intersect =
                        VectorMath.RayPlaneIntersection(
                            cursorray,
                            normal,
                            median);
                }
                else if (GetAxis() == EditMode.AXIS_YZ)
                {
                    normal = axisx;
                    intersect =
                        VectorMath.RayPlaneIntersection(
                            cursorray,
                            normal,
                            median);
                }
                else
                {
                    Vector3 projectionaxis = axisx;

                    if (GetAxis() == EditMode.AXIS_Y)
                        projectionaxis = axisy;

                    if (GetAxis() == EditMode.AXIS_Z)
                        projectionaxis = axisz;

                    normal = cursorray.origin - median;
                    normal -=
                        Vector3.Dot(normal, projectionaxis)
                        * projectionaxis;

                    normal = Vector3.Normalize(normal);

                    Vector3? closest =
                        VectorMath.RayPlaneIntersection(
                            cursorray,
                            normal,
                            median);

                    if (closest == null)
                        return;

                    intersect =
                        Vector3.Dot(
                            closest.Value - median,
                            projectionaxis)
                        * projectionaxis
                        + median;
                }

                if (intersect == null)
                    return;

                float dist =
                    Vector3.Distance(
                        intersect.Value,
                        median);

                if (scalingFirstUpdate)
                {
                    initialScalingDistance = dist;
                    scalingFirstUpdate = false;
                }

                float adjusteddist =
                    dist / initialScalingDistance;

                Vector3 newscale = Vector3.One;

                if ((GetAxis() & EditMode.AXIS_X) != 0)
                    newscale.X = adjusteddist;

                if ((GetAxis() & EditMode.AXIS_Y) != 0)
                    newscale.Y = adjusteddist;

                if ((GetAxis() & EditMode.AXIS_Z) != 0)
                    newscale.Z = adjusteddist;

                SetScaleCallbacks[i](
                    newscale * OriginalScales[i]);

                if (GetPivotMode() ==
                    EditMode.PIVOT_INDIVIDUAL_ORIGINS)
                {
                    continue;
                }

                SetPositionCallbacks[i](
                    newscale
                    * (OriginalPositions[i] - referencepoint)
                    + referencepoint);
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

                    return OriginalPositions.Last();
            }
        }

        public void UpdateRotations(
            Ray cursorray,
            Vector3 cameranormal)
        {
            // Buncha fancy math that mimics Blender's fancy rotation edit.
            // Global rotation rotates strictly around global axes.

            for (int i = 0; i < count; i++)
            {
                Vector3 referencepoint =
                    GetReferencePoint();

                Vector3 cameraToObject =
                    Vector3.Normalize(
                        referencepoint - cursorray.origin);

                Vector3 rotationvector =
                    Vector3.Normalize(
                        cursorray.direction
                        - cameraToObject
                        * Vector3.Dot(
                            cameraToObject,
                            cursorray.direction));

                if (rotatingFirstUpdate)
                {
                    initialRotationVector = rotationvector;
                    rotatingFirstUpdate = false;
                }

                float angle =
                    CalculateSignedAngle(
                        rotationvector,
                        initialRotationVector,
                        -cameranormal);

                EditMode axis = GetAxis();
                Vector3 rotateaxis = cameranormal;

                if (axis == EditMode.AXIS_X ||
                    axis == EditMode.AXIS_YZ)
                {
                    rotateaxis = Vector3.UnitX;
                }

                if (axis == EditMode.AXIS_Y ||
                    axis == EditMode.AXIS_XZ)
                {
                    rotateaxis = Vector3.UnitY;
                }

                if (axis == EditMode.AXIS_Z ||
                    axis == EditMode.AXIS_XY)
                {
                    rotateaxis = Vector3.UnitZ;
                }

                if (Vector3.Dot(
                        rotateaxis,
                        cameranormal) < 0)
                {
                    angle *= -1;
                }

                Matrix4x4 originalrotationmatrix =
                    Matrix4x4.CreateFromYawPitchRoll(
                        OriginalRotations[i].X,
                        OriginalRotations[i].Y,
                        OriginalRotations[i].Z);

                Matrix4x4 rotate =
                    Matrix4x4.CreateFromAxisAngle(
                        Vector3.Normalize(rotateaxis),
                        angle);

                Matrix4x4 newrotationmatrix =
                    originalrotationmatrix * rotate;

                var (yaw, pitch, roll) =
                    MatrixToYawPitchRoll(
                        newrotationmatrix);

                SetRotationCallbacks[i](
                    new Vector3(
                        yaw,
                        pitch,
                        roll));

                // This was GetMode() in the old file, but pivot state lives in
                // MASK_PIVOT, so GetPivotMode() is the intended check here.
                if (GetPivotMode() ==
                    EditMode.PIVOT_INDIVIDUAL_ORIGINS)
                {
                    continue;
                }

                Vector3 offset =
                    OriginalPositions[i] - referencepoint;

                // System.Numerics uses Vector * Matrix semantics in Vector3.Transform.
                // This is the equivalent of the old OpenTK transpose + Matrix * Vector.
                Vector3 rotatedOffset =
                    Vector3.Transform(
                        offset,
                        rotate);

                SetPositionCallbacks[i](
                    rotatedOffset + referencepoint);
            }
        }

        private static Vector3 GetAxisX(
            Matrix4x4 matrix)
        {
            return Vector3.Normalize(
                Vector3.TransformNormal(
                    Vector3.UnitX,
                    matrix));
        }

        private static Vector3 GetAxisY(
            Matrix4x4 matrix)
        {
            return Vector3.Normalize(
                Vector3.TransformNormal(
                    Vector3.UnitY,
                    matrix));
        }

        private static Vector3 GetAxisZ(
            Matrix4x4 matrix)
        {
            return Vector3.Normalize(
                Vector3.TransformNormal(
                    Vector3.UnitZ,
                    matrix));
        }

        private static float CalculateSignedAngle(
            Vector3 from,
            Vector3 to,
            Vector3 normal)
        {
            from = Vector3.Normalize(from);
            to = Vector3.Normalize(to);
            normal = Vector3.Normalize(normal);

            float dot =
                Math.Clamp(
                    Vector3.Dot(from, to),
                    -1.0f,
                    1.0f);

            float angle =
                MathF.Acos(dot);

            float direction =
                Vector3.Dot(
                    Vector3.Cross(from, to),
                    normal);

            if (direction < 0.0f)
                angle *= -1.0f;

            return angle;
        }

        private static (
            float yaw,
            float pitch,
            float roll)
            MatrixToYawPitchRoll(
                Matrix4x4 matrix)
        {
            // Inverse of System.Numerics.Matrix4x4.CreateFromYawPitchRoll().
            //
            // Yaw   = rotation around Y
            // Pitch = rotation around X
            // Roll  = rotation around Z

            float sinPitch =
                Math.Clamp(
                    -matrix.M32,
                    -1.0f,
                    1.0f);

            float pitch =
                MathF.Asin(sinPitch);

            float cosPitch =
                MathF.Cos(pitch);

            float yaw;
            float roll;

            if (MathF.Abs(cosPitch) > 0.00001f)
            {
                yaw =
                    MathF.Atan2(
                        matrix.M31,
                        matrix.M33);

                roll =
                    MathF.Atan2(
                        matrix.M12,
                        matrix.M22);
            }
            else
            {
                // Gimbal lock: yaw and roll are no longer independently
                // recoverable. Preserve the equivalent orientation by folding
                // the remaining rotation into yaw and setting roll to zero.
                yaw =
                    MathF.Atan2(
                        -matrix.M13,
                        matrix.M11);

                roll = 0.0f;
            }

            return (yaw, pitch, roll);
        }

        private void SetPositionCallback(
            List<Vector3> newpos)
        {
            for (int i = 0;
                 i < SetPositionCallbacks.Count;
                 i++)
            {
                SetPositionCallbacks[i](
                    newpos[i]);
            }
        }

        private void SetRotationCallback(
            List<Vector3> newrot)
        {
            for (int i = 0;
                 i < SetRotationCallbacks.Count;
                 i++)
            {
                SetRotationCallbacks[i](
                    newrot[i]);
            }
        }

        private void SetScaleCallback(
            List<Vector3> newscale)
        {
            for (int i = 0;
                 i < SetScaleCallbacks.Count;
                 i++)
            {
                SetScaleCallbacks[i](
                    newscale[i]);
            }
        }

        public void Update(
            Ray cursorray,
            Vector3 cameranormal)
        {
            if (GetMode() == EditMode.NONE)
                return;

            if (GetMode() == EditMode.POSITION)
                UpdatePositions(
                    cursorray,
                    cameranormal);

            if (GetMode() == EditMode.SCALE)
                UpdateScales(
                    cursorray,
                    cameranormal);

            if (GetMode() == EditMode.ROTATION)
                UpdateRotations(
                    cursorray,
                    cameranormal);

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

        public void StartEdit(EditMode mode)
        {
            if (currentEditMode != EditMode.NONE)
                CancelEdit();


            OnStartCallback();

            OriginalPositions.Clear();
            OriginalRotations.Clear();
            OriginalScales.Clear();

            for (int i = 0; i < count; i++)
            {
                OriginalPositions.Add(
                    GetPositionCallbacks[i]());

                OriginalRotations.Add(
                    GetRotationCallbacks[i]());

                OriginalScales.Add(
                    GetScaleCallbacks[i]());
            }

            scalingFirstUpdate = true;
            rotatingFirstUpdate = true;

            currentEditMode =
                mode
                | EditMode.AXIS_ALL
                | EditMode.PIVOT_MEDIAN_POINT;

            OnUpdateCallback();
        }

        public void CancelEdit()
        {
            SetPositionCallback(
                OriginalPositions);

            SetRotationCallback(
                OriginalRotations);

            SetScaleCallback(
                OriginalScales);

            currentEditMode =
                EditMode.NONE;

            OnUpdateCallback();
            OnEditEndCallback?.Invoke();
        }

        public void ApplyEdit()
        {
            currentEditMode =
                EditMode.NONE;

            OnUpdateCallback();
            OnEditEndCallback?.Invoke();
        }

        public bool IsEditing()
        {
            return (currentEditMode & EditMode.MASK_TYPE)
                   != EditMode.NONE;
        }

        public EditMode GetPivotMode()
        {
            return currentEditMode
                   & EditMode.MASK_PIVOT;
        }

        public EditMode GetAxis()
        {
            return currentEditMode
                   & EditMode.MASK_AXIS;
        }

        public void SetAxis(EditMode axis)
        {
            currentEditMode =
                currentEditMode
                & ~EditMode.MASK_AXIS
                | axis;
        }

        public EditMode GetMode()
        {
            return currentEditMode
                   & EditMode.MASK_TYPE;
        }

        public void SetMode(EditMode mode)
        {
            currentEditMode =
                currentEditMode
                & ~EditMode.MASK_TYPE
                | mode;
        }

        // Temporary
        public static Vector3 TransformPosition(
            Vector3 startpos,
            EditMode editmode,
            Ray cursorray)
        {
            // Temporary: Intersection of the XZ Plane

            float h =
                cursorray.origin.Y
                - startpos.Y;

            float denom =
                -cursorray.direction.Y;

            if (Math.Abs(denom) < 0.01f)
                return Vector3.Zero;

            if (h * denom < 0)
                return Vector3.Zero;

            return cursorray.origin
                   + cursorray.direction
                   * h
                   / denom;
        }
    }
}
