using Cal;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET
{
    public class BattleComponentAwakeSystem : AwakeSystem<BattleComponent>
    {
        public override void Awake(BattleComponent self)
        {
            self.unit = self.GetParent<Unit>();
        }
    }

    public static class BattleComponentSystem
    {
        public static void Damage(this BattleComponent self, BallisticData data, ISkillSender skillSender)
        {
            try
            {
                var unit = self.GetParent<Unit>();
                if (unit.IsAlive && data.value > 0)
                {
                    var attacker = unit.GetComponent<AttackComponent>().attacker;
                    var num = unit.GetComponent<NumericComponent>();
                    int value = MathHelper.RoundToInt(data.value);

                    var modifierContainer = unit.GetComponent<ModifierContainerComponent>();
                    if (modifierContainer.modifierStateDic.TryGetValueByKey1(ModifierStateType.免疫伤害, out var stateStateType))
                        if (stateStateType == StateStateType.启用)
                            return;
                    foreach (var modifierLogic in modifierContainer.modifierDic.Values)
                    {
                        var shield = modifierLogic.shield;
                        if (shield <= 0)
                            continue;
                        if (shield <= value)
                        {
                            value -= shield;
                            modifierLogic.shield = 0;
                            modifierContainer.RemoveModifier(modifierLogic);
                            Log.Info($"【护盾】已破，buff消失,受到伤害 = {value}");
                        }
                        else
                        {
                            modifierLogic.shield -= value;
                            value = 0;
                            Log.Info($"【护盾】未破 = {modifierLogic.shield}");
                        }
                    }
                    {
                        var (optionList, modifierLogic) = modifierContainer.GetSkillOptionBaseArr(ModifierEventCondition.当拥有modifier的单位受到伤害时);
                        if (optionList != null)
                            foreach (var item in optionList)
                                item.HandleEvent(new ModifierSkillSender
                                {
                                    owner = attacker,
                                    target = unit,
                                    skillLogic = modifierLogic.skillLogic,
                                    modifierLogic = modifierLogic
                                });
                    }

                    num.SetAdd(NumericType.HpBase, -value);

                    int delayTime =skillSender.skillLogic.skillConfig.DelayTime;
                    //BuffBrocastComponent.Instance.Add(delayTime, new EventType.BattleSkillRet
                    //{
                    //    targetUnit = unit,
                    //    HpValue = -value,
                    //    IsCrit = data.isCrit,
                    //});

                    if (num.GetAsInt(NumericType.Hp) <= 0)
                    {

                        if (modifierContainer.modifierStateDic.TryGetValueByKey1(ModifierStateType.不死, out var noDieState))
                            if (noDieState == StateStateType.启用)
                                return;
                        if (modifierContainer.modifierStateDic.TryGetValueByKey1(ModifierStateType.死亡复活, out var dieWillLiveState))
                            if (dieWillLiveState == StateStateType.启用)
                            {
                                //unit.DeadWillLive();
                                //!添加复活buff
                                Revival(modifierContainer, attacker, 6000).Coroutine();
                                return;
                            }

                        unit.Dead();
                        var attackerTargerComponent = attacker.GetComponent<TargetableUnitComponent>();
                        if (attackerTargerComponent.selectedEnermy?.Id == unit.Id)
                        {
                            attackerTargerComponent.selectedEnermy = null;
                        }
                        {
                            var attackerModifierContainer = attacker.GetComponent<ModifierContainerComponent>();
                            var (optionList, modifierLogic) = attackerModifierContainer.GetSkillOptionBaseArr(ModifierEventCondition.当拥有modifier的单位击杀目标时);
                            if (optionList != null)
                                foreach (var item in optionList)
                                    item.HandleEvent(new ModifierSkillSender
                                    {
                                        owner = attacker,
                                        target = unit,
                                        skillLogic = modifierLogic.skillLogic,
                                        modifierLogic = modifierLogic,
                                    });
                        }
                        {
                            var (optionList, modifierLogic) = modifierContainer.GetSkillOptionBaseArr(ModifierEventCondition.当拥有modifier的单位死亡时);
                            if (optionList != null)
                                foreach (var item in optionList)
                                    item.HandleEvent(new ModifierSkillSender
                                    {
                                        owner = attacker,
                                        target = unit,
                                        skillLogic = modifierLogic.skillLogic,
                                        modifierLogic = modifierLogic,
                                    });
                        }
                        //BattleMgrCompnent.NotifyUnitDead(unit);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }

        private static async ETVoid Revival(ModifierContainerComponent modifierContainer, Unit attacker, int time)
        {
            await TimerComponent.Instance.WaitAsync(time);
            var (optionList, modifierLogic) = modifierContainer.GetSkillOptionBaseArr(ModifierEventCondition.当拥有modifier的单位复活时);
            if (optionList != null)
                foreach (var item in optionList)
                    item.HandleEvent(new ModifierSkillSender
                    {
                        owner = attacker,
                        target = modifierContainer.GetParent<Unit>(),
                        skillLogic = modifierLogic.skillLogic,
                        modifierLogic = modifierLogic,
                    });

        }

        public static void Treat(this BattleComponent self, BallisticData data, ISkillSender skillSender)
        {
            try
            {
                var unit = self.GetParent<Unit>();
                if (unit.IsAlive && data.value > 0)
                {
                    var attacker = unit.GetComponent<AttackComponent>().attacker;
                    var num = self.unit.GetComponent<NumericComponent>();
                    int value = MathHelper.RoundToInt(data.value);
                    num.SetAdd(NumericType.HpBase, value);
                    int delayTime = skillSender.skillLogic.skillConfig.DelayTime;
                    //BuffBrocastComponent.Instance.Add(delayTime, new EventType.BattleSkillRet
                    //{
                    //    targetUnit = unit,
                    //    HpValue = value,
                    //    IsCrit = data.isCrit,
                    //});
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
