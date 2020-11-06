using ET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ET
{
    public sealed class Hotfix : Object
    {
        public Scene zoneScene;
        public Assembly hotfix;
        public Assembly hotfixView;

        public const string HotfixName = "Unity.Hotfix";
        public const string HotfixViewName = "Unity.HotfixView";

        public static string HotfixPdb = $"{HotfixName}.pdb";
        public static string HotfixViewPdb = $"{HotfixViewName}.pdb";

        public static string HotfixDllAssetBytes = $"Assets/Res/Code/{HotfixName}.dll.bytes";
        public static string HotfixViewDllAssetBytes = $"Assets/Res/Code/{HotfixViewName}.dll.bytes";

        public async ETTask LoadHotfixAssembly(Scene zoneScene)
        {
            try
            {
                this.zoneScene = zoneScene;
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
                hotfix = Assembly.Load(assBytes, pdbBytes);

                Type hotfixInit = hotfix.GetType("ET.Init");
                var start = new MonoStaticMethod(hotfixInit, "Start");

                hotfixView = Assembly.Load(assViewBytes, pdbViewBytes);


                start.Run();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        //public void LoadHotfixAssembly()
        //{
        //    try
        //    {
        //        HotfixHelper.StartHotfix();
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e);
        //    }
        //}
    }
}