using System;
using System.Collections.Generic;

namespace ET
{
    public enum AIType
    {
        None,
        Tower,
        Monster,

    }
    public abstract class AIBase:Entity
    {
        public abstract AIType aiType { get; }
    }
}
