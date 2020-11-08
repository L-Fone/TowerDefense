using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class PlayAnimationEvent : AEvent<PlayAnimation>
    {
        public override async ETTask Run(PlayAnimation args)
        {
            var unit = args.unit;
            var animator = unit.GetComponent<UnitView>().gameObject.GetComponent<SpriteAnimator>();
            animator?.PlayAnimation(args.ainmationKey, args.dir);
            await ETTask.CompletedTask;
        }
    }
}
