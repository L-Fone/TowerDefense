using System;
using System.Collections.Generic;
using System.Text;

namespace ET
{
    public class TargetableSingleUnitAwakeSystem : AwakeSystem<TargetableSingleUnit,Unit>
    {
        public override void Awake(TargetableSingleUnit self,Unit unit)
        {
            self.target = unit;
        }
    }
    public class TargetableSingleUnitDestroySystem : DestroySystem<TargetableSingleUnit>
    {
        public override void Destroy(TargetableSingleUnit self)
        {
            self.target = null;
        }
    }

    public class TargetableSingleUnitSystem
    {

    }
}
