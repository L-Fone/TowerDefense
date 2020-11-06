using ET.EventType;
using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class OnDisposeUnitEvent : AEvent<OnDisposeUnit>
    {
        public override async ETTask Run(OnDisposeUnit args)
        {
            var unit = args.unit;
            if (unit.UnitType == UnitType.NPC)
            {
                NPCHudComponent.Instance.Remove(unit);
            }
            else
            if (unit.UnitType == UnitType.TransPoint)
                return;
            else
                HudComponent.Instance.Remove(unit);
            await ETTask.CompletedTask;
        }
    }
}
