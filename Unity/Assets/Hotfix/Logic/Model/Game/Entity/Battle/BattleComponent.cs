using System;
using System.Collections.Generic;
using System.Text;

namespace ET
{
    public class BattleComponent:Entity
    {
        public Unit unit;
        public Unit targeUnit;
        public BattleType BattleType{ get; set; }
    }
}
