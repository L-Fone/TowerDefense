using System;
using System.Threading.Tasks;

namespace DCET
{
	[ActorMessageHandler]
	public class ObjectRemoveRequestHandler : AMActorRpcHandler<Scene, ObjectRemoveRequest, ObjectRemoveResponse>
	{
		protected override async Task Run(Scene scene, ObjectRemoveRequest request, ObjectRemoveResponse response, Action reply)
		{
			await scene.GetComponent<LocationComponent>().Remove(request.Key);
			reply();
			await Task.CompletedTask;
		}
	}
}