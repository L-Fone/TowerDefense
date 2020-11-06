using ET;
namespace ET
{
//=================好友===========================
	[Message(HotfixOpcode3.FriendInfo)]
	public partial class FriendInfo {}

	[Message(HotfixOpcode3.RequestAddFriendInfo)]
	public partial class RequestAddFriendInfo {}

	[Message(HotfixOpcode3.C2M_GetFriend)]
	public partial class C2M_GetFriend : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_GetFriend)]
	public partial class M2C_GetFriend : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_FindFriend)]
	public partial class C2M_FindFriend : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_FindFriend)]
	public partial class M2C_FindFriend : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_AddFriend)]
	public partial class C2M_AddFriend : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_AddFriend)]
	public partial class M2C_AddFriend : IActorLocationResponse {}

	[Message(HotfixOpcode3.M2C_SendAddFriendRequest)]
	public partial class M2C_SendAddFriendRequest : IActorMessage {}

	[Message(HotfixOpcode3.C2M_HandleAddFriend)]
	public partial class C2M_HandleAddFriend : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_HandleAddFriend)]
	public partial class M2C_HandleAddFriend : IActorLocationResponse {}

	[Message(HotfixOpcode3.M2C_HandleAddFriendResult)]
	public partial class M2C_HandleAddFriendResult : IActorMessage {}

	[Message(HotfixOpcode3.C2M_DeleteFriend)]
	public partial class C2M_DeleteFriend : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_DeleteFriend)]
	public partial class M2C_DeleteFriend : IActorLocationResponse {}

//=================家族===========================
	[Message(HotfixOpcode3.FamilyMemberInfo)]
	public partial class FamilyMemberInfo {}

	[Message(HotfixOpcode3.FamilyPositionMap)]
	public partial class FamilyPositionMap {}

	[Message(HotfixOpcode3.FamilyInfo)]
	public partial class FamilyInfo {}

	[Message(HotfixOpcode3.C2M_CrateFamily)]
	public partial class C2M_CrateFamily : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_CrateFamily)]
	public partial class M2C_CrateFamily : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_GetFamily)]
	public partial class C2M_GetFamily : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_GetFamily)]
	public partial class M2C_GetFamily : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_FindFamily)]
	public partial class C2M_FindFamily : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_FindFamily)]
	public partial class M2C_FindFamily : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_DeleteFamily)]
	public partial class C2M_DeleteFamily : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_DeleteFamily)]
	public partial class M2C_DeleteFamily : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_RequestEnterFamily)]
	public partial class C2M_RequestEnterFamily : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_RequestEnterFamily)]
	public partial class M2C_RequestEnterFamily : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_LeaveFamily)]
	public partial class C2M_LeaveFamily : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_LeaveFamily)]
	public partial class M2C_LeaveFamily : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_HandleEnterFamiy)]
	public partial class C2M_HandleEnterFamiy : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_HandleEnterFamiy)]
	public partial class M2C_HandleEnterFamiy : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_DeleteFamilyMember)]
	public partial class C2M_DeleteFamilyMember : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_DeleteFamilyMember)]
	public partial class M2C_DeleteFamilyMember : IActorLocationResponse {}

