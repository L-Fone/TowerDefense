using Cal.DataTable;
using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public static class TowerHelper
    {
        public static void GenerateTower(long id)
        {
            var towerPoint = TowerPointComponent.instance.Get(id);
            if (towerPoint == null)
            {
                Log.Error($"towerPoint == null when id == {id}");
                return;
            }
            GenerateTower(towerPoint);
        }

        private static void GenerateTower(TowerPointInfo towerPoint)
        {
            if (towerPoint.unit != null)
            {
                Log.Info($"重复放置防御塔");
                return;
            }
            RoleConfig roleConfig = ConfigHelper.Get<RoleConfig>(RoleConfigId.TestTower);
            Unit unit = UnitFactory.Create(roleConfig, UnitType.Tower);
            towerPoint.unit = unit;
            unit.Position = towerPoint.position;


            var num = unit.AddComponent<NumericComponent>();
            var skillComponent = unit.AddComponent<UnitSkillComponent>();
            skillComponent.InitSkill(100001);
            skillComponent.LearnSkill(100001);

            var skillAI = unit.AddComponent<SkillAI>();
            skillAI.UpdateAutoSkill(new int[] { 100001});

            unit.AddComponent<BattleComponent>();
            unit.AddComponent<AttackComponent>();
            unit.AddComponent<TargetableUnitComponent>();

            var skillMgr = unit.AddComponent<SkillMgrComponent>();
            foreach (var unitSkill in skillComponent.GetLearnedSkills())
            {
                //if (unitSkill.IsPassive)
                //    continue;
                skillMgr.AddSkill(unit, unitSkill);
            }
            unit.AddComponent<ModifierContainerComponent>();

            num.Set(NumericType.HpBase, roleConfig.Hp);
            num.Set(NumericType.MaxHpBase, roleConfig.Hp);
            num.Set(NumericType.AtkBase, roleConfig.Atk);
            num.Set(NumericType.DefBase, roleConfig.Def);
            num.Set(NumericType.AtkFieldBase, roleConfig.AtkField);
            num.Set(NumericType.AtkSpdBase, roleConfig.AtkInterval);
            num.Set(NumericType.MoveSpdBase, roleConfig.Spd);

            unit.AddComponent<TowerAI>();
        }
    }
}
