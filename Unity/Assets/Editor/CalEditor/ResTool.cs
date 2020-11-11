 using ET;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Sprites;
using UnityEngine;
using Component = UnityEngine.Component;

namespace ETEditor
{ 
    [CreateAssetMenu]
    [InitializeOnLoad]
    public class ResTool : SerializedScriptableObject
    {
        public static ResTool Instance;

        private void OnEnable()
        {
            Instance = this;
        }

        [FolderPath]
        [LabelText("生成出的EffectAnimation的路径")]
        public string animationPath = "Assets/Download/Animators/RoleEffect";
        [FolderPath]
        [LabelText("生成出的RoleAnimation的路径")]
        public string animationRolePath = "Assets/Download/Animators";

        public Texture2D texture2D;

        public AinmationKey aimpationKey;
        public Ainmation8DirectionKey ainmation8DirectionKey;

        [OnValueChanged("FrameChanged")]
        public int firstFrame, frameCount;

        void FrameChanged()
        {
            endFrame = firstFrame + frameCount - 1;
        }

        [ReadOnly]
        public int endFrame;
        [Button("生成动画文件")]
        private void AutoFillSprite()
        {
            string path = $"Assets/Download/Config/SpriteInfoConig.json";
            long id = AnimatorIdGenerater.GetId(texture2D.name, aimpationKey, ainmation8DirectionKey);
            SpriteInfoConig spriteInfoConig;
            Dictionary<long, SpriteInfoConig> configDic;
            if (!File.Exists(path))
            {
                spriteInfoConig = new SpriteInfoConig { Id = id, list = new List<SpriteInfo>() };
                configDic = new Dictionary<long, SpriteInfoConig>(); 
                configDic.Add(id,spriteInfoConig);
            }
            else
            {
                string configStr = File.ReadAllText(path);
                configDic = MongoHelper.FromJson<Dictionary<long, SpriteInfoConig>>(configStr);
                if (!configDic.TryGetValue(id, out spriteInfoConig))
                {
                    spriteInfoConig = new SpriteInfoConig { Id = id, list = new List<SpriteInfo>() };
                    configDic[id] = spriteInfoConig;
                }
                else
                {
                    spriteInfoConig.list.Clear();
                }
            }
            string assetPath = AssetDatabase.GetAssetPath(texture2D);
            TextureImporter texImport = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            for (int i = firstFrame; i <= endFrame; i++)
            {
                var item = texImport.spritesheet[i];
                SpriteInfo spriteInfo = new SpriteInfo
                {
                    name = item.name,
                    pivot = new System.Numerics.Vector2(item.pivot.x, item.pivot.y),
                    x = item.rect.x,
                    y = item.rect.y,
                    width = item.rect.width,
                    height = item.rect.height,
                };
                spriteInfoConig.list.Add(spriteInfo);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("[\n");
            foreach (var kv in configDic)
            {
                sb.Append("[");
                var str = MongoHelper.ToJson(kv.Value);
                sb.Append($"{kv.Key},");
                sb.Append($"{str}");
                sb.Append("],\n");
            }
            sb.Append("]");
            Utility.FileOpation.Delete(path);
            var fs = Utility.FileOpation.Create(path);
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(sb.ToString());
            }
            AssetDatabase.Refresh();
        }


