using ET.EventType;
using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class UpdateUnitPositionEvent : AEvent<UpdateUnitPosition>
    {
        public override async ETTask Run(UpdateUnitPosition args)
        {
            var unit = args.unit;
            var unitView = unit.GetComponent<UnitView>();
            if (unitView == null)
            {
                Log.Error($"uniView == null");
                return;
            }
            unitView.Position = args.pos;
            await ETTask.CompletedTask;
        }
    }
}
