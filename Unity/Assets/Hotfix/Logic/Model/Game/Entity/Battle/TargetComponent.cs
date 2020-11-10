using System;
using System.Collections.Generic;

namespace ET
{
    public class TargetComponentAwakeSystem : AwakeSystem<TargetComponent>
    {
    	public override void Awake(TargetComponent self)
    	{
    		self.Awake();
    	}
    }
    public class TargetComponent:Entity
    {

        internal void Awake()
        {

        }
        public void AddTrigger(Unit unit,Action<Unit> onEnter,Action<Unit> onExit)
        {
            Game.EventSystem.Publish_Sync(new ET.EventType.AddTrigger
            { 
               unit =unit,
               onEnter=onEnter,
               onExit =onExit
            });
        }
    }
}
