using ET.EventType;
using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class PopupDamageEvent : AEvent<PopupDamage>
    {
        public override async ETTask Run(PopupDamage args)
        {
            var unit = args.unit;
            PopupComponent popupComponent = unit.GetComponent<PopupComponent>();
            popupComponent.PlayDamage(args.value, args.isCrit);
            await ETTask.CompletedTask;
        }
    }
}
