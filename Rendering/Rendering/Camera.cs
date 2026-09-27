using System;
using System.Numerics;
using Plankton.EditingTools;

namespace Plankton.Rendering
{
    public class Plane
    {
        public Vector3 normal;
        public float D;

        public Plane(Vector3 normal, float D)
        {
            this.normal = normal;
            this.D = D;
        }

        public float Distance(Vector3 point)
        {
            return Vector3.Dot(point - Vector3.Zero, normal) + D;
        }
    }

    public class Frustum
    {
        public Plane[] planes = new Plane[6];

        public void Update(Matrix4x4 viewproj)
        {
            planes[0] = new Plane(
                new Vector3(
                    viewproj.M14 + viewproj.M11,
                    viewproj.M24 + viewproj.M21,
                    viewproj.M34 + viewproj.M31),
                viewproj.M44 + viewproj.M41
            );

            planes[1] = new Plane(
                new Vector3(
                    viewproj.M14 - viewproj.M11,
                    viewproj.M24 - viewproj.M21,
                    viewproj.M34 - viewproj.M31),
                viewproj.M44 - viewproj.M41
            );

            planes[2] = new Plane(
                new Vector3(
                    viewproj.M14 + viewproj.M12,
                    viewproj.M24 + viewproj.M22,
                    viewproj.M34 + viewproj.M32),
                viewproj.M44 + viewproj.M42
            );

            planes[3] = new Plane(
                new Vector3(
                    viewproj.M14 - viewproj.M12,
                    viewproj.M24 - viewproj.M22,
                    viewproj.M34 - viewproj.M32),
                viewproj.M44 - viewproj.M42
            );

            // OpenGL clip-space near plane: z + w >= 0
            planes[4] = new Plane(
                new Vector3(
                    viewproj.M14 + viewproj.M13,
                    viewproj.M24 + viewproj.M23,
                    viewproj.M34 + viewproj.M33),
                viewproj.M44 + viewproj.M43
            );

            planes[5] = new Plane(
                new Vector3(
                    viewproj.M14 - viewproj.M13,
                    viewproj.M24 - viewproj.M23,
                    viewproj.M34 - viewproj.M33),
                viewproj.M44 - viewproj.M43
            );

            for (int i = 0; i < planes.Length; i++)
            {
                float length = planes[i].normal.Length();

                if (length <= float.Epsilon)
                    continue;

                planes[i].normal /= length;
                planes[i].D /= length;
            }
        }

        public bool isSphereInsideFrustum(Vector3 center, float radius)
        {
            foreach (Plane plane in planes)
            {
                float distance = plane.Distance(center);

                if (distance < -radius)
                    return false;
            }

            return true;
        }
    }

    public class Camera
    {
        private float _fov;
        private float _aspectratio;
        private float _dist;
        private Vector3 _orbit;

        public float fov
        {
            get => _fov;
            set
            {
                _fov = value;
                UpdateViewProj();
            }
        }

        public float aspectratio
        {
            get => _aspectratio;
            set
            {
                _aspectratio = value;
                UpdateViewProj();
            }
        }

        public float dist
        {
            get => _dist;
            set
            {
                _dist = value;
                UpdateViewProj();
            }
        }

        public Vector3 orbit
        {
            get => _orbit;
            set
            {
                _orbit = value;
                EndCameraPreview();
                UpdateViewProj();
            }
        }

        private float rotationy;
        private float rotationz;

        public float RotY
        {
            get => rotationy;
            set
            {
                rotationy = value;
                EndCameraPreview();
                UpdateYRotMatrix();
            }
        }

        public float RotZ
        {
            get => rotationz;
            set
            {
                rotationz = value;
                EndCameraPreview();
                UpdateZRotMatrix();
            }
        }

        private Matrix4x4 yrotmat = Matrix4x4.Identity;
        private Matrix4x4 zrotmat = Matrix4x4.Identity;
        private Matrix4x4 projectionmat = Matrix4x4.Identity;
        private Matrix4x4 viewmat = Matrix4x4.Identity;
        private Matrix4x4 viewprojmat = Matrix4x4.Identity;

        public Frustum frustum;

        private Func<float> previewfovcallback = null!;
        private Func<Vector3> previewPosCallback = null!;
        private Func<Vector3> previewRotCallback = null!;
        private Func<Matrix4x4> previewviewmat = null!;

        private float previewfov => previewfovcallback();

        private bool isPreviewing = false;

        public Camera(
            float dist,
            Vector3 orbit,
            float fov,
            float aspectratio,
            float rotationy = 0,
            float rotationz = 0)
        {
            frustum = new Frustum();

            _dist = dist;
            _orbit = orbit;
            _fov = fov;
            _aspectratio = aspectratio;

            this.rotationy = rotationy;
            this.rotationz = rotationz;

            yrotmat = Matrix4x4.CreateRotationY(rotationy);
            zrotmat = Matrix4x4.CreateRotationZ(rotationz);

            UpdateViewProj();
        }

        public void UpdateViewProj()
        {
            UpdateProjectionMatrix();
            UpdateViewMatrix();

            // System.Numerics uses row-vector transform semantics:
            // vector * view * projection
            viewprojmat = viewmat * projectionmat;

            frustum.Update(viewprojmat);
        }

        private void UpdateYRotMatrix()
        {
            yrotmat =
                Matrix4x4.CreateRotationY(rotationy);

            UpdateViewProj();
        }

        private void UpdateZRotMatrix()
        {
            zrotmat =
                Matrix4x4.CreateRotationZ(rotationz);

            UpdateViewProj();
        }

