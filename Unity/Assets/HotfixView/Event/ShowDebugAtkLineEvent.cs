using ET.EventType;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class ShowDebugAtkLineEvent : AEvent_Sync<ShowDebugAtkLine>
    {
        public override void Run(ShowDebugAtkLine args)
        {
            var unit = args.unit;
            var target = args.target;
            var view = target.GetComponent<UnitView>();
            Debug.DrawLine(unit.GetComponent<UnitView>().Position, view.Position, Color.red,0.3f);
        }
    }
}
