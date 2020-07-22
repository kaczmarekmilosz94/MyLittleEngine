using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleRendererV8
{
    public class Camera 
    {
        public static Camera main;
        
        public string Name = "new camera";
        public CameraSettings settings;

        public Vector3 position { get; set; }
        public Vector3 eulerAngles { get;  set; }


        private Vector3 forward;
        public Vector3 Forward
        {
            set
            {
                forward = value;

                // Calculate new Up direction
                Vector3 vUp = Vector3.Up;
                Vector3 a = value * Vector3.DotProduct(vUp, value);
                Vector3 newUp = vUp - a;
                Up = newUp.Normalize();
                // New Right direction is easy, its just cross product
                Right = Vector3.CrossProduct(newUp, value);
            }

            get => forward;
        }
        public Vector3 Up;
        public Vector3 Right;

        public Camera()
        {
            eulerAngles = Vector3.Zero;
            position = Vector3.Zero;
            settings = new CameraSettings();
        }

     
        public void Move(Vector3 distance)
        {
            position += distance;
        }
    
    }
}
