using System;
using System.Collections.Generic;

namespace ET
{
    public class MapSceneComponenAwakeSystem : AwakeSystem<MapSceneComponent>
    {
        public override void Awake(MapSceneComponent self)
        {
            self.Awake();
        }
    }
    public class MapSceneComponentDestroySystem : DestroySystem<MapSceneComponent>
    {
        public override void Destroy(MapSceneComponent self)
        {
            self.Destroy();
        }
    }
    public class MapSceneComponent : Entity
    {
        public static MapSceneComponent inst;

        private Dictionary<int, MapScene> sceneDic = new Dictionary<int, MapScene>();

        internal void Awake()
        {

        }
        private MapScene Create(int mapId)
        {
            MapScene scene = EntityFactory.CreateWithParent<MapScene>(inst);
            scene.mapId = mapId;
            if(!sceneDic.TryAdd(mapId, scene))
            {
                Log.Error($"dic has this key == {mapId}");
            }
            return scene;
        }
        public MapScene GetScene(int mapId)
        {
            if (sceneDic.TryGetValue(mapId, out var scene)) ;
            return scene;
        }

        internal void Destroy()
        {
            foreach (var item in sceneDic.Values)
            {
                item.Dispose();
            }
            sceneDic.Clear();
        }
    }
}
