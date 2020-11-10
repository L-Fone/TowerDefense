using Cal;
using Cal.DataTable;
using System;
using System.Collections.Generic;

namespace ET
{
    public class SkillLogic : Entity
    {
        public int skillId => skillLogicConfig.skillId;

        public int skillLevel;
        public int skillConfigId => skillLogicConfig.skillId * 100 + skillLevel;

        public Unit owner;

        public SkillLogicConfig skillLogicConfig;

        public SkillConfig skillConfig;

        private SkillAI _skillAI;
        public SkillAI skillAI
        {
            get => _skillAI = _skillAI??owner.GetComponent<SkillAI>();
            set => _skillAI = value;
        }
        public long lastCDTime;

        public FP dataX10000 = 1;
        public FP dataOldX10000 = 1;

        private AttackComponent attacker;
        /// <summary>
        /// 伤害倍数
        /// </summary>
        public FP multipleDamageX10000
        {
            get
            {
                attacker = attacker??owner.GetComponent<AttackComponent>();
                return attacker.multipleDamageX10000;
            }
            set
            {
                if (attacker)
                    attacker.multipleDamageX10000 = value;
            }
        }
        /// <summary>
        /// 释放次数
        /// </summary>
        public int playAmount
        {
            get
            {
                attacker = attacker??owner.GetComponent<AttackComponent>();
                return attacker.playAmount;
            }
            set
            {
                if (attacker)
                    attacker.playAmount = value;
            }
        }

    }
}
