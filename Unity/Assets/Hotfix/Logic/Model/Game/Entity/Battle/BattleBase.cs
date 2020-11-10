using System;
using System.Collections.Generic;

namespace ET
{
    public abstract class BattleBase:Entity
    {
        protected enum BattleResultType { None,Victory,Defeat}
        public abstract GameType gameType{ get;}

        /// <summary>
        /// 击杀数
        /// </summary>
        protected KillInfo killInfo;
        /// <summary>
        /// 玩家生命值
        /// </summary>
        protected int health;
        /// <summary>
        /// 战斗结果
        /// </summary>
        protected BattleResultType battleResult;
        protected Dictionary<long, AIBase> aiDic = new Dictionary<long, AIBase>();


        public void MonsterDead(int count = 1)
        {
            killInfo = killInfo.UpdateAmount(count);
        }
        public bool MonterEnterHeart()
        {
            health--;
            if (health <= 0)
            {
                OnDefeat();
                return true;
            }
            return false;
        }
        protected abstract void OnDefeat();
    }
}
