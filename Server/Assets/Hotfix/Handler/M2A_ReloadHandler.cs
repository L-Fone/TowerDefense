using System;
using System.Threading.Tasks;

namespace DCET
{
	[MessageHandler]
	public class M2A_ReloadHandler : AMRpcHandler<M2A_Reload, A2M_Reload>
	{
		protected override async Task Run(Session session, M2A_Reload request, A2M_Reload response, Action reply)
		{
			Game.EventSystem.Init();
			reply();
			await Task.CompletedTask;
		}
	}
}