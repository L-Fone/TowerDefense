
using ET;
using MongoDB.Bson.Serialization.Attributes;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using System.Linq;
using CalEditor;
#endif

namespace Cal
{
    [GUIColor(0.8f, 0.8f, 1f)]
    public struct SkillParam
    {

        [LabelText("数据来源")]
        public SkillSourcetype skillSourcetype;

        [LabelText("输入数值")]
        [ShowIf("skillSourcetype", SkillSourcetype.Input)]
        public float value;

        [LabelText("选择参数")]
        [ShowIf("skillSourcetype", SkillSourcetype.DataTable)]
        public SkillDataTableArgs args;

        [Button("不需要参数")]
        [ButtonGroup("param")]
        void NoneParam()
        {
            skillSourcetype = SkillSourcetype.None;
        }
        [Button("输入型")]
        [ButtonGroup("param")]
        void InputParam()
        {
            skillSourcetype = SkillSourcetype.Input;
        }
        [Button("表格型")]
        [ButtonGroup("param")]
        void DataTableParam()
        {
            skillSourcetype = SkillSourcetype.DataTable;
        }

    }

    [HideReferenceObjectPicker]
    public class ValueCalculate
    {
        [LabelText("伤害计算类型")]
        public ValueCalculateType calculateType;

        [LabelText("伤害系数")]
        public SkillParam param;

    }
    public abstract class SkillOptionBase
    {
        [LabelText("操作类型")]
        [ShowInInspector]
        public abstract SkillOptionType optionType { get; }

#if UNITY
        public abstract void HandleEvent(ISkillSender skillSender);
#endif

    }
    [GUIColor(0.95f, 0.08f, 0f)]
    public class SkillOption_伤害 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.伤害;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget
        {
            targetType = SingleTargetType.Modifier容器,
        };

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("是否触发暴击事件")]
        public bool isCritEvent = true;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("伤害类型")]
        public SkillDamageType damageType = SkillDamageType.物理伤害;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("基于释放者属性的伤害系数")]
        public ValueCalculate damageCalculate_Self = new ValueCalculate
        {
            calculateType = ValueCalculateType.物理攻击力x系数,
            param = new SkillParam
            {
                args = SkillDataTableArgs.Args1
            }
        };

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("基于目标属性的伤害系数")]
        public ValueCalculate damageCalculate_Target;
#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {

                var owner = skillSender.owner;

                //!++测试
                Game.EventSystem.Publish_Sync(new ET.EventType.ShowDebugAtkLine
                {
                    unit = owner,
                    target = target,
                });

                var modifierContainer = owner.GetComponent<ModifierContainerComponent>();
                var (optionList, __modifierLogic) = modifierContainer.GetSkillOptionBaseArr(ModifierEventCondition.当拥有modifier的单位开始攻击某个目标);
                if (optionList != null)
                    foreach (var item in optionList)
                        item.HandleEvent(new ModifierSkillSender
                        {
                            owner = owner,
                            target = target,
                            skillLogic = __modifierLogic.skillLogic,
                            modifierLogic = __modifierLogic
                        });

                var attacker = owner.GetComponent<AttackComponent>();
                bool isCrit = false;
                ModifierSkillSender modifierSkillSender = default;
                ModifierLogic modifierLogic = null;
                BattleHelper.Calculate(damageType, skillSender, target, damageCalculate_Self, damageCalculate_Target, out BallisticData data);
                FP finalValue = data.value;
                if (skillSender is ModifierSkillSender _modifierSkillSender)
                {
                    modifierLogic = _modifierSkillSender.modifierLogic;
                    if (modifierLogic)
                    {
                        finalValue *= modifierLogic.overlay;
                        isCrit = isCritEvent && data.isCrit;
                        if (isCrit)
                            modifierSkillSender = _modifierSkillSender;
                    }
                }
                data.ChangeValue(finalValue * skillSender.skillLogic.multipleDamageX10000);
                attacker.AttackTarget(target, data, skillSender);
                if (isCrit)
                {
                    if (skillSender.skillLogic.skillLogicConfig.skillEventDic.TryGetValue(SkillEventCondition.当技能暴击时, out var skillList))
                        foreach (var item in skillList)
                            item.HandleEvent(skillSender);
                    if (modifierLogic)
                    {
                        var dic = modifierLogic.modifierConfig.modifierEventDic;
                        if (dic != null &&
                            dic.TryGetValue(ModifierEventCondition.当拥有modifier的单位被暴击时, out var optionBaseList))
                            foreach (var item in optionBaseList)
                                item.HandleEvent(modifierSkillSender);
                    }
                }

            });
        }
