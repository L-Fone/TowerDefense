using ET.EventType;
using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class UpdateUnitPositionEvent : AEvent_Sync<UpdateUnitPosition>
    {
        public override void Run(UpdateUnitPosition args)
        {
            var unit = args.unit;
            var unitView = unit.GetComponent<UnitView>();
            if (unitView == null)
            {
                Log.Error($"uniView == null");
                return;
            }
            unitView.Position = args.pos;
        }
    }
}
