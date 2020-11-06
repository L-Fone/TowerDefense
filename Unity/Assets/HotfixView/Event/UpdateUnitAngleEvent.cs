using ET.EventType;
using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class UpdateUnitAngleEvent : AEvent<UpdateUnitAngle>
    {
        public override async ETTask Run(UpdateUnitAngle args)
        {
            var unit = args.unit;
            var unitView = unit.GetComponent<UnitView>();
            if (unitView == null)
            {
                Log.Error($"uniView == null");
                return;
            }
            unitView.IsFlip = args.yAngle==180;
            await ETTask.CompletedTask;
        }
    }
}
