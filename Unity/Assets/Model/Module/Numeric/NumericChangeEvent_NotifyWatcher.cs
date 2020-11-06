namespace ET
{
	// 分发数值监听
	[OldEvent(EventIdType.NumbericChange)]
	public class NumericChangeEvent_NotifyWatcher: AOldEvent<long, NumericType, int>
	{
		public override void Run(long id, NumericType numericType, int value)
		{
			Game.Scene.GetComponent<NumericWatcherComponent>().Run(numericType, id, value);
		}
	}
}
