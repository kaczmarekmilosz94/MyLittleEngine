using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MyLittleRendererV8;

namespace MyLittlePhysics
{
    public delegate void OnCollisionEvent(Collider other);
    public abstract class Collider
    {
        internal static List<Collider> colliders;
        public Vector3 position;
        public bool isTrigger;
        public string Name { get; set; }
        public dynamic gameObject;
        
        public OnCollisionEvent OnCollision;

        public Collider()
        {
            if (colliders == null) colliders = new List<Collider>();           
            OnCollision = new OnCollisionEvent(OnCollisionBase);
        }

        public void SetPosition(Vector3 position)
        {
            this.position = position;
        }

        public virtual bool OnPositionChange()
        {
            return false;
        }

        public void OnCollisionBase(Collider other)
        {    
        }
    }
}
