using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public class ModifierLogic:Entity
    {
        public ModifierId modifierId => modifierConfig.Id;
        public SkillLogic skillLogic;
        public ModifierConfig modifierConfig;

        public int overlay;
        public int interval;
        public int continueTime;
        public int shield;
        public FP multiDamageX10000;
        public int playAmount;

        public long leastTime;
        public ETCancellationToken cancellationToken;
    }
}
