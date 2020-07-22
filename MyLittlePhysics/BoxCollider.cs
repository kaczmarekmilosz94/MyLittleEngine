using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLittleRendererV8;

namespace MyLittlePhysics
{
    public class BoxCollider : Collider
    {
        public float SizeX;
        public float SizeY;
        public float SizeZ;
        
        public BoxCollider(Vector3 position, float sizeX, float sizeY, float sizeZ) : base()
        {
            this.position = position;
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;

            colliders.Add(this);
        }

        public bool Intersect(BoxCollider other)
        {
            float A_minX = position.x - (SizeX / 2);
            float A_maxX = position.x + (SizeX / 2);

            float A_minY = position.y - (SizeY / 2);
            float A_maxY = position.y + (SizeY / 2);

            float A_minZ = position.z - (SizeZ / 2);
            float A_maxZ = position.z + (SizeZ / 2);

            float B_minX = other.position.x - (other.SizeX / 2);
            float B_maxX = other.position.x + (other.SizeX / 2);
                                               
            float B_minY = other.position.y - (other.SizeY / 2);
            float B_maxY = other.position.y + (other.SizeY / 2);
                                               
            float B_minZ = other.position.z - (other.SizeZ / 2);
            float B_maxZ = other.position.z + (other.SizeZ / 2);


            if ((A_minX <= B_maxX && A_maxX >= B_minX) &&
                (A_minY <= B_maxY && A_maxY >= B_minY) &&
                (A_minZ <= B_maxZ && A_maxZ >= B_minZ))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool OnPositionChange()
        {
            base.OnPositionChange();

            foreach (Collider col in colliders)
            {
                if (col != this)
                {
                    if (Intersect(col as BoxCollider))
                    {
                        OnCollision.Invoke(col);
                        return true;
                    }
                }              
            }
            return false;
        }
    }
}
