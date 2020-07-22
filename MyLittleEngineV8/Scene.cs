using System.Collections.Generic;
using MyLittleRendererV8;


namespace MyLittleEngineV8
{
    class Scene
    {
        public static Scene currentScene;

        public List<GameObject> gameObjects { get; }
        public List<Model> models { get; }

        public Scene()
        {
            gameObjects = new List<GameObject>();
            models = new List<Model>();
        }

        public GameObject GetGameObjectOfIndex(int id)
        {
            foreach (GameObject go in gameObjects)
            {
                if (go.ID == id) return go;
            }

            return null;
        }
    }
}
