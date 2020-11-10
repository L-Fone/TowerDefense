using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public class SkillLogicDstroySystem : DestroySystem<SkillLogic>
    {
        public override void Destroy(SkillLogic self)
        {
            self.dataX10000 = 1 ;
            self.dataOldX10000 = 1;
            self.owner = null;
            self.skillLogicConfig = null;
            self.skillAI = null;
            self.lastCDTime = 0;
            self.skillLevel = 0;
        }
    }
    public static class SkillLogicSystem
    {
        public static void HandleEvent(this SkillLogic self, SkillEventCondition skillEventCondition,ISkillSender skillSender)
        {
            if (!self.skillLogicConfig.skillEventDic.TryGetValue(skillEventCondition, out var skillOptionBaseList))
            {
                return;
            }
            foreach (var option in skillOptionBaseList)
            {
                //SkillOptionLogicBase skillOptionLogicBase = new SkillOptionLogic_伤害();
                //skillOptionLogicBase.skillOptionBase = option;
                //skillOptionLogicBase.HandleEvent(skillSender);
                option.HandleEvent(skillSender);
            }
        }
        public static void UpdateLevel(this SkillLogic self)
        {
            throw new NotImplementedException();
        }
    }
}
