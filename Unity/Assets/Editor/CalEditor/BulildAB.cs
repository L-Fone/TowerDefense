using ET;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.U2D;
using UnityEditor.U2D;
using Object = UnityEngine.Object;
using Sirenix.Utilities;
using libx;

namespace ETEditor
{
    [CreateAssetMenu]
    public class BulildAB : ScriptableObject
    {
        private void OnEnable()
        {
            if (File.Exists(globalHotfixConfigPath))
            {
                this.globalHotfixProto = MongoHelper.FromJson<GlobalHotfixProto>(File.ReadAllText(globalHotfixConfigPath));
            }
            else
            {
                this.globalHotfixProto = new GlobalHotfixProto();
            }
            if (File.Exists(globalConfigPath))
            {
                this.globalProto = MongoHelper.FromJson<GlobalProto>(File.ReadAllText(globalConfigPath));
            }
            else
            {
                this.globalProto = new GlobalProto();
            }

        }
        [HorizontalGroup("Common", LabelWidth = 80)]
        [VerticalGroup("Common/Left")]
        [LabelText("版本号")]
        public string ClientVersion = "1.0";

        [VerticalGroup("Common/Right")]
        [Button(ButtonSizes.Medium)]
        [LabelText("升级版本")]
        public void UpdateResourceVersion()
        {
            string version = ClientVersion;
            string[] arr = version.Split('.');
            int.TryParse(arr[1], out int shortVersion);
            version = string.Format("{0}.{1}", arr[0], ++shortVersion);
            ClientVersion = version;

            globalHotfixProto.ClientVersion = ClientVersion;
            globalProto.ClientVersion = ClientVersion;
            File.WriteAllText(globalHotfixConfigPath, MongoHelper.ToJson(this.globalHotfixProto));
            File.WriteAllText(globalConfigPath, MongoHelper.ToJson(this.globalProto));
            AssetDatabase.Refresh();
        }

        [HorizontalGroup("Common1", LabelWidth = 70)]
        [VerticalGroup("Common1/Left")]
        [LabelText("编辑器模式")]
        [ReadOnly]
        public bool IsEditorMode;

