using ET;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Cal
{
    public enum SingleTargetType
    {
        施法者,
        重新选其他的目标,
        攻击者,
        [LabelText("Modifier附着体")]
        Modifier容器
    }
    public enum TeamType
    {
        无,
        敌方队伍,
        友方队伍,
        全体队伍
    }
    [Flags]
    public enum TargetFlagType
    {
        无 = 0,
        死亡的 = 1,
        无敌的 = 1 << 1,
        隐身的 = 1 << 2,
        免疫控制的 = 1 << 3,
        召唤的 = 1 << 4,
        可造成伤害的 = 死亡的 | 无敌的 | 隐身的,
    }
    [GUIColor(1f, 1f, 0.35f)]
    public abstract class SelectTargetBase
    {
        [LabelText("队伍类型")]
        public TeamType teamType;

        [LabelText("选定排除的状态")]
        public TargetFlagType flags;
#if UNITY
        public abstract TargetableUnitBase GetTarget(ISkillSender skillSender);

        /// <summary>
        /// 选取可以攻击的目标
        /// </summary>
        protected void SelectUnitByFlags(List<Unit> sourceList, List<Unit> retList)
        {
            retList.Clear();
            if (sourceList == null || sourceList.Count == 0)
                return;
            foreach (var u in sourceList)
            {
                if (IsValid(u))
                    retList.Add(u);
            }

        }

        private bool IsValid(Unit unit)
        {
            if (flags.HasFlag(TargetFlagType.死亡的))
            {
                if (!unit.IsAlive)
                {
                    return false;
                }
            }
            if (flags.HasFlag(TargetFlagType.无敌的))
            {

            }
            if (flags.HasFlag(TargetFlagType.隐身的))
            {

            }
            if (flags.HasFlag(TargetFlagType.免疫控制的))
            {

            }
            if (flags.HasFlag(TargetFlagType.召唤的))
            {

            }
            return true;
        }
#endif

    }
    public class SelectSingleTarget : SelectTargetBase
    {
        [LabelText("选择的单体类型")]
        public SingleTargetType targetType = SingleTargetType.施法者;

#if UNITY
        public override TargetableUnitBase GetTarget(ISkillSender skillSender)
        {
            var component = skillSender.owner.GetComponent<TargetableUnitComponent>();
            if(component==null)
            {
                Log.Warning($"战斗已经结束");
                return null;
            }
            switch (targetType)
            {
                case SingleTargetType.施法者:
                    component.targetList.Add(skillSender.owner);
                    component.currTarget = skillSender.owner;
                    break;
                case SingleTargetType.重新选其他的目标:
                    switch (teamType)
                    {
                        default:
                        case TeamType.无:
                            break;
                        case TeamType.敌方队伍:
                            SelectUnitByFlags(component.allEnermy, component.targetList);
                            if (component.selectedEnermy != null)
                                component.currTarget = component.selectedEnermy;
                            else
                            {
                                //!随机选吧
                                var list = component.targetList;
                                if (list.Count == 0)
                                    component.currTarget = null;
                                else
                                    component.currTarget = list[RandomHelper.RandomNumber(0, list.Count)];
                            }
                            break;
                        case TeamType.友方队伍:
                            SelectUnitByFlags(component.allTeam, component.targetList);
                            if (component.selectedTeamMember != null)
                                component.currTarget = component.selectedTeamMember;
                            else
                            {
                                //!随机选吧
                                var list = component.targetList;
                                if (list.Count == 0)
                                    component.currTarget = null;
                                else
                                    component.currTarget = list[RandomHelper.RandomNumber(0, list.Count)];
                            }
                            break;
                        case TeamType.全体队伍:
                            //!不做
                            break;
                    }
                    break;
                case SingleTargetType.攻击者:
                    component.currTarget = skillSender.owner;
                    break;
                case SingleTargetType.Modifier容器:
                    component.currTarget = skillSender.target;
                    break;
                default:
                    break;
            }

            TargetableSingleUnit targetableSingleUnit = EntityFactory.Create<TargetableSingleUnit, Unit>(Game.Scene, component.currTarget);
            return targetableSingleUnit;
        }
//#elif UNITY_EDITOR
        [Button("敌方单体目标")]
        [ButtonGroup("")]
        public void EnermySingle()
        {
            this.flags = TargetFlagType.可造成伤害的;
            this.targetType = SingleTargetType.重新选其他的目标;
            this.teamType = TeamType.敌方队伍;
        }
        [Button("友方单体目标")]
        [ButtonGroup("")]
        public void TeamSingle()
        {
            this.flags = TargetFlagType.死亡的;
            this.targetType = SingleTargetType.重新选其他的目标;
            this.teamType = TeamType.友方队伍;
        }

#endif
    }

    public class SelectMultiTarget : SelectTargetBase
    {

        [LabelText("是否随机数量")]
        public bool isRndom = false;

        [LabelText("最小目标数量")]
        [ShowIf("isRndom")]
        public SkillParam minCount;

        [LabelText("最大目标数量")]
        public SkillParam maxCount;

#if UNITY
        public override TargetableUnitBase GetTarget(ISkillSender skillSender)
        {
            var component = skillSender.owner.GetComponent<TargetableUnitComponent>();
            if (component == null)
            {
                Log.Warning($"战斗已经结束");
                return null;
            }
            switch (teamType)
            {
                default:
                case TeamType.无:
                    break;
                case TeamType.敌方队伍:
                    SelectUnitByFlags(component.allEnermy, component.targetList);
                    if (component.targetList.Count == 0)
                        return null;
                    if (component.selectedEnermy)
                        component.currTarget = component.selectedEnermy;
                    else
                    {
                        component.currTarget = null;
                    }
                    break;
                case TeamType.友方队伍:
                    SelectUnitByFlags(component.allTeam, component.targetList);
                    if (component.targetList.Count == 0)
                        return null;
                    if (component.selectedTeamMember)
                        component.currTarget = component.selectedTeamMember;
                    else
                    {
                        component.currTarget = null;
                    }

                    break;
                case TeamType.全体队伍:
                    //!不做
                    break;
            }

            SelectUnits(component, skillSender);

            TargetableMultiUnit targetableMultiUnit = EntityFactory.Create<TargetableMultiUnit, IEnumerable<Unit>>(Game.Scene, component.targetList);
            return targetableMultiUnit;
        }
        private void SelectUnits(TargetableUnitComponent component, ISkillSender skillSender)
        {
            SkillHelper.SelectUnits(component.targetList, component.currTarget, SkillHelper.RandomNumber(skillSender.skillLogic.skillConfigId, minCount, maxCount, isRndom));
        }
#endif
    }
}
