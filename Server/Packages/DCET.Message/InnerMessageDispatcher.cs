namespace DCET
{
    public class InnerMessageDispatcher: IMessageDispatcher
    {
        public void Dispatch(Session session, ushort opcode, object message)
        {
            // 收到actor消息,放入actor队列
            switch (message)
            {
                case IActorRequest iActorRequest:
                {
                    InnerMessageDispatcherHelper.HandleIActorRequest(session, iActorRequest);
                    return;
                }
                case IActorMessage iactorMessage:
                {
                    InnerMessageDispatcherHelper.HandleIActorMessage(session, iactorMessage);
                    return;
                }
                case IActorResponse iActorResponse:
                {
                    InnerMessageDispatcherHelper.HandleIActorResponse(session, iActorResponse);
                    return;
                }
                default:
                {
                    MessageDispatcherComponent.Instance.Handle(session, opcode, message);
                    break;
                }
            }
        }
		

    }
}