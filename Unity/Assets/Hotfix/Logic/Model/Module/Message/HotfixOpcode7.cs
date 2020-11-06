using ET;
namespace ET
{
//=================邮箱======================
	[Message(HotfixOpcode7.MailItem)]
	public partial class MailItem {}

	[Message(HotfixOpcode7.Mail)]
	public partial class Mail {}

	[Message(HotfixOpcode7.C2M_GetMail)]
	public partial class C2M_GetMail : IActorLocationRequest {}

	[Message(HotfixOpcode7.M2C_GetMail)]
	public partial class M2C_GetMail : IActorLocationResponse {}

	[Message(HotfixOpcode7.C2M_ReceiveMail)]
	public partial class C2M_ReceiveMail : IActorLocationRequest {}

	[Message(HotfixOpcode7.M2C_ReceiveMail)]
	public partial class M2C_ReceiveMail : IActorLocationResponse {}

	[Message(HotfixOpcode7.C2M_DeleteMail)]
	public partial class C2M_DeleteMail : IActorLocationRequest {}

	[Message(HotfixOpcode7.M2C_DeleteMail)]
	public partial class M2C_DeleteMail : IActorLocationResponse {}

	[Message(HotfixOpcode7.C2M_ReceiveAllMail)]
	public partial class C2M_ReceiveAllMail : IActorLocationRequest {}

	[Message(HotfixOpcode7.M2C_ReceiveAllMail)]
	public partial class M2C_ReceiveAllMail : IActorLocationResponse {}

	[Message(HotfixOpcode7.C2M_DeleteAllMail)]
	public partial class C2M_DeleteAllMail : IActorLocationRequest {}

	[Message(HotfixOpcode7.M2C_DeleteAllMail)]
	public partial class M2C_DeleteAllMail : IActorLocationResponse {}

//==============排行榜=======================
	[Message(HotfixOpcode7.RankingInfo)]
	public partial class RankingInfo {}

	[Message(HotfixOpcode7.C2M_GetRanking)]
	public partial class C2M_GetRanking : IActorLocationRequest {}

	[Message(HotfixOpcode7.M2C_GetRanking)]
	public partial class M2C_GetRanking : IActorLocationResponse {}

//==============聊天=========================
	[Message(HotfixOpcode7.C2M_RequestChat)]
	public partial class C2M_RequestChat : IActorLocationRequest {}

	[Message(HotfixOpcode7.M2C_RequestChat)]
	public partial class M2C_RequestChat : IActorLocationResponse {}

	[Message(HotfixOpcode7.M2C_SendNormalChat)]
	public partial class M2C_SendNormalChat : IActorMessage {}

	[Message(HotfixOpcode7.M2C_SendSystemChat)]
	public partial class M2C_SendSystemChat : IActorMessage {}

}
namespace ET
{
	public static partial class HotfixOpcode7
	{
		 public const ushort MailItem = 17001;
		 public const ushort Mail = 17002;
		 public const ushort C2M_GetMail = 17003;
		 public const ushort M2C_GetMail = 17004;
		 public const ushort C2M_ReceiveMail = 17005;
		 public const ushort M2C_ReceiveMail = 17006;
		 public const ushort C2M_DeleteMail = 17007;
		 public const ushort M2C_DeleteMail = 17008;
		 public const ushort C2M_ReceiveAllMail = 17009;
		 public const ushort M2C_ReceiveAllMail = 17010;
		 public const ushort C2M_DeleteAllMail = 17011;
		 public const ushort M2C_DeleteAllMail = 17012;
		 public const ushort RankingInfo = 17013;
		 public const ushort C2M_GetRanking = 17014;
		 public const ushort M2C_GetRanking = 17015;
		 public const ushort C2M_RequestChat = 17016;
		 public const ushort M2C_RequestChat = 17017;
		 public const ushort M2C_SendNormalChat = 17018;
		 public const ushort M2C_SendSystemChat = 17019;
	}
}
