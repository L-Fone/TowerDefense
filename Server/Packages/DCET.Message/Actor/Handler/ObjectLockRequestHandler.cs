using System;
using System.Threading.Tasks;

namespace DCET
{
	[ActorMessageHandler]
	public class ObjectLockRequestHandler : AMActorRpcHandler<Scene, ObjectLockRequest, ObjectLockResponse>
	{
		protected override async Task Run(Scene scene, ObjectLockRequest request, ObjectLockResponse response, Action reply)
		{
			scene.GetComponent<LocationComponent>().Lock(request.Key, request.InstanceId, request.Time);
			reply();
			await Task.CompletedTask;
		}
	}
}