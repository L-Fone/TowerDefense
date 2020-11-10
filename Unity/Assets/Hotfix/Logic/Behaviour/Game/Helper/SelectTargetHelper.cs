using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public static class SelectTargetHelper
    {
        public static void GetTarget(SelectTargetBase selectTarget, ISkillSender skillSender,Action<Unit,ISkillSender> action)
        {
            var targetBase = selectTarget.GetTarget(skillSender);
            if (!targetBase) return;
            if (targetBase is TargetableSingleUnit singleUnit)
            {
                var target = singleUnit.target;
                if (!target)
                {
                    Log.Error($"单体目标为空 {skillSender}");
                    targetBase.Dispose();
                    return;
                }
                action?.Invoke(target,skillSender);
            }
            else if (targetBase is TargetableMultiUnit multiUnit)
            {
                var targetList = multiUnit.targetList;
                if (targetList == null)
                {
                    Log.Error($"多个目标为空 {skillSender}");
                    targetBase.Dispose();
                    return;
                }
                var owner = skillSender.owner;
                var attacker = owner.GetComponent<AttackComponent>();
                foreach (var target in targetList)
                {
                    action?.Invoke(target,skillSender);
                }
            }
            targetBase.Dispose();
        }
    }
}