        [VerticalGroup("Common1/Right")]
        [Button(ButtonSizes.Medium)]
        [LabelText("切换模式")]
        public void SwitchEditorMode()
        {
            this.globalProto.isEditorMode = IsEditorMode = !IsEditorMode;
            File.WriteAllText(globalConfigPath, MongoHelper.ToJson(this.globalProto));

            EditorBuildSettingsScene[] arrScene = EditorBuildSettings.scenes;
            for (int i = 0; i < arrScene.Length; i++)
            {
                if (arrScene[i].path.IndexOf("download", System.StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    arrScene[i].enabled = IsEditorMode;
                }
            }
            EditorBuildSettings.scenes = arrScene;

            AssetDatabase.Refresh();
            EditorSceneManager.SaveOpenScenes();
        }
        [HorizontalGroup("Common2", LabelWidth = 70)]
        [VerticalGroup("Common2/Left")]
        [LabelText("本地服务器")]
        [ReadOnly]
        public bool IsLocal;
        private bool IsRemote;

        [VerticalGroup("Common2/Right")]
        [Button(ButtonSizes.Medium)]
        [LabelText("切换模式")]
        public void SwitchLocalMode()
        {
            this.globalProto.isLocal = IsLocal = !IsLocal;
            IsRemote = !IsLocal;
            if (IsLocal)
            {
                this.globalProto.LocalAssetBundleServerUrl = LocalResPath;
                this.globalProto.LocalAddress = LocalServerPath;
            }
            else
            {
                this.globalProto.AssetBundleServerUrl = ResPath;
                this.globalProto.Address = ServerPath;
            }
            File.WriteAllText(globalConfigPath, MongoHelper.ToJson(this.globalProto));
            AssetDatabase.Refresh();
            EditorSceneManager.SaveOpenScenes();
        }

        const string globalConfigPath = @"./Assets/Resources/Config/GlobalProto.txt";
        const string globalHotfixConfigPath = @"./Assets/Download/Config/GlobalConfig.txt";

        private GlobalProto globalProto;
        private GlobalHotfixProto globalHotfixProto;
        [ShowIf("IsLocal")]
        [HorizontalGroup("Common3", LabelWidth = 80)]
        [VerticalGroup("Common3/Left")]
        [LabelText("web url")]
        public string LocalResPath = "http://127.0.0.1:7979/";
        [ShowIf("IsLocal")]
        [HorizontalGroup("Common3", LabelWidth = 80)]
        [VerticalGroup("Common3/Left")]
        [LabelText("游戏服务器")]
        public string LocalServerPath = "127.0.0.1:7955";
        [ShowIf("IsRemote")]
        [HorizontalGroup("Common3", LabelWidth = 80)]
        [VerticalGroup("Common3/Left")]
        [LabelText("远程web url")]
        public string ResPath = "http://127.0.0.1:7979/";
        [ShowIf("IsRemote")]
        [HorizontalGroup("Common3", LabelWidth = 80)]
        [VerticalGroup("Common3/Left")]
        [LabelText("远程游戏服务器")]
        public string ServerPath = "127.0.0.1:7955";

        private string _key;
        [LabelText("加密"), PropertyOrder(2)]
        public string key => _key;

        private string _keyIV;
        [LabelText("加密IV"), PropertyOrder(2)]
        public string keyIV => _keyIV;

        [Button("刷新密码"), PropertyOrder(2)]
        private void RefreshPsd()
        {
            var scene = EditorSceneManager.OpenScene("Assets/Res/Common/Init.unity");
            foreach (var go in scene.GetRootGameObjects())
            {
                if (go.name.Equals("Global"))
                {
                    _key = go.GetComponent<Init>().key;
                    _keyIV = go.GetComponent<Init>().keyIV;
                    break;
                }
            }
            if (_key.IsNullOrWhitespace())
                throw new Exception("未找到密码");
        }


        [Button(ButtonSizes.Gigantic), ResponsiveButtonGroup("DefaultButtonSize"), PropertyOrder(1)]
        public void Build()
        {
            if (IsEditorMode)
            {
                EditorUtility.DisplayDialog("错误", "编辑器模式不能打包", "确定");
                return;
            }
            try
            {
                RefreshPsd();
                if (BuildSecurity())
                {
                    libx.MenuItems.BuildAssetBundles();
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return;
            }
        }

        //[Button("生成Lua"), PropertyOrder(1)]
        // private void GenerateLua()
        //{
        //    GenHotfix.CStoLua();
        //}

        [FolderPath]
        public string atlasRootPath;
        [ReadOnly]
        public List<string> atlasPaths;
        [PropertySpace(20)]
        [Button(ButtonSizes.Small, Name = "删除图集文件夹"), PropertyOrder(3)]
        public void DeleteSpriteAltasPaths()
        {
            if (!EditorUtility.DisplayDialog("删除图集文件夹", "是否删除", "Ok", "我不"))
            {
                return;
            }
            atlasPaths.Clear();
            EditorUtility.SetDirty(this);
        }
        private List<string> atlasReadyPaths = new List<string>();
        [Button(ButtonSizes.Small, Name = "创建图集"), PropertyOrder(4)]
        public void CreateAllSpriteAltas()
        {
            try
            {
                if (!EditorUtility.DisplayDialog("创建图集", "是否创建图集", "Ok", "我不"))
                {
                    return;
                }
                string delPath = atlasRootPath;
                var info = Utility.FileOpation.CreateDirectory(delPath);
                atlasReadyPaths.Clear();
                foreach (var item in info.GetFiles(".spriteatlas", SearchOption.AllDirectories))
                {
                    atlasReadyPaths.Add(item.Name.Replace(".spriteatlas", string.Empty));
                }
                int count = 0; int max = atlasPaths.Count;
                foreach (var item in atlasPaths)
                {
                    count++;
                    if (EditorUtility.DisplayCancelableProgressBar(string.Format("创建图集{0}/{1}", count, max), item, count / (float)max))
                        break;
                    CreateSpriteAltas(atlasRootPath, item);
                }
                AssetDatabase.Refresh();
                Log.Info($"创建成功");
                EditorUtility.ClearProgressBar();
            }
            catch (Exception e)
            {
                Log.Error(e);
                EditorUtility.ClearProgressBar();
            }

        }
        private void CreateSpriteAltas(string atlasPath, string textureDirPath)
        {
            int startIndex = textureDirPath.IndexOf("Texture");
            string altasName = textureDirPath.Substring(startIndex, textureDirPath.Length - startIndex);
            altasName = altasName.Replace("\\", "+");
            altasName = altasName.Replace("Texture+", "");

            if (atlasReadyPaths.Contains(altasName))
            {
                return;
            }

            SpriteAtlas atlas = new SpriteAtlas();
            // 设置参数 可根据项目具体情况进行设置
            SpriteAtlasPackingSettings packSetting = new SpriteAtlasPackingSettings()
            {
                blockOffset = 1,
                enableRotation = false,
                enableTightPacking = false,
                padding = 2,
            };
            atlas.SetPackingSettings(packSetting);

            SpriteAtlasTextureSettings textureSetting = new SpriteAtlasTextureSettings()
            {
                readable = false,
                generateMipMaps = false,
                sRGB = true,
                filterMode = FilterMode.Bilinear,
            };
            atlas.SetTextureSettings(textureSetting);

            TextureImporterPlatformSettings platformSetting = new TextureImporterPlatformSettings()
            {
                maxTextureSize = 2048,
                format = TextureImporterFormat.Automatic,
                crunchedCompression = true,
                textureCompression = TextureImporterCompression.Compressed,
                compressionQuality = 50,
            };
            atlas.SetPlatformSettings(platformSetting);

            // 1、添加文件
            //DirectoryInfo dir = new DirectoryInfo(textureDirPath);
            startIndex = textureDirPath.IndexOf("Assets");
            string path = textureDirPath.Replace(textureDirPath.Substring(0, startIndex), string.Empty);
            //// 这里我使用的是png图片，已经生成Sprite精灵了
            //FileInfo[] files = dir.GetFiles("*.png");
            //foreach (FileInfo file in files)
            //{
            //    atlas.Add(new[] { AssetDatabase.LoadAssetAtPath<Sprite>($"{path}/{file.Name}") });
            //}

            // 2、添加文件夹
            Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
            atlas.Add(new[] { obj });

            AssetDatabase.CreateAsset(atlas, atlasPath + "/" + altasName + ".spriteatlas");
            AssetDatabase.SaveAssets();

        }
        private bool BuildSecurity()
        {
            try
            {
                string dllPath = "Assets/Download/Config/Hotfix.dll.bytes";
                string pdbPath = "Assets/Download/Config/Hotfix.pdb.bytes";
                string dllViewPath = "Assets/Download/Config/HotfixView.dll.bytes";
                string pdbViewPath = "Assets/Download/Config/HotfixView.pdb.bytes";
                var dll = File.ReadAllBytes(dllPath);
                var pdb = File.ReadAllBytes(pdbPath);
                var dllView = File.ReadAllBytes(dllViewPath);
                var pdbView = File.ReadAllBytes(pdbViewPath);
                dll = Utility.Encryption.AesCBCEncrypt(dll, key, keyIV);
                pdb = Utility.Encryption.AesCBCEncrypt(pdb, key, keyIV);
                dllView = Utility.Encryption.AesCBCEncrypt(dllView, key, keyIV);
                pdbView = Utility.Encryption.AesCBCEncrypt(pdbView, key, keyIV);
                ET.Utility.FileOpation.Delete(dllPath);
                ET.Utility.FileOpation.Delete(pdbPath);
                ET.Utility.FileOpation.Delete(dllViewPath);
                ET.Utility.FileOpation.Delete(pdbViewPath);
                using (var fs = new FileStream(dllPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(dll, 0, dll.Length);
                }
                using (var fs = new FileStream(pdbPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(pdb, 0, pdb.Length);
                }
                using (var fs = new FileStream(dllViewPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(dllView, 0, dllView.Length);
                }
                using (var fs = new FileStream(pdbViewPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(pdbView, 0, pdbView.Length);
                }
                Log.Info($"加密成功");
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }

        }
    }
}