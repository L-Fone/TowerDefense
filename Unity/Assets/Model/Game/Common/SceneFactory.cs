using ET;

namespace ET
{
    public static class SceneFactory
    {
        public static Scene CreateZoneScene(long id, int zone, string name)
        {
            Scene zoneScene = EntitySceneFactory.CreateScene(id, zone, SceneType.Zone, name, Game.Scene);

            zoneScene.AddComponent<NetOuterComponent>();

            // UI层的初始化
            //await Game.EventSystem.Publish(new EventType.AfterCreateZoneScene() { zoneScene = zoneScene });

            return zoneScene;
        }
    }
}