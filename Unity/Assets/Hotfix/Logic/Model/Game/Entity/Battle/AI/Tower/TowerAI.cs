using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class TowerAIAwakeSystem : AwakeSystem<TowerAI>
    {
        public override void Awake(TowerAI self)
        {
            self.Awake();
        }
    }
    public class TowerAIUpdateSystem : UpdateSystem<TowerAI>
    {
        public override void Update(TowerAI self)
        {
            self.Update();
        }
    }
    public class TowerAI : AIBase
    {
        public override AIType aiType => AIType.Tower;

        enum State { Wait, Run }

        private Unit unit;
        private TargetableUnitComponent targetComponent;
        private State state;
        internal void Awake()
        {
            unit = GetParent<Unit>();
            targetComponent = unit.GetComponent<TargetableUnitComponent>();
            targetComponent.AddTrigger(unit, OnEnermyEnter, OnEnermyExit);
            state = State.Run;
            Game.EventSystem.Publish_Sync(new ET.EventType.PlayAnimation
            {
                unit = unit,
                ainmationKey = AinmationKey.Idle,
                dir = default,
            });
        }


        private void OnEnermyEnter(Unit unit)
        {
            targetComponent.AddEnermy(unit);
        }

        private void OnEnermyExit(Unit unit)
        {
            targetComponent.RemoveEnermy(unit);
        }
        internal void Update()
        {
            var now = TimeHelper.ClientNow();
            switch (state)
            {
                case State.Wait:
                    return;
                case State.Run:
                    //if (now - lastAtkTime < atkInterval)
                    //    return;
                    if (targetComponent.targetCount <= 0)
                        return;
                    //lastAtkTime = now;

                    BattleHelper.PlayerSkill(unit, now);

                    //Unit target = targetComponent.GetFirst();
                    //if (!target)
                    //{
                    //    return;
                    //}
                    //var targetNum = target.GetComponent<NumericComponent>();
                    //int damage = num.GetAsInt(NumericType.Atk) - targetNum.GetAsInt(NumericType.Def);
                    //damage = Mathf.Clamp(damage, 0, int.MaxValue);
                    ////todo
                    //Game.EventSystem.Publish_Sync(new ET.EventType.ShowDebugAtkLine
                    //{
                    //    unit = unit,
                    //    target = target,
                    //    damage = damage,
                    //    hp = targetNum.GetAsInt(NumericType.Hp),
                    //    maxHp = targetNum.GetAsInt(NumericType.MaxHp),
                    //}); 
                    //target.GetComponent<DamageComponent>().Damage(unit,damage);
                    return;
                default:
                    break;
            }
        }
    }
}
