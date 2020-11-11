using ET.EventType;
using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class UpdateUnitAngleEvent : AEvent_Sync<UpdateUnitRotation>
    {
        public override void Run(UpdateUnitRotation args)
        {
            var unit = args.unit;
            var unitView = unit.GetComponent<UnitView>();
            if (unitView == null)
            {
                Log.Error($"uniView == null");
                return;
            }

            unitView.Rotation = args.rotation;
        }
    }
}
