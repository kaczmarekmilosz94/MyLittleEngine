using System.Collections.Generic;

namespace MyLittleRendererV8
{
    public class Mesh
    {
        internal List<Triangle> Tris;
        internal List<Vector3> Vertices;
               
        public Mesh()
        {
            Tris = new List<Triangle>();
            Vertices = new List<Vector3>();
        }
    }
}
