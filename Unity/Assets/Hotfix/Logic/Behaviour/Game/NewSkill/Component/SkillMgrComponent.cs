using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public class SkillMgrComponent : Entity
    {
        public readonly Dictionary<int, SkillLogic> skillDic = new Dictionary<int, SkillLogic>();
    }
}
