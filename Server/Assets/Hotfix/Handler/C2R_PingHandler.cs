using System;
using System.Threading.Tasks;

namespace DCET
{
	[MessageHandler]
	public class C2R_PingHandler : AMRpcHandler<C2R_Ping, R2C_Ping>
	{
		protected override async Task Run(Session session, C2R_Ping request, R2C_Ping response, Action reply)
		{
			reply();
			await Task.CompletedTask;
		}
	}
}