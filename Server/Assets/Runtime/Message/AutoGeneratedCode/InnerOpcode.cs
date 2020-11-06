namespace DCET
{
	[Message(InnerOpcode.M2M_TrasferUnitRequest)]
	public partial class M2M_TrasferUnitRequest : IActorRequest
	{
	}
	[Message(InnerOpcode.M2M_TrasferUnitResponse)]
	public partial class M2M_TrasferUnitResponse : IActorResponse
	{
	}
	[Message(InnerOpcode.M2A_Reload)]
	public partial class M2A_Reload : IActorRequest
	{
	}
	[Message(InnerOpcode.A2M_Reload)]
	public partial class A2M_Reload : IActorResponse
	{
	}
	[Message(InnerOpcode.G2G_LockRequest)]
	public partial class G2G_LockRequest : IActorRequest
	{
	}
	[Message(InnerOpcode.G2G_LockResponse)]
	public partial class G2G_LockResponse : IActorResponse
	{
	}
	[Message(InnerOpcode.G2G_LockReleaseRequest)]
	public partial class G2G_LockReleaseRequest : IActorRequest
	{
	}
	[Message(InnerOpcode.G2G_LockReleaseResponse)]
	public partial class G2G_LockReleaseResponse : IActorResponse
	{
	}
	[Message(InnerOpcode.R2G_GetLoginKey)]
	public partial class R2G_GetLoginKey : IActorRequest
	{
	}
	[Message(InnerOpcode.G2R_GetLoginKey)]
	public partial class G2R_GetLoginKey : IActorResponse
	{
	}
	[Message(InnerOpcode.G2M_CreateUnit)]
	public partial class G2M_CreateUnit : IActorRequest
	{
	}
	[Message(InnerOpcode.M2G_CreateUnit)]
	public partial class M2G_CreateUnit : IActorResponse
	{
	}
	[Message(InnerOpcode.G2M_SessionDisconnect)]
	public partial class G2M_SessionDisconnect : IActorLocationMessage
	{
	}
	public static partial class InnerOpcode
	{
		 public const ushort M2M_TrasferUnitRequest = 1001;
		 public const ushort M2M_TrasferUnitResponse = 1002;
		 public const ushort M2A_Reload = 1003;
		 public const ushort A2M_Reload = 1004;
		 public const ushort G2G_LockRequest = 1005;
		 public const ushort G2G_LockResponse = 1006;
		 public const ushort G2G_LockReleaseRequest = 1007;
		 public const ushort G2G_LockReleaseResponse = 1008;
		 public const ushort R2G_GetLoginKey = 1009;
		 public const ushort G2R_GetLoginKey = 1010;
		 public const ushort G2M_CreateUnit = 1011;
		 public const ushort M2G_CreateUnit = 1012;
		 public const ushort G2M_SessionDisconnect = 1013;
	}
}
