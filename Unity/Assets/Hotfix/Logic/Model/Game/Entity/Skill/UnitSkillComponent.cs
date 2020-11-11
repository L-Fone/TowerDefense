using System;
using System.Collections.Generic;

namespace ET
{
    public class UnitSkillComponentDestroySystem : DestroySystem<UnitSkillComponent>
    {
    	public override void Destroy(UnitSkillComponent self)
    	{
    		self.Destroy();
    	}
    }
    public class UnitSkillComponent : Entity
    {
        private Dictionary<int, UnitSkill> learnedSkillDic = new Dictionary<int, UnitSkill>();
        private Dictionary<int, UnitSkill> unLearnedSkillDic = new Dictionary<int, UnitSkill> ();


        public void InitSkill(int skillId)
        {
            if(!unLearnedSkillDic.TryAdd(skillId,new UnitSkill
            {
                Id =skillId,
            }))
            {
                Log.Error($"unLearnedSkillDic has the key which is {skillId}");
            }
        }
        public void LearnSkill(int skillId)
        {
            if (!unLearnedSkillDic.TryGetValue(skillId, out var unitSkill))
            {
                if (!learnedSkillDic.TryGetValue(skillId, out unitSkill))
                {
                    Log.Error($"skillId = {skillId} is invalid");
                }
                unitSkill.Level++;
                learnedSkillDic[skillId] = unitSkill;
            }
            unLearnedSkillDic.Remove(skillId);
            unitSkill.Level++;
            learnedSkillDic[skillId] = unitSkill;
        }
        public IEnumerable<UnitSkill> GetLearnedSkills()
        {
            return learnedSkillDic.Values;
        }

        public UnitSkill GetLearnedSkill(int skillId)
        {
            learnedSkillDic.TryGetValue(skillId, out var unitSkill);
            return unitSkill;
        }

        internal void Destroy()
        {
            learnedSkillDic.Clear();
            unLearnedSkillDic.Clear();
        }
    }
}