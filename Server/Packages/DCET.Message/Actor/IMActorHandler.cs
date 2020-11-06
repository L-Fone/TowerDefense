using System;
using System.Threading.Tasks;

namespace DCET
{
	public interface IMActorHandler
	{
		Task Handle(Session session, Entity entity, object actorMessage);
		Type GetMessageType();
	}
}