using ET;
namespace ET
{
//登录相关==================================
	[Message(HotfixOpcode1.C2R_Login)]
	public partial class C2R_Login : IRequest {}

	[Message(HotfixOpcode1.R2C_Login)]
	public partial class R2C_Login : IResponse {}

	[Message(HotfixOpcode1.C2R_Regist)]
	public partial class C2R_Regist : IRequest {}

	[Message(HotfixOpcode1.R2C_Regist)]
	public partial class R2C_Regist : IResponse {}

	[Message(HotfixOpcode1.C2G_HeartBeat)]
	public partial class C2G_HeartBeat : IRequest {}

	[Message(HotfixOpcode1.G2C_HeartBeat)]
	public partial class G2C_HeartBeat : IResponse {}

	[Message(HotfixOpcode1.C2G_LoginGate)]
	public partial class C2G_LoginGate : IRequest {}

	[Message(HotfixOpcode1.G2C_LoginGate)]
	public partial class G2C_LoginGate : IResponse {}

	[Message(HotfixOpcode1.C2R_CreateRole)]
	public partial class C2R_CreateRole : IRequest {}

	[Message(HotfixOpcode1.R2C_CreateRole)]
	public partial class R2C_CreateRole : IResponse {}

	[Message(HotfixOpcode1.C2R_DelRole)]
	public partial class C2R_DelRole : IRequest {}

	[Message(HotfixOpcode1.R2C_DelRole)]
	public partial class R2C_DelRole : IResponse {}

	[Message(HotfixOpcode1.UnitPosInfo)]
	public partial class UnitPosInfo {}

	[Message(HotfixOpcode1.UnitScene)]
	public partial class UnitScene {}

	[Message(HotfixOpcode1.Frame_ClickMap)]
	public partial class Frame_ClickMap : IActorLocationMessage {}

	[Message(HotfixOpcode1.C2M_CheckSceneError)]
	public partial class C2M_CheckSceneError : IActorLocationMessage {}

	[Message(HotfixOpcode1.M2C_PathfindingResult)]
	public partial class M2C_PathfindingResult : IActorMessage {}

	[Message(HotfixOpcode1.M2C_SendUnitInfo)]
	public partial class M2C_SendUnitInfo : IActorMessage {}

	[Message(HotfixOpcode1.M2C_SendUnitInfoList)]
	public partial class M2C_SendUnitInfoList : IActorMessage {}

	[Message(HotfixOpcode1.C2G_EnterGame)]
	public partial class C2G_EnterGame : IRequest {}

	[Message(HotfixOpcode1.G2C_EnterGame)]
	public partial class G2C_EnterGame : IResponse {}

	[Message(HotfixOpcode1.C2M_GetBasicalInfo)]
	public partial class C2M_GetBasicalInfo : IActorLocationMessage {}

	[Message(HotfixOpcode1.C2M_GetStateReback)]
	public partial class C2M_GetStateReback : IActorLocationMessage {}

//==============地图传送=====================
	[Message(HotfixOpcode1.C2M_RequestEnterMap)]
	public partial class C2M_RequestEnterMap : IActorLocationRequest {}

	[Message(HotfixOpcode1.M2C_RequestEnterMap)]
	public partial class M2C_RequestEnterMap : IActorLocationResponse {}

	[Message(HotfixOpcode1.M2C_ChangeMap)]
	public partial class M2C_ChangeMap : IActorMessage {}

	[Message(HotfixOpcode1.M2C_EnterMap)]
	public partial class M2C_EnterMap : IActorMessage {}

	[Message(HotfixOpcode1.M2C_UnitsInMap)]
	public partial class M2C_UnitsInMap : IActorMessage {}

	[Message(HotfixOpcode1.M2C_LeaveMap)]
	public partial class M2C_LeaveMap : IActorMessage {}

	[Message(HotfixOpcode1.M2C_OffLine)]
	public partial class M2C_OffLine : IActorMessage {}

	[Message(HotfixOpcode1.M2C_StartupTransPoint)]
	public partial class M2C_StartupTransPoint : IActorMessage {}

	[Message(HotfixOpcode1.C2M_StopMove)]
	public partial class C2M_StopMove : IActorLocationMessage {}

//=================测试===========================
	[Message(HotfixOpcode1.C2G_GetNotice)]
	public partial class C2G_GetNotice : IRequest {}

	[Message(HotfixOpcode1.G2C_GetNotice)]
	public partial class G2C_GetNotice : IResponse {}

	[Message(HotfixOpcode1.C2M_BackMainCity)]
	public partial class C2M_BackMainCity : IActorLocationRequest {}

	[Message(HotfixOpcode1.M2C_BackMainCity)]
	public partial class M2C_BackMainCity : IActorLocationResponse {}

	[Message(HotfixOpcode1.C2M_GetBuffTime)]
	public partial class C2M_GetBuffTime : IActorLocationMessage {}

	[Message(HotfixOpcode1.C2M_AddExp)]
	public partial class C2M_AddExp : IActorLocationMessage {}

}
namespace ET
{
	public static partial class HotfixOpcode1
	{
		 public const ushort C2R_Login = 11001;
		 public const ushort R2C_Login = 11002;
		 public const ushort C2R_Regist = 11003;
		 public const ushort R2C_Regist = 11004;
		 public const ushort C2G_HeartBeat = 11005;
		 public const ushort G2C_HeartBeat = 11006;
		 public const ushort C2G_LoginGate = 11007;
		 public const ushort G2C_LoginGate = 11008;
		 public const ushort C2R_CreateRole = 11009;
		 public const ushort R2C_CreateRole = 11010;
		 public const ushort C2R_DelRole = 11011;
		 public const ushort R2C_DelRole = 11012;
		 public const ushort UnitPosInfo = 11013;
		 public const ushort UnitScene = 11014;
		 public const ushort Frame_ClickMap = 11015;
		 public const ushort C2M_CheckSceneError = 11016;
		 public const ushort M2C_PathfindingResult = 11017;
		 public const ushort M2C_SendUnitInfo = 11018;
		 public const ushort M2C_SendUnitInfoList = 11019;
		 public const ushort C2G_EnterGame = 11020;
		 public const ushort G2C_EnterGame = 11021;
		 public const ushort C2M_GetBasicalInfo = 11022;
		 public const ushort C2M_GetStateReback = 11023;
		 public const ushort C2M_RequestEnterMap = 11024;
		 public const ushort M2C_RequestEnterMap = 11025;
		 public const ushort M2C_ChangeMap = 11026;
		 public const ushort M2C_EnterMap = 11027;
		 public const ushort M2C_UnitsInMap = 11028;
		 public const ushort M2C_LeaveMap = 11029;
		 public const ushort M2C_OffLine = 11030;
		 public const ushort M2C_StartupTransPoint = 11031;
		 public const ushort C2M_StopMove = 11032;
		 public const ushort C2G_GetNotice = 11033;
		 public const ushort G2C_GetNotice = 11034;
		 public const ushort C2M_BackMainCity = 11035;
		 public const ushort M2C_BackMainCity = 11036;
		 public const ushort C2M_GetBuffTime = 11037;
		 public const ushort C2M_AddExp = 11038;
	}
}
