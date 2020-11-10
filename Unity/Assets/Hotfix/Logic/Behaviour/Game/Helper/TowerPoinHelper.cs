using Cal.DataTable;
using System;
using System.Collections.Generic;

namespace ET
{
    public static class TowerPointHelper
    {
        public static void GenerateTower(long id)
        {
            var towerPoint = TowerPointComponent.instance.Get(id);
            if (towerPoint == null)
            {
                Log.Error($"towerPoint == null when id == {id}");
                return;
            }
            GenerateTowerAsync(towerPoint).Coroutine();
        }

        private static async ETVoid GenerateTowerAsync(TowerPointInfo towerPoint)
        {

            RoleConfig roleConfig = ConfigHelper.Get<RoleConfig>(RoleConfigId.TestTower);
            Unit unit = UnitFactory.Create(roleConfig, UnitType.Tower);
            unit.Position = towerPoint.position;
            unit.AddComponent<TargetComponent>();
            var ai = unit.AddComponent<TowerAI>();
        }
    }
}