#endif
    }
    [GUIColor(0.6f, 0.8f, 0f)]
    public class SkillOption_治愈 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.治愈;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget
        {
            targetType = SingleTargetType.Modifier容器,
        };

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("是否触发暴击事件")]
        public bool isCritEvent = true;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("基于释放者属性的治疗系数")]
        public ValueCalculate treatCalculate_Self;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("基于目标属性的治疗系数")]
        public ValueCalculate treatCalculate_Target;
#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
              {
                  var owner = skillSender.owner;
                  var attacker = owner.GetComponent<AttackComponent>();
                  bool isCrit = false;
                  ModifierSkillSender modifierSkillSender = default;
                  ModifierLogic modifierLogic = null;
                  BattleHelper.Calculate(SkillDamageType.护盾治疗, skillSender, target, treatCalculate_Self, treatCalculate_Target, out BallisticData data);
                  FP finalValue = data.value;
                  if (skillSender is ModifierSkillSender _modifierSkillSender)
                  {
                      modifierLogic = _modifierSkillSender.modifierLogic;
                      if (modifierLogic != null)
                      {
                          finalValue *= modifierLogic.overlay;
                          isCrit = isCritEvent && data.isCrit;
                          if (isCrit)
                              modifierSkillSender = _modifierSkillSender;
                      }
                  }
                  data.ChangeValue(finalValue);
                  attacker.TreatTarget(target, data, skillSender);
                  if (isCrit)
                  {
                      var dic = modifierLogic.modifierConfig.modifierEventDic;
                      if (dic != null &&
                          dic.TryGetValue(ModifierEventCondition.当拥有modifier的单位被暴击时, out var optionList))
                          foreach (var item in optionList)
                              item.HandleEvent(modifierSkillSender);
                  }
              });
        }
#endif
    }
    [GUIColor(0.6f, 0.8f, 0f)]
    public class SkillOption_护盾 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.护盾;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("是否触发暴击事件")]
        public bool isCritEvent = true;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("基于释放者属性的治疗系数")]
        public ValueCalculate treatCalculate_Self;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("基于目标属性的治疗系数")]
        public ValueCalculate treatCalculate_Target;
#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                if (!(skillSender is ModifierSkillSender modifierSkillSender))
                    return;
                var modifierLogic = modifierSkillSender.modifierLogic;
                if (modifierLogic == null)
                    return;
                var num = skillSender.owner.GetComponent<NumericComponent>();
                var targetNum = target.GetComponent<NumericComponent>();
                BattleHelper.Calculate(SkillDamageType.护盾治疗, skillSender, target, treatCalculate_Self, treatCalculate_Target, out var data);
                modifierLogic.shield = MathHelper.RoundToInt(data.value);

                if (data.isCrit && isCritEvent)
                {
                    var dic = modifierLogic.modifierConfig.modifierEventDic;
                    if (dic != null &&
                        dic.TryGetValue(ModifierEventCondition.当拥有modifier的单位被暴击时, out var optionList))
                        foreach (var item in optionList)
                            item.HandleEvent(modifierSkillSender);
                }

            });
        }
#endif
    }
    [GUIColor(0.5f, 0.8f, 0.5f)]
    public class SkillOption_几率 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.几率;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("几率(0-1)")]
        public SkillParam param;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("事件成功操作列表"), PropertyOrder(100)]
        public SkillOptionBase[] succeedOptionList = { new SkillOption_应用Modifier { } };

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("事件失败操作列表"), PropertyOrder(100)]
        public SkillOptionBase[] failOptionList;

#if UNITY
        public override void HandleEvent(ISkillSender skillSender)
        {
            if (!SkillHelper.GetParam(param, skillSender.skillLogic.skillConfigId, out var value))
                return;
            if (MathHelper.IsHit(value / 100))
            {
                if (succeedOptionList != null)
                    foreach (var item in succeedOptionList)
                        item.HandleEvent(skillSender);
            }
            else
            {
                if (failOptionList != null)
                    foreach (var item in failOptionList)
                        item.HandleEvent(skillSender);
            }
        }
#endif
    }
    [GUIColor(0.8f, 0.9f, 0.2f)]
    public class SkillOption_应用Modifier : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.应用Modifier;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget { targetType = SingleTargetType.Modifier容器 };

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("Modifier的Id")]
        public ModifierId modifierId;
