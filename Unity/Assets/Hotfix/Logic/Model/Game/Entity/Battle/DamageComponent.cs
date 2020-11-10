using System;
using System.Collections.Generic;

namespace ET
{
    public class DamageComponent:Entity
    {
        public void Damage(Unit attacker, int value)
        {
            if (value < 0)
                return;
            var unit = GetParent<Unit>();
            if (!unit.IsAlive)
                return;
            var num = unit.GetComponent<NumericComponent>();
            num.SetAdd(NumericType.Hp, -value);
            if(num.GetAsInt(NumericType.Hp) <= 0)
            {
                attacker.GetComponent<TargetComponent>().RemoveTraget(unit);
                unit.Dead();
            }
        }
    }
}
