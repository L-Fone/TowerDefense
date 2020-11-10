using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public class ModifierContainerComponent:Entity
    {
        public readonly Dictionary<ModifierId, ModifierLogic> modifierDic = new Dictionary<ModifierId, ModifierLogic>();

        public readonly DoubleDictionary<ModifierStateType, ModifierId, StateStateType> modifierStateDic= new DoubleDictionary<ModifierStateType, ModifierId, StateStateType>();
        public readonly Dictionary<ModifierId, long> timeDic = new Dictionary<ModifierId, long>();


        public readonly UnOrderMultiMapSet<ModifierEventCondition, ModifierLogic> modifierOptionByConditionDic = new UnOrderMultiMapSet<ModifierEventCondition, ModifierLogic>();
    }
}
