using System;

namespace ET
{
    public class SkillAI:Entity
    {
        public bool canSkill;
        internal int lastSkillTime;
        internal float roundCD;

        public void PlayAutoSkill(long now)
        {
            throw new NotImplementedException();
        }

        public void PlaySkill()
        {
            throw new NotImplementedException();
        }
    }
}