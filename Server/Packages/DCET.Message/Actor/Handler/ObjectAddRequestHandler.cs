using System;
using System.Threading.Tasks;

namespace DCET
{
	[ActorMessageHandler]
	public class ObjectAddRequestHandler : AMActorRpcHandler<Scene, ObjectAddRequest, ObjectAddResponse>
	{
		protected override async Task Run(Scene scene, ObjectAddRequest request, ObjectAddResponse response, Action reply)
		{
			await scene.GetComponent<LocationComponent>().Add(request.Key, request.InstanceId);
			reply();
		}
	}
}