namespace DCET
{
	public class OuterMessageDispatcher: IMessageDispatcher
	{
		public void Dispatch(Session session, ushort opcode, object message)
		{
			DispatchAsync(session, opcode, message);
		}
		
		public void DispatchAsync(Session session, ushort opcode, object message)
		{
			// 根据消息接口判断是不是Actor消息，不同的接口做不同的处理
			switch (message)
			{
				case IActorLocationRequest actorLocationRequest:
				{
					Game.EventSystem.Run(MessageEventIdType.MessageActorLocationRequest, session, opcode, actorLocationRequest);					
					break;
				}
				case IActorLocationMessage actorLocationMessage:
				{
					Game.EventSystem.Run(MessageEventIdType.MessageActorLocationMessage, session, opcode, actorLocationMessage);
					break;
				}
				case IActorRequest actorRequest:  // 分发IActorRequest消息，目前没有用到，需要的自己添加
				{
					Game.EventSystem.Run(MessageEventIdType.MessageActorRequest, session, opcode, actorRequest);
					break;
				}
				case IActorMessage actorMessage:  // 分发IActorMessage消息，目前没有用到，需要的自己添加
				{
					Game.EventSystem.Run(MessageEventIdType.MessageActorMessage, session, opcode, actorMessage);
					break;
				}
				
				default:
				{
					// 非Actor消息
					MessageDispatcherComponent.Instance.Handle(session, opcode, message);
					break;
				}
			}
		}
	}
}
