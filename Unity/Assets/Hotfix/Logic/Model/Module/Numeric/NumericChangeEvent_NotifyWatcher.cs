namespace ET
{
	// 分发数值监听
	[OldEvent(EventIdType.NumbericChange)]
	public class NumericChangeEvent_NotifyWatcher: AOldEvent<Unit, NumericType, int>
	{
		public override void Run(Unit unit, NumericType numericType, int value)
		{
			Game.Scene.GetComponent<NumericWatcherComponent>().Run(numericType, unit, value);
		}
	}
}