        public void UpdateViewMatrix()
        {
            Vector3 pos =
                RotateOrbitVector(new Vector3(dist, 0f, 0f))
                + orbit;

            viewmat =
                isPreviewing
                    ? previewviewmat()
                    : Matrix4x4.CreateLookAt(
                        pos,
                        orbit,
                        Vector3.UnitY);
        }

        public Matrix4x4 GetViewMatrix()
        {
            return viewmat;
        }

        public void RelativeMove(Vector3 movement)
        {
            orbit += RotateOrbitVector(movement);
        }

        public void UpdateProjectionMatrix()
        {
            float currentFov =
                isPreviewing
                    ? Math.Clamp(previewfov, 0.001f, 179.9f)
                    : fov;

            projectionmat =
                CreateOpenGLPerspectiveFieldOfView(
                    currentFov * (MathF.PI / 180.0f),
                    aspectratio,
                    0.1f,
                    1500.0f);
        }

        public Matrix4x4 GetProjectionMatrix()
        {
            return projectionmat;
        }

        public Ray NDCToWorldRay(float NDCx, float NDCy)
        {
            // OpenGL NDC depth range is -1..1.
            Vector4 nearPlaneRay =
                new Vector4(NDCx, NDCy, -1f, 1f);

            Vector4 farPlaneRay =
                new Vector4(NDCx, NDCy, 1f, 1f);

            if (!Matrix4x4.Invert(
                    viewprojmat,
                    out Matrix4x4 inverseVP))
            {
                throw new InvalidOperationException(
                    "Could not invert the camera view-projection matrix.");
            }

            Vector4 nearWorld =
                Vector4.Transform(
                    nearPlaneRay,
                    inverseVP);

            Vector4 farWorld =
                Vector4.Transform(
                    farPlaneRay,
                    inverseVP);

            nearWorld /= nearWorld.W;
            farWorld /= farWorld.W;

            Vector3 rayOrigin =
                new Vector3(
                    nearWorld.X,
                    nearWorld.Y,
                    nearWorld.Z);

            Vector3 farPoint =
                new Vector3(
                    farWorld.X,
                    farWorld.Y,
                    farWorld.Z);

            Vector3 rayDirection =
                Vector3.Normalize(
                    farPoint - rayOrigin);

            return new Ray(
                rayOrigin,
                rayDirection);
        }

        public Vector3 GetPosition()
        {
            if (!isPreviewing)
            {
                return RotateOrbitVector(
                           new Vector3(dist, 0f, 0f))
                       + orbit;
            }

            return previewPosCallback();
        }

        public void PreviewCamera(
            Func<Vector3> pos,
            Func<Vector3> rotation,
            Func<float> fovcallback)
        {
            previewfovcallback = fovcallback;
            previewPosCallback = pos;
            previewRotCallback = rotation;

            previewviewmat = () =>
            {
                Vector3 cameraRotation =
                    previewRotCallback();

                Matrix4x4 rotationYaw =
                    Matrix4x4.CreateRotationY(
                        MathF.PI - cameraRotation.X);

                Matrix4x4 rotationPitch =
                    Matrix4x4.CreateRotationX(
                        cameraRotation.Y);

                Matrix4x4 rotationRoll =
                    Matrix4x4.CreateRotationZ(
                        cameraRotation.Z);

                Matrix4x4 rotationMatrix =
                    rotationYaw
                    * rotationPitch
                    * rotationRoll;

                Vector3 inversePosition =
                    -previewPosCallback();

                Matrix4x4 translationMatrix =
                    Matrix4x4.CreateTranslation(
                        inversePosition);

                return translationMatrix
                       * rotationMatrix
                       * Matrix4x4.CreateTranslation(
                           0f,
                           0f,
                           0.0001f);
            };

            isPreviewing = true;
            UpdateViewProj();
        }

        public void EndCameraPreview()
        {
            isPreviewing = false;
        }

        public Vector3 GetNormal()
        {
            if (!Matrix4x4.Invert(
                    GetViewMatrix(),
                    out Matrix4x4 inverseView))
            {
                return Vector3.UnitZ;
            }

            // In view space, a right-handed camera looks down -Z.
            return Vector3.Normalize(
                Vector3.TransformNormal(
                    -Vector3.UnitZ,
                    inverseView));
        }

        private Vector3 RotateOrbitVector(
            Vector3 vector)
        {
            vector =
                Vector3.TransformNormal(
                    vector,
                    zrotmat);

            vector =
                Vector3.TransformNormal(
                    vector,
                    yrotmat);

            return vector;
        }

        private static Matrix4x4 CreateOpenGLPerspectiveFieldOfView(
            float fieldOfView,
            float aspectRatio,
            float nearPlaneDistance,
            float farPlaneDistance)
        {
            if (fieldOfView <= 0f ||
                fieldOfView >= MathF.PI)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(fieldOfView));
            }

            if (aspectRatio <= 0f)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(aspectRatio));
            }

            if (nearPlaneDistance <= 0f)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(nearPlaneDistance));
            }

            if (farPlaneDistance <= nearPlaneDistance)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(farPlaneDistance));
            }

            float yScale =
                1f / MathF.Tan(fieldOfView * 0.5f);

            float xScale =
                yScale / aspectRatio;

            float depth =
                nearPlaneDistance - farPlaneDistance;

            // Row-vector form of the standard OpenGL perspective matrix.
            // This preserves OpenGL's NDC Z range of -1..1.
            return new Matrix4x4(
                xScale, 0f, 0f, 0f,
                0f, yScale, 0f, 0f,
                0f, 0f, (farPlaneDistance + nearPlaneDistance) / depth, -1f,
                0f, 0f, (2f * nearPlaneDistance * farPlaneDistance) / depth, 0f
            );
        }
    }

}
