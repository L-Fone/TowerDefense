using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public class ModifierContainerComponentUpdateSystem : UpdateSystem<ModifierContainerComponent>
    {
        private HashSet<ModifierLogic> needModifedList = new HashSet<ModifierLogic>();
        private HashSet<(SkillOptionBase, ISkillSender)> optionList = new HashSet<(SkillOptionBase, ISkillSender)>();
        public override void Update(ModifierContainerComponent self)
        {
            needModifedList.Clear();
            optionList.Clear();
            var now = TimeHelper.ClientNow();
            foreach (var kv in self.timeDic)
            {
                if (now < kv.Value) return;
                var modifierLogic = self.modifierDic[kv.Key];
                needModifedList.Add(modifierLogic);
                var dic = modifierLogic.modifierConfig.modifierEventDic;
                if (dic == null)
                    continue;
                if (dic.TryGetValue(ModifierEventCondition.循环执行定时器操作, out var list))
                {
                    foreach (var item in list)
                    {
                        optionList.Add((item, new ModifierSkillSender
                        {
                            owner = modifierLogic.skillLogic.owner,
                            skillLogic = modifierLogic.skillLogic,
                            modifierLogic = modifierLogic,
                            target = self.GetParent<Unit>()
                        }));
                    }
                }
            }
            foreach (var logic in needModifedList)
            {
                self.timeDic[logic.modifierId] = now + logic.interval;
            }
            foreach (var (option, skillSender) in optionList)
            {
                option.HandleEvent(skillSender);
            }
        }


    }

    public class ModifierContainerComponentDestroySystem : DestroySystem<ModifierContainerComponent>
    {
        public override void Destroy(ModifierContainerComponent self)
        {
            foreach (var item in self.modifierDic.Values)
            {
                self.RemoveModifier(item);
            }
            self.modifierDic.Clear();
            self.modifierStateDic.Clear();
            self.timeDic.Clear();
            self.modifierOptionByConditionDic.Clear();
        }
    }
    public static class ModifierContainerComponentSystem
    {
        public static void ApplyModifier(this ModifierContainerComponent self, Unit owner, SkillLogic skillLogic, ModifierId modifierId)
        {
            if (!skillLogic.skillLogicConfig.modifierDic.TryGetValue(modifierId, out var modifierConfig))
            {
                Log.Error($"要创建的modifier 不存在 {modifierId}");
                return;
            }
            if (!modifierConfig.levelList.Contains(skillLogic.skillLevel))
                return;

            var target = self.GetParent<Unit>();
            if (!self.modifierDic.TryGetValue(modifierId, out var logic))
            {
                logic = EntityFactory.CreateWithParent<ModifierLogic>(self);
                logic.skillLogic = skillLogic;
                logic.modifierConfig = modifierConfig;
                if (!SkillHelper.GetParam(modifierConfig.continueTime, skillLogic.skillConfigId, out var continueTime))
                {
                    Log.Error($"continueTime is invalid where modifierId = {modifierId}");
                }
                //!特殊处理
                if (continueTime == 0)
                {
                    if (!IgnoreInvalid(modifierConfig.attribute))
                        return;
                    AddToDic();
                    CreateEvent();
                    self.RemoveModifier(logic);
                    return;
                }
                logic.continueTime = (int)(continueTime * 1000);
                var now = TimeHelper.ClientNow();
                SetThinkerInterval(self,now,logic);

                if (logic.continueTime != -1 * 1000)
                {
                    logic.cancellationToken = new ETCancellationToken();
                    logic.leastTime = logic.continueTime + now;
                    self.RemoveModifierIntetnal(logic).Coroutine();
                }

            }
            else
            {
                Log.Warning($"dic already has the key:{modifierId} when create");
            }

            if (logic == null)
            {
                return;
            }
            //!判断很多条件，改变数值属性以及状态
            bool canRefresh = false;
            //!已经存在此modifier
            if (self.modifierDic.TryGetValue(logic.modifierId, out _))
            {
                var modifierAttribute = modifierConfig.attribute;
                //!不改变属性（时间）
                if (modifierAttribute == ModifierAttribute.无||
                    !IgnoreInvalid(modifierAttribute))
                    return;
                //!可叠加的情况
                if (modifierAttribute.HasFlag(ModifierAttribute.可叠加))
                {
                    int maxOverlay = SkillHelper.GetOverlableBuffMaxLayer(modifierConfig.overlayType);
                    if (!SkillHelper.GetParam(modifierConfig.perOverlay, logic.skillLogic.skillConfigId, out var perOverlay))
                    {
                        Log.Error($"perOnerlay is invalid where modifierId = {logic.modifierId}");
                        perOverlay = 1;
                    }
                    logic.overlay += (int)perOverlay;
                    logic.overlay = UnityEngine.Mathf.Clamp(logic.overlay, 1, maxOverlay);
                }
                //!可刷新的情况
                if (modifierAttribute.HasFlag(ModifierAttribute.可刷新))
                {
                    if (logic.continueTime != -1 * 1000)
                    {
                        var now = TimeHelper.ClientNow();
                        self.timeDic[logic.modifierId] = now + logic.interval;
                        logic.cancellationToken.Cancel();
                        Log.Debug($"modifier 原本结束时间：{TimeHelper.GetTime(logic.leastTime)}，刷新后结束时间：{TimeHelper.GetTime(TimeHelper.ClientNow() + logic.continueTime)}");
                        logic.leastTime = now + logic.continueTime;
                        self.RemoveModifierIntetnal(logic).Coroutine();
                    }
                    canRefresh = true;
                }
            }
            //!不存在此modifier
            else
            {
                AddToDic();
                canRefresh = true;

                if (modifierConfig.valueK != ModifierValueType.无)
                    self.ChangeNumeric(target, logic, ValueChangeType.Plus);
                if (modifierConfig.stateK != ModifierStateType.无)
                    self.ChangeState(owner, target, logic, ValueChangeType.Plus);
            }

            if (canRefresh)
            {
                CreateEvent();
            }

            bool IgnoreInvalid(ModifierAttribute modifierAttribute)
            {
                if (!modifierAttribute.HasFlag(ModifierAttribute.忽视无敌))
                    if (self.modifierStateDic.TryGetValueByKey1(ModifierStateType.无敌, out var stateStateType))
                        if (stateStateType == StateStateType.启用)
                            return false;
                return true;
            }
            void AddToDic()
            {
                float perOverlay = 1;
                if (modifierConfig.attribute.HasFlag(ModifierAttribute.可叠加))
                {
                    if (!SkillHelper.GetParam(modifierConfig.perOverlay, logic.skillLogic.skillConfigId, out perOverlay))
                    {
                        Log.Error($"perOnerlay is invalid where modifierId = {logic.modifierId}");
                        perOverlay = 1;
                    }
                }
                logic.overlay += (int)perOverlay;
                if (!self.modifierDic.TryAdd(logic.modifierId, logic))
                {
                    Log.Error($"dic add the modifier failly:{logic.modifierId} when apply");
                }
                else
                {
                    var dic = logic.modifierConfig.modifierEventDic;
                    if (dic != null)
                        foreach (var kv in dic)
                        {
                            self.modifierOptionByConditionDic.Add(kv.Key, logic);
                        }
                }
            }
            void CreateEvent()
            {
                logic.HandleEvent(ModifierEventCondition.当modifier被创建时, new ModifierSkillSender
                {
                    owner = owner,
                    target = target,
                    skillLogic = logic.skillLogic,
                    modifierLogic = logic
                });
            }
        }

        private static void SetThinkerInterval(ModifierContainerComponent self, long now, ModifierLogic logic)
        {
            var modifierConfig = logic.modifierConfig; 
            if (modifierConfig.thinkerType == ThinkerType.无)
            {
                return;
            }
            else
            if (modifierConfig.thinkerType == ThinkerType.自定义 &&
                SkillHelper.GetParam(modifierConfig.thinkInterval, logic.skillLogic.skillConfigId, out var inteval))
            {
                if (inteval <=0)
                {
                    return;
                }
                logic.interval = (int)(inteval * 1000);
            }
            else
            {
                logic.interval  = SkillHelper.GetThinkerInterval(thinkerType: modifierConfig.thinkerType);
            }
            self.timeDic.Add(logic.modifierId, now + logic.interval);
        }

        public static ModifierLogic GetModifierLogic(this ModifierContainerComponent self, ModifierId modifierId)
        {
            self.modifierDic.TryGetValue(modifierId, out var modifierLogic);
            return modifierLogic;
        }
        public static (SkillOptionBase[], ModifierLogic) GetSkillOptionBaseArr(this ModifierContainerComponent self, ModifierEventCondition modifierEventCondition)
        {
            var set = self.modifierOptionByConditionDic[modifierEventCondition];
            if (set.Count > 0)
                foreach (var modifierLogic in set)
                {
                    var dic = modifierLogic.modifierConfig.modifierEventDic;
                    if (dic != null)
                        if (dic.TryGetValue(modifierEventCondition, out var list))
                            return (list, modifierLogic);
                }
            return (null, null);
        }
        private static async ETVoid RemoveModifierIntetnal(this ModifierContainerComponent self, ModifierLogic modifierLogic)
        {
            bool ret = await TimerComponent.Instance.WaitTillAsync(modifierLogic.leastTime, modifierLogic.cancellationToken);
            if (ret)
                self.RemoveModifier(modifierLogic);
        }
        /// <summary>
        /// 改变数值
        /// </summary>
        /// <param name="self"></param>
        private static void ChangeNumeric(this ModifierContainerComponent self, Unit target, ModifierLogic logic, ValueChangeType valueChangeType)
        {
            var key = logic.modifierConfig.valueK;
            if (key == ModifierValueType.无) return;
            var configValue = logic.modifierConfig.valueV;
            var numTarget = target.GetComponent<NumericComponent>();
            FP sign;
            switch (valueChangeType)
            {
                case ValueChangeType.Zero:
                    sign = 0;
                    break;
                case ValueChangeType.Plus:
                    sign = 1;
                    break;
                case ValueChangeType.Minus:
                    sign = -1;
                    break;
                default:
                    throw new Exception("类型错误");
            }
            if (!SkillHelper.GetParam(configValue, logic.skillLogic.skillConfigId, out var value))
            {
                Log.Error($"cann't get the value where modifier = {logic.modifierId} type = {key}");
                return;
            }
            if (sign == -1)
            {
                sign *= logic.skillLogic.dataOldX10000;
            }
            else
            {
                sign *= logic.skillLogic.dataX10000;
            }
            FP finalValue = value * sign * FP.EN2;
            Log.Debug($"{valueChangeType}了数值属性：{key} 改变量：{finalValue.ToString("p2")}");
            switch (key)
            {
                case ModifierValueType.无:
                    break;
                //case ModifierValueType.修改基础_生命值上限:
                //    numTarget.AddSet(NumericType.MaxHpBuffPct, finalValue);
                //    break;
                //case ModifierValueType.修改基础_精力值上限:
                //    numTarget.AddSet(NumericType.MaxMpBuffPct, finalValue);
                //    break;
                //case ModifierValueType.修改攻防_攻击力:
                //    numTarget.AddSet(NumericType.PhyAtkBuffPct, finalValue);
                //    numTarget.AddSet(NumericType.SpiAtkBuffPct, finalValue);
                //    break;
                //case ModifierValueType.修改攻防_物理攻击力:
                //    numTarget.AddSet(NumericType.PhyAtkBuffPct, finalValue);
                //    break;
                //case ModifierValueType.修改攻防_精神攻击力:
                //    numTarget.AddSet(NumericType.SpiAtkBuffPct, finalValue);
                //    break;
                //case ModifierValueType.修改攻防_防御力:
                //    numTarget.AddSet(NumericType.PhyDefBuffPct, finalValue);
                //    numTarget.AddSet(NumericType.SpiDefBuffPct, finalValue);
                //    break;
                //case ModifierValueType.修改攻防_物理防御力:
                //    numTarget.AddSet(NumericType.PhyDefBuffPct, finalValue);
                //    break;
                //case ModifierValueType.修改攻防_精神防御力:
                //    numTarget.AddSet(NumericType.SpiDefBuffPct, finalValue);
                //    break;
                //case ModifierValueType.修改暴击_暴击率:
                //    numTarget.AddSet(NumericType.PcrirBuffAdd, finalValue);
                //    numTarget.AddSet(NumericType.McrirBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改暴击_物理暴击率:
                //    numTarget.AddSet(NumericType.PcrirBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改暴击_精神暴击率:
                //    numTarget.AddSet(NumericType.McrirBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改暴击_暴击值:
                //    numTarget.AddSet(NumericType.PcriBuffAdd, finalValue);
                //    numTarget.AddSet(NumericType.McriBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改暴击_物理暴击值:
                //    numTarget.AddSet(NumericType.PcriBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改暴击_精神暴击值:
                //    numTarget.AddSet(NumericType.McriBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改抗暴击_抗暴击率:
                //    numTarget.AddSet(NumericType.RpcrirBuffAdd, finalValue);
                //    numTarget.AddSet(NumericType.RmcrirBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改抗暴击_抗物理暴击率:
                //    numTarget.AddSet(NumericType.RpcrirBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改抗暴击_抗精神暴击率:
                //    numTarget.AddSet(NumericType.RmcrirBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改抗暴击_抗暴击值:
                //    numTarget.AddSet(NumericType.RpcriBuffAdd, finalValue);
                //    numTarget.AddSet(NumericType.RmcriBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改抗暴击_抗物理暴击值:
                //    numTarget.AddSet(NumericType.RpcriBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改抗暴击_抗精神暴击值:
                //    numTarget.AddSet(NumericType.RmcriBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改免伤_免伤:
                //    numTarget.AddSet(NumericType.NphyiBuffAdd, finalValue);
                //    numTarget.AddSet(NumericType.NmeniBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改免伤_物理免伤:
                //    numTarget.AddSet(NumericType.NphyiBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改免伤_精神免伤:
                //    numTarget.AddSet(NumericType.NmeniBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改辅助值:
                //    numTarget.AddSet(NumericType.DvoBuffAdd, finalValue);
                //    break;
                //case ModifierValueType.修改速度:
                //    numTarget.AddSet(NumericType.SpdBuffPct, finalValue);
                //    break;
                //case ModifierValueType.修改命中:
                //    numTarget.AddSet(NumericType.HitBuffPct, finalValue);
                //    break;
                //case ModifierValueType.修改抵抗:
                //    numTarget.AddSet(NumericType.ResBuffPct, finalValue);
                //    break;
                default:
                    break;
            }
            int delayTime = 1000 + logic.skillLogic.skillConfig.DelayTime;
            int continueTime = sign >= 0 ? (int)(logic.leastTime - TimeHelper.ClientNow()) : 0;
            //BuffBrocastComponent.Instance.AddChangeState(delayTime, new EventType.BuffStateRet
            //{
            //    targetUnit = target,
            //    type = sign >= 0 ? M2C_BattleChangeState.Types.ChangeType.Add : M2C_BattleChangeState.Types.ChangeType.Reduce,
            //    valueType = (int)key,
            //    stateType = 0,
            //    isBuff = logic.modifierConfig.buffType == BuffType.Buff,
            //    time = continueTime,
            //});
        }
        /// <summary>
        /// 附加状态
        /// </summary>
        /// <param name="self"></param>
        private static void ChangeState(this ModifierContainerComponent self, Unit owner, Unit target, ModifierLogic logic, ValueChangeType valueChangeType)
        {
            var config = logic.modifierConfig;
            if (config.stateK == ModifierStateType.无) return;
            Log.Debug($"{valueChangeType}了状态属性：{config.stateK} ");
            if (valueChangeType == ValueChangeType.Plus)
                self.modifierStateDic.Add(config.stateK, config.Id, config.stateV);
            else
            if (valueChangeType == ValueChangeType.Minus)
                self.modifierStateDic.Remove(config.stateK, config.Id, config.stateV);

            int delayTime = 1000 + logic.skillLogic.skillConfig.DelayTime;
            int continueTime = valueChangeType == ValueChangeType.Plus ? (int)(logic.leastTime - TimeHelper.ClientNow()) : 0;
            //BuffBrocastComponent.Instance.AddChangeState(delayTime, new EventType.BuffStateRet
            //{
            //    targetUnit = target,
            //    type = valueChangeType == ValueChangeType.Plus ? M2C_BattleChangeState.Types.ChangeType.Add : M2C_BattleChangeState.Types.ChangeType.Reduce,
            //    valueType = 0,
            //    stateType = (int)config.stateK,
            //    isBuff = logic.modifierConfig.buffType == BuffType.Buff,
            //    time = continueTime,
            //});
        }
        public static void RemoveModifier(this ModifierContainerComponent self, ISkillSender skillSender)
        {
            try
            {
                var modifierLogic = ((ModifierSkillSender)skillSender).modifierLogic;
                if (modifierLogic == null)
                    Log.Error($"modifier is null where id = {modifierLogic.modifierId}");
                self.RemoveModifier(modifierLogic);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        private static HashSet<(ModifierEventCondition, ModifierLogic)> needRemoveModifierSet = new HashSet<(ModifierEventCondition, ModifierLogic)>();
        public static void RemoveModifier(this ModifierContainerComponent self, ModifierLogic modifierLogic)
        {
            try
            {
                if (!modifierLogic)
                    return;
                var owner = modifierLogic.skillLogic.owner;
                var target = self.GetParent<Unit>();
                var modifierId = modifierLogic.modifierId;

                //!改变状态
                modifierLogic.skillLogic.multipleDamageX10000 -= modifierLogic.multiDamageX10000;
                modifierLogic.skillLogic.playAmount -= modifierLogic.playAmount;

                var dic = modifierLogic.skillLogic.skillLogicConfig.modifierDic;
                if (dic == null)
                {
                    Log.Error($"modifierDic is null where id = {modifierId}");
                    return;
                }
                if (!dic.TryGetValue(modifierId, out var modifierConfig))
                {
                    Log.Error($"[数据填写错误] the modifier which id is {modifierId} is not exist in skillLogicConfig.ModifierDic");
                    return;
                }

                if (modifierConfig.valueK != ModifierValueType.无)
                    self.ChangeNumeric(target, modifierLogic, ValueChangeType.Minus);
                if (modifierConfig.stateK != ModifierStateType.无)
                    self.ChangeState(owner, target, modifierLogic, ValueChangeType.Minus);
                modifierLogic.HandleEvent(ModifierEventCondition.当modifier被移除时, new ModifierSkillSender
                {
                    owner = owner,
                    target = target,
                    skillLogic = modifierLogic.skillLogic,
                    modifierLogic = modifierLogic
                });

                self.timeDic.Remove(modifierId);

                needRemoveModifierSet.Clear();
                foreach (var kv in self.modifierOptionByConditionDic.GetDictionary())
                {
                    foreach (var _logic in kv.Value)
                    {
                        if (_logic.modifierId == modifierId)
                        {
                            needRemoveModifierSet.Add((kv.Key, _logic));
                        }
                    }
                }
                foreach (var (eventCondition, __logic) in needRemoveModifierSet)
                {
                    self.modifierOptionByConditionDic.Remove(eventCondition, __logic);
                }
                //!同步执行，没有问题
                modifierLogic.Dispose();

                if (!self.modifierDic.Remove(modifierId))
                {
                    Log.Error($"dic remove has the key failly:{modifierId}");
                    return;
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
