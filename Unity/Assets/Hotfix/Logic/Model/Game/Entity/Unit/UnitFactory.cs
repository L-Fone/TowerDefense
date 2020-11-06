using ET;
using Cal.DataTable;
using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        public static async ETTask<Unit> Create(long id,int skinId,JobType jobType, UnitType unitType = UnitType.Player,int jobId = 0)
        {
            SkinBase skinBase =skinId==-1?null:DataTableHelper.Get<SkinBase>(skinId);
            int prefabId;
            if (skinBase == null)
            {
                Log.Error($"没有skinId = {skinId}");
                prefabId = jobId;
            }
            else
            {
                prefabId = skinBase.PrfabId;
            }
            Unit unit = EntityFactory.CreateWithParentAndId<Unit>(UnitComponent.Instance, id);
            unit.SkinId = skinId;
            unit.UnitType = unitType;


            await Game.EventSystem.Publish(new ET.EventType.OnCreateUnit { unit = unit,prefabId =prefabId });

            //unit.AddComponent<MoveComponent>();
            //unit.AddComponent<TurnComponent>();

            UnitComponent.Instance.Add(unit);
            return unit;
        }
        public static async ETTask<Unit> Create(int prefabId, UnitType unitType )
        {
           
            Unit unit = EntityFactory.CreateWithParent<Unit>(UnitComponent.Instance);
            unit.UnitType = unitType;


            await Game.EventSystem.Publish(new ET.EventType.OnCreateUnit { unit = unit, prefabId = prefabId });

            //unit.AddComponent<MoveComponent>();
            //unit.AddComponent<TurnComponent>();

            UnitComponent.Instance.Add(unit);
            return unit;
        }
        public static async ETTask<Unit> Create(long id, int prefabId, UnitType unitType = UnitType.MainStoryMonster)
        {
            
            Unit unit = EntityFactory.CreateWithParentAndId<Unit>(UnitComponent.Instance, id);
            unit.UnitType = unitType;

            await Game.EventSystem.Publish(new ET.EventType.OnCreateUnit { unit = unit, prefabId = prefabId });

            UnitComponent.Instance.Add(unit);
            return unit;
        }
        public static async ETTask<Unit> CreateNPC(int prefabId,int configId)
        {
            Unit unit = EntityFactory.CreateWithParent<Unit>(UnitComponent.Instance);
            unit.UnitType =  UnitType.NPC;
            unit.ConfigId = configId; 

            await Game.EventSystem.Publish(new ET.EventType.OnCreateUNPC { unit = unit, prefabId = prefabId});
            UnitComponent.Instance.Add(unit);
            return unit;
        }
        public static async ETTask<Unit> CreateTransPoint(Scene scene,long id)
        {

            Unit unit = EntityFactory.CreateWithParentAndId<Unit>(UnitComponent.Instance, id);
            unit.UnitType = UnitType.TransPoint;

            await Game.EventSystem.Publish(new ET.EventType.OnCreateTransPoint {zoneScene = scene, unit = unit });

            UnitComponent.Instance.Add(unit);
            return unit;
        }
    }
}