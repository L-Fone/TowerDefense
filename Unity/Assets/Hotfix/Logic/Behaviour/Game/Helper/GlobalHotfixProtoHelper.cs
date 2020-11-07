using ET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ET
{
    public class GlobalHotfixProtoHelper
    {
        private static string clientVer;
        public static async ETTask<string> GetClintVersion()
        {
            if (clientVer == null)
            {
                var config = await ResourceHelper.LoadAssetAsync<TextAsset>("Assets/Download/Config/GlobalConfig.txt");
                var globalProto = MongoHelper.FromJson<GlobalHotfixProto>(config.text);
                clientVer = globalProto.ClientVersion;
            }
            return clientVer;
        }
    }
}
