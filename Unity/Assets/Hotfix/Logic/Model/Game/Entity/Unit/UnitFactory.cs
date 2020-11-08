using ET;
using Cal.DataTable;
using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        
        public static async ETTask<Unit> Create(int configId,int prefabId, UnitType unitType )
        {
           
            Unit unit = EntityFactory.CreateWithParent<Unit>(UnitComponent.Instance);
            unit.UnitType = unitType;
            unit.ConfigId = configId;

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