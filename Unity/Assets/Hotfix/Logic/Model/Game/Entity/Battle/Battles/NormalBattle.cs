using Cal.DataTable;
using System;
using System.Collections.Generic;

namespace ET
{
    public class NormalBattleAwakeSystem : AwakeSystem<NormalBattle>
    {
        public override void Awake(NormalBattle self)
        {
            self.Awake();
        }
    }
    public class NormalBattleUpdateSystem : UpdateSystem<NormalBattle>
    {
        public override void Update(NormalBattle self)
        {
            self.Update();
        }
    }
    public class NormalBattleDestroySystem : DestroySystem<NormalBattle>
    {
        public override void Destroy(NormalBattle self)
        {
            self.Destroy();
        }
    }
    public class NormalBattle : BattleBase
    {
        public enum State 
        { 
            Wait,
            Ready,
            Battle,
            /// <summary>
            /// 结算
            /// </summary>
            Statements,
            End,
}
        private State state;
        public override GameType gameType => GameType.Normal;

        internal void Awake()
        {
            state = State.Wait;
        }
        public void Init()
        {
            state = State.Ready;
        }
        internal void Update()
        {
            switch (state)
            {
                case State.Wait:
                    return;
                case State.Ready:
                    Ready();
                    state = State.Battle;
                    return;
                case State.Battle:
                    UpdateBattle();
                    return;
                case State.Statements:
                    Statements();
                    state = State.End;
                    return;
                case State.End:
                    EndEvent();
                    return;
                default:
                    return;
            }

        }

        private void Ready()
        {
            ReadyAsync().Coroutine();
        }

        private async ETVoid ReadyAsync()
        {
            LevelConfig levelConfig = ConfigHelper.Get<LevelConfig>(GlobalVariable.MapId);
            RoleConfig roleConfig = ConfigHelper.Get<RoleConfig>(RoleConfigId.TestMonster);
            var unit = await UnitFactory.Create(roleConfig.PrefabId, UnitType.Monster);
            unit.SetPosition(levelConfig.InitPos);
            unit.SetYAngle(levelConfig.InitAngle);
        }

        private void UpdateBattle()
        {

        }
        /// <summary>
        /// 结算
        /// </summary>
        private void Statements()
        {

        }
        private void EndEvent()
        {

        }
        internal void Destroy()
        {

        }

    }
}
