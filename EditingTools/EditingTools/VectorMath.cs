using System;
using System.Numerics;

namespace Plankton.EditingTools
{
    public static class VectorMath
    {
        public static Vector3 DirectionFromYawPitchRoll(Vector3 ypr)
        {
            Vector3 dir = new Vector3(
                (float)(Math.Cos(ypr.Y) * Math.Sin(ypr.X)),
                -(float)Math.Sin(ypr.Y),
                (float)(Math.Cos(ypr.Y) * Math.Cos(ypr.X))
            );

            return Vector3.Normalize(dir);
        }

        public static float PointPlaneDistance(
            Vector3 point,
            Vector3 normal,
            Vector3 origin)
        {
            return Vector3.Dot(point - origin, normal);
        }

        public static Vector3? RayPlaneIntersection(
            Ray ray,
            Vector3 normal,
            Vector3 origin)
        {
            // normal: plane normal
            // origin: any point lying on the plane

            float denominator =
                Vector3.Dot(ray.direction, normal);

            float t =
                -PointPlaneDistance(
                    ray.origin,
                    normal,
                    origin)
                / denominator;

            if (t < 0f)
                return null;

            return ray.origin + ray.direction * t;
        }

        public static Vector3 ExtractYawPitchRoll(
            Matrix4x4 matrix)
        {
            // Remove translation.
            matrix.M41 = 0f;
            matrix.M42 = 0f;
            matrix.M43 = 0f;

            // Remove scaling using Gram-Schmidt to orthonormalize
            // the rotation basis.
            Vector3 x = new Vector3(
                matrix.M11,
                matrix.M12,
                matrix.M13);

            Vector3 y = new Vector3(
                matrix.M21,
                matrix.M22,
                matrix.M23);

            x = Vector3.Normalize(x);

            y = Vector3.Normalize(
                y - Vector3.Dot(y, x) * x);

            Vector3 z = Vector3.Normalize(
                Vector3.Cross(x, y));

            Matrix4x4 rotation = new Matrix4x4(
                x.X, x.Y, x.Z, 0f,
                y.X, y.Y, y.Z, 0f,
                z.X, z.Y, z.Z, 0f,
                0f, 0f, 0f, 1f
            );

            float pitch;
            float yaw;
            float roll;

            // Handle gimbal lock.
            if (Math.Abs(rotation.M31) < 0.999f)
            {
                pitch =
                    (float)Math.Asin(-rotation.M31);

                yaw =
                    (float)Math.Atan2(
                        rotation.M21,
                        rotation.M11);

                roll =
                    (float)Math.Atan2(
                        rotation.M32,
                        rotation.M33);
            }
            else
            {
                // Gimbal lock: pitch is +/-90 degrees.
                pitch =
                    rotation.M31 <= -1f
                        ? (float)(Math.PI / 2.0)
                        : (float)(-Math.PI / 2.0);

                yaw =
                    (float)Math.Atan2(
                        -rotation.M12,
                        rotation.M22);

                roll = 0f;
            }

            return new Vector3(
                yaw,
                pitch,
                roll);
        }
    }
}
