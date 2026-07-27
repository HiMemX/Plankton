using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using SharpDX.Mathematics.Interop;

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

        public float Distance(Vector3 point) {
            return Vector3.Dot(point, normal) + D;
        }
    }

    public class Frustum
    {
        public Plane[] planes = new Plane[6];

        public void Update(Matrix4 viewproj)
        {
            //viewproj.Transpose();
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

            planes[4] = new Plane(
                new Vector3(
                    viewproj.M13,
                    viewproj.M23,
                    viewproj.M33),
                viewproj.M43
            );

            planes[5] = new Plane(
                new Vector3(
                    viewproj.M14 - viewproj.M13,
                    viewproj.M24 - viewproj.M23,
                    viewproj.M34 - viewproj.M33),
                viewproj.M44 - viewproj.M43
            );

            for (int i = 0; i < 6; i++)
            {
                var n = planes[i].normal;
                float length = n.Length;
                planes[i].normal /= length;
                planes[i].D /= length;
            }
        }

        public bool isSphereInsideFrustum(Vector3 center, float radius)
        {
            foreach(Plane plane in planes)
            {
                float distance = plane.Distance(center);
                if (distance < -radius) return false;
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


        public float fov { get => _fov; set {  _fov = value; UpdateViewProj(); } }
        public float aspectratio { get => _aspectratio; set { _aspectratio = value; UpdateViewProj(); } }
        public float dist { get => _dist; set { _dist = value; UpdateViewProj(); } } // Distance from the orbit
        public Vector3 orbit { get => _orbit; set { _orbit = value; EndCameraPreview(); UpdateViewProj(); } }// This is what the camera is looking at and rotating around

        float rotationy;
        float rotationz;

        public float RotY { 
            get { return rotationy; } set {
                rotationy = value;
                UpdateYRotMatrix();
                EndCameraPreview();
            }
        }// Rotating around the Y axis
        public float RotZ
        {
            get { return rotationz; }
            set
            {
                rotationz = value;
                UpdateZRotMatrix();
                EndCameraPreview();
            }
        }  // "Height" angle around the orbit

        Matrix3 yrotmat;
        Matrix3 zrotmat;
        Matrix4 projectionmat;
        Matrix4 viewmat;
        Matrix4 viewprojmat;

        public Frustum frustum;


        Func<float> previewfovcallback;
        Func<Vector3> previewPosCallback;
        Func<Vector3> previewRotCallback;
        float previewfov { get { return previewfovcallback(); } }
        Func<Matrix4> previewviewmat;
        bool isPreviewing = false;



        public Camera(float dist, Vector3 orbit, float fov, float aspectratio, float rotationy = 0, float rotationz = 0)
        {
            frustum = new Frustum();

            _dist = dist;
            _orbit = orbit;
            _fov = fov;
            _aspectratio = aspectratio;
            RotY = rotationy;
            RotZ = rotationz;

        }

        public void UpdateViewProj()
        {
            UpdateProjectionMatrix();
            UpdateViewMatrix();
            viewprojmat = GetViewMatrix() * GetProjectionMatrix();

            frustum.Update(viewprojmat);
            
        }

        void UpdateYRotMatrix()
        {
            yrotmat = Matrix3.CreateRotationY(-rotationy);
            /*new Matrix3(
            new Vector3((float)Math.Cos(rotationx), 0, -(float)Math.Sin(rotationx)),
            new Vector3(0, 1, 0),
            new Vector3((float)Math.Sin(rotationx), 0, (float)Math.Cos(rotationx))
        );*/
            UpdateViewProj();

        }

        void UpdateZRotMatrix()
        {

            zrotmat = Matrix3.CreateRotationZ(-rotationz);
            /*zrotmat = new Matrix3(
                new Vector3((float)Math.Cos(rotationz), -(float)Math.Sin(rotationz), 0),
                new Vector3((float)Math.Sin(rotationz), (float)Math.Cos(rotationz), 0),
                new Vector3(0, 0, 1)
            );*/
            UpdateViewProj();
        }

        public void UpdateViewMatrix()
        {
            Vector3 pos = yrotmat * (zrotmat * new Vector3(dist, 0, 0)) + orbit;
            viewmat = isPreviewing ? previewviewmat() : Matrix4.LookAt(pos, orbit, new Vector3(0,1,0));
        }

        public Matrix4 GetViewMatrix()
        {
            return viewmat;
        }


        public void RelativeMove(Vector3 movement)
        {
            orbit += yrotmat * (zrotmat * movement);
        }

        public void UpdateProjectionMatrix()
        {
            if (isPreviewing) // Input fovx must be converted to fovy
            {
                //return Matrix4.CreatePerspectiveFieldOfView(previewfovx * (float)Math.PI / 180.0f, aspectratio, 0.1f, 1500f);
                projectionmat = Matrix4.CreatePerspectiveFieldOfView(Math.Clamp(previewfov, 0, 179.9f) * (float)Math.PI / 180.0f, aspectratio, 0.1f, 1500f);
                return;
            }


            projectionmat =  Matrix4.CreatePerspectiveFieldOfView(fov * (float)Math.PI / 180.0f, aspectratio, 0.1f, 1500f);

        }

        public Matrix4 GetProjectionMatrix()
        {
            return projectionmat;
        }

        public Ray NDCToWorldRay(float NDCx, float NDCy)
        {
            Vector4 nearPlaneRay = new Vector4(NDCx, NDCy, -1, 1);
            Vector4 farPlaneRay = new Vector4(NDCx, NDCy, 1, 1);

            Matrix4 inverseVP = Matrix4.Invert(viewprojmat);

            // Unproject near and far points
            Vector4 nearWorld = Vector4.Transform(nearPlaneRay, inverseVP);
            Vector4 farWorld = Vector4.Transform(farPlaneRay, inverseVP);

            // Convert from homogeneous to 3D coordinates
            nearWorld /= nearWorld.W;
            farWorld /= farWorld.W;

            // Define ray origin and direction
            Vector3 rayOrigin = new Vector3(nearWorld.X, nearWorld.Y, nearWorld.Z);
            Vector3 rayDirection = Vector3.Normalize(new Vector3(farWorld.X, farWorld.Y, farWorld.Z) - rayOrigin);

            return new Ray(rayOrigin, rayDirection);
        }

        public Vector3 GetPosition()
        {
            if(!isPreviewing) return yrotmat * (zrotmat * new Vector3(dist, 0, 0)) + orbit;
            return previewPosCallback();
        }

        public void PreviewCamera(Func<Vector3> pos, Func<Vector3> rotation, Func<float> fovcallback)
        {
            previewfovcallback = fovcallback;
            previewPosCallback = pos;
            previewRotCallback = rotation;

            // View matrix
            previewviewmat = () =>
            {
                Matrix4 rotationYaw = Matrix4.CreateRotationY((float)Math.PI - rotation().X);
                Matrix4 rotationPitch = Matrix4.CreateRotationX(rotation().Y);
                Matrix4 rotationRoll = Matrix4.CreateRotationZ(rotation().Z);

                Matrix4 rotationMatrix = rotationYaw * rotationPitch * rotationRoll;

                Vector3 inversePosition = -pos();

                Matrix4 translationMatrix = Matrix4.CreateTranslation(inversePosition);

                return translationMatrix * (rotationMatrix * Matrix4.CreateTranslation(0, 0, 0.0001f)); // Fudge so that only the camera rectangle renders
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
            /*
            if (!isPreviewing) return Vector3.Normalize(orbit - GetPosition());
            return GetPosition();
            */
            return Vector3.Normalize((GetViewMatrix() * new Vector4(0, 0, dist, 1) - GetViewMatrix() * new Vector4(0, 0, 0, 1)).Xyz);
        }
    }

    public class Ray
    {
        public Vector3 origin;
        public Vector3 direction;

        public Ray(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }
    }
}
