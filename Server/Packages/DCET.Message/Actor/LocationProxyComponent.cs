using System;
using System.Threading.Tasks;

namespace DCET
{
	[ObjectSystem]
	public class LocationProxyComponentSystem : AwakeSystem<LocationProxyComponent>
	{
		public override void Awake(LocationProxyComponent self)
		{
			self.Awake();
		}
	}

	public static class LocationProxyComponentEx
	{
		public static async Task AddLocation(this Entity self)
		{
			await Game.Scene.GetComponent<LocationProxyComponent>().Add(self.Id, self.InstanceId);
		}

		public static async Task RemoveLocation(this Entity self)
		{
			await Game.Scene.GetComponent<LocationProxyComponent>().Remove(self.Id);
		}
	}

	public class LocationProxyComponent : Entity
	{
		public static LocationProxyComponent Instance;

		public void Awake()
		{
			Instance = this;
		}

		public async Task Add(long key, long instanceId)
		{
			await ActorHelper.CallActor(
				StartConfigComponent.Instance.GetInstanceId(SceneType.Location),
				new ObjectAddRequest() { Key = key, InstanceId = instanceId });
		}

		public async Task Lock(long key, long instanceId, int time = 1000)
		{
			await ActorHelper.CallActor(
				StartConfigComponent.Instance.GetInstanceId(SceneType.Location),
				new ObjectLockRequest() { Key = key, InstanceId = instanceId, Time = time });
		}

		public async Task UnLock(long key, long oldInstanceId, long instanceId)
		{
			await ActorHelper.CallActor(
				StartConfigComponent.Instance.GetInstanceId(SceneType.Location),
				new ObjectUnLockRequest() { Key = key, OldInstanceId = oldInstanceId, InstanceId = instanceId });
		}

		public async Task Remove(long key)
		{
			await ActorHelper.CallActor(
				StartConfigComponent.Instance.GetInstanceId(SceneType.Location),
				new ObjectRemoveRequest() { Key = key });
		}

		public async Task<long> Get(long key)
		{
			if (key == 0)
			{
				throw new Exception($"get location key 0");
			}
			ObjectGetResponse response =
					(ObjectGetResponse)await ActorHelper.CallActor(
						StartConfigComponent.Instance.GetInstanceId(SceneType.Location),
						new ObjectGetRequest() { Key = key });
			return response.InstanceId;
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			
			base.Dispose();

			Instance = null;
		}
	}
}