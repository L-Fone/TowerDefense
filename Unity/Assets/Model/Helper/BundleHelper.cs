using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
	public static class BundleHelper
	{
		public static async ETTask DownloadBundle()
		{
			//await Game.Scene.GetComponent<Resource>().InitStreamingAssestsBundleInfo();

			Log.Info("检查资源更新完毕");
			await ETTask.CompletedTask;
		}

	}
}
