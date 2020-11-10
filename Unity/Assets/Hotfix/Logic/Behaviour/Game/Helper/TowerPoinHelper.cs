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
            GenerateTowerAsync(towerPoint);
        }

        private static void GenerateTowerAsync(TowerPointInfo towerPoint)
        {

            RoleConfig roleConfig = ConfigHelper.Get<RoleConfig>(RoleConfigId.TestTower);
            Unit unit = UnitFactory.Create(roleConfig, UnitType.Tower);
            unit.Position = towerPoint.position;
            unit.AddComponent<TargetComponent>();

            var num = unit.AddComponent<NumericComponent>();
            num.Set(NumericType.HpBase, roleConfig.Hp);
            num.Set(NumericType.MaxHpBase, roleConfig.Hp);
            num.Set(NumericType.AtkBase, roleConfig.Atk);
            num.Set(NumericType.DefBase, roleConfig.Def);
            num.Set(NumericType.AtkFieldBase, roleConfig.AtkField);
            num.Set(NumericType.AtkSpdBase, roleConfig.AtkInterval);
            num.Set(NumericType.MoveSpdBase, roleConfig.Spd);

            var ai = unit.AddComponent<TowerAI>();
        }
    }
}
