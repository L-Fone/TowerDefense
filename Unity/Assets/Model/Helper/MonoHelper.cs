using System;
using System.Reflection;
using UnityEngine;

namespace ET
{
    public static class MonoHelper
    {
        public const string Hotfix = "Unity.Hotfix";
        public const string HotfixView = "Unity.HotfixView";

        public static string HotfixPdb = $"{Hotfix}.pdb";
        public static string HotfixViewPdb = $"{HotfixView}.pdb";

        public static string HotfixDllAssetBytes = $"Assets/Res/Code/{Hotfix}.dll.bytes";
        public static string HotfixViewDllAssetBytes = $"Assets/Res/Code/{HotfixView}.dll.bytes";

        public static async ETTask LoadHotfixAssembly()
        {
            try
            {
                byte[] assBytes = (await ResourceHelper.LoadAssetAsync<TextAsset>(PathHelper.HotfixDll)).bytes;
                byte[] pdbBytes = (await ResourceHelper.LoadAssetAsync<TextAsset>(PathHelper.HotfixPdb)).bytes;
                byte[] assViewBytes = (await ResourceHelper.LoadAssetAsync<TextAsset>(PathHelper.HotfixViewDll)).bytes;
                byte[] pdbViewBytes = (await ResourceHelper.LoadAssetAsync<TextAsset>(PathHelper.HotfixViewPdb)).bytes;
                if (!Define.IsEditorMode)
                {
                    var key = GameKeyComponent.Instance.key;
                    var keyIV = GameKeyComponent.Instance.keyIV;
                    assBytes = Utility.Encryption.AesCBCDecrypt(assBytes, key, keyIV);
                    pdbBytes = Utility.Encryption.AesCBCDecrypt(pdbBytes, key, keyIV);
                    assViewBytes = Utility.Encryption.AesCBCDecrypt(assViewBytes, key, keyIV);
                    pdbViewBytes = Utility.Encryption.AesCBCDecrypt(pdbViewBytes, key, keyIV);
                }
                var hotfix = Assembly.Load(assBytes, pdbBytes);

                Type hotfixInit = hotfix.GetType("ETHotfix.Init");
                var start = new MonoStaticMethod(hotfixInit, "Start");

                var hotfixView = Assembly.Load(assViewBytes, pdbViewBytes);


                start.Run();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        //public static async ETVoid StartHotfix()
        //{
        //    Log.Debug($"当前使用的是Mono模式");


        //    Assembly.Load(GameObjectHelper.GetTextAsset(code, CoreDll).bytes, GameObjectHelper.GetTextAsset(code, CorePdb).bytes);
        //    Assembly.Load(GameObjectHelper.GetTextAsset(code, MessageDll).bytes, GameObjectHelper.GetTextAsset(code, MessagePdb).bytes);
        //    Assembly.Load(GameObjectHelper.GetTextAsset(code, ConfigDll).bytes, GameObjectHelper.GetTextAsset(code, ConfigPdb).bytes);
        //    Assembly.Load(GameObjectHelper.GetTextAsset(code, BehaviorTreeDll).bytes, GameObjectHelper.GetTextAsset(code, BehaviorTreePdb).bytes);
        //    Assembly.Load(GameObjectHelper.GetTextAsset(code, FairyGUIDll).bytes, GameObjectHelper.GetTextAsset(code, FairyGUIPdb).bytes);

        //    var mainAssembly = Assembly.Load(GameObjectHelper.GetTextAsset(code, HotfixDll).bytes, GameObjectHelper.GetTextAsset(code, HotfixPdb).bytes);
        //    var start = new MonoStaticMethod(mainAssembly.GetType("DCET.Init"), "Start");
        //    start.Run();

        //}
    }
}
