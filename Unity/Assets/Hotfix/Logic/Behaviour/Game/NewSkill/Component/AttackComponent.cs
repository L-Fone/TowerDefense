using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public struct BallisticData
    {
        public FP value;
        public bool isCrit;

        public void ChangeValue(FP value)
        {
            this.value = value;
        }
        public void ChangeCrit(bool value)
        {
            isCrit = value;
        }
        public override string ToString()
        {
            return $"value:{value:f2} isCrit:{isCrit}";
        }

        public static BallisticData operator +(BallisticData a, BallisticData b)
        {
            return new BallisticData
            {
                value = a.value + b.value,
                isCrit = a.isCrit || b.isCrit
            };
        }
    }
    public class AttackComponent : Entity
    {
        /// <summary>
        /// 伤害倍数
        /// </summary>
        public FP multipleDamageX10000 = 1;
        /// <summary>
        /// 释放次数
        /// </summary>
        public int playAmount = 1;

        public BallisticData attackData;

        public Unit attacker;
    }
}
