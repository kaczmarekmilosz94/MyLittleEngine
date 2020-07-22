using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using MyLittleRendererV8;
using System.Drawing;

namespace MyLittleEngineV8
{
    class MeshRenderer : Component
    {
        private Model model;

        internal Model Model
        {
            get => model;
            set
            {
                model = value;
                Scene.currentScene.models.Add(model);
            }
        }
        public string MeshPath { get; set; }
        public string TexturePath { get; set; }

        private byte a;
        private byte r;
        private byte g;
        private byte b;

        public byte A { get => a; set { a = value; SetColor(); } }
        public byte R { get => r; set { r = value; SetColor(); } }
        public byte G { get => g; set { g = value; SetColor(); } }
        public byte B { get => b; set { b = value; SetColor(); } }
       
        //public Texture texture;

        public MeshRenderer()
        {
            Model = new Model();
            a = 255;
            r = 180;
            g = 200;
            b = 210;
        }

        private void SetColor()
        {
            model.Color = Color.FromArgb(a, r, g, b);
        }
    }
}
