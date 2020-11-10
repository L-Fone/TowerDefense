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
        private TargetComponent targetComponent;
        private NumericComponent num;
        private float atkInterval = 1;
        private State state;
        private long lastAtkTime;
        internal void Awake()
        {
            unit = GetParent<Unit>();
            targetComponent = unit.GetComponent<TargetComponent>();
            targetComponent.AddTrigger(unit, OnEnermyEnter, OnEnermyExit);
            num = unit.GetComponent<NumericComponent>();
            atkInterval = num.GetAsFloat(NumericType.AtkSpd) * 1000;
            state = State.Run;
        }


        private void OnEnermyEnter(Unit unit)
        {
            targetComponent.AddTarget(unit);
        }

        private void OnEnermyExit(Unit unit)
        {
            targetComponent.RemoveTraget(unit);
        }
        internal void Update()
        {
            var now = TimeHelper.ClientNow();
            switch (state)
            {
                case State.Wait:
                    return;
                case State.Run:
                    if (now - lastAtkTime < atkInterval)
                        return;
                    if (targetComponent.TargetCount <= 0)
                        return;
                    lastAtkTime = now;
                    Unit target = targetComponent.GetFirst();
                    if (!target)
                    {
                        return;
                    }
                    var targetNum = target.GetComponent<NumericComponent>();
                    int damage = num.GetAsInt(NumericType.Atk) - targetNum.GetAsInt(NumericType.Def);
                    damage = Mathf.Clamp(damage, 0, int.MaxValue);
                    //todo
                    Game.EventSystem.Publish_Sync(new ET.EventType.ShowDebugAtkLine
                    {
                        unit = unit,
                        target = target,
                        damage = damage,
                        hp = targetNum.GetAsInt(NumericType.Hp),
                        maxHp = targetNum.GetAsInt(NumericType.MaxHp),
                    }); 
                    target.GetComponent<DamageComponent>().Damage(unit,damage);
                    return;
                default:
                    break;
            }
        }
    }
}
