using System;
using System.Threading.Tasks;

namespace DCET
{
	[ActorMessageHandler]
	public class ObjectUnLockRequestHandler : AMActorRpcHandler<Scene, ObjectUnLockRequest, ObjectUnLockResponse>
	{
		protected override async Task Run(Scene scene, ObjectUnLockRequest request, ObjectUnLockResponse response, Action reply)
		{
			scene.GetComponent<LocationComponent>().UnLock(request.Key, request.OldInstanceId, request.InstanceId);
			reply();
			await Task.CompletedTask;
		}
	}
}