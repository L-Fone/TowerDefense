namespace DCET
{
	[Event(MessageEventIdType.MessageActorLocationMessage)]
    public class MessageActorLocationRequest_ReplyResponse : AEvent<Session, ushort, IActorLocationRequest>
    {
		// gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
		public async override void Run(Session session, ushort opcode, IActorLocationRequest actorLocationRequest)
        {
			long unitId = session.GetComponent<SessionPlayerComponent>().Player.UnitId;
			int rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
			long instanceId = session.InstanceId;
			IResponse response = await ActorLocationSenderComponent.Instance.Call(unitId, actorLocationRequest);
			response.RpcId = rpcId;
			// session可能已经断开了，所以这里需要判断
			if (session.InstanceId == instanceId)
			{
				session.Reply(response);
			}
		}
    }
}
