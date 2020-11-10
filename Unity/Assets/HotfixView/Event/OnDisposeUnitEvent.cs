using ET.EventType;
using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class OnDisposeUnitEvent : AEvent_Sync<OnDisposeUnit>
    {
        public override void Run(OnDisposeUnit args)
        {
            var unit = args.unit;
            if (unit.UnitType == UnitType.NPC)
            {

            }
            else
            if (unit.UnitType == UnitType.TransPoint)
                return;
            else
                return;
        }
    }
}
