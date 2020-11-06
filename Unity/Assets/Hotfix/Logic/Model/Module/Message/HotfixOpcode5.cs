using ET;
namespace ET
{
//================商城/商店==================
	[Message(HotfixOpcode5.C2M_GetMarket)]
	public partial class C2M_GetMarket : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_GetMarket)]
	public partial class M2C_GetMarket : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_BuyInShop)]
	public partial class C2M_BuyInShop : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_BuyInShop)]
	public partial class M2C_BuyInShop : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_BuyInMarket)]
	public partial class C2M_BuyInMarket : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_BuyInMarket)]
	public partial class M2C_BuyInMarket : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_SellItem)]
	public partial class C2M_SellItem : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_SellItem)]
	public partial class M2C_SellItem : IActorLocationResponse {}

//================寄售=======================
	[Message(HotfixOpcode5.ConsignMap)]
	public partial class ConsignMap {}

	[Message(HotfixOpcode5.C2M_GetConsignment)]
	public partial class C2M_GetConsignment : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_GetConsignment)]
	public partial class M2C_GetConsignment : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_PutInConsignment)]
	public partial class C2M_PutInConsignment : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_PutInConsignment)]
	public partial class M2C_PutInConsignment : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_BuyInConsignment)]
	public partial class C2M_BuyInConsignment : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_BuyInConsignment)]
	public partial class M2C_BuyInConsignment : IActorLocationResponse {}

//================仓库=======================
	[Message(HotfixOpcode5.M2C_OpenStoreUI)]
	public partial class M2C_OpenStoreUI : IActorMessage {}

	[Message(HotfixOpcode5.C2M_GetStore)]
	public partial class C2M_GetStore : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_GetStore)]
	public partial class M2C_GetStore : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_SortStore)]
	public partial class C2M_SortStore : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_SortStore)]
	public partial class M2C_SortStore : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_PutInStore)]
	public partial class C2M_PutInStore : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_PutInStore)]
	public partial class M2C_PutInStore : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_TakeOffStore)]
	public partial class C2M_TakeOffStore : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_TakeOffStore)]
	public partial class M2C_TakeOffStore : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_PutCoinInStore)]
	public partial class C2M_PutCoinInStore : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_PutCoinInStore)]
	public partial class M2C_PutCoinInStore : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_TakeCoinOutStore)]
	public partial class C2M_TakeCoinOutStore : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_TakeCoinOutStore)]
	public partial class M2C_TakeCoinOutStore : IActorLocationResponse {}

//================打造=======================
	[Message(HotfixOpcode5.C2M_ForgeEquip)]
	public partial class C2M_ForgeEquip : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_ForgeEquip)]
	public partial class M2C_ForgeEquip : IActorLocationResponse {}

//================NPC========================
	[Message(HotfixOpcode5.C2M_ClickNPC)]
	public partial class C2M_ClickNPC : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_ClickNPC)]
	public partial class M2C_ClickNPC : IActorLocationResponse {}

	[Message(HotfixOpcode5.M2C_OpenMeltEquipUI)]
	public partial class M2C_OpenMeltEquipUI : IActorMessage {}

	[Message(HotfixOpcode5.M2C_OpenShopUI)]
	public partial class M2C_OpenShopUI : IActorMessage {}

	[Message(HotfixOpcode5.M2C_OpenConsignmentUI)]
	public partial class M2C_OpenConsignmentUI : IActorMessage {}

	[Message(HotfixOpcode5.M2C_OpenForgeUI)]
	public partial class M2C_OpenForgeUI : IActorMessage {}

//熔炼
	[Message(HotfixOpcode5.C2M_MeltEquip)]
	public partial class C2M_MeltEquip : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_MeltEquip)]
	public partial class M2C_MeltEquip : IActorLocationResponse {}

