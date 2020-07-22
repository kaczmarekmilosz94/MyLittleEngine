using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLittleRendererV8;

namespace MyLittlePhysics
{
    public class Rigidbody
    {
        public bool IsKinematic { get; set; }
        public float Mass { get; set; }

        private Vector3 currentVelocity;

        private float G = 10;
        private float T = 1;
        private float maxFallSpeed = -50;

        public Rigidbody()
        {
            this.IsKinematic = false;
            this.Mass = 1;
            currentVelocity = Vector3.Zero;
        }

        public Vector3 GetVelocity()
        {
            currentVelocity.y -= G / 6;

            if (currentVelocity.y < maxFallSpeed)
                currentVelocity.y = maxFallSpeed;

            if (currentVelocity.x > 0)
                currentVelocity.x -= T / 6;
            else if (currentVelocity.x < 0)
                currentVelocity.x += T / 6;

            if (currentVelocity.z > 0)
                currentVelocity.z -= T / 6;
            else if (currentVelocity.z < 0)
                currentVelocity.z += T / 6;

            return currentVelocity/100;
        }

        public void AddVelocity(Vector3 vector)
        {
            currentVelocity += vector;
        }
    }
}
