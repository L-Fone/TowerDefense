using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class AddTriggerEvent : AEvent_Sync<AddTrigger>
    {
        public override void Run(AddTrigger args)
        {
            var unitView = args.unit?.GetComponent<UnitView>();
            if (unitView == null)
            {
                Log.Error($"unitView == null when id = {args.unit?.Id}");
                return;
            }
            var go = unitView.gameObject;
            var triggerMono = go.GetOrAddComponent<TriggerMono>();
            triggerMono.Clear();

            if (args.onEnter != null)
                triggerMono.onTriggerEnter2D += obj =>
                {
                    Unit unit = obj.GetComponent<ComponentView>().Component.As<UnitView>().GetParent<Unit>();
                    args.onEnter.Invoke(unit);
                };
            if (args.onExit != null)
                triggerMono.onTriggerExit2D += obj =>
               {
                   Unit unit = obj.GetComponent<ComponentView>().Component.As<UnitView>().GetParent<Unit>();
                   args.onExit.Invoke(unit);
               };
        }


    }
}
