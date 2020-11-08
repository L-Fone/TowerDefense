using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
	public static class ConfigHelper
	{
		public static string GetText(string key)
		{
			try
			{
				string path = $"Assets/Download/Config/{key}.json";
				//GameObject config = (await Game.Scene.GetComponent<Resource>().ResourceLoaderComponent.LoadMainAsset(AssetCategory.Config, "Assets/Download/Config/Config.prefab")).Target as GameObject;
				TextAsset textAsset = ResourceHelper.LoadAsset<TextAsset>(path);
				//string configStr = config.Get<TextAsset>(key).text;
				return textAsset.text;
			}
			catch (Exception e)
			{
				throw new Exception($"load config file fail, key: {key}", e);
			}
		}
		
		public static string GetGlobal()
		{
			try
			{
				string configStr = Resources.Load<TextAsset>("Config/GlobalProto").text;
				return configStr;
			}
			catch (Exception e)
			{
				throw new Exception($"load global config file fail", e);
			}
		}
        public static T Get<T>(long id) where T : class
        {
            var iconfig = ConfigComponent.instance.Get(typeof(T), id);
            if (iconfig == null)
            {
				Log.Error($"iconfig == null where type is {typeof(T)} id = {id}");
				return null;
            }
            return iconfig as T;
        }
        public static IEnumerable<T> GetAll<T>() where T : class
        {
            var iconfig = ConfigComponent.instance.GetAll<T>(typeof(T));
            return iconfig;
        }


        public static T ToObject<T>(string str)
		{
			return MongoHelper.FromJson<T>(str);
		}
	}
}