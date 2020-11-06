using System;
using UnityEngine.UI;

namespace ET
{
	public static class ActionHelper
	{
		public static void Add(this Button.ButtonClickedEvent buttonClickedEvent, Action action)
		{
			buttonClickedEvent.AddListener(() => { action(); });
		}
		public static void Add(this DoubleClickButton.DoubleClickedEvent buttonClickedEvent, Action<DoubleClickButton> action)
		{
			buttonClickedEvent.AddListener(thisBtn => { action(thisBtn); });
		}
	}
}