//任务
	[Message(HotfixOpcode5.TansferTask)]
	public partial class TansferTask {}

	[Message(HotfixOpcode5.NPCTask)]
	public partial class NPCTask {}

	[Message(HotfixOpcode5.M2C_OpenTaskUI)]
	public partial class M2C_OpenTaskUI : IActorMessage {}

	[Message(HotfixOpcode5.C2M_AcceptTask)]
	public partial class C2M_AcceptTask : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_AcceptTask)]
	public partial class M2C_AcceptTask : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_CompleteTask)]
	public partial class C2M_CompleteTask : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_CompleteTask)]
	public partial class M2C_CompleteTask : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_GetTask)]
	public partial class C2M_GetTask : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_GetTask)]
	public partial class M2C_GetTask : IActorLocationResponse {}

	[Message(HotfixOpcode5.C2M_GetTaskState)]
	public partial class C2M_GetTaskState : IActorLocationRequest {}

	[Message(HotfixOpcode5.M2C_GetTaskState)]
	public partial class M2C_GetTaskState : IActorLocationResponse {}

	[Message(HotfixOpcode5.M2C_SendTaskState)]
	public partial class M2C_SendTaskState : IActorMessage {}

}
namespace ET
{
	public static partial class HotfixOpcode5
	{
		 public const ushort C2M_GetMarket = 15001;
		 public const ushort M2C_GetMarket = 15002;
		 public const ushort C2M_BuyInShop = 15003;
		 public const ushort M2C_BuyInShop = 15004;
		 public const ushort C2M_BuyInMarket = 15005;
		 public const ushort M2C_BuyInMarket = 15006;
		 public const ushort C2M_SellItem = 15007;
		 public const ushort M2C_SellItem = 15008;
		 public const ushort ConsignMap = 15009;
		 public const ushort C2M_GetConsignment = 15010;
		 public const ushort M2C_GetConsignment = 15011;
		 public const ushort C2M_PutInConsignment = 15012;
		 public const ushort M2C_PutInConsignment = 15013;
		 public const ushort C2M_BuyInConsignment = 15014;
		 public const ushort M2C_BuyInConsignment = 15015;
		 public const ushort M2C_OpenStoreUI = 15016;
		 public const ushort C2M_GetStore = 15017;
		 public const ushort M2C_GetStore = 15018;
		 public const ushort C2M_SortStore = 15019;
		 public const ushort M2C_SortStore = 15020;
		 public const ushort C2M_PutInStore = 15021;
		 public const ushort M2C_PutInStore = 15022;
		 public const ushort C2M_TakeOffStore = 15023;
		 public const ushort M2C_TakeOffStore = 15024;
		 public const ushort C2M_PutCoinInStore = 15025;
		 public const ushort M2C_PutCoinInStore = 15026;
		 public const ushort C2M_TakeCoinOutStore = 15027;
		 public const ushort M2C_TakeCoinOutStore = 15028;
		 public const ushort C2M_ForgeEquip = 15029;
		 public const ushort M2C_ForgeEquip = 15030;
		 public const ushort C2M_ClickNPC = 15031;
		 public const ushort M2C_ClickNPC = 15032;
		 public const ushort M2C_OpenMeltEquipUI = 15033;
		 public const ushort M2C_OpenShopUI = 15034;
		 public const ushort M2C_OpenConsignmentUI = 15035;
		 public const ushort M2C_OpenForgeUI = 15036;
		 public const ushort C2M_MeltEquip = 15037;
		 public const ushort M2C_MeltEquip = 15038;
		 public const ushort TansferTask = 15039;
		 public const ushort NPCTask = 15040;
		 public const ushort M2C_OpenTaskUI = 15041;
		 public const ushort C2M_AcceptTask = 15042;
		 public const ushort M2C_AcceptTask = 15043;
		 public const ushort C2M_CompleteTask = 15044;
		 public const ushort M2C_CompleteTask = 15045;
		 public const ushort C2M_GetTask = 15046;
		 public const ushort M2C_GetTask = 15047;
		 public const ushort C2M_GetTaskState = 15048;
		 public const ushort M2C_GetTaskState = 15049;
		 public const ushort M2C_SendTaskState = 15050;
	}
}