#if UNITY_EDITOR
        [BoxGroup("$optionType", CenterLabel = true)]
        [Button("自动匹配Id")]
        void AutoId()
        {
            try
            {
                int index;
                int skillId = SkillAndModifierSObject.Instance.skillId;
                var keyList = SkillAndModifierSObject.Instance.data.modifierDic.Keys.Select(t => t.Value % 100).ToList();
                keyList.Sort();
                if (keyList.Count != 0)
                    index = keyList[keyList.Count - 1];
                else
                    index = 10;
                while (!keyList.Contains(++index))
                {
                    break;
                }
                modifierId.Value = skillId * 100 + index;
                UnityEditor.AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        [BoxGroup("$optionType", CenterLabel = true)]
        [Button("生成Modifier")]
        void GenerateModifier()
        {
            try
            {
                var dic = SkillAndModifierSObject.Instance.data.modifierDic;
                if (dic == null)
                {
                    dic = SkillAndModifierSObject.Instance.data.modifierDic = new Dictionary<ModifierId, ModifierConfig>();
                }
                dic.Add(modifierId, new ModifierConfig { Id = modifierId });
                UnityEditor.AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
#endif
#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                var modifierComponent = target.GetComponent<ModifierContainerComponent>();
                modifierComponent.ApplyModifier(skillSender.owner, skillSender.skillLogic, modifierId);
            });
        }
#endif
    }
    [GUIColor(0.8f, 0.9f, 0.2f)]
    public class SkillOption_移除Modifier : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.移除Modifier;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("Modifier的Id")]
        public ModifierId modifierId;
#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {

            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                if (skillSender is ModifierSkillSender modifierSkillSender)
                    target.GetComponent<ModifierContainerComponent>().RemoveModifier(modifierSkillSender);
            });

        }
#endif
    }
    [GUIColor(0.8f, 0.9f, 0.2f)]
    public class SkillOption_修改Modifier数据 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.修改Modifier数据;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("Modifier的Id")]
        public ModifierId modifierId;

#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            var owner = _skillSender.owner;
            SkillLogic skillLogic = _skillSender.skillLogic;
            var num = owner.GetComponent<NumericComponent>();
            var modifierContainer = skillLogic.owner.GetComponent<ModifierContainerComponent>();
            var logic = modifierContainer.GetModifierLogic(modifierId);
            if (logic != null)
            {
                modifierContainer.RemoveModifier(logic);
            }
            skillLogic.dataX10000 = (1 - (FP)num.GetAsInt(NumericType.Hp) / num.GetAsInt(NumericType.MaxHp)) / FP.EN1;
            modifierContainer.ApplyModifier(skillLogic.owner, skillLogic, modifierId);
        }
#endif
    }
    [GUIColor(0.8f, 0.9f, 0.2f)]
    public class SkillOption_延迟操作 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.延迟操作;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("延迟的回合数")]
        public SkillParam round;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("操作列表")]
        public SkillOptionBase[] value;
#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {

        }
#endif
    }
    [GUIColor(0.8f, 0.9f, 0.2f)]
    public class SkillOption_额外攻击目标 : SkillOptionBase
    {
        //!递减系数
        const float ReduceCoefficent = 0.75f;
        public override SkillOptionType optionType => SkillOptionType.额外攻击目标;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("伤害系数")]
        public SkillParam valueParam;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("是否随机数量")]
        public bool isRndom;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("最小额外攻击数")]
        [ShowIf("isRndom")]
        public SkillParam minCount;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("最大额外攻击数")]
        public SkillParam maxCount;
#if UNITY
        public override void HandleEvent(ISkillSender skillSender)
        {
            var attacker = skillSender.owner.GetComponent<AttackComponent>();
            var targetUnits = skillSender.owner.GetComponent<TargetableUnitComponent>();
            var currTarget = targetUnits.currTarget;
            if (targetUnits.targetList.Count <= 1)
                return;
            int index = 0;
            int skillid = skillSender.skillLogic.skillConfigId;
            int count = SkillHelper.RandomNumber(skillid, minCount, maxCount, isRndom);
            foreach (var target in targetUnits.targetList)
            {
                if (target == currTarget)
                    continue;
                if (!SkillHelper.GetParam(valueParam, skillid, out var coefficent))
                {
                    Log.Error($"the data is wrong where skillid is {skillid}");
                    coefficent = 1;
                }
                var attackData = attacker.GetAttackData();
                attackData.ChangeValue(MathHelper.RoundToInt(coefficent * attackData.value * Math.Pow(ReduceCoefficent, ++index)));
                attacker.AttackTarget(target, attackData, skillSender, false);
                if (index >= count)
                    break;
            }
        }
