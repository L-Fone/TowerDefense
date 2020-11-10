using System;
using System.Collections.Generic;

namespace ET
{
    public abstract class TargetableUnitBase : Entity
    {

    }
    public class TargetableSingleUnit : TargetableUnitBase
    {
        public Unit target;
    }
    public class TargetableMultiUnit : TargetableUnitBase
    {
        public List<Unit> targetList = new List<Unit>();
    }
}
