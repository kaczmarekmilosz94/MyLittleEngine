using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLittleRendererV8;

namespace MyLittleEngineV8
{
    class CameraComponent : Component
    {
        public Camera camera { get; }
        private bool isActive;
        public bool Active { set { isActive = value; SetMainCamera(); } }
        public CameraSettings settings;

        public CameraComponent()
        {
            camera = new Camera();
            isActive = false;
            settings = camera.settings;

            if (Camera.main == null)
            {
                isActive = true;
                Camera.main = this.camera;
            }
               
        }

        private void SetMainCamera()
        {
            if (isActive) Camera.main = this.camera;
            else if (Camera.main == this.camera) Camera.main = null;
        }
    }
}
