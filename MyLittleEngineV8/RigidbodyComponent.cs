using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleEngineV8
{
    public class RigidbodyComponent : Component
    {
        public MyLittlePhysics.Rigidbody Rigidbody { get; set; }

        public RigidbodyComponent()
        {
            this.Rigidbody = new MyLittlePhysics.Rigidbody();
        }
    }
}
