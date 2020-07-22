using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;

namespace MyLittleRendererV8
{
    public abstract class Renderer
    {
        public static Graphics ScreenTarget;
        private static CameraSettings currentSettings;

        #region Properties
        public static float nearPlane { get => currentSettings.nearPlane; }
        public static float farPlane { get => currentSettings.farPlane; }
        public static float FOV { get => currentSettings.FOV; }
        public static float AspectRatio { get => currentSettings.AspectRatio; }
        public static float ScreenHeight { get => currentSettings.ScreenHeight; }
        public static float ScreenWidth { get => currentSettings.ScreenWidth; }
        public static float ClippingDistance { get => currentSettings.ClippingDistance; }
        #endregion

        private static void SetRenderSettings(Camera camera)
        {
            currentSettings = camera.settings;
            Matrix4x4.SetProjectionMatrix(); // Matricies got to be updated , it depends on camera settings
        }


        public static float Render(List<Model> models, Camera camera)
        {
            Stopwatch stopwatch;

            SetRenderSettings(camera);

            stopwatch = Stopwatch.StartNew();

            Matrix4x4 m = Matrix4x4.Projection;

            List<Triangle> TrisToConvert = new List<Triangle>();
            List<Triangle> TrisToRender = new List<Triangle>();
            List<Triangle> TrisToRender2 = new List<Triangle>();
            List<Triangle> TrisToRender3 = new List<Triangle>();
            List<Triangle> TrianglesToRaster = new List<Triangle>();


            Vector3 Up = Vector3.Up;
            Vector3 Target = Vector3.Forward;
            camera.Forward = Vector3.MultiplyByMatrix(Target, Matrix4x4.RotationY(camera.eulerAngles.y));
            camera.Forward = Vector3.MultiplyByMatrix(camera.Forward, Matrix4x4.RotationX(camera.eulerAngles.x));
            Target = camera.position + camera.Forward;

            Matrix4x4 matCamera = Matrix4x4.PointAt(camera.position, Target, Up);
            Matrix4x4 matView = Matrix4x4.QuickInverse(matCamera);

            //Transform triangles by camera position
            foreach (Model model in models)
            {
                foreach (Triangle triangle in model.mesh.Tris)
                {
                    Triangle convertedTriangle = new Triangle();
                    convertedTriangle.color = model.Color;

                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 convertedVertex = triangle.vertices[i];

                        convertedTriangle.vertices[i] = convertedVertex;
                    }

                    TrisToConvert.Add(convertedTriangle);
                }
            }

            //Selecting triangles that should be rendered
            foreach (Triangle triangle in TrisToConvert)
            {
                #region Finding normal of triangle  

                Vector3 line1, line2, normal;

                line1 = triangle.vertices[1] - triangle.vertices[0];
                line2 = triangle.vertices[2] - triangle.vertices[0];

                normal = Vector3.CrossProduct(line1, line2);
                normal = normal.Normalize();

                triangle.normal = normal;

                #endregion

                // Render the triangle if its facing the camera

                Vector3 cameraRay = triangle.vertices[0] - camera.position;

                if (Vector3.DotProduct(normal, cameraRay) < 0)
                {
                    #region Calculating light and color

                    Vector3 lightDirection = new Vector3(0, 0, -1);
                    lightDirection = lightDirection.Normalize();


                    // Control of light intensity
                    float lumination = triangle.normal.x * lightDirection.x + triangle.normal.y * lightDirection.y + triangle.normal.z + lightDirection.z;
                    lumination /= -2;

                    // Setting color based on Calculated Light
                    Color col = Color.FromArgb(
                        triangle.color.A,
                        (byte)(triangle.color.R * lumination),
                        (byte)(triangle.color.G * lumination),
                        (byte)(triangle.color.B * lumination));
                    triangle.color = col;

                    #endregion

                    TrianglesToRaster.Add(triangle);
                }
            }

            // Convert World Space --> View Space
            foreach (Triangle triangle in TrianglesToRaster)
            {
                Triangle viewedTriangle = new Triangle(
                   Vector3.MultiplyByMatrix(triangle.vertices[0], matView),
                   Vector3.MultiplyByMatrix(triangle.vertices[1], matView),
                   Vector3.MultiplyByMatrix(triangle.vertices[2], matView))
                {
                    color = triangle.color
                };

                TrisToRender.Add(viewedTriangle);
            }

            //Clipping along Z axis
            foreach (Triangle triangle in TrisToRender)
            {
                int nClippedTriangles = 0;
                Triangle[] clipped = new Triangle[2];
                Triangle viewedTriangle = new Triangle
                {
                    vertices = triangle.vertices,
                    color = triangle.color,
                    normal = triangle.normal
                };


                nClippedTriangles = Triangle.ClipAgainstPlane(new Vector3(0.0f, 0.0f, ClippingDistance), new Vector3(0.0f, 0.0f, 1.0f), viewedTriangle, out clipped[0], out clipped[1]);

                for (int n = 0; n < nClippedTriangles; n++)
                {
                    // Store triangle for sorting 
                    clipped[n].color = triangle.color;
                    TrisToRender2.Add(clipped[n]);
                }
            }

            //Sorting triangles Z-Buffer ( Depth Buffer  )
            TrisToRender2.Sort((triangle1, triangle2) => triangle1.GetDepth().CompareTo(triangle2.GetDepth()));
            TrisToRender2.Reverse();



            //Clipping along screen broder not implemented yet
            foreach (Triangle triangle in TrisToRender2)
            {
                TrisToRender3.Add(triangle);
            }

            //Scaling and render
            foreach (Triangle triangle in TrisToRender3)
            {
                PointF[] verticesP = new PointF[3];

                for (int i = 0; i < 3; i++)
                {
                    Vector3 projectedVertex = Vector3.MultiplyByMatrix(triangle.vertices[i], m);
                    projectedVertex /= projectedVertex.w;

                    PointF pointF = new PointF(0, 0)
                    {
                        X = projectedVertex.x * 0.5f * currentSettings.ScreenWidth,
                        Y = -projectedVertex.y * 0.5f * currentSettings.ScreenHeight
                    };
                    pointF.X += 0.5f * currentSettings.ScreenWidth;
                    pointF.Y += 0.5f * currentSettings.ScreenHeight;

                    verticesP[i] = pointF;
                }


                // Printing Triangle on the screen. Finally...
                

                ScreenTarget.FillPolygon(new SolidBrush(triangle.color), verticesP);

            }

            return 1000 / (stopwatch.ElapsedMilliseconds+0.001f);
        }        
    }
}
