using System;
using System.Net;
using System.Threading.Tasks;

namespace DCET
{
	[ObjectSystem]
	public class BenchmarkComponentAwakeSystem : AwakeSystem<BenchmarkComponent, string>
	{
		public override void Awake(BenchmarkComponent self, string a)
		{
			self.Awake(a);
		}
	}

	public class BenchmarkComponent : Entity
	{
		public int k;
		public long time1 = TimeHelper.CurrentLocalMilliseconds();

		public void Awake(string address)
		{
			try
			{
				IPEndPoint ipEndPoint = NetworkHelper.ToIPEndPoint(address);
				NetOuterComponent networkComponent = Game.Scene.GetComponent<NetOuterComponent>();
				for (int i = 0; i < 2000; i++)
				{
					TestAsync(networkComponent, ipEndPoint, i);
				}
			}
			catch (Exception e)
			{
				Log.Exception(e);
			}
		}

		public async void TestAsync(NetOuterComponent networkComponent, IPEndPoint ipEndPoint, int j)
		{
			try
			{
				using (Session session = networkComponent.Create(ipEndPoint))
				{
					int i = 0;
					while (i < 100000000)
					{
						++i;
						await Send(session, j);
					}
				}
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

				if (k % 100000 != 0)
				{
					return;
				}

				long time2 = TimeHelper.CurrentLocalMilliseconds();
				long time = time2 - time1;
				time1 = time2;
				Log.Info($"Benchmark k: {k} 每10W次耗时: {time} ms {session.Network.Count}");
			}
			catch (Exception e)
			{
				Log.Exception(e);
			}
		}
	}
}