using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using System.Text;
using System.Threading.Tasks;

namespace Plankton.Rendering
{
    internal static class VectorMath
    {
        public static System.Numerics.Vector3 DirectionFromYawPitchRoll(System.Numerics.Vector3 ypr)
        {
            /*
            System.Numerics.Vector3 dir = new System.Numerics.Vector3
                (
                    (float)(Math.Cos(ypr.Y) * Math.Cos(ypr.X)),  
                    (float)(Math.Sin(ypr.Y)),                  
                    (float)(Math.Cos(ypr.Y) * Math.Sin(ypr.X))  
                );*/

            System.Numerics.Vector3 dir = new System.Numerics.Vector3
                (
                    (float)(Math.Cos(ypr.Y) * Math.Sin(ypr.X)),
                    -(float)Math.Sin(ypr.Y),
                    (float)(Math.Cos(ypr.Y) * Math.Cos(ypr.X))
                );

            return dir / dir.Length();
        }

        public static float PointPlaneDistance(Vector3 point, Vector3 normal, Vector3 origin)
        {
            return Vector3.Dot(point - origin, normal);
        }

        public static Vector3? RayPlaneIntersection(Ray ray, Vector3 normal, Vector3 origin) // normal: Normal vector of plane (normalized); origin: Vector the plane sits on
        {
            float t = -PointPlaneDistance(ray.origin, normal, origin) / Vector3.Dot(ray.direction, normal);

            if(t < 0f) { return null; }
            return t * ray.direction + ray.origin;
        }
        public static Vector3 ExtractYawPitchRoll(System.Numerics.Matrix4x4 matrix)
        {
            // Remove translation
            Vector3 translation = new Vector3(matrix.M41, matrix.M42, matrix.M43);
            matrix.M41 = matrix.M42 = matrix.M43 = 0;

            // Remove scaling using Gram-Schmidt to orthonormalize the basis
            Vector3 x = new Vector3(matrix.M11, matrix.M12, matrix.M13);
            Vector3 y = new Vector3(matrix.M21, matrix.M22, matrix.M23);
            Vector3 z = new Vector3(matrix.M31, matrix.M32, matrix.M33);

            x = Vector3.Normalize(x);
            y = Vector3.Normalize(y - Vector3.Dot(y, x) * x);
            z = Vector3.Normalize(Vector3.Cross(x, y)); // ensure orthogonality
            
            System.Numerics.Matrix4x4 rotation = new System.Numerics.Matrix4x4(
                x.X, x.Y, x.Z, 0,
                y.X, y.Y, y.Z, 0,
                z.X, z.Y, z.Z, 0,
                0, 0, 0, 1
            );

            // Extract yaw, pitch, roll from the rotation matrix
            float pitch, yaw, roll;

            // Handle gimbal lock
            if (Math.Abs(rotation.M31) < 0.999f)
            {
                pitch = (float)Math.Asin(-rotation.M31);
                yaw = (float)Math.Atan2(rotation.M21, rotation.M11);
                roll = (float)Math.Atan2(rotation.M32, rotation.M33);
            }
            else
            {
                // Gimbal lock: pitch is +/-90 degrees
                pitch = rotation.M31 <= -1 ? (float)(Math.PI / 2) : (float)(-Math.PI / 2);
                yaw = (float)Math.Atan2(-rotation.M12, rotation.M22);
                roll = 0;
            }

            return new Vector3(yaw, pitch, roll);
        }
    }
}
