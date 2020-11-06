using System;
using System.Threading.Tasks;

namespace DCET
{
	[ObjectSystem]
	public class WebSocketBenchmarkComponentSystem : AwakeSystem<WebSocketBenchmarkComponent, string>
	{
		public override void Awake(WebSocketBenchmarkComponent self, string address)
		{
			self.Awake(address);
		}
	}

	public class WebSocketBenchmarkComponent : Entity
	{
		public int k;
		public long time1 = TimeHelper.CurrentLocalMilliseconds();

		public void Awake(string address)
		{
			try
			{
				NetOuterComponent networkComponent = Game.Scene.GetComponent<NetOuterComponent>();
				for (int i = 0; i < 1000; i++)
				{
					TestAsync(networkComponent, i, address);
				}
			}
			catch (Exception e)
			{
				Log.Exception(e);
			}
		}

		public async void TestAsync(NetOuterComponent networkComponent, int j, string address)
		{
			try
			{
				using (Session session = networkComponent.Create(address))
				{
					int i = 0;
					while (i < 100000000)
					{
						++i;
						await Send(session, j);
					}
				}
			}
			catch (RpcException e)
			{
				Log.Exception(e);
			}
			catch (Exception e)
			{
				Log.Exception(e);
			}
		}

		public async Task Send(Session session, int j)
		{
			try
			{
				await session.Call(new C2R_Ping());
				++k;

				if (k % 10000 != 0)
				{
					return;
				}

				long time2 = TimeHelper.CurrentLocalMilliseconds();
				long time = time2 - time1;
				time1 = time2;
				Log.Info($"Benchmark k: {k} 每1W次耗时: {time} ms {session.Network.Count}");
			}
			catch (Exception e)
			{
				Log.Exception(e);
			}
		}
	}
}