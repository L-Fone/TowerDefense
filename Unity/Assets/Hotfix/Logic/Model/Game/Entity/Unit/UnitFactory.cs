using ET;
using Cal.DataTable;
using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        
        public static Unit Create(RoleConfig roleConfig, UnitType unitType )
        {
           
            Unit unit = EntityFactory.CreateWithParent<Unit>(UnitComponent.Instance);
            unit.UnitType = unitType;
            unit.ConfigId = (int)roleConfig.Id;

            Game.EventSystem.Publish_Sync(new ET.EventType.OnCreateUnit { unit = unit, roleConfig = roleConfig });


            UnitComponent.Instance.Add(unit);
            return unit;
        }

    }
}