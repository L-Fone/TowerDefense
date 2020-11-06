using ET;
namespace ET
{
//==============主Ui=========================
	[Message(HotfixOpcode6.MainUISlotInfo)]
	public partial class MainUISlotInfo {}

	[Message(HotfixOpcode6.C2M_GetMainUISetting)]
	public partial class C2M_GetMainUISetting : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_GetMainUISetting)]
	public partial class M2C_GetMainUISetting : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_DropSkill)]
	public partial class C2M_DropSkill : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_DropSkill)]
	public partial class M2C_DropSkill : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_DropItem)]
	public partial class C2M_DropItem : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_DropItem)]
	public partial class M2C_DropItem : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_UseMainUISkill)]
	public partial class C2M_UseMainUISkill : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_UseMainUISkill)]
	public partial class M2C_UseMainUISkill : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_UseMainUIGoods)]
	public partial class C2M_UseMainUIGoods : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_UseMainUIGoods)]
	public partial class M2C_UseMainUIGoods : IActorLocationResponse {}

//===============技能================================
//技能信息
	[Message(HotfixOpcode6.SkillInfo)]
	public partial class SkillInfo {}

	[Message(HotfixOpcode6.C2M_GetSkill)]
	public partial class C2M_GetSkill : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_GetSkill)]
	public partial class M2C_GetSkill : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_SaveAutoSkill)]
	public partial class C2M_SaveAutoSkill : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_SaveAutoSkill)]
	public partial class M2C_SaveAutoSkill : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_LearnSkill)]
	public partial class C2M_LearnSkill : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_LearnSkill)]
	public partial class M2C_LearnSkill : IActorLocationResponse {}

//==============人物属性=====================
	[Message(HotfixOpcode6.C2M_GetCharacter)]
	public partial class C2M_GetCharacter : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_GetCharacter)]
	public partial class M2C_GetCharacter : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_AddPoint)]
	public partial class C2M_AddPoint : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_AddPoint)]
	public partial class M2C_AddPoint : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_ResetPoint)]
	public partial class C2M_ResetPoint : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_ResetPoint)]
	public partial class M2C_ResetPoint : IActorLocationResponse {}

	[Message(HotfixOpcode6.M2C_SendCharacter)]
	public partial class M2C_SendCharacter : IActorMessage {}

	[Message(HotfixOpcode6.M2C_SendMoney)]
	public partial class M2C_SendMoney : IActorMessage {}

	[Message(HotfixOpcode6.M2C_SendLevel)]
	public partial class M2C_SendLevel : IActorMessage {}

	[Message(HotfixOpcode6.M2C_SendEnergy)]
	public partial class M2C_SendEnergy : IActorMessage {}

//==============背包=========================
	[Message(HotfixOpcode6.C2M_GetBag)]
	public partial class C2M_GetBag : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_GetBag)]
	public partial class M2C_GetBag : IActorLocationResponse {}

	[Message(HotfixOpcode6.M2C_SendBag)]
	public partial class M2C_SendBag : IActorMessage {}

	[Message(HotfixOpcode6.C2M_AddRandomItem)]
	public partial class C2M_AddRandomItem : IActorLocationMessage {}

	[Message(HotfixOpcode6.M2C_SendTip)]
	public partial class M2C_SendTip : IActorMessage {}

	[Message(HotfixOpcode6.C2M_DeleteItem)]
	public partial class C2M_DeleteItem : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_DeleteItem)]
	public partial class M2C_DeleteItem : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_ChangeItemPos)]
	public partial class C2M_ChangeItemPos : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_ChangeItemPos)]
	public partial class M2C_ChangeItemPos : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_SplitItem)]
	public partial class C2M_SplitItem : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_SplitItem)]
	public partial class M2C_SplitItem : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_SortBag)]
	public partial class C2M_SortBag : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_SortBag)]
	public partial class M2C_SortBag : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_PutOn)]
	public partial class C2M_PutOn : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_PutOn)]
	public partial class M2C_PutOn : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_Takeoff)]
	public partial class C2M_Takeoff : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_Takeoff)]
	public partial class M2C_Takeoff : IActorLocationResponse {}

	[Message(HotfixOpcode6.C2M_UseGoods)]
	public partial class C2M_UseGoods : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_UseGoods)]
	public partial class M2C_UseGoods : IActorLocationResponse {}

//=====================进化====================
	[Message(HotfixOpcode6.C2M_Upgrade)]
	public partial class C2M_Upgrade : IActorLocationRequest {}

	[Message(HotfixOpcode6.M2C_Upgrade)]
	public partial class M2C_Upgrade : IActorLocationResponse {}

}
namespace ET
{
	public static partial class HotfixOpcode6
	{
		 public const ushort MainUISlotInfo = 16001;
		 public const ushort C2M_GetMainUISetting = 16002;
		 public const ushort M2C_GetMainUISetting = 16003;
		 public const ushort C2M_DropSkill = 16004;
		 public const ushort M2C_DropSkill = 16005;
		 public const ushort C2M_DropItem = 16006;
		 public const ushort M2C_DropItem = 16007;
		 public const ushort C2M_UseMainUISkill = 16008;
		 public const ushort M2C_UseMainUISkill = 16009;
		 public const ushort C2M_UseMainUIGoods = 16010;
		 public const ushort M2C_UseMainUIGoods = 16011;
		 public const ushort SkillInfo = 16012;
		 public const ushort C2M_GetSkill = 16013;
		 public const ushort M2C_GetSkill = 16014;
		 public const ushort C2M_SaveAutoSkill = 16015;
		 public const ushort M2C_SaveAutoSkill = 16016;
		 public const ushort C2M_LearnSkill = 16017;
		 public const ushort M2C_LearnSkill = 16018;
		 public const ushort C2M_GetCharacter = 16019;
		 public const ushort M2C_GetCharacter = 16020;
		 public const ushort C2M_AddPoint = 16021;
		 public const ushort M2C_AddPoint = 16022;
		 public const ushort C2M_ResetPoint = 16023;
		 public const ushort M2C_ResetPoint = 16024;
		 public const ushort M2C_SendCharacter = 16025;
		 public const ushort M2C_SendMoney = 16026;
		 public const ushort M2C_SendLevel = 16027;
		 public const ushort M2C_SendEnergy = 16028;
		 public const ushort C2M_GetBag = 16029;
		 public const ushort M2C_GetBag = 16030;
		 public const ushort M2C_SendBag = 16031;
		 public const ushort C2M_AddRandomItem = 16032;
		 public const ushort M2C_SendTip = 16033;
		 public const ushort C2M_DeleteItem = 16034;
		 public const ushort M2C_DeleteItem = 16035;
		 public const ushort C2M_ChangeItemPos = 16036;
		 public const ushort M2C_ChangeItemPos = 16037;
		 public const ushort C2M_SplitItem = 16038;
		 public const ushort M2C_SplitItem = 16039;
		 public const ushort C2M_SortBag = 16040;
		 public const ushort M2C_SortBag = 16041;
		 public const ushort C2M_PutOn = 16042;
		 public const ushort M2C_PutOn = 16043;
		 public const ushort C2M_Takeoff = 16044;
		 public const ushort M2C_Takeoff = 16045;
		 public const ushort C2M_UseGoods = 16046;
		 public const ushort M2C_UseGoods = 16047;
		 public const ushort C2M_Upgrade = 16048;
		 public const ushort M2C_Upgrade = 16049;
	}
}
