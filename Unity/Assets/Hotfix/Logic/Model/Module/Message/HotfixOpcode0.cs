using ET;
namespace ET
{
	[Message(HotfixOpcode0.BagMap)]
	public partial class BagMap {}

	[Message(HotfixOpcode0.Item)]
	public partial class Item {}

	[Message(HotfixOpcode0.UnitCharacter)]
	public partial class _UnitCharacter {}

	[Message(HotfixOpcode0.UnitCommonCharacter)]
	public partial class UnitCommonCharacter {}

//战斗属性
	[Message(HotfixOpcode0.BattleCharacter)]
	public partial class BattleCharacter {}

	[Message(HotfixOpcode0.EquipTransMessage)]
	public partial class EquipTransMessage {}

}
namespace ET
{
	public static partial class HotfixOpcode0
	{
		 public const ushort BagMap = 10001;
		 public const ushort Item = 10002;
		 public const ushort UnitCharacter = 10003;
		 public const ushort UnitCommonCharacter = 10004;
		 public const ushort BattleCharacter = 10005;
		 public const ushort EquipTransMessage = 10006;
	}
}
