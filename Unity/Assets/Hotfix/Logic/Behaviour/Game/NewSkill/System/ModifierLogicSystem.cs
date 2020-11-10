using Cal;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET
{
    public class ModifierLogicDstroySystem : DestroySystem<ModifierLogic>
    {
        public override void Destroy(ModifierLogic self)
        {
            self.overlay = 0;
            self.interval = 0;
            self.leastTime = 0;
            self.shield = 0;
            self.multiDamageX10000 =0;
            self.playAmount =0;
            self.modifierConfig = null;
            self.cancellationToken = null;
            self.skillLogic = null;
        }
    }
    public static class ModifierLogicSystem
    {
        public static void HandleEvent(this ModifierLogic self, ModifierEventCondition modifierEventCondition, ISkillSender skillSender)
        {
            var dic = self.modifierConfig.modifierEventDic;
            if (dic == null) return;
            if(!dic.TryGetValue(modifierEventCondition,out var list))
            {
                return;
            }
            foreach (var item in list)
            {
                item.HandleEvent(skillSender);
            }
        }

    }
}
