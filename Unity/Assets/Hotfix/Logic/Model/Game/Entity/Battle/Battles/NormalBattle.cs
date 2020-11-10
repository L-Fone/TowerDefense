using Cal.DataTable;
using System;
using System.Collections.Generic;
using UnityEngine;

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
        private LevelInfo info;

        public override GameType gameType => GameType.Normal;


        internal void Awake()
        {
            health = 10;
            state = State.Wait;
            killInfo = new KillInfo();
            battleResult = BattleResultType.None;
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
                    return;
                case State.Battle:
                    UpdateBattle();
                    return;
                case State.Statements:
                    Statements();
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
            MapSceneConfig mapSceneConfig = ConfigHelper.Get<MapSceneConfig>(GlobalVariable.MapId);
            string path = $"Assets/Download/Config/Levels/{mapSceneConfig.Name}.json";
            var str = await ResourceHelper.LoadAssetAsync<TextAsset>(path);
            info = MongoHelper.FromJson<LevelInfo>(str.text);

            TowerPointGenerate();
            int count = 5;
            killInfo = killInfo.Init(this, count,OnVictory);
            MonsterSpawn(count).Coroutine();

            

            //!开始
            state = State.Battle;

        }
        private void TowerPointGenerate()
        {
            foreach (var v3 in info.towerList)
            {
                Vector3 vector3 = v3.ToUnityVector3();
                TowerPointInfo towerPointInfo = TowerPointComponent.instance.Create(vector3);
                Game.EventSystem.Publish(new ET.EventType.GenerateTowerPoint
                {
                    zoneScene = null,
                    towerPointInfo = towerPointInfo,
                    point = vector3
                }).Coroutine();
            }
        }

        private async ETVoid MonsterSpawn( int count)
        {
            for (int i = 0; i < count; i++)
            {
                await TimerComponent.Instance.WaitAsync(2000);

                long roleId = RandomHelper.RandomNumber(RoleConfigId.TestMonster1, RoleConfigId.TestMonster4 + 1);
                RoleConfig roleConfig = ConfigHelper.Get<RoleConfig>(roleId);
                var unit = UnitFactory.Create(roleConfig, UnitType.Monster);
                unit.Position = info.initPos.ToUnityVector3();

                unit.AddComponent<DamageComponent>();
                var num = unit.AddComponent<NumericComponent>();
                num.Set(NumericType.HpBase,roleConfig.Hp);
                num.Set(NumericType.MaxHpBase,roleConfig.Hp);
                num.Set(NumericType.AtkBase,roleConfig.Atk);
                num.Set(NumericType.DefBase,roleConfig.Def);
                num.Set(NumericType.AtkFieldBase,roleConfig.AtkField);
                num.Set(NumericType.AtkSpdBase,roleConfig.AtkInterval);
                num.Set(NumericType.MoveSpdBase,roleConfig.Spd);

                var monsterAI = unit.AddComponent<MonsterAI>();
                monsterAI.path = info.path;
                if (!this.aiDic.TryAdd(unit.Id, monsterAI))
                    Log.Error($"aiDic hasc the key = {unit.Id}");
                monsterAI.isRun = true;
            }
        }

        private void UpdateBattle()
        {



        }
        private void OnVictory(BattleBase battle)
        {
            Log.Info($"战斗胜利啦");
            battleResult = BattleResultType.Victory;
            state = State.Statements;
        }
        protected override void OnDefeat()
        {
            Log.Info($"战斗失败啦");
            battleResult = BattleResultType.Defeat;
            state = State.Statements;
        }
        /// <summary>
        /// 结算
        /// </summary>
        private void Statements()
        {
            Log.Info($"结算啦");


            state = State.End;
        }
        private void EndEvent()
        {
            Log.Info($"战斗结束啦");
            state = State.Wait;

            BattleMgrComponent.Remove();
        }
        internal void Destroy()
        {
            TowerPointComponent.instance.RemoveAll();
            aiDic.Clear();
            UnitComponent.Instance.RemoveAll();
        }

    }
}
