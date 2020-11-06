using System;
using System.Threading.Tasks;

namespace DCET
{
	[ActorMessageHandler]
	public class M2M_TrasferUnitRequestHandler : AMActorRpcHandler<Scene, M2M_TrasferUnitRequest, M2M_TrasferUnitResponse>
	{
		protected override async Task Run(Scene scene, M2M_TrasferUnitRequest request, M2M_TrasferUnitResponse response, Action reply)
		{
			if(request.Unit != null)
			{
				Unit unit = EntityFactory.CreateWithId<Unit, UnitType>(scene, request.Unit.UnitId, request.Unit.UnitType);
				unit.Position = new UnityEngine.Vector3(request.Unit.X, request.Unit.Y, request.Unit.Z);
				// 将unit加入事件系统
				Log.Debug(DCETRuntime.MongoHelper.ToJson(request.Unit));
				// 这里不需要注册location，因为unlock会更新位置
				unit.AddComponent<MailBoxComponent>();
				scene.GetComponent<UnitComponent>().Add(unit);
				response.InstanceId = unit.InstanceId;
				reply();
				await Task.CompletedTask;
			}
		}
	}
}