namespace ET
{
	/// <summary>
	/// 监视hp数值变化
	/// </summary>
	[NumericWatcher(NumericType.Hp)]
	public class NumericWatcher_Hp : INumericWatcher
	{
		public void Run(Unit unit, int value)
		{
			 Game.EventSystem.Publish_Sync(new ET.EventType.SetHudCharacter_ChangeHp
            {
            	zoneScene =unit.ZoneScene(),
				unit =unit
            });
		}
	}
}
