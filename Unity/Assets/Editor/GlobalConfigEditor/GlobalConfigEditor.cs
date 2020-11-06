using System.IO;
using ET;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace ETEditor
{
    public class GlobalProtoEditor : EditorWindow
    {
        const string path = @"./Assets/Res/Config/GlobalProto.txt";

        private GlobalProto globalProto;

        [MenuItem("Tools/全局配置")]
        public static void ShowWindow()
        {
            GetWindow<GlobalProtoEditor>();
        }

        public void Awake()
        {
            if (File.Exists(path))
            {
                this.globalProto = MongoHelper.FromJson<GlobalProto>(File.ReadAllText(path));
            }
            else
            {
                this.globalProto = new GlobalProto();
            }
        }
        private bool isLocal;

        public void OnGUI()
        {
            globalProto.isLocal = EditorGUILayout.Toggle("本地IP：", globalProto.isLocal);
            if (globalProto.isLocal)
            {
                this.globalProto.LocalAssetBundleServerUrl = EditorGUILayout.TextField("本地资源路径:", this.globalProto.LocalAssetBundleServerUrl);
                this.globalProto.LocalAddress = EditorGUILayout.TextField("本地服务器地址:", this.globalProto.LocalAddress);
            }
            else
            {
                this.globalProto.AssetBundleServerUrl = EditorGUILayout.TextField("资源路径:", this.globalProto.AssetBundleServerUrl);
                this.globalProto.Address = EditorGUILayout.TextField("服务器地址:", this.globalProto.Address);
            }
            if (isLocal != globalProto.isLocal)
            {
                isLocal = globalProto.isLocal;
                File.WriteAllText(path, MongoHelper.ToJson(this.globalProto));
                AssetDatabase.Refresh();
            }

        }
    }
}
