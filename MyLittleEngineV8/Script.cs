using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLittlePhysics;
using MyLittleRendererV8;
using Newtonsoft.Json;

namespace MyLittleEngineV8
{
    public class Script : Component
    {
        internal string Name = "";
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void OnCollision(Collider other) { }

        [JsonProperty("GO_IDs")]
        internal Dictionary<string, int> go_ids = new Dictionary<string, int>();

        internal Script Clone()
        {
           return (Script)this.MemberwiseClone();
        }
    }
}
