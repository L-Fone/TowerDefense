using System;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

namespace ET
{
    public class MonsterAIAwakeSystem : AwakeSystem<MonsterAI>
    {
        public override void Awake(MonsterAI self)
        {
            self.Awake();
        }
    }
    public class MonsterAIUpdateSystem : UpdateSystem<MonsterAI>
    {
        public override void Update(MonsterAI self)
        {
            self.Update();
        }
    }
    public class MonsterAI : AIBase
    {
        public override AIType aiType => AIType.Monster;

        private int pathIndex;

        public List<System.Numerics.Vector3> path;
        private bool isTurn;

        internal void Awake()
        {
            pathIndex = 0;
        }

        internal void Update()
        {
            if (!isRun)
                return;

            var unit = GetParent<Unit>();
            var point = path[pathIndex];
            var unityPoint = point.ToUnityVector3();
            var dV3 = unityPoint - unit.Position;
            if (isTurn)
            {
                isTurn = false;
                Game.EventSystem.Publish(new ET.EventType.PlayAnimation
                {
                    unit = unit,
                    ainmationKey = AinmationKey.Walk,
                    dir = dV3,
                }).Coroutine();
            }
            var dis = dV3.sqrMagnitude;
            if (dis <= 0.2f * 0.2f)
            {
                pathIndex++;
                if (pathIndex >= path.Count)
                {
                    isRun = false;
                }
                isTurn = true;
            }
            unit.ChangePosition(dV3.normalized * Time.deltaTime * 1);
            if (!isRun)
            {
                UnitComponent.Instance.Remove(unit);
            }
        }
    }
}
