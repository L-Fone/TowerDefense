using System;
using System.Threading.Tasks;

namespace DCET
{
	[ActorMessageHandler]
	public class C2M_TestActorRequestHandler : AMActorLocationRpcHandler<Unit, C2M_TestActorRequest, M2C_TestActorResponse>
	{
		protected override async Task Run(Unit unit, C2M_TestActorRequest message, M2C_TestActorResponse response, Action reply)
		{
			response.Info = "actor rpc response";
			reply();
			await Task.CompletedTask;
		}
	}
}