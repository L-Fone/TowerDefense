using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class TowerPointInfoDestroySystem : DestroySystem<TowerPointInfo>
    {
    	public override void Destroy(TowerPointInfo self)
    	{
    		self.Destroy();
    	}
    }
    public class TowerPointInfo:Entity
    {
        public Vector3 position;
        public Unit unit;

        internal void Destroy()
        {
            unit = null;
        }
    }
}
