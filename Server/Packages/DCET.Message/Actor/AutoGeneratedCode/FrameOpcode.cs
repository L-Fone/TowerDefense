namespace DCET
{
	[Message(FrameOpcode.ObjectAddRequest)]
	public partial class ObjectAddRequest : IActorRequest
	{
	}
	[Message(FrameOpcode.ObjectAddResponse)]
	public partial class ObjectAddResponse : IActorResponse
	{
	}
	[Message(FrameOpcode.ObjectLockRequest)]
	public partial class ObjectLockRequest : IActorRequest
	{
	}
	[Message(FrameOpcode.ObjectLockResponse)]
	public partial class ObjectLockResponse : IActorResponse
	{
	}
	[Message(FrameOpcode.ObjectUnLockRequest)]
	public partial class ObjectUnLockRequest : IActorRequest
	{
	}
	[Message(FrameOpcode.ObjectUnLockResponse)]
	public partial class ObjectUnLockResponse : IActorResponse
	{
	}
	[Message(FrameOpcode.ObjectRemoveRequest)]
	public partial class ObjectRemoveRequest : IActorRequest
	{
	}
	[Message(FrameOpcode.ObjectRemoveResponse)]
	public partial class ObjectRemoveResponse : IActorResponse
	{
	}
	[Message(FrameOpcode.ObjectGetRequest)]
	public partial class ObjectGetRequest : IActorRequest
	{
	}
	[Message(FrameOpcode.ObjectGetResponse)]
	public partial class ObjectGetResponse : IActorResponse
	{
	}
	public static partial class FrameOpcode
	{
		 public const ushort ObjectAddRequest = 101;
		 public const ushort ObjectAddResponse = 102;
		 public const ushort ObjectLockRequest = 103;
		 public const ushort ObjectLockResponse = 104;
		 public const ushort ObjectUnLockRequest = 105;
		 public const ushort ObjectUnLockResponse = 106;
		 public const ushort ObjectRemoveRequest = 107;
		 public const ushort ObjectRemoveResponse = 108;
		 public const ushort ObjectGetRequest = 109;
		 public const ushort ObjectGetResponse = 110;
	}
}
