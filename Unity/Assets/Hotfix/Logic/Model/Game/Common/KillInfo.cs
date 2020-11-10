using System;
using System.Collections.Generic;

namespace ET
{
    public struct KillInfo
    {
        public int totalCount;
        public int currCount;
        Action<BattleBase> onVictory;
        BattleBase battleBase;
        public KillInfo Init(BattleBase battleBase, int count, Action<BattleBase> onVictory)
        {
            totalCount = count;
            currCount = 0;
            this.onVictory = onVictory;
            this.battleBase = battleBase;
            return this;
        }
        public KillInfo UpdateAmount(int count)
        {
            currCount += count;
            if (currCount >= totalCount)
            {
                onVictory?.Invoke(battleBase);
            }
            return this;
        }
    }
}