        [MenuItem("Assets/资源操作/临时修改", false, 97)]
        static void ModifyDir() => Instance.ModifyDirInternel();
        private void ModifyDirInternel()
        {
            try
            {
                string[] strs = Selection.assetGUIDs;
                string targetParentPath = Path.GetFullPath("Assets/Download/Texture/EffectSources/RoleEffect");
                foreach (var item in strs)
                {
                    string dirPath = AssetDatabase.GUIDToAssetPath(item);
                    foreach (var fielInfo in Utility.FileOpation.GetFiles(dirPath, "*.png", SearchOption.AllDirectories))
                    {
                        var pName = fielInfo.Name.Split('_')[0];
                        var ppName = pName.Substring(0, pName.Length - 1);
                        string path = $"{targetParentPath}/{ppName}/{pName}/";
                        string metaName = fielInfo.FullName + ".meta";
                        var meta = new FileInfo(metaName);
                        fielInfo.MoveTo(path + fielInfo.Name);
                        meta.MoveTo(path + fielInfo.Name + ".meta");
                    }
                }
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
        [MenuItem("Assets/资源操作/清空文件夹", false, 97)]
        static void ClearDir() => Instance.ClearDirInternel();
        private void ClearDirInternel()
        {
            string[] strs = Selection.assetGUIDs;
            //Transform OBJ = GameObject.Find("OBJ").transform;
            foreach (var item in strs)
            {
                string dirPath = AssetDatabase.GUIDToAssetPath(item);
                Utility.FileOpation.ClearDirectory(dirPath);
            }
            AssetDatabase.Refresh();
        }
        [MenuItem("Assets/资源操作/删除文件夹", false, 99)]
        static void DeleteDir() => Instance.DeleteDirInternel();
        private void DeleteDirInternel()
        {
            string[] strs = Selection.assetGUIDs;
            //Transform OBJ = GameObject.Find("OBJ").transform;
            foreach (var item in strs)
            {
                string dirPath = AssetDatabase.GUIDToAssetPath(item);
                Utility.FileOpation.DeleteDirectory(dirPath);
            }
            AssetDatabase.Refresh();
        }

        #region 特效
        [MenuItem("Assets/资源操作/特效/规范文件名A", false, 97)]
        private static void ModifyA() => Instance.ModifyAInternel();
        private void ModifyAInternel()
        {
            try
            {
                string[] strs = Selection.assetGUIDs;
                foreach (var item in strs)
                {
                    string dirPath = AssetDatabase.GUIDToAssetPath(item);
                    if (Directory.Exists(dirPath))
                    {
                        FileInfo[] infoArr = Utility.FileOpation.GetFiles(dirPath, "*.png", SearchOption.AllDirectories);
                        Execute(infoArr);
                    }
                    else
                    {

                        FileInfo fileInfo = new FileInfo(dirPath);
                        if (fileInfo.Extension != ".png")
                            continue;
                        Execute(new FileInfo[] { fileInfo });
                    }
                }
                AssetDatabase.Refresh();

                void Execute(FileInfo[] fileInfoArr)
                {
                    int i = 0;
                    foreach (var fileInfo in fileInfoArr)
                    {
                        string dirPath = fileInfo.DirectoryName;

                        string ext = "" + ++i;
                        if (i < 10)
                            ext = "0" + ext;

                        //!+命名，根据实际情况修改
                        DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
                        string name = directoryInfo.Parent.Parent.Name;
                        Regex reg = new Regex(@"[0-9]+", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        int index = int.Parse(reg.Match(directoryInfo.Parent.Name).Value);
                        name += index;
                        string metaName = fileInfo.FullName + ".meta";
                        FileInfo meta = new FileInfo(metaName);
                        name = dirPath + "/" + name + "_A_" + ext + ".png";
                        fileInfo.MoveTo(name);
                        meta.MoveTo(name + ".meta");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
        [MenuItem("Assets/资源操作/特效/规范文件名B", false, 97)]
        private static void ModifyB() => Instance.ModifyBInternel();
        private void ModifyBInternel()
        {
            try
            {
                string[] strs = Selection.assetGUIDs;
                foreach (var item in strs)
                {
                    string dirPath = AssetDatabase.GUIDToAssetPath(item);
                    if (Directory.Exists(dirPath))
                    {
                        FileInfo[] infoArr = Utility.FileOpation.GetFiles(dirPath, "*.png", SearchOption.AllDirectories);
                        Execute(infoArr);
                    }
                    else
                    {

                        FileInfo fileInfo = new FileInfo(dirPath);
                        if (fileInfo.Extension != ".png")
                            continue;
                        Execute(new FileInfo[] { fileInfo });
                    }
                }
                AssetDatabase.Refresh();

                void Execute(FileInfo[] fileInfoArr)
                {
                    int i = 0;
                    foreach (var fileInfo in fileInfoArr)
                    {
                        string dirPath = fileInfo.DirectoryName;

                        string ext = "" + ++i;
                        if (i < 10)
                            ext = "0" + ext;

                        //!+命名，根据实际情况修改
                        DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
                        string name = directoryInfo.Parent.Parent.Name;
                        Regex reg = new Regex(@"[0-9]+", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        int index = int.Parse(reg.Match(directoryInfo.Parent.Name).Value);
                        name += index;
                        string metaName = fileInfo.FullName + ".meta";
                        FileInfo meta = new FileInfo(metaName);
                        name = dirPath + "/" + name + "_B_" + ext + ".png";
                        fileInfo.MoveTo(name);
                        meta.MoveTo(name + ".meta");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }


        [MenuItem("Assets/资源操作/特效/生成序列帧动画", false, 98)]
        static void BuildAniamtion() => Instance.BuildAniamtionInterner();
        private void BuildAniamtionInterner()
        {
            try
            {
                string[] strs = Selection.assetGUIDs;
                List<FileInfo> animAList = new List<FileInfo>();
                List<FileInfo> animBList = new List<FileInfo>();
                foreach (var item in strs)
                {
                    string dirPath = AssetDatabase.GUIDToAssetPath(item);

                    DirectoryInfo raw = new DirectoryInfo(dirPath);
                    var dirArr = raw.GetDirectories();
                    int count = 0; int max = dirArr.Length;
                    foreach (DirectoryInfo dictorys in dirArr)
                    {
                        count++;
                        animAList.Clear();
                        animBList.Clear();
                        if (EditorUtility.DisplayCancelableProgressBar(string.Format("创建动画{0}/{1}", count, max), dictorys.Name, count / (float)max))
                            break;
                        FileInfo[] images = dictorys.GetFiles("*.png");
                        foreach (var info in images)
                        {
                            if (info.Name.Contains("_A_"))
                                animAList.Add(info);
                            else if (info.Name.Contains("_B_"))
                                animBList.Add(info);
                        }
                        if (animAList.Count > 0)
                        {
                            string pngName = animAList[0].Name;
                            string animName = pngName.Substring(0, pngName.LastIndexOf("_"));
                            animName = $"{animationPath}/{raw.Name}/{animName}.anim";
                            BuildAnimationClip(animAList, animName);
                        }
                        if (animBList.Count > 0)
                        {
                            string pngName = animBList[0].Name;
                            string animName = pngName.Substring(0, pngName.LastIndexOf("_"));
                            animName = $"{animationPath}/{raw.Name}/{animName}.anim";
                            BuildAnimationClip(animBList, animName);
                        }
                    }
                }
                AssetDatabase.Refresh();
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("生成动画", "生成动画已经完成", "确定");

            }
            catch (Exception e)
            {
                Log.Error(e);
                EditorUtility.ClearProgressBar();
            }
        }


        [MenuItem("Assets/资源操作/特效/自动给Effect赋值动画", false, 99)]
        static void AutoAddClipToEffect() => Instance.AutoAddClipToEffectInterner();
        private void AutoAddClipToEffectInterner()
        {
            try
            {
                string animClipDirPath = "Assets/Download/Animators";
                string[] strs = Selection.assetGUIDs;
                foreach (var item in strs)
                {
                    string dirPath = AssetDatabase.GUIDToAssetPath(item);
                    if (string.IsNullOrEmpty(dirPath) || !Directory.Exists(dirPath))
                        continue;
                    animClipDirPath += "/" + Utility.FileOpation.CreateDirectory(dirPath).Parent.Name;
                    var fileArr = Utility.FileOpation.GetFiles(dirPath, "*.prefab", SearchOption.AllDirectories);
                    int count = 0; int max = fileArr.Length;
                    foreach (var fileInfo in fileArr)
                    {
                        count++;
                        if (EditorUtility.DisplayCancelableProgressBar(string.Format("自动赋值动画{0}/{1}", count, max), fileInfo.Name, count / (float)max))
                            break;
                        string prefabName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
                        string clipPath = animClipDirPath + "/" + Directory.GetParent(fileInfo.FullName).Name + "/" + prefabName + ".anim";
                        AnimationClipInfo[] clipInfo = new AnimationClipInfo[0];
                        var animName = Utility.PathOption.GetRegularPath(clipPath);
                        var anim = AssetDatabase.LoadAssetAtPath<AnimationClip>(animName);
                        if (anim == null)
                        {
                            Log.Error($"anim == null where name = {fileInfo.Name}");
                            continue;
                        }
                        ArrayUtility.Add(ref clipInfo, new AnimationClipInfo
                        {
                            AnimationClip = anim
                        });

                        var prefanName = Utility.PathOption.GetRegularPath(fileInfo.FullName);
                        var go = AssetDatabase.LoadAssetAtPath<GameObject>(DataPathToAssetPath(prefanName));
                        MonoAnimancer animancers = go.GetComponent<MonoAnimancer>();
                        animancers.Animator = go.GetComponentInChildren<Animator>();
                        animancers.SetClips(clipInfo);
                        //!赋值Sprite
                        AddSpriteInterner(animancers);

                        EditorUtility.SetDirty(go);
                        AssetDatabase.SaveAssets();
                    }
                }
                AssetDatabase.Refresh();

                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("自动赋值动画", "自动赋值动画已经完成", "确定");
            }
            catch (Exception e)
            {
                Log.Error(e);
                EditorUtility.ClearProgressBar();
            }
        }

        #endregion

        #region 角色
        [MenuItem("Assets/资源操作/角色/批量规范文件名", false, 97)]
        static void MultiModify() => Instance.MultiModifyInternel();
        private void MultiModifyInternel()
        {
            try
            {
                string[] strs = Selection.assetGUIDs;
                foreach (var item in strs)
                {
                    string dirPath = AssetDatabase.GUIDToAssetPath(item);
                    FileInfo[] infoArr = Utility.FileOpation.GetFiles(dirPath, "*.png", SearchOption.AllDirectories);
                    Execute(infoArr, ".png");
                    infoArr = Utility.FileOpation.GetFiles(dirPath, "*.meta", SearchOption.AllDirectories);
                    Execute(infoArr, ".png.meta");
                    AssetDatabase.Refresh();
                    void Execute(FileInfo[] _infoArr, string extion)
                    {
                        SortAsFileName(_infoArr);
                        List<FileInfo> animHurtList = new List<FileInfo>();
                        List<FileInfo> animRunList = new List<FileInfo>();
                        List<FileInfo> animIdleList = new List<FileInfo>();
                        List<FileInfo> animAttackList = new List<FileInfo>();
                        string dirFilePath = _infoArr[0].DirectoryName;
                        string dirName = Directory.GetParent(_infoArr[0].FullName).Name;
                        dirFilePath += "/" + dirName;
                        foreach (var fileInfo in _infoArr)
                        {

                            if (fileInfo.FullName.Contains("hurt") ||
                              fileInfo.FullName.Contains("hit") ||
                              fileInfo.FullName.Contains("Hurt") ||
                              fileInfo.FullName.Contains("Hit")
                              )
                            {
                                animHurtList.Add(fileInfo);
                            }
                            else if (fileInfo.FullName.Contains("run") ||
                          fileInfo.FullName.Contains("Run")
                          )
                            {
                                animRunList.Add(fileInfo);
                            }
                            else if (fileInfo.FullName.Contains("Idle") ||
                          fileInfo.FullName.Contains("idle")
                          )
                            {
                                animIdleList.Add(fileInfo);
                            }
                            else if (fileInfo.FullName.Contains("attack") ||
                                fileInfo.FullName.Contains("Attack")
                                )
                            {
                                animAttackList.Add(fileInfo);
                            }
                            else
                            {
                                Log.Error($"{fileInfo.Name} 命名错误");
                            }
                        }
                        int i = 0;
                        foreach (var animInfo in animHurtList)
                        {
                            string ext = "" + ++i;
                            if (i < 10)
                                ext = "0" + ext;
                            name = dirFilePath + "_Hurt_" + ext + extion;

                            animInfo.MoveTo(name);
                        }
                        i = 0;
                        foreach (var animInfo in animRunList)
                        {
                            string ext = "" + ++i;
                            if (i < 10)
                                ext = "0" + ext;
                            name = dirFilePath + "_Run_" + ext + extion;
                            animInfo.MoveTo(name);

                        }
                        i = 0;
                        foreach (var animInfo in animIdleList)
                        {
                            string ext = "" + ++i;
                            if (i < 10)
                                ext = "0" + ext;
                            name = dirFilePath + "_Idle_" + ext + extion;
                            animInfo.MoveTo(name);

                        }
                        i = 0;
                        foreach (var animInfo in animAttackList)
                        {
                            string ext = "" + ++i;
                            if (i < 10)
                                ext = "0" + ext;
                            name = dirFilePath + "_Attack_" + ext + extion;
                            animInfo.MoveTo(name);

                        }
                    }
                }
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }


        [MenuItem("Assets/资源操作/角色/生成序列帧动画", false, 98)]
        static void BuildRoleAniamtion() => Instance.BuildRoleAniamtionInterner();
        private void BuildRoleAniamtionInterner()
        {
            try
            {
                string[] strs = Selection.assetGUIDs;
                List<FileInfo> animHurtList = new List<FileInfo>();
                List<FileInfo> animRunList = new List<FileInfo>();
                List<FileInfo> animIdleList = new List<FileInfo>();
                List<FileInfo> animAttackList = new List<FileInfo>();
                foreach (var item in strs)
                {
                    string dirPath = AssetDatabase.GUIDToAssetPath(item);

                    DirectoryInfo raw = new DirectoryInfo(dirPath);
                    var dirArr = raw.GetDirectories();
                    int count = 0; int max = dirArr.Length;
                    foreach (DirectoryInfo dictorys in dirArr)
                    {
                        count++;
                        animHurtList.Clear();
                        animRunList.Clear();
                        animIdleList.Clear();
                        animAttackList.Clear();
                        if (EditorUtility.DisplayCancelableProgressBar(string.Format("创建动画{0}/{1}", count, max), dictorys.Name, count / (float)max))
                            break;
                        FileInfo[] images = dictorys.GetFiles("*.png");
                        foreach (var info in images)
                        {
                            if (info.Name.Contains("_Attack_"))
                                animAttackList.Add(info);
                            else if (info.Name.Contains("_Hurt_"))
                                animHurtList.Add(info);
                            else if (info.Name.Contains("_Idle_"))
                                animIdleList.Add(info);
                            else if (info.Name.Contains("_Run_"))
                                animRunList.Add(info);
                        }
                        if (animAttackList.Count > 0)
                        {
                            string pngName = animAttackList[0].Name;
                            string animName = pngName.Substring(0, pngName.LastIndexOf("_"));
                            string animParentName = pngName.Substring(0, pngName.IndexOf("_"));
                            animName = $"{animationRolePath}/{raw.Name}/{animParentName}/{animName}.anim";
                            BuildAnimationClip(animAttackList, animName);
                        }
                        if (animHurtList.Count > 0)
                        {
                            string pngName = animHurtList[0].Name;
                            string animName = pngName.Substring(0, pngName.LastIndexOf("_"));
                            string animParentName = pngName.Substring(0, pngName.IndexOf("_"));
                            animName = $"{animationRolePath}/{raw.Name}/{animParentName}/{animName}.anim";
                            BuildAnimationClip(animHurtList, animName);
                        }
                        if (animIdleList.Count > 0)
                        {
                            string pngName = animIdleList[0].Name;
                            string animName = pngName.Substring(0, pngName.LastIndexOf("_"));
                            string animParentName = pngName.Substring(0, pngName.IndexOf("_"));
                            animName = $"{animationRolePath}/{raw.Name}/{animParentName}/{animName}.anim";
                            BuildAnimationClip(animIdleList, animName);
                        }
                        if (animRunList.Count > 0)
                        {
                            string pngName = animRunList[0].Name;
                            string animName = pngName.Substring(0, pngName.LastIndexOf("_"));
                            string animParentName = pngName.Substring(0, pngName.IndexOf("_"));
                            animName = $"{animationRolePath}/{raw.Name}/{animParentName}/{animName}.anim";
                            BuildAnimationClip(animRunList, animName);
                        }
                    }
                }
                AssetDatabase.Refresh();
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("生成动画", "生成动画已经完成", "确定");
            }
            catch (Exception e)
            {
                Log.Error(e);
                EditorUtility.ClearProgressBar();
            }
        }


        [MenuItem("Assets/资源操作/角色/自动给Role赋值动画", false, 99)]
        static void AutoAddClip() => Instance.AutoAddClipInterner();
        private void AutoAddClipInterner()
        {
            try
            {
                string[] strs = Selection.assetGUIDs;
                foreach (var item in strs)
                {
                    string dirPath = AssetDatabase.GUIDToAssetPath(item);
                    if (string.IsNullOrEmpty(dirPath) || !Directory.Exists(dirPath))
                        continue;
                    string animClipDirPath = animationRolePath + "/" + Utility.FileOpation.CreateDirectory(dirPath).Name;
                    var fileArr = Utility.FileOpation.GetFiles(dirPath, "*.prefab", SearchOption.AllDirectories);
                    int count = 0; int max = fileArr.Length;
                    foreach (var fileInfo in fileArr)
                    {
                        count++;
                        if (EditorUtility.DisplayCancelableProgressBar(string.Format("自动赋值动画{0}/{1}", count, max), fileInfo.Name, count / (float)max))
                            break;
                        string prefabName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
                        string clipDirPath = animClipDirPath + "/" + prefabName;
                        AnimationClipInfo[] clipInfo = new AnimationClipInfo[0];
                        var arr = Utility.FileOpation.GetFiles(clipDirPath, "*.anim", SearchOption.AllDirectories);
                        if (arr == null)
                        {
                            Log.Error($"arr == null where ame = {prefabName}");
                            continue;
                        }
                        foreach (var animInfo in arr)
                        {
                            var animName = Utility.PathOption.GetRegularPath(animInfo.FullName);
                            var anim = AssetDatabase.LoadAssetAtPath<AnimationClip>(DataPathToAssetPath(animName));
                            ArrayUtility.Add(ref clipInfo, new AnimationClipInfo
                            {
                                AnimationClip = anim
                            });
                        }
                        var prefanName = Utility.PathOption.GetRegularPath(fileInfo.FullName);
                        var go = AssetDatabase.LoadAssetAtPath<GameObject>(DataPathToAssetPath(prefanName));
                        MonoAnimancer animancers = go.GetComponent<MonoAnimancer>();
                        animancers.Animator = go.GetComponentInChildren<Animator>();
                        animancers.SetClips(clipInfo);
                        AddSpriteInterner(animancers);

                        EditorUtility.SetDirty(go);
                        AssetDatabase.SaveAssets();
                    }
                }
                AssetDatabase.Refresh();

                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("自动赋值动画", "自动赋值动画已经完成", "确定");
            }
            catch (Exception e)
            {
                Log.Error(e);
                EditorUtility.ClearProgressBar();
            }
        }
        #endregion

        #region 8向
        [MenuItem("Assets/资源操作/8向/批量规范文件名", false, 97)]
        static void Animation8Direction() => Instance.Animation8DirectionInternel();
        private void Animation8DirectionInternel()
        {
            try
            {
                string[] strs = Selection.assetGUIDs;
                foreach (var item in strs)
                {
                    string dirPath = AssetDatabase.GUIDToAssetPath(item);
                    FileInfo[] infoArr = Utility.FileOpation.GetFiles(dirPath, "*.png", SearchOption.AllDirectories);
                    Execute(infoArr, ".png");
                    infoArr = Utility.FileOpation.GetFiles(dirPath, "*.meta", SearchOption.AllDirectories);
                    Execute(infoArr, ".png.meta");
                    AssetDatabase.Refresh();
                    void Execute(FileInfo[] _infoArr, string extion)
                    {
                        SortAsFileName(_infoArr);
                        List<FileInfo> animHurtList = new List<FileInfo>();
                        List<FileInfo> animRunList = new List<FileInfo>();
                        List<FileInfo> animIdleList = new List<FileInfo>();
                        List<FileInfo> animAttackList = new List<FileInfo>();
                        string dirFilePath = _infoArr[0].DirectoryName;
                        string dirName = Directory.GetParent(_infoArr[0].FullName).Name;
                        dirFilePath += "/" + dirName;
                        foreach (var fileInfo in _infoArr)
                        {

                            if (fileInfo.FullName.Contains("hurt") ||
                              fileInfo.FullName.Contains("hit") ||
                              fileInfo.FullName.Contains("Hurt") ||
                              fileInfo.FullName.Contains("Hit")
                              )
                            {
                                animHurtList.Add(fileInfo);
                            }
                            else if (fileInfo.FullName.Contains("run") ||
                          fileInfo.FullName.Contains("Run")
                          )
                            {
                                animRunList.Add(fileInfo);
                            }
                            else if (fileInfo.FullName.Contains("Idle") ||
                          fileInfo.FullName.Contains("idle")
                          )
                            {
                                animIdleList.Add(fileInfo);
                            }
                            else if (fileInfo.FullName.Contains("attack") ||
                                fileInfo.FullName.Contains("Attack")
                                )
                            {
                                animAttackList.Add(fileInfo);
                            }
                            else
                            {
                                Log.Error($"{fileInfo.Name} 命名错误");
                            }
                        }
                        int i = 0;
                        foreach (var animInfo in animHurtList)
                        {
                            string ext = "" + ++i;
                            if (i < 10)
                                ext = "0" + ext;
                            name = dirFilePath + "_Hurt_" + ext + extion;

                            animInfo.MoveTo(name);
                        }
                        i = 0;
                        foreach (var animInfo in animRunList)
                        {
                            string ext = "" + ++i;
                            if (i < 10)
                                ext = "0" + ext;
                            name = dirFilePath + "_Run_" + ext + extion;
                            animInfo.MoveTo(name);

                        }
                        i = 0;
                        foreach (var animInfo in animIdleList)
                        {
                            string ext = "" + ++i;
                            if (i < 10)
                                ext = "0" + ext;
                            name = dirFilePath + "_Idle_" + ext + extion;
                            animInfo.MoveTo(name);

                        }
                        i = 0;
                        foreach (var animInfo in animAttackList)
                        {
                            string ext = "" + ++i;
                            if (i < 10)
                                ext = "0" + ext;
                            name = dirFilePath + "_Attack_" + ext + extion;
                            animInfo.MoveTo(name);

                        }
                    }
                }
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
        #endregion

        private AnimationClip BuildAnimationClip(List<FileInfo> images, string animationName)
        {
            //查找所有图片，因为我找的测试动画是.jpg 
            AnimationClip clip = new AnimationClip();
            //clip.legacy = true;
            //UnityEditor.AnimationClipSettings
            AnimationUtility.SetAnimationType(clip, ModelImporterAnimationType.Generic);
            EditorCurveBinding curveBinding = new EditorCurveBinding();
            curveBinding.type = typeof(SpriteRenderer);
            curveBinding.path = "";
            curveBinding.propertyName = "m_Sprite";
            ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[images.Count];
            //动画长度是按秒为单位，1/10就表示1秒切10张图片，根据项目的情况可以自己调节
            float frame = 12;
            //if (animationName.IndexOf("Attck") >= 0)
            //{
            //    frame = 24;
            //}

            float frameTime = 1 / frame;
            for (int i = 0; i < images.Count; i++)
            {
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(DataPathToAssetPath(images[i].FullName));
                keyFrames[i] = new ObjectReferenceKeyframe();
                keyFrames[i].time = frameTime * i;
                keyFrames[i].value = sprite;
            }
            //动画帧率，30比较合适
            clip.frameRate = frame;

            //有些动画我希望天生它就动画循环
            if (animationName.IndexOf("Idle") >= 0 || animationName.IndexOf("Run") >= 0)
            {
                //设置idle文件为循环动画
                SerializedObject serializedClip = new SerializedObject(clip);
                AnimationClipSettings clipSettings = new AnimationClipSettings(serializedClip.FindProperty("m_AnimationClipSettings"));
                clipSettings.loopTime = true;
                serializedClip.ApplyModifiedProperties();
            }
            AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
            string dirPath = animationName.Substring(0, animationName.LastIndexOf('/'));
            string dirFullPath = Path.GetFullPath(dirPath);
            if (!Directory.Exists(dirFullPath))
            {
                Directory.CreateDirectory(dirFullPath);
            }
            AssetDatabase.CreateAsset(clip, animationName);
            AssetDatabase.SaveAssets();
            return clip;
        }
        [MenuItem("Assets/资源操作/自动赋值Sprite", false, 101)]
        static void AutoAddSprite() => Instance.AutoAddSpriteInterner();
        private void AutoAddSpriteInterner()
        {
            try
            {

                string[] strs = Selection.assetGUIDs;
                foreach (var item in strs)
                {
                    string dirPath = AssetDatabase.GUIDToAssetPath(item);
                    if (string.IsNullOrEmpty(dirPath) || !Directory.Exists(dirPath))
                        continue;
                    var fileArr = Utility.FileOpation.GetFiles(dirPath, "*.prefab", SearchOption.AllDirectories);
                    int count = 0; int max = fileArr.Length;
                    foreach (var fileInfo in fileArr)
                    {
                        count++;
                        if (EditorUtility.DisplayCancelableProgressBar(string.Format("赋值Sprite{0}/{1}", count, max), fileInfo.Name, count / (float)max))
                            break;
                        string prefabName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
                        var prefanName = Utility.PathOption.GetRegularPath(fileInfo.FullName);
                        var go = AssetDatabase.LoadAssetAtPath<GameObject>(DataPathToAssetPath(prefanName));
                        MonoAnimancer animancers = go.GetComponent<MonoAnimancer>();
                        AddSpriteInterner(animancers);

                        EditorUtility.SetDirty(go);
                        AssetDatabase.SaveAssets();
                    }
                }
                AssetDatabase.Refresh();

                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("自动赋值Sprite", "自动赋值Sprite已经完成", "确定");
            }
            catch (Exception e)
            {
                Log.Error(e);
                EditorUtility.ClearProgressBar();
            }
        }

        /// <summary>
        /// 将Animancers中第一个动画的第一帧赋值给Sprite
        /// </summary>
        /// <param name="animancers"></param>
        private void AddSpriteInterner(MonoAnimancer animancers)
        {
            try
            {
                var clipArr = animancers.GetClips();
                if (clipArr == null)
                {
                    Log.Error($"clipArr == null where name = {animancers.name}");
                    return;
                }
                AnimationClip clip = clipArr[0].AnimationClip;
                var keyArr = AnimationUtility.GetObjectReferenceCurve(clip, curveBinding);
                if (keyArr == null)
                {
                    Log.Error($"keyArr == null where name =  fileInfo.Name");
                    return;
                }
                var key = keyArr[0];
                Sprite sprite = key.value.As<Sprite>();
                animancers.GetComponent<SpriteRenderer>().sprite = sprite;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }


        [MenuItem("Assets/资源操作/加入图集", false, 110)]
        static void AddToAlats() => Instance.AddToAlatsInterner();
        private void AddToAlatsInterner()
        {
            BulildAB bulildAB = AssetDatabase.LoadAssetAtPath<BulildAB>("Assets/Model/Cal/CalAssets/BulildAB.asset");
            string[] strs = Selection.assetGUIDs;
            foreach (var item in strs)
            {
                string dirPath = AssetDatabase.GUIDToAssetPath(item);
                foreach (var fileInfo in Utility.FileOpation.GetFiles(dirPath, "*.*", SearchOption.AllDirectories))
                {
                    if (fileInfo.Extension.Equals(".meta"))
                        continue;
                    string path = fileInfo.DirectoryName;
                    if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                        continue;
                    if (bulildAB.atlasPaths.Contains(path))
                        continue;
                    bulildAB.atlasPaths.Add(path);
                }

            }
            EditorUtility.SetDirty(bulildAB);
            AssetDatabase.Refresh();
        }

        class AnimationClipSettings
        {
            SerializedProperty m_Property;

            private SerializedProperty Get(string property) { return m_Property.FindPropertyRelative(property); }

            public AnimationClipSettings(SerializedProperty prop) { m_Property = prop; }

            public float startTime { get { return Get("m_StartTime").floatValue; } set { Get("m_StartTime").floatValue = value; } }
            public float stopTime { get { return Get("m_StopTime").floatValue; } set { Get("m_StopTime").floatValue = value; } }
            public float orientationOffsetY { get { return Get("m_OrientationOffsetY").floatValue; } set { Get("m_OrientationOffsetY").floatValue = value; } }
            public float level { get { return Get("m_Level").floatValue; } set { Get("m_Level").floatValue = value; } }
            public float cycleOffset { get { return Get("m_CycleOffset").floatValue; } set { Get("m_CycleOffset").floatValue = value; } }

            public bool loopTime { get { return Get("m_LoopTime").boolValue; } set { Get("m_LoopTime").boolValue = value; } }
            public bool loopBlend { get { return Get("m_LoopBlend").boolValue; } set { Get("m_LoopBlend").boolValue = value; } }
            public bool loopBlendOrientation { get { return Get("m_LoopBlendOrientation").boolValue; } set { Get("m_LoopBlendOrientation").boolValue = value; } }
            public bool loopBlendPositionY { get { return Get("m_LoopBlendPositionY").boolValue; } set { Get("m_LoopBlendPositionY").boolValue = value; } }
            public bool loopBlendPositionXZ { get { return Get("m_LoopBlendPositionXZ").boolValue; } set { Get("m_LoopBlendPositionXZ").boolValue = value; } }
            public bool keepOriginalOrientation { get { return Get("m_KeepOriginalOrientation").boolValue; } set { Get("m_KeepOriginalOrientation").boolValue = value; } }
            public bool keepOriginalPositionY { get { return Get("m_KeepOriginalPositionY").boolValue; } set { Get("m_KeepOriginalPositionY").boolValue = value; } }
            public bool keepOriginalPositionXZ { get { return Get("m_KeepOriginalPositionXZ").boolValue; } set { Get("m_KeepOriginalPositionXZ").boolValue = value; } }
            public bool heightFromFeet { get { return Get("m_HeightFromFeet").boolValue; } set { Get("m_HeightFromFeet").boolValue = value; } }
            public bool mirror { get { return Get("m_Mirror").boolValue; } set { Get("m_Mirror").boolValue = value; } }
        }

        static EditorCurveBinding curveBinding
        {
            get
            {
                EditorCurveBinding curveBinding = new EditorCurveBinding();
                curveBinding.type = typeof(SpriteRenderer);
                curveBinding.path = "";
                curveBinding.propertyName = "m_Sprite";
                return curveBinding;
            }
        }
        private static void SortAsFileName(FileInfo[] arrFi)
        {
            Array.Sort(arrFi, new StringCompare());
        }
        public class StringCompare : IComparer<FileInfo>
        {
            public int Compare(FileInfo x, FileInfo y)
            {
                string a = x.Name.Trim();
                string b = y.Name.Trim();
                string r = @"[0-9]+";
                Regex reg = new Regex(r, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                MatchCollection mcA = reg.Matches(a);//设定要查找的字符串

                int intA = -1;
                foreach (Match m in mcA)
                {
                    intA = int.Parse(m.Groups[0].Value);
                }
                MatchCollection mcB = reg.Matches(b);//设定要查找的字符串

                int intB = -1;
                foreach (Match m in mcB)
                {
                    intB = int.Parse(m.Groups[0].Value);
                }
                if (intA == -1 || intB == -1)
                {
                    return a.CompareTo(b);
                }

                return intA.CompareTo(intB);
            }
        }
        private static string DataPathToAssetPath(string path)
        {
            if (path.Contains("\\"))
                return path.Substring(path.IndexOf("Assets\\"));
            else
                return path.Substring(path.IndexOf("Assets/"));
        }
    }
}