//=================组队============================
//玩家处理信息
	[Message(HotfixOpcode3.HandleInfo)]
	public partial class HandleInfo {}

	[Message(HotfixOpcode3.C2M_RequestTeam)]
	public partial class C2M_RequestTeam : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_RequestTeam)]
	public partial class M2C_RequestTeam : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_InviteTeam)]
	public partial class C2M_InviteTeam : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_InviteTeam)]
	public partial class M2C_InviteTeam : IActorLocationResponse {}

	[Message(HotfixOpcode3.M2C_RequestList)]
	public partial class M2C_RequestList : IActorMessage {}

	[Message(HotfixOpcode3.M2C_InviteList)]
	public partial class M2C_InviteList : IActorMessage {}

	[Message(HotfixOpcode3.C2M_HandleTeam)]
	public partial class C2M_HandleTeam : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_HandleTeam)]
	public partial class M2C_HandleTeam : IActorLocationResponse {}

	[Message(HotfixOpcode3.M2C_TeamMember)]
	public partial class M2C_TeamMember : IActorMessage {}

	[Message(HotfixOpcode3.C2M_TransferTeamLeader)]
	public partial class C2M_TransferTeamLeader : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_TransferTeamLeader)]
	public partial class M2C_TransferTeamLeader : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_QuitTeam)]
	public partial class C2M_QuitTeam : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_QuitTeam)]
	public partial class M2C_QuitTeam : IActorLocationResponse {}

	[Message(HotfixOpcode3.C2M_KickoutTeam)]
	public partial class C2M_KickoutTeam : IActorLocationRequest {}

	[Message(HotfixOpcode3.M2C_KickoutTeam)]
	public partial class M2C_KickoutTeam : IActorLocationResponse {}

}
namespace ET
{
	public static partial class HotfixOpcode3
	{
		 public const ushort FriendInfo = 13001;
		 public const ushort RequestAddFriendInfo = 13002;
		 public const ushort C2M_GetFriend = 13003;
		 public const ushort M2C_GetFriend = 13004;
		 public const ushort C2M_FindFriend = 13005;
		 public const ushort M2C_FindFriend = 13006;
		 public const ushort C2M_AddFriend = 13007;
		 public const ushort M2C_AddFriend = 13008;
		 public const ushort M2C_SendAddFriendRequest = 13009;
		 public const ushort C2M_HandleAddFriend = 13010;
		 public const ushort M2C_HandleAddFriend = 13011;
		 public const ushort M2C_HandleAddFriendResult = 13012;
		 public const ushort C2M_DeleteFriend = 13013;
		 public const ushort M2C_DeleteFriend = 13014;
		 public const ushort FamilyMemberInfo = 13015;
		 public const ushort FamilyPositionMap = 13016;
		 public const ushort FamilyInfo = 13017;
		 public const ushort C2M_CrateFamily = 13018;
		 public const ushort M2C_CrateFamily = 13019;
		 public const ushort C2M_GetFamily = 13020;
		 public const ushort M2C_GetFamily = 13021;
		 public const ushort C2M_FindFamily = 13022;
		 public const ushort M2C_FindFamily = 13023;
		 public const ushort C2M_DeleteFamily = 13024;
		 public const ushort M2C_DeleteFamily = 13025;
		 public const ushort C2M_RequestEnterFamily = 13026;
		 public const ushort M2C_RequestEnterFamily = 13027;
		 public const ushort C2M_LeaveFamily = 13028;
		 public const ushort M2C_LeaveFamily = 13029;
		 public const ushort C2M_HandleEnterFamiy = 13030;
		 public const ushort M2C_HandleEnterFamiy = 13031;
		 public const ushort C2M_DeleteFamilyMember = 13032;
		 public const ushort M2C_DeleteFamilyMember = 13033;
		 public const ushort HandleInfo = 13034;
		 public const ushort C2M_RequestTeam = 13035;
		 public const ushort M2C_RequestTeam = 13036;
		 public const ushort C2M_InviteTeam = 13037;
		 public const ushort M2C_InviteTeam = 13038;
		 public const ushort M2C_RequestList = 13039;
		 public const ushort M2C_InviteList = 13040;
		 public const ushort C2M_HandleTeam = 13041;
		 public const ushort M2C_HandleTeam = 13042;
		 public const ushort M2C_TeamMember = 13043;
		 public const ushort C2M_TransferTeamLeader = 13044;
		 public const ushort M2C_TransferTeamLeader = 13045;
		 public const ushort C2M_QuitTeam = 13046;
		 public const ushort M2C_QuitTeam = 13047;
		 public const ushort C2M_KickoutTeam = 13048;
		 public const ushort M2C_KickoutTeam = 13049;
	}
}
