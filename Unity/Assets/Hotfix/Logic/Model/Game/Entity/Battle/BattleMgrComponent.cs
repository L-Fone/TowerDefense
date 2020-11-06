using System;
using System.Collections.Generic;

namespace ET
{
    public enum GameType
    {
        None,
        Normal,
    }
    public class BattleMgrComponentAwakeSystem : AwakeSystem<BattleMgrComponent>
    {
        public override void Awake(BattleMgrComponent self)
        {
            self.Awake();
        }
    }

    public class BattleMgrComponent:Entity
    {
        private static BattleMgrComponent inst;

        private GameType _currGameType;

        private BattleBase _currBattle;

        internal void Awake()
        {
            inst = this;
        }

        public static GameType currGameType => inst._currGameType;
        public static BattleBase currBattle => inst._currBattle;

        public static T Create<T>()where T : BattleBase
        {
            if(inst._currGameType != GameType.None)
            {
                Log.Error($"不能重复进行战斗");
                return null;
            }
            T t = EntityFactory.CreateWithParent<T>(inst);
            inst._currBattle = t;
            inst._currGameType = t.gameType;
            return t;
        }
        public static void Remove()
        {
            inst._currBattle.Dispose();
            inst._currBattle = null;
            inst._currGameType = default;
        }
    }
}