#endif
    }

    [GUIColor(0.8f, 0.9f, 0.2f)]
    public class SkillOption_添加技能 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.添加技能;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("skill Id")]
        public int skillId;
#if UNITY
        public override void HandleEvent(ISkillSender skillSender)
        {
            //SelectTargetHelper.GetTarget(selectTarget, skillSender, (target, skillSender) =>
            //{
            //    var skillComponent = target.GetComponent<UnitSkillComponent>();
            //    target.GetComponent<SkillMgrComponent>().AddSkill(skillSender.owner, skillComponent.GetSkill(skillId));
            //});

        }
#endif
    }
    [GUIColor(0.8f, 0.9f, 0.2f)]
    public class SkillOption_移除技能 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.移除技能;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("skill Id")]
        public int skillId;
#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                target.GetComponent<SkillMgrComponent>().RemoveSkill(skillSender.owner, skillId);
            });

        }
#endif
    }
    [GUIColor(0.95f, 0.08f, 0f)]
    public class SkillOption_反伤 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.反伤;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("反伤百分比")]
        public SkillParam param = new SkillParam { skillSourcetype = SkillSourcetype.DataTable };

#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                var owner = skillSender.owner;
                var attacker = owner.GetComponent<AttackComponent>();
                var damageData = attacker.GetAttackData();
                if (!SkillHelper.GetParam(param, skillSender.skillLogic.skillConfigId, out var value))
                    return;
                damageData.ChangeValue(damageData.value * value);
                attacker.AttackTarget(target, damageData, skillSender, false);
            });
        }
#endif
    }
    [GUIColor(0.95f, 0.08f, 0f)]
    public class SkillOption_反击 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.反击;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                var owner = skillSender.owner;
                var attacker = owner.GetComponent<AttackComponent>();
                int skillId = SkillHelper.GetNomalSkillId(owner);

                var skillLogic = owner.GetComponent<SkillMgrComponent>().GetSkill(skillId);
                skillLogic.HandleEvent(SkillEventCondition.当技能中途释放, new SkillSender
                {
                    skillLogic = skillSender.skillLogic,
                    owner = owner,
                    target = target,
                });
            });
        }
#endif
    }
    [GUIColor(0.6f, 0.8f, 0f)]
    public class SkillOption_吸血 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.吸血;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("吸血百分比")]
        public SkillParam param = new SkillParam { skillSourcetype = SkillSourcetype.DataTable };

#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                var owner = skillSender.owner;
                var attacker = owner.GetComponent<AttackComponent>();
                var damageData = attacker.GetAttackData();
                if (!SkillHelper.GetParam(param, skillSender.skillLogic.skillConfigId, out var value))
                    return;
                damageData.ChangeValue(damageData.value * value);
                attacker.TreatTarget(target, damageData, skillSender);
            });
        }
#endif
    }
    [GUIColor(0.6f, 0.8f, 0f)]
    public class SkillOption_改变公共CD : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.改变公共CD;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("改变量")]
        public SkillParam param = new SkillParam { skillSourcetype = SkillSourcetype.DataTable };

#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                var logic = skillSender.skillLogic;
                if (SkillHelper.GetParam(param, logic.skillConfigId, out var value))
                {
                    var skillAI = logic.skillAI;
                    skillAI.lastSkillTime -= MathHelper.RoundToInt(skillAI.roundCD * value / 100);
                }
            });

        }
#endif
    }
    [GUIColor(0.6f, 0.8f, 0f)]
    public class SkillOption_改变技能CD : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.改变技能CD;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("改变量")]
        public SkillParam param = new SkillParam { skillSourcetype = SkillSourcetype.DataTable };

#if UNITY
        public override void HandleEvent(ISkillSender skillSender)
        {

        }
#endif
    }

    [GUIColor(0.95f, 0.08f, 0f)]
    public class SkillOption_根据人数改变伤害 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.根据人数改变伤害;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("倍数系数")]
        public SkillParam param = new SkillParam { skillSourcetype = SkillSourcetype.DataTable };
