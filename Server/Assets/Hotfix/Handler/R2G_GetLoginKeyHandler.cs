using System;
using System.Threading.Tasks;

namespace DCET
{
	[ActorMessageHandler]
	public class R2G_GetLoginKeyHandler : AMActorRpcHandler<Scene, R2G_GetLoginKey, G2R_GetLoginKey>
	{
		protected override async Task Run(Scene scene, R2G_GetLoginKey request, G2R_GetLoginKey response, Action reply)
		{
			var key = RandomHelper.RandInt64();
			scene.GetComponent<GateSessionKeyComponent>().Add(key, request.Account);
			response.Key = key;
			response.GateId = scene.Id;
			reply();
			await Task.CompletedTask;
		}
	}
}