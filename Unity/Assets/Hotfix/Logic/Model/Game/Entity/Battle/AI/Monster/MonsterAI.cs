using System;
using System.Collections.Generic;
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
        private float moveSpd;
        private Unit unit;

        internal void Awake()
        {
            pathIndex = 0;
            unit = GetParent<Unit>();
            moveSpd = unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.MoveSpd) / 100;
        }

        internal void Update()
        {
            if (!isRun)
                return;

            var point = path[pathIndex];
            var unityPoint = point.ToUnityVector3();
            var dV3 = unityPoint - unit.Position;
            if (isTurn)
            {
                isTurn = false;
                Game.EventSystem.Publish_Sync(new ET.EventType.PlayAnimation
                {
                    unit = unit,
                    ainmationKey = AinmationKey.Walk,
                    dir = dV3,
                });
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
            unit.ChangePosition(dV3.normalized * Time.deltaTime * moveSpd);
            if (!isRun)
            {
                var battle = BattleMgrComponent.currBattle;
                if (battle == null)
                {
                    Log.Error($" battle == null when unit which id is{unit.Id} dead");
                    return;
                }
                if (!battle.MonterEnterHeart())
                    battle.MonsterDead();
                UnitComponent.Instance.Remove(unit);
            }
        }
    }
}
