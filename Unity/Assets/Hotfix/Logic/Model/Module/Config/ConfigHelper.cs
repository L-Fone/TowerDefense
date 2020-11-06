//using System;
//using ET;
//using UnityEngine;

//namespace ET
//{
//	public static class ConfigHelper
//	{
//		public static async ETTask<string> GetText(string key)
//		{
//			try
//			{
//				GameObject config =await ResourceHelper.LoadAssetAsync<GameObject>(PathHelper.Config);
//				string configStr = config.Get<TextAsset>(key).text;
//				return configStr;
//			}
//			catch (Exception e)
//			{
//				throw new Exception($"load config file fail, key: {key}", e);
//			}
//		}

//		public static T ToObject<T>(string str)
//		{
//			return MongoHelper.FromJson<T>(str);
//		}
//	}
//}