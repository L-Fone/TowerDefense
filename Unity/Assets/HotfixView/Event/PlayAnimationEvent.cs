using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class PlayAnimationEvent : AEvent_Sync<PlayAnimation>
    {
        public override void Run(PlayAnimation args)
        {
            var unit = args.unit;
            var animator = unit.GetComponent<UnitView>().spriteAnimator;
            animator?.PlayAnimation(args.ainmationKey, args.dir);
        }
    }
}
