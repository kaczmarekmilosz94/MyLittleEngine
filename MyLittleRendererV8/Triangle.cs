using System.Diagnostics;
using System.Drawing;
using System;

namespace MyLittleRendererV8
{
    internal class Triangle
    {
        public Vector3[] vertices = new Vector3[3];

        public Vector3 normal;

        public Color color;

        public Triangle(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
        {
            vertices[0] = vertex1;
            vertices[1] = vertex2;
            vertices[2] = vertex3;

            color = Color.Gray;
        }
        public Triangle()
        {
            vertices[0] = Vector3.Zero;
            vertices[1] = Vector3.Zero;
            vertices[2] = Vector3.Zero;

            color = Color.Gray;
        }

        public float GetDepth()
        {
            float depth = vertices[0].z + vertices[1].z + vertices[2].z;
            return depth / 3;
        }

        public void SetNormal()
        {
            Vector3 line1, line2, normal;
            normal = Vector3.Zero;

            line1 = vertices[1] - vertices[0];
            line2 = vertices[2] - vertices[0];

            normal.x = line1.y * line2.z - line1.z * line2.y;
            normal.y = line1.z * line2.x - line1.x * line2.z;
            normal.z = line1.x * line2.y - line1.y * line2.x;

            float normal_length = (float)Math.Sqrt(normal.x * normal.x + normal.y * normal.y + normal.z * normal.z);
            normal /= normal_length;

            this.normal = normal;
        }

       

        private static float Dist(Vector3 p, Vector3 plane_p, Vector3 plane_n)
        {
            return (plane_n.x * p.x + plane_n.y * p.y + plane_n.z * p.z - Vector3.DotProduct(plane_n, plane_p));
        }
        public static int ClipAgainstPlane(Vector3 plane_p, Vector3 plane_n, Triangle in_tri, out Triangle out_tri1, out Triangle out_tri2)
        {
            // Make sure plane normal is indeed normal
            plane_n = plane_n.Normalize();

            // Create two temporary storage arrays to classify points either side of plane
            // If distance sign is positive, point lies on "inside" of plane
            Vector3[] inside_points = new Vector3[3];
            int nInsidePointCount = 0;
            Vector3[] outside_points = new Vector3[3];
            int nOutsidePointCount = 0;

            // Get signed distance of each point in triangle to plane
            float d0 = Dist(in_tri.vertices[0], plane_p, plane_n);
            float d1 = Dist(in_tri.vertices[1], plane_p, plane_n);
            float d2 = Dist(in_tri.vertices[2], plane_p, plane_n);


            if (d0 >= 0)
                inside_points[nInsidePointCount++] = in_tri.vertices[0];
            else
                outside_points[nOutsidePointCount++] = in_tri.vertices[0];
            if (d1 >= 0)
                inside_points[nInsidePointCount++] = in_tri.vertices[1];
            else
                outside_points[nOutsidePointCount++] = in_tri.vertices[1];
            if (d2 >= 0)
                inside_points[nInsidePointCount++] = in_tri.vertices[2];
            else
                outside_points[nOutsidePointCount++] = in_tri.vertices[2];

            // Now classify triangle points, and break the input triangle into 
            // smaller output triangles if required. There are four possible
            // outcomes...

            

            if (nInsidePointCount == 3)
            {
                // All points lie on the inside of plane, so do nothing
                // and allow the triangle to simply pass through
                out_tri1 = in_tri;
                out_tri2 = null;

                return 1; // Just the one returned original triangle is valid
            }

            if (nInsidePointCount == 1 && nOutsidePointCount == 2)
            {
                // Triangle should be clipped. As two points lie outside
                // the plane, the triangle simply becomes a smaller triangle

                // Copy appearance info to new triangle
                out_tri1 = new Triangle();
                out_tri2 = null;

                out_tri1.color = in_tri.color;

                // The inside point is valid, so keep that...
                out_tri1.vertices[0] = inside_points[0];

                // but the two new points are at the locations where the 
                // original sides of the triangle (lines) intersect with the plane
                out_tri1.vertices[1] = Vector3.IntersectPlane(plane_p, plane_n, inside_points[0], outside_points[0]);
                out_tri1.vertices[2] = Vector3.IntersectPlane(plane_p, plane_n, inside_points[0], outside_points[1]);

                return 1; // Return the newly formed single triangle
            }

            if (nInsidePointCount == 2 && nOutsidePointCount == 1)
            {
                // Triangle should be clipped. As two points lie inside the plane,
                // the clipped triangle becomes a "quad". Fortunately, we can
                // represent a quad with two new triangles

                // Copy appearance info to new triangles
                out_tri1 = new Triangle
                {
                    color = in_tri.color
                };
                out_tri2 = new Triangle
                {
                    color = in_tri.color
                };

                // The first triangle consists of the two inside points and a new
                // point determined by the location where one side of the triangle
                // intersects with the plane
                out_tri1.vertices[0] = inside_points[0];
                out_tri1.vertices[1] = inside_points[1];
                out_tri1.vertices[2] = Vector3.IntersectPlane(plane_p, plane_n, inside_points[0], outside_points[0]);

                // The second triangle is composed of one of he inside points, a
                // new point determined by the intersection of the other side of the 
                // triangle and the plane, and the newly created point above
                out_tri2.vertices[0] = inside_points[1];
                out_tri2.vertices[1] = out_tri1.vertices[2];
                out_tri2.vertices[2] = Vector3.IntersectPlane(plane_p, plane_n, inside_points[1], outside_points[0]);

                return 2; // Return two newly formed triangles which form a quad
            }

            else 
            {
                // All points lie on the outside of plane, so clip whole triangle
                // It ceases to exist
                out_tri1 = null;
                out_tri2 = null;

                return 0; // No returned triangles are valid
            }
        }

        internal Vector3[] IntersectPlane(Vector3 point, float clippingDistance)
        {
            throw new NotImplementedException();
        }
    }
}
