using System;
using System.Collections.Generic;
using System.Text;

namespace ET
{
    public class TargetableMultiUnitAwakeSystem : AwakeSystem<TargetableMultiUnit, IEnumerable<Unit>>
    {
        public override void Awake(TargetableMultiUnit self, IEnumerable<Unit> unit)
        {
            self.targetList.AddRange(unit);
        }
    }
    public class TargetableMultiUnitDestroySystem : DestroySystem<TargetableMultiUnit>
    {
        public override void Destroy(TargetableMultiUnit self)
        {
            self.targetList.Clear();
        }
    }
    public class TargetableMultiUnitSystem
    {

    }
}
