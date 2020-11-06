using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class HideUnitComponentAwakeSystem : AwakeSystem<HideUnitComponent>
    {
        public override void Awake(HideUnitComponent self)
        {
            self.Awake();
        }
    }

    public class HideUnitComponent : Entity
    {
        private static HideUnitComponent inst;

        internal void Awake()
        {
            inst = this;
        }
        public static bool Add(Unit unit)
        {
            if (inst.Children.ContainsKey(unit.Id))
                return false;
            inst.AddChild(unit);
            return true;
        }
        public static IEnumerable<Entity> GetAll()
        {
            return inst.Children.Values;
        }
        public static bool Remove(Unit unit)
        {
            return inst.RemoveChild(unit);
        }

    }
}
