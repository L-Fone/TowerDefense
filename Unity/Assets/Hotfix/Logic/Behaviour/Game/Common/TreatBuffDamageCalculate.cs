using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public static class TreatBuffDamageCalculate
    {
        //最大暴击率
        private const float MaxCritRate = 0.95f;
        //基础暴击伤害
        private const float BaseCritDamage = 1.5f;
        public static void Calculate(NumericComponent num, NumericComponent numTarget , ValueCalculate valueCalculate_Self, ValueCalculate valueCalculate_Target, int skillId, out BallisticData data)
        {
            data = default;
            //float baseHurt1 = 0, baseHurt2 = 0;
            ////攻击数值 = 攻击*技能百分比+技能数值威力
            ////基础伤害=攻击数值*攻击数值/（攻击数值+对方防御）
            //if (valueCalculate_Self != null && SkillHelper.GetParam(valueCalculate_Self.param, skillId, out var percValue1))
            //{
            //    BattleHelper.GetNumType(valueCalculate_Self, NumTargetType.Self, num, numTarget, out var num1, out var numericType1);
            //    baseHurt1 = num1.GetAsInt(numericType1) * percValue1 / 100;
            //}
            //if (valueCalculate_Target != null && SkillHelper.GetParam(valueCalculate_Target.param, skillId, out var percValue2))
            //{
            //    BattleHelper.GetNumType(valueCalculate_Target, NumTargetType.Target, num, numTarget, out var num2, out var numericType2);
            //    baseHurt2 = num2.GetAsInt(numericType2) * percValue2 / 100;
            //}
            //var baseHurt = baseHurt1 + baseHurt2;

            //int phyAtk = num.GetAsInt(NumericType.PhyAtk);
            //int spiAtk = num.GetAsInt(NumericType.SpiAtk);

            //FP rPcrir, rPcri, rMcrir, rMcri;
            //FP criHurt;
            //if (phyAtk > spiAtk)
            //{
            //    if (num.Parent.GetComponent<ModifierContainerComponent>().modifierStateDic.TryGetValueByKey1(ModifierStateType.必定暴击, out _))
            //    {
            //        data.isCrit = true;
            //    }
            //    else
            //    {
            //        //暴击概率=0.05f + 0.6f*(暴击率/（爆击率+对方暴击率抵抗）)
            //        FP crirBase = num.GetAsFloat(NumericType.Pcrir);
                   
            //            rPcrir = 0;
            //        FP cri = 0.05f + 0.6f * (crirBase * crirBase / (crirBase + rPcrir + 0.01f));

            //        //暴击概率 = 暴击概率>0.8f ? 0.8f:暴击概率
            //        cri = cri > MaxCritRate ? MaxCritRate : cri;

            //        //是否暴击 = 0-1 的随机数 <= 暴击概率
            //        data.isCrit = RandomHelper.RandomFloat() <= cri;
            //    }

            //    //暴击伤害倍率 = 有暴击？（暴击伤害*2 + FirstCritDamage)/（暴击伤害*0.6f +暴击伤害抵抗 + 1）：1f
            //    FP criBase = num.GetAsFloat(NumericType.Pcri);
            //        rPcri = 0;
            //    criHurt = data.isCrit ? ((criBase * 2 + BaseCritDamage) / (criBase * 0.6f + rPcri + 1)) : 1;
            //}
            //else
            //{
            //    if (num.Parent.GetComponent<ModifierContainerComponent>().modifierStateDic.TryGetValueByKey1(ModifierStateType.必定暴击,out _))
            //    {
            //        data.isCrit = true;
            //    }
            //    else
            //    {
            //        //暴击概率=0.05f+0.6f*(暴击率/（爆击率+对方暴击率抵抗）)
            //        FP crirBase = num.GetAsFloat(NumericType.Mcrir);
            //            rMcrir = 0;
            //        FP cri = 0.05f + 0.6f * (crirBase * crirBase / (crirBase + rMcrir + 0.01f));

            //        //暴击概率 = 暴击概率>0.8f ? 0.8f:暴击概率
            //        cri = cri > MaxCritRate ? MaxCritRate : cri;

            //        //是否暴击 = 0-1 的随机数 <= 暴击概率
            //        data.isCrit = RandomHelper.RandomFloat() <= cri;
            //    }

            //    //暴击伤害倍率 = 有暴击？（暴击伤害*2 + FirstCritDamage)/（暴击伤害*0.6f +暴击伤害抵抗 + 1）：1f
            //    FP criBase = num.GetAsFloat(NumericType.Mcri);
            //        rMcri = 0;
            //    criHurt = data.isCrit ? ((criBase * 2 + BaseCritDamage) / (criBase * 0.6f + rMcri + 1)) : 1;
            //}

            //data.value = baseHurt * criHurt * RandomHelper.RandomFloat(0.9f, 1.1f);
        }
    }
    //public class RealBuffPosionDamageCalculate : IBuffDamageCalculate
    //{
    //    public void Calculate(NumericComponent num, NumericComponent numTarget, BasicDamageBuffData damageEffcetData, out int value, out bool isCrit)
    //    {
    //        float baseHurt = 0;

    //        var buffComponent = num.Parent.GetComponent<BuffComponent>();
    //        if (buffComponent.HasBuffByWorkType(BuffWorkTypes.必定暴击))
    //        {
    //            isCrit = true;
    //        }
    //        else
    //        {
    //            isCrit = false;
    //        }
    //        var targetBuffComponent = numTarget.Parent.GetComponent<BuffComponent>();
    //        var poisonList = targetBuffComponent.GetBuffByWorkType(BuffWorkTypes.中毒);
    //        if (poisonList != null)
    //        {
    //            foreach (var buff in poisonList)
    //            {
    //                var buffEffect = buff.buffDataBase.As<ChangePropertyBuffData>().buffEffect;
    //                BattleHelper.GetNumType(buffEffect.data, num, numTarget, out var __num, out var numericType);
    //                //!辅助值影响毒爆伤害
    //                var tempHurt = buff.CurrentOverlay * (buffEffect.data.percValue * __num.GetAsInt(numericType)) * (1 + num.GetAsFloat(NumericType.Dvo));
    //                //!计算最大伤害
    //                int atk = CharacterHelper.GetLargerAtk(num);
    //                float maxHurt = atk * buffEffect.data.constValue * buff.CurrentOverlay;

    //                baseHurt += MathHelper.RoundToInt(Math.Clamp(tempHurt, 1, maxHurt));
    //                buff.CurrentOverlay = 0;
    //            }
    //        }
    //        else
    //        {
    //            var unit = numTarget.GetParent<Unit>();
    //            Log.Error($"【({unit.Id})】有【毒爆】没有【中毒】");
    //        }

    //        float criHurt = 1f;
    //        //!辅助值影响附加伤害
    //        value = MathHelper.RoundToInt(baseHurt * criHurt * RandomHelper.RandomFloat(0.9f, 1.1f));
    //    }
    //}
}
