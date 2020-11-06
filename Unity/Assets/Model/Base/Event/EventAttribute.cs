using System;

namespace ET
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class OldEventAttribute: BaseAttribute
	{
		public string Type { get; }

		public OldEventAttribute(string type)
		{
			this.Type = type;
		}
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EventAttribute : BaseAttribute
    {

        public EventAttribute()
        {
        }
    }
}