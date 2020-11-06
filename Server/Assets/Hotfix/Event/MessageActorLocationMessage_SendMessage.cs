namespace DCET
{
	[Event(MessageEventIdType.MessageActorLocationMessage)]
    public class MessageActorLocationMessage_SendMessage : AEvent<Session, ushort, IActorLocationMessage>
    {
        public async override void Run(Session session, ushort opcode, IActorLocationMessage actorLocationMessage)
        {
            long unitId = session.GetComponent<SessionPlayerComponent>().Player.UnitId;
            ActorLocationSenderComponent.Instance.Send(unitId, actorLocationMessage);
        }
    }
}
