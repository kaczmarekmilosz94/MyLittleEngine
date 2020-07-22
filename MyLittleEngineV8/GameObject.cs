using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using MyLittleRendererV8;
using MyLittlePhysics;

namespace MyLittleEngineV8
{
    [Serializable]
    public class GameObject
    {
        [JsonProperty ("ID")]
        internal int ID;
        public string Name;

        [JsonProperty("components")]
        private List<Component> components = new List<Component>();

        public GameObject(string name = "new game object")
        {
            Random rand = new Random();
            ID = rand.Next(1000000, 9999999);
            Name = name;
            AddComponent<Transform>();
            GetComponent<Transform>().gameObject = this;
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (dynamic c in components)
            {
                if (c is T) return c;
            }
            return null;
        }

        public T[] GetComponents<T>() where T : Component
        {
            int counter = 0;

            foreach (dynamic c in components)
            {
                if (c is T) counter++;
            }

            T[] comps = new T[counter];

            counter = 0;

            foreach (dynamic c in components)
            {
                if (c is T)
                {
                    comps[counter] = c;
                    counter++;
                }
            }
            return comps;
        }

        public void AddComponent<T>(ref T comp) where T : Component, new()
        {
            comp.gameObject = this;
            this.components.Add(comp);

            if (comp is Script && GetComponent<BoxColliderComponent>() != null)
            {
                GetComponent<BoxColliderComponent>().boxCollider.OnCollision += (comp as Script).OnCollision;
            }
        }

        public void AddComponent<T>() where T : Component, new()
        {
            T c = new T
            {
                gameObject = this
            };

            this.components.Add(c);
            if (c is CameraComponent)
            {
                CameraComponent cameraComponent = c as CameraComponent;
                GetComponent<Transform>().camera = cameraComponent.camera;
            }
        }

        public void RemoveComponent(Component component)
        {
            if (component is MeshRenderer) Scene.currentScene.models.Remove(this.GetComponent<Transform>().model);
            else if (component is CameraComponent)
            {
                CameraComponent c = component as CameraComponent;
                if (c.camera == Camera.main) Camera.main = null;
            }
            components.Remove(component);
        }

