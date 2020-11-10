using Cal;
using Cal.DataTable;
using System;
using System.Collections.Generic;

namespace ET
{
    public class SkillMgrComponentDstroySystem : DestroySystem<SkillMgrComponent>
    {
        public override void Destroy(SkillMgrComponent self)
        {
            self.skillDic.Clear();
        }
    }
    public static class SkillMgrComponentSystem
    {
        public static void AddSkill(this SkillMgrComponent self, Unit skillFrom, UnitSkill unitSkill)
        {
            if (unitSkill == null)
            {
                Log.Error($"unitSkill==null where unitId ={skillFrom.Id}");
                return;
            }
            int skillId = (int)unitSkill.Id;
            var unit = self.GetParent<Unit>();
            if (self.skillDic.TryGetValue(skillId, out var skillLogic))
            {
                ExecuteEvent();
                return;
            }
            skillLogic = EntityFactory.CreateWithParent<SkillLogic>(self);
            //if (!SkillConfigComponent.SkillLogicCollection.skillDic.TryGetValue(skillId, out var skillLogicConfig))
            //{
            //    Log.Error($"skillLogicConfig==null where skillId = {skillId}");
            //    return;
            //}
            SkillLogicConfig skillLogicConfig = null;
            skillLogic.skillLogicConfig = skillLogicConfig;
            skillLogic.owner = skillFrom;
            skillLogic.skillLevel = unitSkill.Level;

            SkillConfig skillConfig = ConfigHelper.Get<SkillConfig>(skillId*100);
            skillLogic.skillConfig = skillConfig;

            if (!self.skillDic.TryAdd(skillId, skillLogic))
            {
                Log.Error($"[上面已经判断了不含有这个键]");
                return;
            }

            //!执行添加技能的事件
            ExecuteEvent();

            void ExecuteEvent()
            {
                if (unitSkill.IsPassive)
                    skillLogic.HandleEvent(SkillEventCondition.当技能添加, new SkillSender
                    {
                        owner = skillFrom,
                        target = unit,
                        skillLogic = skillLogic
                    });
            }
        }
        public static SkillLogic GetSkill(this SkillMgrComponent self, int skillId)
        {
            self.skillDic.TryGetValue(skillId, out var skillLogic);
            return skillLogic;
        }
        public static void UpdateSkill(this SkillMgrComponent self, Unit skillFrom, int skillId)
        {
            if (!self.skillDic.TryGetValue(skillId, out var skillLogic))
            {
                Log.Error($"skillLogic == null where skillid = {skillId}");
                return;
            }
            //!升级
            skillLogic.UpdateLevel();

            var unit = self.GetParent<Unit>();
            //!执行添加技能的事件
            skillLogic.HandleEvent(SkillEventCondition.当技能升级, new SkillSender
            {
                owner = skillFrom,
                target = unit,
                skillLogic = skillLogic
            });
        }
        public static void RemoveSkill(this SkillMgrComponent self, Unit skillFrom, int skillId)
        {
            if (!self.skillDic.TryGetValue(skillId, out var skillLogic))
            {
                Log.Error($"skillLogic == null where skillid = {skillId}");
                return;
            }
            skillLogic.Dispose();
            self.skillDic.Remove(skillId);

            var unit = self.GetParent<Unit>();
            //!执行添加技能的事件
            skillLogic.HandleEvent(SkillEventCondition.当技能移除, new SkillSender
            {
                owner = skillFrom,
                target = unit,
                skillLogic = skillLogic
            });
        }
    }
}
