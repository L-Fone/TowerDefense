using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class SetHudCharacter_ChangeHpEvent : AEvent_Sync<SetHudCharacter_ChangeHp>
    {
        public override void Run(SetHudCharacter_ChangeHp args)
        {
            var unit = args.unit;
            var hud = unit.GetComponent<HudComponenet>();
            if (hud == null)
                return;
            hud.ChangeHp();
        }
    }
}
