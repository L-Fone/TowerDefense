using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public class SkillAI:Entity
    {
        public bool canSkill;
        public long lastSkillTime;
        public FP roundCD;
        public LinkedList<int> AutoSkillList;
        public LinkedListNode<int> CurrSkillNode;
    }
}