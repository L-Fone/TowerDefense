using libx;
using System;
using System.Collections.Generic;

namespace ET
{
    public class ResourceHelper
    {
        private static UnOrderMultiMap<string, AssetRequest> requestDic = new UnOrderMultiMap<string, AssetRequest>();
        public static T LoadAsset<T>(string assetPath)
            where T : UnityEngine.Object
        {
            return Assets.LoadAsset(assetPath, typeof(T)).asset as T;
        }
        public static UnityEngine.Object LoadAsset(string assetPath, Type type)
        {
            return Assets.LoadAsset(assetPath, type).asset;
        }
        public static async ETTask<T> LoadAssetAsync<T>(string assetPath)
          where T : UnityEngine.Object
        {
            var list = requestDic[assetPath];
            if (list != null && list.Count > 0)
            {
                if (list[0]?.asset is T t)
                    return t;
            }

            ETTaskCompletionSource<T> tcs = new ETTaskCompletionSource<T>();
            var assetRequest = Assets.LoadAssetAsync(assetPath, typeof(T));
            requestDic.Add(assetPath, assetRequest);
            assetRequest.completed = (AssetRequest request) =>
            {
                tcs.SetResult(request.asset as T);
            };
            return await tcs.Task;
        }
        private const string SceneExt = ".unity";
        public static ETTask LoadSceneAsync(string sceneName, bool isAddtion)
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            if (!sceneName.Contains(SceneExt))
                sceneName += SceneExt;
            SceneAssetRequest sceneRequest = Assets.LoadSceneAsync(sceneName, isAddtion);
            sceneRequest.completed += (AssetRequest request) =>
            {
                tcs.SetResult();
            };
            return tcs.Task;
        }


    }
}