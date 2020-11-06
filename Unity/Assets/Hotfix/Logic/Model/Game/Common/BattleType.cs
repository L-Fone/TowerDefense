using System;
using System.Collections.Generic;

namespace ET
{
    public enum BattleType : byte
    {
        None = 0,
        MainStory = 1,
        TrialCopy = 1 << 1,
        PK = 1 << 2,
        Boss = 1 << 3,
        IdleBattle = 1 << 4,
        TestBattle = 1 << 5,
    }
}
