using Cal;
using Cal.DataTable;
using System;
using System.Collections.Generic;

namespace ET
{

    public class PlayerSkillAIAwakeSystem : AwakeSystem<SkillAI>
    {
        public override void Awake(SkillAI self)
        {
            var num = self.Parent.GetComponent<NumericComponent>();
            self.roundCD = num.GetAsFloat(NumericType.AtkSpd)*1000;
        }
    }
    public class SkillAIDestroySystem : DestroySystem<SkillAI>
    {
        public override void Destroy(SkillAI self)
        {
            self.canSkill = false;
            self.AutoSkillList.Clear();
            self.CurrSkillNode = null;
        }
    }
    public static class PlayerSkillAISystem
    {
        public static bool CheckCD(this SkillAI self, long now)
        {
            if (now - self.lastSkillTime >= self.roundCD.AsFloat())
            {
                self.lastSkillTime = now;
                return true;
            }
            return false;
        }
        public static void PlayAutoSkill(this SkillAI self, long now)
        {
            if (!self.canSkill) return;
            self.canSkill = false;
            var skills = self.AutoSkillList;
            if (skills.Count == 0) return;
            self.PlaySkill(skills, now).Coroutine();
        }
        private static async ETVoid PlaySkill(this SkillAI self, LinkedList<int> list, long now)
        {
            try
            {
                Unit unit = self.GetParent<Unit>();
                int skillCount = list.Count;
                for (int i = 0; i < skillCount; i++)
                {
                    var skillId = self.GetCurrSkillId(list);
                    if (skillId == 0) continue;
                    var skill = unit.GetComponent<UnitSkillComponent>().GetLearnedSkill(skillId);
                    if (skill == UnitSkill.Empty)
                    {
                        Log.Error($"使用了没学会的技能");
                        return;
                    }
                    int level = skill.Level;

                    //!+暂时
                    var skillMgr = unit.GetComponent<SkillMgrComponent>();
                    SkillLogic skillLogic = skillMgr.GetSkill(skillId);
                    if (skillLogic == null)
                    {
                        Log.Error($"skillLogic == null where skillid = {skillId}");
                        return;
                    }
                    skillLogic.skillLevel = level;
                    //!判断状态
                    if (CheckUnActionState(unit))
                    {
                        Log.Debug($"禁足");
                        return;
                    }
                    //var coolTimeDic = self.UserSetting.GetAutoSkillCoolTime();
                    //if (coolTimeDic.TryGetValue(skillId, out var coolTime))

                    //if (!SkillConfigComponent.SkillLogicCollection.skillDataDic.TryGetValue(skillId, out var skillData))
                    //{
                    //    Log.Error($"skillData == null where skillId = {skillId}");
                    //    continue;
                    //}
                    SkillConfig skillConfig = ConfigHelper.Get<SkillConfig>(skillId * 100);
                    if (now - skillLogic.lastCDTime > skillConfig.CD)
                    {
                        long time = skillLogic.lastCDTime;
                        skillLogic.lastCDTime = now;

                        var ret = await unit.GetComponent<AttackComponent>().StartSpellSkill(skillLogic);
                        if (!ret)
                        {
                            skillLogic.lastCDTime = time;
                        }
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
        /// <summary>
        /// 怪物释放逻辑
        /// </summary>
        /// <param name="self"></param>
        public static void PlaySkill(this SkillAI self)
        {
            try
            {
                Unit unit = self.GetParent<Unit>();
                if (!unit) return;
                var list = self.AutoSkillList;
                var skillId = self.GetCurrSkillId(list);
                if (skillId == 0) return;

                var skillConponent = unit.GetComponent<UnitSkillComponent>();
                var skill = skillConponent.GetLearnedSkill(skillId);
                int level = skill.Level;

                var attacker = unit.GetComponent<AttackComponent>();
                var skillMgr = unit.GetComponent<SkillMgrComponent>();
                SkillLogic skillLogic = skillMgr.GetSkill(skillId);
                if (skillLogic == null)
                {
                    Log.Error($"skillLogic == null where skillid = {skillId}");
                    return;
                }
                skillLogic.skillLevel = level;
                //!判断状态
                if (CheckUnActionState(unit))
                {
                    Log.Debug($"禁足");
                    return;
                }
                attacker.StartSpellSkill(skillLogic).Coroutine();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
        public static void UpdateAutoSkill(this SkillAI self,IEnumerable<int> skillList)
        {
            //var list = self.Parent.GetComponent<UserSetting>().GetAutoSkills();
            self.AutoSkillList = self.AutoSkillList ?? new LinkedList<int>();
            self.AutoSkillList.Clear();
            foreach (var item in skillList)
            {
                self.AutoSkillList.AddLast(item);
            }
        }
        private static bool CheckUnActionState(Unit unit)
        {
            var modifierContainer = unit.GetComponent<ModifierContainerComponent>();
            return Check(modifierContainer, ModifierStateType.眩晕) &&
                   Check(modifierContainer, ModifierStateType.石化) &&
                   Check(modifierContainer, ModifierStateType.冰冻) &&
                   Check(modifierContainer, ModifierStateType.沉默);
           
        }
        private static bool Check(ModifierContainerComponent modifierContainer, ModifierStateType modifierStateType)
        {
            if (modifierContainer.modifierStateDic.TryGetValueByKey1(modifierStateType, out var stateStateType))
                if (stateStateType == StateStateType.启用)
                    return true;
            return false;
        }
        private static int GetCurrSkillId(this SkillAI self, LinkedList<int> list)
        {
            var curr = self.CurrSkillNode ?? list.First;
            if (curr == list.Last)
                self.CurrSkillNode = null;
            else
                self.CurrSkillNode = curr.Next;
            return curr.Value;
        }
    }
}
