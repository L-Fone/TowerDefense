using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public enum NumTargetType
    {
        Self,
        Target
    }
    public static class BattleHelper
    {
        public static void GetNumType(ValueCalculate valueCalculate, NumTargetType numTargetType, NumericComponent _num, NumericComponent _numTarget, out NumericComponent num, out NumericType numericType)
        {
            switch (numTargetType)
            {
                case NumTargetType.Self:
                    num = _num;
                    break;
                case NumTargetType.Target:
                    num = _numTarget ?? throw new Exception("numTarget is null ,baseOriType  类型错误!");
                    break;
                default:
                    throw new Exception("baseOriType  类型错误!");
                    break;
            }
            switch (valueCalculate.calculateType)
            {
                case ValueCalculateType.物理攻击力x系数:
                    numericType = NumericType.Atk;
                    break;
                case ValueCalculateType.精神攻击力x系数:
                    numericType = NumericType.Atk;
                    break;
                case ValueCalculateType.生命值上限x系数:
                    numericType = NumericType.MaxHp;
                    break;
                case ValueCalculateType.当前生命值x系数:
                    numericType = NumericType.Hp;
                    break;
                default:
                    throw new Exception("calculateType  类型错误!");
                    break;
            } 
        }

        /// <summary>
        /// 计算伤害
        /// </summary>
        /// <param name="damageType"></param>
        /// <param name="owner"></param>
        /// <param name="target"></param>
        /// <param name="damageCalculate_Self"></param>
        /// <param name="damageCalculate_Target"></param>
        /// <param name="data"></param>
        internal static void Calculate(SkillDamageType damageType, ISkillSender skillSender, Unit target, ValueCalculate damageCalculate_Self, ValueCalculate damageCalculate_Target, out BallisticData data)
        {
            if (damageCalculate_Self == null && damageCalculate_Target == null)
            {
                data = default;
                return;
            }
            var num = skillSender.owner.GetComponent<NumericComponent>();
            var numTarget = target.GetComponent<NumericComponent>();
            switch (damageType)
            {
                default:
                case SkillDamageType.None:
                    data = default;
                    break;
                case SkillDamageType.物理伤害:
                    PhyBuffDamageCalculate.Calculate(num, numTarget, damageCalculate_Self, damageCalculate_Target, skillSender.skillLogic.skillConfigId, out data);
                    break;
                case SkillDamageType.精神伤害:
                    SpiBuffDamageCalculate.Calculate(num, numTarget, damageCalculate_Self, damageCalculate_Target, skillSender.skillLogic.skillConfigId, out data);
                    break;
                case SkillDamageType.真实伤害:
                    RealBuffDamageCalculate.Calculate(num, numTarget, damageCalculate_Self, damageCalculate_Target, skillSender.skillLogic.skillConfigId, out data);
                    break;
                case SkillDamageType.护盾治疗:
                    TreatBuffDamageCalculate.Calculate(num, numTarget, damageCalculate_Self, damageCalculate_Target, skillSender.skillLogic.skillConfigId, out data);
                    break;
            }
        }

        internal static void SetTargets(IEnumerable<Unit> teamList, IEnumerable<Unit> targetTeamList)
        {
            foreach (var unit in teamList)
            {
                //!增加目标
                unit.AddComponent<AttackComponent>();
                var targetUnits = unit.AddComponent<TargetableUnitComponent>();
                targetUnits.AddEnermy(targetTeamList);
                targetUnits.AddTeam(teamList);
                targetUnits.selectedEnermy = null;

                var skillMgr = unit.GetComponent<SkillMgrComponent>();
                var skillComponent = unit.GetComponent<UnitSkillComponent>();
                foreach (var unitSkill in skillComponent.GetLearnedSkills())
                {
                    //if (unitSkill.IsPassive)
                    //    continue;
                    skillMgr.AddSkill(unit, unitSkill);
                }
            }

            foreach (var unit in targetTeamList)
            {
                //!增加目标
                unit.AddComponent<AttackComponent>();
                var targetUnits = unit.AddComponent<TargetableUnitComponent>();
                targetUnits.AddEnermy(teamList);
                targetUnits.AddTeam(targetTeamList);
                targetUnits.selectedEnermy = null;

                var skillMgr = unit.GetComponent<SkillMgrComponent>();
                var skillComponent = unit.GetComponent<UnitSkillComponent>();
                foreach (var unitSkill in skillComponent.GetLearnedSkills())
                {
                    //if (unitSkill.IsPassive)
                    //    continue;
                    skillMgr.AddSkill(unit, unitSkill);
                }
            }
            return;



        }
        //!玩家释放技能
        public static void PlayerSkill(Unit unit, long now)
        {
            if (!unit.IsAlive) return;
            //if (unit.GetComponent<UserSetting>().IsAutoSkill)
            //{
                var ai = unit.GetComponent<SkillAI>();
                ai.canSkill = true;
                ai.PlayAutoSkill(now);
            //}
        }
        //!怪物释放技能
        public static void MonsterSkill(Unit unit)
        {
            if (!unit.IsAlive) return;
            var ai = unit.GetComponent<SkillAI>();
            ai.PlaySkill();
        }

    }
}
