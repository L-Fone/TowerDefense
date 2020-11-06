using ET;
namespace ET
{
//===============烟雨================================
	[Message(HotfixOpcode10.UserInput_SkillCmd)]
	public partial class UserInput_SkillCmd : IActorLocationMessage {}

	[Message(HotfixOpcode10.M2C_UserInput_SkillCmd)]
	public partial class M2C_UserInput_SkillCmd : IActorMessage {}

	[Message(HotfixOpcode10.G2C_PlayerOffline)]
	public partial class G2C_PlayerOffline : IMessage {}

	[Message(HotfixOpcode10.Actor_CreateSpiling)]
	public partial class Actor_CreateSpiling : IActorLocationMessage {}

//所归属的父实体id
	[Message(HotfixOpcode10.M2C_FrieBattleEvent_PlayEffect)]
	public partial class M2C_FrieBattleEvent_PlayEffect : IActorMessage {}

//=================其他============================
	[Message(HotfixOpcode10.G2C_TestHotfixMessage)]
	public partial class G2C_TestHotfixMessage : IMessage {}

	[Message(HotfixOpcode10.C2M_TestActorRequest)]
	public partial class C2M_TestActorRequest : IActorLocationRequest {}

	[Message(HotfixOpcode10.M2C_TestActorResponse)]
	public partial class M2C_TestActorResponse : IActorLocationResponse {}

	[Message(HotfixOpcode10.C2M_Cheat)]
	public partial class C2M_Cheat : IActorLocationRequest {}

	[Message(HotfixOpcode10.M2C_Cheat)]
	public partial class M2C_Cheat : IActorLocationResponse {}

	[Message(HotfixOpcode10.PlayerInfo)]
	public partial class PlayerInfo : IMessage {}

	[Message(HotfixOpcode10.C2G_PlayerInfo)]
	public partial class C2G_PlayerInfo : IRequest {}

	[Message(HotfixOpcode10.G2C_PlayerInfo)]
	public partial class G2C_PlayerInfo : IResponse {}

}
namespace ET
{
	public static partial class HotfixOpcode10
	{
		 public const ushort UserInput_SkillCmd = 20001;
		 public const ushort M2C_UserInput_SkillCmd = 20002;
		 public const ushort G2C_PlayerOffline = 20003;
		 public const ushort Actor_CreateSpiling = 20004;
		 public const ushort M2C_FrieBattleEvent_PlayEffect = 20005;
		 public const ushort G2C_TestHotfixMessage = 20006;
		 public const ushort C2M_TestActorRequest = 20007;
		 public const ushort M2C_TestActorResponse = 20008;
		 public const ushort C2M_Cheat = 20009;
		 public const ushort M2C_Cheat = 20010;
		 public const ushort PlayerInfo = 20011;
		 public const ushort C2G_PlayerInfo = 20012;
		 public const ushort G2C_PlayerInfo = 20013;
	}
}
