using System;

namespace ET
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIEventAttribute : BaseAttribute
    {
        public string UIType { get; }

        public UIEventAttribute(string uiType)
        {
            this.UIType = uiType;
        }
    }
}