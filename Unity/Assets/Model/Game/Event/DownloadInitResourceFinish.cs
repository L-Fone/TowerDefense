
using ET.EventType;
using libx;
using UnityEngine;

namespace ET
{
	public class DownloadInitResourceFinishEvent: AEvent<DownloadInitResourceFinish>
	{
		public override async ETTask Run(DownloadInitResourceFinish args)
		{
			Game.Scene.RemoveComponent<Updater>();
			Game.Scene.RemoveComponent<NetworkMonitor>();
			Game.Scene.RemoveComponent<Downloader>();
			Game.Hotfix.LoadHotfixAssembly(args.zoneScene);
		}
	}
}