using libx;
using System;
using System.Collections.Generic;

namespace ET
{
    public class ResourceHelper
    {
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
            var assetRequest = Assets.LoadAssetAsync(assetPath, typeof(T));
            if (assetRequest.isDone)
            {
                return assetRequest.asset as T;
            }
            ETTaskCompletionSource<T> tcs = new ETTaskCompletionSource<T>();
            assetRequest.completed = (AssetRequest request) =>
            {
                var asset = request.asset;
                tcs.SetResult(asset as T);
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