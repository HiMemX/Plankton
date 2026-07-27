using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Plankton.Rendering
{
    public static class ConverterTools
    {
        public static System.Numerics.Vector3 ToRadians(System.Numerics.Vector3 input)
        {
            return input * (float)Math.PI / 180.0f;
        }
        public static System.Numerics.Vector3 FromRadians(System.Numerics.Vector3 input)
        {
            return input / (float)Math.PI * 180.0f;
        }

        public static float CalculateAngle(Vector3 u, Vector3 v, Vector3 perpendicular)
        {
            
            // Normalize the input vectors
            u.Normalize();
            v.Normalize();
            perpendicular.Normalize();

            // Compute the dot product and the unsigned angle
            double dot = Vector3.Dot(u, v);
            float angle = (float)Math.Acos(Math.Clamp(dot, -1, 1));

            // Compute the cross product
            Vector3 cross = Vector3.Cross(u, v);

            // Determine the signed direction using the reference vector
            float direction = Vector3.Dot(cross, perpendicular);

            // Adjust the angle based on the direction
            if (direction < 0)
            {
                angle = 2*(float)Math.PI - angle;
            }

            return angle;
        }

        public static (float yaw, float pitch, float roll) MatrixToYawPitchRoll(Matrix4 matrix)
        {
            float pitch = (float)Math.Asin(-matrix.M32);

            float yaw, roll;

            if (Math.Abs(Math.Cos(pitch)) > 1e-6) // To avoid gimbal lock
            {
                // Extract yaw and roll when pitch is not near ±90 degrees
                yaw = (float)Math.Atan2(matrix.M31, matrix.M33);
                roll = (float)Math.Atan2(matrix.M12, matrix.M22);
            }
            else
            {
                // Handle gimbal lock: when pitch is ±90 degrees, yaw and roll are coupled
                yaw = (float)Math.Atan2(-matrix.M13, matrix.M11);
                roll = 0.0f; // Set roll to zero (or any fixed value) as it cannot be uniquely determined
            }

            return (yaw, pitch, roll);

        }
        public static Vector3 GetAxisX(Matrix4 mat) {
            return new Vector3(mat.Row0);
        }
        public static Vector3 GetAxisY(Matrix4 mat)
        {
            return new Vector3(mat.Row1);
        }
        public static Vector3 GetAxisZ(Matrix4 mat)
        {
            return new Vector3(mat.Row2);
        }

        public static System.Numerics.Vector3 GetAxisX(System.Numerics.Matrix4x4 mat)
        {
            return new System.Numerics.Vector3(mat.M11, mat.M12, mat.M13);
        }
        public static System.Numerics.Vector3 GetAxisY(System.Numerics.Matrix4x4 mat)
        {
            return new System.Numerics.Vector3(mat.M21, mat.M22, mat.M23);
        }
        public static System.Numerics.Vector3 GetAxisZ(System.Numerics.Matrix4x4 mat)
        {
            return new System.Numerics.Vector3(mat.M31, mat.M32, mat.M33);
        }

        public static Vector3 ToOpenTK(System.Numerics.Vector3 input)
        {
            return new Vector3(input.X, input.Y, input.Z);
        }
        public static System.Numerics.Vector3 FromOpenTK(Vector3 input)
        {

            return new System.Numerics.Vector3(input.X, input.Y, input.Z);
        }

        public static Matrix4 ToOpenTK(System.Numerics.Matrix4x4 systemMatrix)
        {
            return new Matrix4(
                systemMatrix.M11, systemMatrix.M12, systemMatrix.M13, systemMatrix.M14,
                systemMatrix.M21, systemMatrix.M22, systemMatrix.M23, systemMatrix.M24,
                systemMatrix.M31, systemMatrix.M32, systemMatrix.M33, systemMatrix.M34,
                systemMatrix.M41, systemMatrix.M42, systemMatrix.M43, systemMatrix.M44
            );
        }

    }
}
