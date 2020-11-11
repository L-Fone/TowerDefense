using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class SetHudCharacterEvent : AEvent_Sync<SetHudCharacter>
    {
        public override void Run(SetHudCharacter args)
        {
            var unit = args.unit;
            var hud = unit.AddComponent<HudComponenet>();
            hud.Init(args.name,args.level,args.hp,args.maxHp,args.progressTitleType);

        }
    }
}
