using System.Collections.Generic;

namespace ET
{
    public static class Game
    {
        public static EventSystem EventSystem
        {
            get
            {
                return EventSystem.Instance;
            }
        }

        private static Scene scene;

        public static Scene Scene
        {
            get
            {
                return scene ?? (scene = EntitySceneFactory.CreateScene(1, SceneType.Process, "Process"));
            }
        }
        public static Hotfix Hotfix = new Hotfix();
        public static ObjectPool ObjectPool
        {
            get
            {
                return ObjectPool.Instance;
            }
        }
#if !SERVER

        public readonly static Dictionary<SceneName, Scene> SceneDic = new Dictionary<SceneName, Scene>();
#endif
        public static void Close()
        {
            scene?.Dispose();
            scene = null;
            ObjectPool.Instance.Dispose();
            EventSystem.Instance.Dispose();
        }
    }
}