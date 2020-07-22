using System;
using System.Diagnostics;

namespace MyLittleRendererV8
{
    internal class Matrix4x4
    {
        public float[,] value = new float[4, 4];


        public static Matrix4x4 Projection;


        internal static void SetProjectionMatrix()
        {
            Matrix4x4 pMatrix = new Matrix4x4();

            // Projection Matrix
            float fNear = Renderer.nearPlane;
            float fFar = Renderer.farPlane;
            float fFov = Renderer.FOV;
            float fAspectRatio = Renderer.AspectRatio * Renderer.ScreenHeight / Renderer.ScreenWidth;
            float fFovRad = (float)(1.0f / Math.Tan(fFov * 0.5f / 180.0f * 3.14159f));

            pMatrix.value[0, 0] = fAspectRatio * fFovRad;
            pMatrix.value[1, 1] = fFovRad;
            pMatrix.value[2, 2] = fFar / (fFar - fNear);
            pMatrix.value[3, 2] = (-fFar * fNear) / (fFar - fNear);
            pMatrix.value[2, 3] = 1.0f;
            pMatrix.value[3, 3] = 0.0f;

            Projection = pMatrix;
        }
        public static Matrix4x4 RotationZ(float fAngleRad)
        {
            Matrix4x4 matrix = new Matrix4x4();

            matrix.value[0, 0] = (float)Math.Cos(fAngleRad);
            matrix.value[0, 1] = (float)Math.Sin(fAngleRad);
            matrix.value[1, 0] = (float)-Math.Sin(fAngleRad);
            matrix.value[1, 1] = (float)Math.Cos(fAngleRad);
            matrix.value[2, 2] = 1.0f;
            matrix.value[3, 3] = 1.0f;

            return matrix;
        }
        public static Matrix4x4 RotationY(float fAngleRad)
        {
            Matrix4x4 matrix = new Matrix4x4();

            matrix.value[0, 0] = (float)Math.Cos(fAngleRad);
            matrix.value[0, 2] = (float)-Math.Sin(fAngleRad);
            matrix.value[1, 1] = 1;
            matrix.value[2, 0] = (float)Math.Sin(fAngleRad);
            matrix.value[2, 2] = (float)Math.Cos(fAngleRad);

            return matrix;
        }
        public static Matrix4x4 RotationX(float fAngleRad)
        {
            Matrix4x4 matrix = new Matrix4x4();

            matrix.value[0, 0] = 1;
            matrix.value[1, 1] = (float)Math.Cos(fAngleRad);
            matrix.value[1, 2] = (float)Math.Sin(fAngleRad);
            matrix.value[2, 1] = (float)-Math.Sin(fAngleRad);
            matrix.value[2, 2] = (float)Math.Cos(fAngleRad);
            matrix.value[3, 3] = 1;

            return matrix;
        }
        public static Matrix4x4 Translation(float x, float y, float z)
        {
            Matrix4x4 matrix = new Matrix4x4();

            matrix.value[0, 0] = 1.0f;
            matrix.value[1, 1] = 1.0f;
            matrix.value[2, 2] = 1.0f;
            matrix.value[3, 3] = 1.0f;
            matrix.value[3, 0] = x;
            matrix.value[3, 1] = y;
            matrix.value[3, 2] = z;

            return matrix;
        }
        public static Matrix4x4 Identity()
        {
            Matrix4x4 matrix = new Matrix4x4();

            matrix.value[0, 0] = 1;
            matrix.value[1, 1] = 1;
            matrix.value[2, 2] = 1;
            matrix.value[3, 3] = 1;

            return matrix;
        }
        public static Matrix4x4 MultiplyMatrix(Matrix4x4 m1, Matrix4x4 m2)
        {
            Matrix4x4 matrix = new Matrix4x4();

            for (int c = 0; c < 4; c++)
            {
                for (int r = 0; r < 4; r++)
                {
                    matrix.value[r, c] = m1.value[r, 0] * m2.value[0, c] + m1.value[r, 1] * m2.value[1, c] + m1.value[r, 2] * m2.value[2, c] + m1.value[r, 3] * m2.value[3, c];
                }
            }
            return matrix;
        }
        public static Matrix4x4 PointAt(Vector3 pos, Vector3 target, Vector3 up)
        {
            // Calculate new forward direction
            Vector3 newForward = target - pos;
            newForward = newForward.Normalize();

            // Calculate new Up direction
            Vector3 a = newForward * Vector3.DotProduct(up, newForward);
            Vector3 newUp = up - a;
            newUp = newUp.Normalize();

            // New Right direction is easy, its just cross product
            Vector3 newRight = Vector3.CrossProduct(newUp, newForward);

            // Construct Dimensioning and Translation Matrix	
            Matrix4x4 matrix = new Matrix4x4();

            matrix.value[0, 0] = newRight.x;
            matrix.value[0, 1] = newRight.y;
            matrix.value[0, 2] = newRight.z;
            matrix.value[0, 3] = 0.0f;
            matrix.value[1, 0] = newUp.x;
            matrix.value[1, 1] = newUp.y;
            matrix.value[1, 2] = newUp.z;
            matrix.value[1, 3] = 0.0f;
            matrix.value[2, 0] = newForward.x;
            matrix.value[2, 1] = newForward.y;
            matrix.value[2, 2] = newForward.z;
            matrix.value[2, 3] = 0.0f;
            matrix.value[3, 0] = pos.x;
            matrix.value[3, 1] = pos.y;
            matrix.value[3, 2] = pos.z;
            matrix.value[3, 3] = 1.0f;

            return matrix;
        }
        public static Matrix4x4 QuickInverse(Matrix4x4 m) // Only for Rotation/Translation Matrices
        {
            Matrix4x4 matrix = new Matrix4x4();

            matrix.value[0, 0] = m.value[0, 0];
            matrix.value[0, 1] = m.value[1, 0];
            matrix.value[0, 2] = m.value[2, 0];
            matrix.value[0, 3] = 0.0f;
            matrix.value[1, 0] = m.value[0, 1];
            matrix.value[1, 1] = m.value[1, 1];
            matrix.value[1, 2] = m.value[2, 1];
            matrix.value[1, 3] = 0.0f;
            matrix.value[2, 0] = m.value[0, 2];
            matrix.value[2, 1] = m.value[1, 2];
            matrix.value[2, 2] = m.value[2, 2];
            matrix.value[2, 3] = 0.0f;
            matrix.value[3, 0] = -(m.value[3, 0] * matrix.value[0, 0] + m.value[3, 1] * matrix.value[1, 0] + m.value[3, 2] * matrix.value[2, 0]);
            matrix.value[3, 1] = -(m.value[3, 0] * matrix.value[0, 1] + m.value[3, 1] * matrix.value[1, 1] + m.value[3, 2] * matrix.value[2, 1]);
            matrix.value[3, 2] = -(m.value[3, 0] * matrix.value[0, 2] + m.value[3, 1] * matrix.value[1, 2] + m.value[3, 2] * matrix.value[2, 2]);
            matrix.value[3, 3] = 1.0f;

            return matrix;
        }

    }
}
