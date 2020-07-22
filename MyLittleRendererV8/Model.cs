using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Drawing;

namespace MyLittleRendererV8
{
    public class Model
    {
        public Mesh mesh = new Mesh();
        public Color Color;

        public void Rotate(Vector3 angle)
        {
            if (mesh == null || this == null) return;

            float x = angle.x * 0.0174532925f; //From radians to degree
            float y = angle.y * 0.0174532925f;
            float z = angle.z * 0.0174532925f;

            foreach (Vector3 vertex in mesh.Vertices)
            {
                Vector3 rotated = Vector3.MultiplyByMatrix(vertex, Matrix4x4.RotationY(y));
                rotated = Vector3.MultiplyByMatrix(rotated, Matrix4x4.RotationX(x));
                rotated = Vector3.MultiplyByMatrix(rotated, Matrix4x4.RotationZ(z));

                Vector3 v = new Vector3(0, 0, 0);
                vertex.x = rotated.x;
                vertex.y = rotated.y;
                vertex.z = rotated.z;
            }

        }
        public void Move(Vector3 distance)
        {
            if (mesh == null || this == null) return;

            foreach (Vector3 vector in mesh.Vertices)
            {
                vector.x += distance.x;
                vector.y += distance.y;
                vector.z += distance.z;
            }

        }
        public void Scale(Vector3 scale)
        {

        }

   
        public bool LoadFromObj(string filePath)
        {
            bool exists = File.Exists(filePath);

            if (!exists) return false;

            StreamReader file = new StreamReader(filePath);

            string line;

            mesh.Tris.Clear();
            mesh.Vertices.Clear();

            while ((line = file.ReadLine()) != null)
            {
                string[] words = line.Split(null);

                if (words[0] == "v")
                {
                    var dx = decimal.Parse(words[1], new NumberFormatInfo() { NumberDecimalSeparator = "." });
                    float x = (float)dx;

                    var dy = decimal.Parse(words[2], new NumberFormatInfo() { NumberDecimalSeparator = "." });
                    float y = (float)dy;

                    var dz = decimal.Parse(words[3], new NumberFormatInfo() { NumberDecimalSeparator = "." });
                    float z = (float)dz;


                    Vector3 vector3 = new Vector3(x, y, z);
                    mesh.Vertices.Add(vector3);
                }
                else if (words[0] == "f")
                {
                    Triangle triangle = new Triangle(
                       mesh.Vertices[int.Parse(words[1]) - 1],
                       mesh.Vertices[int.Parse(words[2]) - 1],
                       mesh.Vertices[int.Parse(words[3]) - 1]);

                       mesh.Tris.Add(triangle);
                }
            }

            file.Close();

            return true;
        }
    }
}
