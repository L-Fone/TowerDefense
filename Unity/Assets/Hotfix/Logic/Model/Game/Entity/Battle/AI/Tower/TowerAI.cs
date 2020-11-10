using System;
using System.Collections.Generic;

namespace ET
{
    public class TowerAIAwakeSystem : AwakeSystem<TowerAI>
    {
    	public override void Awake(TowerAI self)
    	{
    		self.Awake();
    	}
    }
    public class TowerAIUpdateSystem : UpdateSystem<TowerAI>
    {
    	public override void Update(TowerAI self)
    	{
    		self.Update();
    	}
    }
    public class TowerAI:AIBase
    {
        public override AIType aiType => AIType.Tower;

        private TargetComponent targetComponent;

        internal void Awake()
        {
            var unit = GetParent<Unit>();
            targetComponent = unit.GetComponent<TargetComponent>();
            targetComponent.AddTrigger(unit, OnEnermyEnter, OnEnermyExit);
        }


        private void OnEnermyEnter(Unit unit)
        {
            //Log.Info($"{Id}: enermy enter:{unit.Id}");
        }

        private void OnEnermyExit(Unit unit)
        {
            //Log.Info($"{Id}: enermy exit:{unit.Id}");
        }
        internal void Update()
        {

        }
    }
}
