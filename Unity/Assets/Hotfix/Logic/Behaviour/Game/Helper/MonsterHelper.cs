using Cal.DataTable;
using System;
using System.Collections.Generic;

namespace ET
{
    public static class MonsterHelper
    {
        public static Unit GenerateMonster()
        {
            long roleId = RandomHelper.RandomNumber(RoleConfigId.TestMonster1, RoleConfigId.TestMonster4 + 1);
            RoleConfig roleConfig = ConfigHelper.Get<RoleConfig>(roleId);
            var unit = UnitFactory.Create(roleConfig, UnitType.Monster);

            //unit.AddComponent<DamageComponent>();
            unit.AddComponent<BattleComponent>();
            unit.AddComponent<AttackComponent>();
            unit.AddComponent<ModifierContainerComponent>();

            var num = unit.AddComponent<NumericComponent>();
            num.Set(NumericType.HpBase, roleConfig.Hp);
            num.Set(NumericType.MaxHpBase, roleConfig.Hp);
            num.Set(NumericType.AtkBase, roleConfig.Atk);
            num.Set(NumericType.DefBase, roleConfig.Def);
            num.Set(NumericType.AtkFieldBase, roleConfig.AtkField);
            num.Set(NumericType.AtkSpdBase, roleConfig.AtkInterval);
            num.Set(NumericType.MoveSpdBase, roleConfig.Spd);

            unit.AddComponent<MonsterAI>();
            Game.EventSystem.Publish_Sync(new ET.EventType.SetHudCharacter
            {
                unit = unit,
                zoneScene = unit.ZoneScene(),
                hp = roleConfig.Hp,
                maxHp = roleConfig.Hp,
                level = 100,
                name = "我是莽撞怪",
                progressTitleType = FairyGUI.ProgressTitleType.ValueAndMax
            });

            return unit;
        }
    }
}
