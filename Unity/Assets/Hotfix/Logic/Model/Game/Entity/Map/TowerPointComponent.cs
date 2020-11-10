using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class TowerPointComponentAwakeSystem : AwakeSystem<TowerPointComponent>
    {
    	public override void Awake(TowerPointComponent self)
    	{
    		self.Awake();
    	}
    }
    public class TowerPointComponentDestroySystem : DestroySystem<TowerPointComponent>
    {
    	public override void Destroy(TowerPointComponent self)
    	{
    		self.Destroy();
    	}
    }
    public class TowerPointComponent:Entity
    {
        public static TowerPointComponent instance;

        public Dictionary<long, TowerPointInfo> towerDic = new Dictionary<long, TowerPointInfo>();

        internal void Awake()
        {
            instance = this;
        }
        public TowerPointInfo Create(Vector3 vector3)
        {
            TowerPointInfo towerPoint = EntityFactory.CreateWithParent<TowerPointInfo>(instance);
            towerPoint.position = vector3;
            if (!towerDic.TryAdd(towerPoint.Id, towerPoint))
            {
                Log.Error($"dic gas the key = {towerPoint.Id}");
            }
            return towerPoint;
        }
        public TowerPointInfo Get(long id)
        {
            towerDic.TryGetValue(id, out var towerPoint);
            return towerPoint;
        }

        public void RemoveAll()
        {
            foreach (var item in towerDic.Values)
            {
                item.Dispose();
            }
            towerDic.Clear();
        }

        internal void Destroy()
        {
            towerDic.Clear();
            instance = null;
        }

    }
}
