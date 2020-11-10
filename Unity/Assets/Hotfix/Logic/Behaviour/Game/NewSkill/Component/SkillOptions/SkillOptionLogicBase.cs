using Cal;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET
{
    public abstract class SkillOptionLogicBase
    {
        public abstract SkillOptionBase skillOptionBase { get; set; }
        public abstract SkillLogic skillLogic { get; set; }
        public abstract void HandleEvent(ISkillSender skillSender);
    }
}
namespace ET
{
    public class SkillOptionLogic_伤害:SkillOptionLogicBase
    {
        public override SkillOptionBase skillOptionBase { get; set; }
        public override SkillLogic skillLogic { get; set; }

        public override void HandleEvent(ISkillSender skillSender)
        {
            //var targetBase = skillOptionBase.As<SkillOption_伤害>().selectTarget.GetTarget(skillSender);
            //if (targetBase is TargetableSingleUnit singleUnit)
            //{
            //    var target = singleUnit.target;
            //    if (target == null)
            //    {
            //        Log.Error($"单体目标为空 {skillSender}");
            //    }
            //    //!计算
            //    var owner = skillSender.owner;
            //    BattleHelper.Calculate(skillOptionBase.As<SkillOption_伤害>().damageType, owner, target, skillOptionBase.As<SkillOption_伤害>().damageCalculate_Self, skillOptionBase.As<SkillOption_伤害>().damageCalculate_Target, out int value, out bool isCrit);
            //    target.GetComponent<BattleComponent>().Damage(value, isCrit);
            //}
            //else if (targetBase is TargetableMultiUnit multiUnit)
            //{
            //    var targetList = multiUnit.targetList;
            //    if (targetList == null)
            //    {
            //        Log.Error($"多个目标为空 {skillSender}");
            //    }
            //    var owner = skillSender.owner;
            //    foreach (var target in targetList)
            //    {
            //        //!计算
            //        BattleHelper.Calculate(skillOptionBase.As<SkillOption_伤害>().damageType, owner, target, skillOptionBase.As<SkillOption_伤害>().damageCalculate_Self, skillOptionBase.As<SkillOption_伤害>().damageCalculate_Target, out int value, out bool isCrit);
            //        target.GetComponent<BattleComponent>().Damage(value, isCrit);
            //    }
            //}

        }
    }
}