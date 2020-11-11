using Cal.DataTable;
using ET.EventType;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ET
{
    public static class TowerHelper
    {
        private static TowerPointInfo towerPoint;
        public static void GenerateTower(long id)
        {
            towerPoint = TowerPointComponent.instance.Get(id);
            if (towerPoint == null)
            {
                Log.Error($"towerPoint == null when id == {id}");
                return;
            }
            GenerateTowerUnit(RoleConfigId.TestTower);
        }

        private static void GenerateTowerUnit(long towerId)
        {
            if (towerPoint.unit != null)
            {
                OnClickTower();
                return;
            }
            RoleConfig roleConfig = ConfigHelper.Get<RoleConfig>(towerId);
            Unit unit = UnitFactory.Create(roleConfig, UnitType.Tower);
            towerPoint.unit = unit;
            unit.Position = towerPoint.position;


            var num = unit.AddComponent<NumericComponent>();
            var skillComponent = unit.AddComponent<UnitSkillComponent>();
            skillComponent.InitSkill(100001);
            skillComponent.LearnSkill(100001);

            var skillAI = unit.AddComponent<SkillAI>();
            skillAI.UpdateAutoSkill(new int[] { 100001 });

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

        static List<ShowPopupUI.PopupMenuInfo> list = new List<ShowPopupUI.PopupMenuInfo>();
        private static void OnClickTower()
        {
            list.Clear();
            list.Add(new ShowPopupUI.PopupMenuInfo
            {
                name = "升级",
                action = OnUpgradeTower
            });
            list.Add(new ShowPopupUI.PopupMenuInfo
            {
                name = "拆卸",
                action = OnRemoveTower
            });
            Game.EventSystem.Publish_Sync(new ET.EventType.ShowPopupUI
            {
                zoneScene = towerPoint.unit.ZoneScene(),
                popupMenuInfo = list
            });
        }


        private static void OnUpgradeTower()
        {
            var unit = towerPoint.unit;
            long configId = unit.ConfigId;
            RoleConfig roleConfig = ConfigHelper.Get<RoleConfig>(configId);
            if (roleConfig.UpgradeId == 0)
            {
                Log.Error($"不能升级了");
                return;
            }
            UnitComponent.Instance.Remove(unit);
            towerPoint.unit = null;
            GenerateTowerUnit(roleConfig.UpgradeId);
        }
        private static void OnRemoveTower()
        {
            Log.Info($"移除防御塔");
            var unit = towerPoint.unit;
            UnitComponent.Instance.Remove(unit);
            towerPoint.unit = null;
        }
    }
}
