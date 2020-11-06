using System;
using System.Collections.Generic;

namespace ET
{
    public abstract class BattleBase:Entity
    {
        public abstract GameType gameType{ get;}

        protected Dictionary<long, AIBase> aiDic = new Dictionary<long, AIBase>();
    }
}
