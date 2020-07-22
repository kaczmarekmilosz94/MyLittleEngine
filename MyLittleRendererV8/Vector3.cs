using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleRendererV8
{
    public class Vector3
    {
        public float x;
        public float y;
        public float z;
        internal float w;


        // Predefined Vectors3  
        #region Default vectors

        private static Vector3 zero = new Vector3(0, 0, 0);
        /// <summary>
        /// Vector (0,0,0)
        /// </summary>
        public static Vector3 Zero
        {
            get => (Vector3)zero.MemberwiseClone();
        }

        private static Vector3 left = new Vector3(-1, 0, 0);
        /// <summary>
        /// Vector (-1,0,0)
        /// </summary>
        public static Vector3 Left
        {
            get => (Vector3)left.MemberwiseClone();
        }

        private static Vector3 right = new Vector3(1, 0, 0);
        /// <summary>
        /// Vector (1,0,0)
        /// </summary>
        public static Vector3 Right
        {
            get => (Vector3)right.MemberwiseClone();
        }

        private static Vector3 forward = new Vector3(0, 0, 1);
        /// <summary>
        /// Vector (0,0,1)
        /// </summary>
        public static Vector3 Forward
        {
            get => (Vector3)forward.MemberwiseClone();
        }

        private static Vector3 backward = new Vector3(0, 0, -1);
        /// <summary>
        /// Vector (0,0,-1)
        /// </summary>
        public static Vector3 Backward
        {
            get => (Vector3)backward.MemberwiseClone();
        }

        private static Vector3 up = new Vector3(0, 1, 0);
        /// <summary>
        /// Vector (0,1,0)
        /// </summary>
        public static Vector3 Up
        {
            get => (Vector3)up.MemberwiseClone();
        }

        private static Vector3 down = new Vector3(0, -1, 0);
        /// <summary>
        /// Vector (0,-1,0)
        /// </summary>
        public static Vector3 Down
        {
            get => (Vector3)down.MemberwiseClone();
        }

        #endregion 
        // Operations of " + " " - " " * " " / " for Vectors3
        #region Operators
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            Vector3 v3 = new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
            return v3;
        }
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            Vector3 v3 = new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
            return v3;
        }
        public static Vector3 operator *(Vector3 v1, float p)
        {
            if (v1 == null)
                return Vector3.Zero;
            else
            {
                Vector3 v3 = new Vector3(v1.x * p, v1.y * p, v1.z * p);
                return v3;
            }           
        }
        public static Vector3 operator /(Vector3 v1, float p)
        {
            Vector3 v3 = new Vector3(v1.x / p, v1.y / p, v1.z / p);
            return v3;
        }
        internal static Vector3 MultiplyByMatrix(Vector3 i, Matrix4x4 m)
        {
            Vector3 o = new Vector3(0, 0, 0)
            {
                x = i.x * m.value[0, 0] + i.y * m.value[1, 0] + i.z * m.value[2, 0] + i.w * m.value[3, 0],
                y = i.x * m.value[0, 1] + i.y * m.value[1, 1] + i.z * m.value[2, 1] + i.w * m.value[3, 1],
                z = i.x * m.value[0, 2] + i.y * m.value[1, 2] + i.z * m.value[2, 2] + i.w * m.value[3, 2],
                w = i.x * m.value[0, 3] + i.y * m.value[1, 3] + i.z * m.value[2, 3] + i.w * m.value[3, 3]
            };

            return o;
        }
      
        #endregion   

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1;
        }

        public static float DotProduct(Vector3 v1, Vector3 v2)
        {
            if (v2 == null)
                return 0;
            else
                return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }
        public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
        {
            if (v2 == null)
                return Vector3.Zero;
            else
                return new Vector3(
                v1.y * v2.z - v1.z * v2.y,
                v1.z * v2.x - v1.x * v2.z,
                v1.x * v2.y - v1.y * v2.x
                );
        }

        public float Distance(Vector3 target)
        {
            float x = this.x - target.x;
            float y = this.y - target.y;
            float z = this.z - target.z;

            return (float)Math.Sqrt(x * x + y * y + z * z);
        }       
        public Vector3 Normalize()
        {
            float normal_length = this.Length();
            return new Vector3
                (x / normal_length,
                 y / normal_length,
                 z / normal_length);
        }       
        public float Length()
        {
            return (float)Math.Sqrt(DotProduct(this, this));
        }
        public Vector3 RotateAroundPoint(Vector3 angle, Vector3 point)
        {
            Vector3 v = new Vector3(x, y, z);

            //Around X
            if (angle.x != 0)
            {
                float sx = (float)Math.Sin(angle.x);
                float cx = (float)Math.Cos(angle.x);

                v.y -= point.y;
                v.z -= point.z;

                float y_x = y * cx - v.z * sx;
                float z_x = y * sx + v.z * cx;

                v.y = y_x + point.y;
                v.z = z_x + point.z;
            }

            //Around Y
            if (angle.y != 0)
            {
                float sy = (float)Math.Sin(angle.y);
                float cy = (float)Math.Cos(angle.y);

                v.x -= point.x;
                v.z -= point.z;

                float x_y = v.x * cy - v.z * sy;
                float z_y = v.x * sy + v.z * cy;

                v.x = x_y + point.y;
                v.z = z_y + point.z;
            }

            //Around Z
            if (angle.z != 0)
            {
                float sz = (float)Math.Sin(angle.z);
                float cz = (float)Math.Cos(angle.z);

                v.x -= point.x;
                v.y -= point.y;

                float x_z = x * cz - v.y * sz;
                float y_z = x * sz + v.y * cz;

                v.x = x_z + point.x;
                v.y = y_z + point.y;
            }

            return v;
        }

        public static Vector3 IntersectPlane(Vector3 plane_p, Vector3 plane_n, Vector3 lineStart, Vector3 lineEnd)
        {
            plane_n = plane_n.Normalize();
            float plane_d = -Vector3.DotProduct(plane_n, plane_p);
            float ad = Vector3.DotProduct(lineStart, plane_n);
            float bd = Vector3.DotProduct(lineEnd, plane_n);
            float t = (-plane_d - ad) / (bd - ad);
            Vector3 lineStartToEnd = lineEnd - lineStart;
            Vector3 lineToIntersect = lineStartToEnd * t;
            return lineStart + lineToIntersect;
        }
    }
}
