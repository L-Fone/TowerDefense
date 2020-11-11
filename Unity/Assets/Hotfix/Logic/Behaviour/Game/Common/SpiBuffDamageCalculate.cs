using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public static class SpiBuffDamageCalculate
    {
        //最大暴击率
        private const float MaxCritRate = 0.95f;
        //基础暴击伤害
        private const float BaseCritDamage = 1.5f;
        public static void Calculate(NumericComponent num, NumericComponent numTarget, ValueCalculate valueCalculate_Self, ValueCalculate valueCalculate_Target, int skillId, out BallisticData data)
        {
            data = new BallisticData();
            //float baseHurt1 = 0, baseHurt2 = 0;
            ////攻击数值 = 攻击*技能百分比+技能数值威力
            ////基础伤害=攻击数值*攻击数值/（攻击数值+对方防御）
            //var def = numTarget.GetAsInt(NumericType.SpiDef) + 0.01f;
            //if (valueCalculate_Self != null && SkillHelper.GetParam(valueCalculate_Self.param, skillId, out var percValue1))
            //{
            //    BattleHelper.GetNumType(valueCalculate_Self, NumTargetType.Self, num, numTarget, out var num1, out var numericType1);
            //    float attackValue1 = num1.GetAsInt(numericType1) * percValue1 / 100;
            //    baseHurt1 = attackValue1 * attackValue1 / (attackValue1 + def);
            //}
            //if (valueCalculate_Target != null && SkillHelper.GetParam(valueCalculate_Target.param, skillId, out var percValue2))
            //{
            //    BattleHelper.GetNumType(valueCalculate_Target, NumTargetType.Target, num, numTarget, out var num2, out var numericType2);
            //    float attackValue2 = num2.GetAsInt(numericType2) * percValue2 / 100;
            //    baseHurt2 = attackValue2 * attackValue2 / (attackValue2 + def);
            //}
            //var baseHurt = baseHurt1 + baseHurt2;


            //if (num.Parent.GetComponent<ModifierContainerComponent>().modifierStateDic.TryGetValueByKey1(ModifierStateType.必定暴击, out _))
            //{
            //    data.isCrit = true;
            //}
            //else
            //{
            //    //暴击概率=0.05f+0.6f*(暴击率/（爆击率+对方暴击率抵抗）)
            //    FP crirBase = num.GetAsFloat(NumericType.Mcrir);
            //    FP cri = 0.05f + 0.6f * (crirBase * crirBase / (crirBase + numTarget.GetAsFloat(NumericType.Rmcrir) + 0.01f));

            //    //暴击概率 = 暴击概率>0.8f ? 0.8f:暴击概率
            //    cri = cri > MaxCritRate ? MaxCritRate : cri;

            //    //是否暴击 = 0-1 的随机数 <= 暴击概率
            //    data.isCrit = RandomHelper.RandomFloat() <= cri;
            //}

            ////暴击伤害倍率 = 有暴击？ （暴击伤害*2 + FirstCritDamage)/（暴击伤害*0.6f +暴击伤害抵抗 + 1）：1f
            //FP criBase = num.GetAsFloat(NumericType.Mcri);
            //FP criHurt = data.isCrit ? ((criBase * 2 + BaseCritDamage) / (criBase * 0.6f + numTarget.GetAsFloat(NumericType.Rmcri) + 1)) : 1;

            //data.value = baseHurt * criHurt * RandomHelper.RandomFloat(0.9f, 1.1f) * (1 - numTarget.GetAsFloat(NumericType.Nmeni));
        }
    }
}
