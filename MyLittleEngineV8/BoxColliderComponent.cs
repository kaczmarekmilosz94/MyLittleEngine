using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLittleRendererV8;

namespace MyLittleEngineV8
{
    public class BoxColliderComponent : Component
    {
        public MyLittlePhysics.BoxCollider boxCollider { get; set; }
        public BoxColliderComponent()
        {
            boxCollider = new MyLittlePhysics.BoxCollider(Vector3.Zero, 1, 1, 1);
        }
    }
}