        public void RemoveComponentsOfType<T>()
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T) RemoveComponent(components[i]);
            }
        }

        internal string SaveAsPrefab(string filePath, bool save)
        {
            foreach (Component c in components)
            {
                if (c is Script)
                {
                    FieldInfo[] fields = c.GetType().GetFields();
                    (c as Script).go_ids.Clear();

                    foreach (FieldInfo field in fields)
                    {
                        if (field.FieldType == typeof(GameObject))
                        {
                            GameObject value = field.GetValue(c) as GameObject;

                            if (value != null)
                            {
                                (c as Script).go_ids.Add(field.Name, value.ID);
                            }
                            else
                            {
                                (c as Script).go_ids.Add(field.Name, 0);
                            }
                            field.SetValue(c, null);
                        }
                    }
                }

                if (c is BoxColliderComponent)
                {
                    (c as BoxColliderComponent).boxCollider.gameObject = null;
                    (c as BoxColliderComponent).boxCollider.OnCollision = null;
                }

                c.gameObject = null;
            }
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            var serializedObject = JsonConvert.SerializeObject(this, Formatting.Indented, settings);

            if(save)
                File.WriteAllText(filePath, serializedObject);


            foreach (Component c in components)
            {
                c.gameObject = this;

                if (c is BoxColliderComponent)
                {
                    (c as BoxColliderComponent).boxCollider.gameObject = this;
                }
            }

            return serializedObject;
        }

        internal static GameObject LoadPrefab(string filePath)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            var serializedObject = File.ReadAllText(filePath);
            var deserializedObject = JsonConvert.DeserializeObject<GameObject>(serializedObject, settings);

            foreach (Component c in deserializedObject.components)
            {
                if (c is MeshRenderer)
                {
                    ((MeshRenderer)c).Model.LoadFromObj(((MeshRenderer)c).MeshPath);
                    deserializedObject.GetComponent<Transform>().model = deserializedObject.GetComponent<MeshRenderer>().Model;
                }

                if (c is BoxColliderComponent)
                {
                    (c as BoxColliderComponent).boxCollider.gameObject = deserializedObject;
                }

               
                if (c is CameraComponent)
                {
                    CameraComponent cameraComponent = c as CameraComponent;
                    deserializedObject.GetComponent<Transform>().camera = cameraComponent.camera;
                }
                if (c is BoxColliderComponent)
                {
                    (c as BoxColliderComponent).boxCollider.gameObject = deserializedObject;
                    (c as BoxColliderComponent).boxCollider.OnCollision = new OnCollisionEvent((c as BoxColliderComponent).boxCollider.OnCollisionBase);

                    foreach (Script s in deserializedObject.GetComponents<Script>())
                    {
                        (c as BoxColliderComponent).boxCollider.OnCollision += s.OnCollision;
                    }
                }
                c.gameObject = deserializedObject;
            }
            return deserializedObject;
        }
        internal static GameObject LoadPrefabFromText(string fileText)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            var serializedObject = fileText;
            var deserializedObject = JsonConvert.DeserializeObject<GameObject>(serializedObject, settings);

            foreach (Component c in deserializedObject.components)
            {
                if (c is MeshRenderer)
                {
                    ((MeshRenderer)c).Model.LoadFromObj(((MeshRenderer)c).MeshPath);
                    deserializedObject.GetComponent<Transform>().model = deserializedObject.GetComponent<MeshRenderer>().Model;
                }


                
                if (c is CameraComponent)
                {
                    CameraComponent cameraComponent = c as CameraComponent;
                    deserializedObject.GetComponent<Transform>().camera = cameraComponent.camera;
                }
                if (c is BoxColliderComponent)
                {
                    (c as BoxColliderComponent).boxCollider.gameObject = deserializedObject;
                    (c as BoxColliderComponent).boxCollider.OnCollision = new OnCollisionEvent((c as BoxColliderComponent).boxCollider.OnCollisionBase);

                    foreach (Script s in deserializedObject.GetComponents<Script>())
                    {
                        (c as BoxColliderComponent).boxCollider.OnCollision += s.OnCollision;
                    }
                }

                c.gameObject = deserializedObject;
            }
            return deserializedObject;
        }

        public static GameObject Instantiate(GameObject original)
        {
            foreach (Component c in original.components)
            {
                c.gameObject = null;
            }

            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            var serializedObject = JsonConvert.SerializeObject(original, Formatting.Indented, settings);

            foreach (Component c in original.components)
            {
                c.gameObject = original;
            }

            var deserializedObject = JsonConvert.DeserializeObject<GameObject>(serializedObject, settings);

            foreach (Component c in deserializedObject.components)
            {
                c.gameObject = deserializedObject;

                if (c is MeshRenderer)
                {
                    ((MeshRenderer)c).Model.LoadFromObj(((MeshRenderer)c).MeshPath);
                    deserializedObject.GetComponent<Transform>().model = deserializedObject.GetComponent<MeshRenderer>().Model;
                }
            }


            if (deserializedObject.GetComponent<MeshRenderer>() != null)
            {
                if (deserializedObject.GetComponent<MeshRenderer>().Model != null)
                {
                    Scene.currentScene.models.Add(deserializedObject.GetComponent<MeshRenderer>().Model);
                }
            }
            TreeNode treeNode = new TreeNode(deserializedObject.Name)
            {
                Tag = deserializedObject
            };
            Program.mainForm.treeViewGOs.Nodes.Add(treeNode);

            return deserializedObject;
        }

        public void Destroy()
        {
            Model m = this.GetComponent<MeshRenderer>().Model;

            if(m!= null)
                Scene.currentScene.models.Remove(m);

            Scene.currentScene.gameObjects.Remove(this);
        }
    }
}