#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            var owner = _skillSender.owner;
            var logic = _skillSender.skillLogic;
            var targetComponent = owner.GetComponent<TargetableUnitComponent>();
            int aliveCount = targetComponent.GetTagetAliveCount();
            if (aliveCount >= 4) return;
            if (!SkillHelper.GetParam(param, logic.skillConfigId, out var value))
                return;
            if (!(_skillSender is ModifierSkillSender modifierSkillSender))
            {
                Log.Error($"skillSender = {_skillSender} is not ModifierSkillSender");
                return;
            }
            var modifierLogic = modifierSkillSender.modifierLogic;
            if (modifierLogic == null)
                return;
            FP valueInt = 1 + (value - 1) / (aliveCount + 0.01f);
            modifierLogic.multiDamageX10000 = valueInt - logic.multipleDamageX10000;
            logic.multipleDamageX10000 = valueInt;
        }
#endif
    }
    [GUIColor(0.6f, 0.8f, 0f)]
    public class SkillOption_承担伤害 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.承担伤害;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("承伤百分比")]
        public SkillParam param = new SkillParam { skillSourcetype = SkillSourcetype.DataTable };

#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                var owner = skillSender.owner;
                var hurter = target.GetComponent<AttackComponent>();
                var attacker = hurter.attacker.GetComponent<AttackComponent>();
                var damageData = attacker.GetAttackData();
                if (!SkillHelper.GetParam(param, skillSender.skillLogic.skillConfigId, out var value))
                    return;
                damageData.ChangeValue(damageData.value * value);
                attacker.AttackTarget(owner, damageData, skillSender);
            });
        }
#endif
    }
    [GUIColor(0.6f, 0.8f, 0f)]
    [BsonIgnoreExtraElements]
    public class SkillOption_播放特效 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.播放特效;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget
        {
            targetType = SingleTargetType.Modifier容器,
        };

#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                var owner = skillSender.owner;
                var targetComponent = owner.GetComponent<TargetableUnitComponent>();
                var list = targetComponent.GetAllTarget();
                foreach (var u in list)
                {
                    //MessageHelper.SendActor(u, new M2C_PlaySkillEffect
                    //{
                    //    UnitId = owner.Id,
                    //    TargetId = target.Id,
                    //    SkillId = skillSender.skillLogic.skillId,
                    //});
                }
            });
        }
#endif
    }
    [GUIColor(0.6f, 0.8f, 0f)]
    [BsonIgnoreExtraElements]
    public class SkillOption_改变释放次数 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.改变释放次数;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("次数")]
        public SkillParam param = new SkillParam { skillSourcetype = SkillSourcetype.DataTable };

#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
            {
                var logic = skillSender.skillLogic;
                if (!SkillHelper.GetParam(param, logic.skillConfigId, out var value))
                    return;
                if (!(skillSender is ModifierSkillSender modifierSkillSender))
                {
                    Log.Error($"skillSender = {skillSender} is not ModifierSkillSender");
                    return;
                }
                var modifierLogic = modifierSkillSender.modifierLogic;
                if (modifierLogic == null)
                    return;
                int valueInt = (int)value;
                modifierLogic.playAmount = valueInt - logic.playAmount;
                logic.playAmount = valueInt;
            });
        }
#endif
    }
    [GUIColor(0.95f, 0.08f, 0f)]
    public class SkillOption_改变伤害倍率 : SkillOptionBase
    {
        public override SkillOptionType optionType => SkillOptionType.改变伤害倍率;

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("目标选择")]
        public SelectTargetBase selectTarget = new SelectSingleTarget();

        [BoxGroup("$optionType", CenterLabel = true)]
        [LabelText("倍数")]
        public SkillParam param = new SkillParam { skillSourcetype = SkillSourcetype.DataTable };
#if UNITY
        public override void HandleEvent(ISkillSender _skillSender)
        {
            SelectTargetHelper.GetTarget(selectTarget, _skillSender, (target, skillSender) =>
           {
               var logic = skillSender.skillLogic;
               if (!SkillHelper.GetParam(param, logic.skillConfigId, out var value))
                   return;
               if (!(skillSender is ModifierSkillSender modifierSkillSender))
               {
                   Log.Error($"skillSender = {skillSender} is not ModifierSkillSender");
                   return;
               }
               var modifierLogic = modifierSkillSender.modifierLogic;
               if (modifierLogic == null)
                   return;
               FP valueInt = value;
               modifierLogic.multiDamageX10000 = valueInt - logic.multipleDamageX10000;
               logic.multipleDamageX10000 = valueInt;
           });
        }
#endif
    }
}
