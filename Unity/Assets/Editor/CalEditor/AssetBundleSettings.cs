//using Sirenix.OdinInspector;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using System.IO;
//using UnityEditor;
//using UnityEngine;
//using System;
//using ET;
//using LitJson;
//using UnityEditor.SceneManagement;
///// <summary>
///// 打包设置
///// </summary>
//[CreateAssetMenu]
//public class AssetBundleSettings : ScriptableObject
//{
//    public static AssetBundleSettings Instance;
//    private void OnEnable()
//    {
//        Instance = this;
//    }
//    public enum CusBuildTarget
//    {
//        Windows,
//        Android,
//        iOS
//    }

//    [HorizontalGroup("Common", LabelWidth = 70)]
//    [VerticalGroup("Common/Left")]
//    [LabelText("资源版本号")]
//    public string ResourceVersion = "1.0.0";

//    [PropertySpace(10)]
//    [VerticalGroup("Common/Left")]
//    [LabelText("目标平台")]
//    public CusBuildTarget CurrBuildTarget;

//    public BuildTarget GetBuildTarget()
//    {
//        switch (CurrBuildTarget)
//        {
//            default:
//            case CusBuildTarget.Windows:
//                return BuildTarget.StandaloneWindows;
//            case CusBuildTarget.Android:
//                return BuildTarget.Android;
//            case CusBuildTarget.iOS:
//                return BuildTarget.iOS;
//        }
//    }

//    [PropertySpace(10)]
//    [VerticalGroup("Common/Left")]
//    [LabelText("参数")]
//    public BuildAssetBundleOptions Options;

//    [VerticalGroup("Common/Right")]
//    [Button(ButtonSizes.Medium)]
//    [LabelText("更新版本号")]
//    public void UpdateResourceVersion()
//    {
//        string version = ResourceVersion;
//        string[] arr = version.Split('.');
//        int.TryParse(arr[2], out int shortVersion);
//        version = string.Format("{0}.{1}.{2}", arr[0], arr[1], ++shortVersion);
//        ResourceVersion = version;
//    }

//    [VerticalGroup("Common/Right")]
//    [Button(ButtonSizes.Medium)]
//    [LabelText("清空资源包")]
//    public void ClearAB()
//    {
//        if (Directory.Exists(TempPath))
//        {
//            Directory.Delete(TempPath, true);
//        }
//        _needUpdateDataPath.Clear();
//        //MoveILRFile();
//        if (IsDeleteRootFile)
//        {
//            string output = Application.persistentDataPath;
//            if (Directory.Exists(output))
//            {
//                Directory.Delete(output, true);
//            }
//        }
//        EditorUtility.DisplayDialog("", "清空完毕", "确定");
//    }

//    /// <summary>
//    /// 移动ILR文件
//    /// </summary>
//    public void MoveILRFile()
//    {
//        for (int i = 0; i < ILRFiles.Length; i++)
//        {
//            if (File.Exists(ILRFiles[i]))
//            {
//                string fileName = Path.GetFileName(ILRFiles[i]);
//                fileName = fileName.Substring(0, fileName.IndexOf(".")) + i.ToString() + ".bytes";
//                File.Copy(ILRFiles[i], ILRPath + fileName, true);
//            }
//            else
//            {
//                Debug.LogError("ILRFile不存在");
//            }
//        }
//        UnityEditor.AssetDatabase.Refresh();
//    }

//    List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

//    [VerticalGroup("Common/Right")]
//    [Button(ButtonSizes.Medium)]
//    [LabelText("打包")]
//    public void BuildAB()
//    {
//        GameObject[] gos = EditorSceneManager.GetActiveScene().GetRootGameObjects();
//        foreach (var go in gos)
//        {
//            if(go.name == "Global")
//            {
//                var init = go.GetComponent<Init>();
//                SerializedObject serializedObject = new SerializedObject(init);
//                SerializedProperty dataProperty = serializedObject.FindProperty("IsEditorMode");
//                isEditorMode = dataProperty.boolValue;
//            }
//        }
//        if (isEditorMode)
//        {
//            EditorUtility.DisplayDialog("严重错误","编辑器模式不能打包","取消");
//            return;
//        }
//        builds.Clear();
//        ClearAB();
//        for (int i = 0; i < Datas.Length; i++)
//        {
//            AssetBundleData assetBundleData = Datas[i];
//            for (int j = 0; j < assetBundleData.Path.Length; j++)
//            {
//                string path = assetBundleData.Path[j];
//                BuildAssetBundleForPath(path, assetBundleData.Overall);
//            }
//        }
//        if (!Directory.Exists(TempPath))
//        {
//            Directory.CreateDirectory(TempPath);
//        }
        
//        if (builds.Count == 0)
//        {
//            Debug.LogError("未找到需要打包的内容");
//        }
//        else
//        {
//            Debug.Log("buildCount=" + builds.Count);
//            BuildPipeline.BuildAssetBundles(TempPath, builds.ToArray(), Options, GetBuildTarget());
//            Debug.Log("临时资源包打包完毕");
//            CopyFile(TempPath);
//            Debug.Log("拷贝完毕");
//            AssetBundleEncrypt();
//            Debug.Log("加密完毕");
//            CreateDependenciesFile();
//            Debug.Log("生成依赖完毕");
//            CreateVersionFile();
//            Debug.Log("生成版本文件完毕");
//            CreateABVision();
//            Debug.Log("****************************************\n打包完成\n****************************************\n****************************************\n");
//        }
//    }

//    private void CreateVersionFile()
//    {
//        string path = OutPath;
//        if (!Directory.Exists(path))
//        {
//            Directory.CreateDirectory(path);
//        }

//        string strVersionFilePath = path + "/VersionFile.txt"; //版本文件路径

//        //如果版本文件存在 则删除
//        IOUtil.DeleteFile(strVersionFilePath);

//        StringBuilder sbContent = new StringBuilder();

//        DirectoryInfo directory = new DirectoryInfo(path);

//        //拿到文件夹下所有文件
//        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

//        sbContent.AppendLine(ResourceVersion);
//        for (int i = 0; i < arrFiles.Length; i++)
//        {
//            FileInfo file = arrFiles[i];

//            if (file.Extension == ".manifest")
//            {
//                continue;
//            }
//            string fullName = file.FullName; //全名 包含路径扩展名

//            //相对路径
//            string name = fullName.Substring(fullName.IndexOf(CurrBuildTarget.ToString()) + CurrBuildTarget.ToString().Length + 1);

//            string md5 = MD5Helper.FileMD5(fullName); //文件的MD5
//            if (md5 == null) continue;

//            string size = file.Length.ToString(); //文件大小

//            bool isFirstData = false; //是否初始数据
//            bool isEncrypt = false;
//            bool isBreak = false;

//            for (int j = 0; j < Datas.Length; j++)
//            {
//                foreach (string xmlPath in Datas[j].Path)
//                {
//                    string tempPath = xmlPath;
//                    //if (xmlPath.IndexOf(".") != -1)
//                    //{
//                    //    tempPath = xmlPath.Substring(0, xmlPath.IndexOf("."));
//                    //}

//                    name = name.Replace("\\", "/");
//                    if (name.IndexOf(tempPath, StringComparison.CurrentCultureIgnoreCase) != -1)
//                    {
//                        isFirstData = Datas[j].IsFirstData;
//                        isEncrypt = Datas[j].IsEncrypt;
//                        isBreak = true;
//                        break;
//                    }
//                }
//                if (isBreak) break;
//            }

//            string strLine = string.Format("{0}|{1}|{2}|{3}|{4}", name, md5, size, isFirstData ? 1 : 0, isEncrypt ? 1 : 0);
//            sbContent.AppendLine(strLine);
//        }

//        IOUtil.CreateTextFile(strVersionFilePath, sbContent.ToString());

//        MMO_MemoryStream ms = new MMO_MemoryStream();
//        string str = sbContent.ToString().Trim();
//        string[] arr = str.Split('\n');
//        int len = arr.Length;
//        ms.WriteInt(len);
//        for (int i = 0; i < len; i++)
//        {
//            if (i == 0)
//            {
//                ms.WriteUTF8String(arr[i]);
//            }
//            else
//            {
//                string[] arrInner = arr[i].Split('|');
//                ms.WriteUTF8String(arrInner[0]);
//                ms.WriteUTF8String(arrInner[1]);
//                ms.WriteULong(ulong.Parse(arrInner[2]));
//                ms.WriteByte(byte.Parse(arrInner[3]));
//                ms.WriteByte(byte.Parse(arrInner[4]));
//            }
//        }

//        string filePath = path + "/VersionFile.bytes"; //版本文件路径
//        byte[] buffer = ms.ToArray();
//        ms.Dispose();
//        ms.Close();

//        //CreateABVision(buffer);
//        buffer = ZipHelper.Compress(buffer);
//        using (FileStream fs = new FileStream(filePath, FileMode.Create))
//        {
//            fs.Write(buffer, 0, buffer.Length);
//            fs.Close();
//            fs.Dispose();
//        }
//    }
//    private void CreateABVision()
//    {
//        //string vision = null;
//        //var visionFile = await ResourceComponent.GetAssetBundleVersionList(buffer, ref vision);
//        string path = OutPath;
//        //if (!Directory.Exists(path))
//        //{
//        //    Directory.CreateDirectory(path);
//        //}

//        string strVersionFilePath = path + "/ABVersionFile.json"; //版本文件路径

//        //如果版本文件存在 则删除
//        IOUtil.DeleteFile(strVersionFilePath);

//        //StringBuilder sbContent = new StringBuilder();

//        //DirectoryInfo directory = new DirectoryInfo(path);

//        ////拿到文件夹下所有文件
//        //FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

//        Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();
//        foreach (var data in Datas)
//        {
//            foreach (var directoryPath in data.Path)
//            {
//                string directoryFullPath = Application.dataPath + "/" + directoryPath;
//                Dictionary<string, string> aBVisionEntity = new Dictionary<string, string>();
//                DirectoryInfo subDirectory = new DirectoryInfo(directoryFullPath);
//                FileInfo[] arrSubFiles = subDirectory.GetFiles("*", SearchOption.AllDirectories);
//                foreach (var subFile in arrSubFiles)
//                {
//                    if (subFile.Extension == ".manifest")
//                    {
//                        continue;
//                    }
//                    if (subFile.Extension == ".meta")
//                    {
//                        continue;
//                    }
//                    string fullName = subFile.FullName; //全名 包含路径扩展名
//                    string md5 = MD5Helper.FileMD5(fullName); //文件的MD5
//                    if (md5 == null) continue;
//                    aBVisionEntity.Add(fullName.Replace("\\","/"), md5);
//                }
//                dic.Add(directoryPath, aBVisionEntity);
//            }

//        }

//        string json = MongoHelper.ToJson(dic);
//        IOUtil.CreateTextFile(strVersionFilePath, json);
//    }
//    private void CreateDependenciesFile()
//    {
//        //第一次循环 把所有的Asset存储在一个列表里

//        List<AssetEntity> tempList = new List<AssetEntity>();
//        for (int i = 0; i < Datas.Length; i++)
//        {
//            AssetBundleData assetBundleData = Datas[i];
//            for (int j = 0; j < assetBundleData.Path.Length; j++)
//            {
//                string path = Application.dataPath + "/" + assetBundleData.Path[j];
//                CollectFileInfo(tempList, path);
//            }
//        }
//        List<AssetEntity> assetList = new List<AssetEntity>();
//        for (int i = 0; i < tempList.Count; i++)
//        {
//            AssetEntity entity = tempList[i];

//            AssetEntity newEntity = new AssetEntity();
//            newEntity.Category = entity.Category;
//            newEntity.AssetName = entity.AssetFullName.Substring(entity.AssetFullName.LastIndexOf("/") + 1);
//            newEntity.AssetName = newEntity.AssetName.Substring(0, newEntity.AssetName.LastIndexOf("."));
//            newEntity.AssetFullName = entity.AssetFullName;
//            newEntity.AssetBundleName = entity.AssetBundleName;

//            assetList.Add(newEntity);

//            //场景不需要检查依赖项
//            if (entity.Category == AssetCategory.Atlas)
//            {
//                continue;
//            }

//            newEntity.DependsAssetList = new List<AssetDependsEntity>();

//            string[] arr = AssetDatabase.GetDependencies(entity.AssetFullName);
//            foreach (string str in arr)
//            {
//                if (!str.Equals(newEntity.AssetFullName, StringComparison.CurrentCultureIgnoreCase) && GetIsAsset(tempList, str))
//                {
//                    AssetDependsEntity assetDepends = new AssetDependsEntity();
//                    assetDepends.Category = GetAssetCategory(str);
//                    assetDepends.AssetFullName = str;

//                    //把依赖资源 加入到依赖资源列表
//                    newEntity.DependsAssetList.Add(assetDepends);
//                }
//            }
//        }
//        //生成一个Json文件
//        string targetPath = OutPath;
//        if (!Directory.Exists(targetPath))
//        {
//            Directory.CreateDirectory(targetPath);
//        }


//        string strJsonFilePath = targetPath + "/AssetInfo.json"; //版本文件路径
//        IOUtil.CreateTextFile(strJsonFilePath, MongoHelper.ToJson(assetList));
//        Debug.Log("生成 AssetInfo.json 完毕");

//        MMO_MemoryStream ms = new MMO_MemoryStream();
//        //生成二进制文件
//        int len = assetList.Count;
//        ms.WriteInt(len);

//        for (int i = 0; i < len; i++)
//        {
//            AssetEntity entity = assetList[i];
//            ms.WriteByte((byte)entity.Category);
//            ms.WriteUTF8String(entity.AssetFullName);
//            ms.WriteUTF8String(entity.AssetBundleName);

//            if (entity.DependsAssetList != null)
//            {
//                //添加依赖资源
//                int depLen = entity.DependsAssetList.Count;
//                ms.WriteInt(depLen);
//                for (int j = 0; j < depLen; j++)
//                {
//                    AssetDependsEntity assetDepends = entity.DependsAssetList[j];
//                    ms.WriteByte((byte)assetDepends.Category);
//                    ms.WriteUTF8String(assetDepends.AssetFullName);
//                }
//            }
//            else
//            {
//                ms.WriteInt(0);
//            }
//        }

//        string filePath = targetPath + "/AssetInfo.bytes"; //版本文件路径
//        byte[] buffer = ms.ToArray();
//        buffer = ZipHelper.Compress(buffer);
//        FileStream fs = new FileStream(filePath, FileMode.Create);
//        fs.Write(buffer, 0, buffer.Length);
//        fs.Close();
//        fs.Dispose();
//        Debug.Log("生成 AssetInfo.bytes 完毕");
//    }

//    #region CollectFileInfo 收集文件信息
//    /// <summary>
//    /// 收集文件信息
//    /// </summary>
//    /// <param name="tempLst"></param>
//    /// <param name="folderPath"></param>
//    private void CollectFileInfo(List<AssetEntity> tempLst, string folderPath)
//    {
//        if (folderPath.IndexOf(".unity") != -1)
//        {
//            int index = folderPath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);
//            //路径
//            string newPath = folderPath.Substring(index);

//            AssetImporter import = AssetImporter.GetAtPath(newPath);

//            AssetEntity entity = new AssetEntity();
//            entity.AssetFullName = newPath.Replace("\\", "/");
//            entity.Category = AssetCategory.Scenes;
//            entity.AssetBundleName = import.assetBundleName + ".assetbundle";
//            tempLst.Add(entity);
//        }
//        else
//        {
//            DirectoryInfo directory = new DirectoryInfo(folderPath);

//            //拿到文件夹下所有文件
//            FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

//            for (int i = 0; i < arrFiles.Length; i++)
//            {
//                FileInfo file = arrFiles[i];
//                if (file.Extension == ".meta")
//                {
//                    continue;
//                }

//                string filePath = file.FullName; //全名 包含路径扩展名

//                //Debug.LogError("filePath=" + filePath);
//                int index = filePath.IndexOf("Assets\\", StringComparison.CurrentCultureIgnoreCase);

//                if (index == -1)
//                    continue;
//                //路径
//                string newPath = filePath.Substring(index);

//                if (newPath.IndexOf(".idea") != -1) //过滤掉idea文件
//                {
//                    continue;
//                }

//                AssetEntity entity = new AssetEntity();
//                entity.AssetFullName = newPath.Replace("\\", "/");
//                entity.Category = GetAssetCategory(newPath.Replace(file.Name, "")); //去掉文件名，只保留路径
//                entity.AssetBundleName = GetAssetBundleName(newPath) + ".assetbundle";
//                tempLst.Add(entity);
//            }
//        }
//    }
//    #endregion

//    #region GetAssetCategory 获取资源分类
//    /// <summary>
//    /// 获取资源分类
//    /// </summary>
//    /// <param name="filePath"></param>
//    /// <returns></returns>
//    private AssetCategory GetAssetCategory(string filePath)
//    {
//        AssetCategory category = AssetCategory.None;

//        if (filePath.IndexOf("Atlas") != -1)
//        {
//            category = AssetCategory.Atlas;
//        }
//        else if (filePath.IndexOf("Audio") != -1)
//        {
//            category = AssetCategory.Audio;
//        }
//        else if (filePath.IndexOf("Animators") != -1)
//        {
//            category = AssetCategory.Animator;
//        }
//        else if (filePath.IndexOf("Config") != -1)
//        {
//            category = AssetCategory.Config;
//        }
//        else if (filePath.IndexOf("CommonPrefab") != -1)
//        {
//            category = AssetCategory.CommonPrefab;
//        }
//        else if (filePath.IndexOf("CusShaders") != -1)
//        {
//            category = AssetCategory.CusShaders;
//        }
//        else if (filePath.IndexOf("DataTable") != -1)
//        {
//            category = AssetCategory.DataTable;
//        }
//        else if (filePath.IndexOf("EffectSources") != -1)
//        {
//            category = AssetCategory.EffectSources;
//        }
//        else if (filePath.IndexOf("ILRunTime") != -1)
//        {
//            category = AssetCategory.ILRunTime;
//        }
//        else if (filePath.IndexOf("RoleEffectPrefab") != -1)
//        {
//            category = AssetCategory.RoleEffectPrefab;
//        }
//        else if (filePath.IndexOf("UIEffectPrefab") != -1)
//        {
//            category = AssetCategory.UIEffectPrefab;
//        }
//        else if (filePath.IndexOf("RolePrefab") != -1)
//        {
//            category = AssetCategory.RolePrefab;
//        }
//        else if (filePath.IndexOf("RoleSources") != -1)
//        {
//            category = AssetCategory.RoleSources;
//        }
//        else if (filePath.IndexOf("Download\\Scenes") != -1)
//        {
//            category = AssetCategory.Scenes;
//        }
//        else if (filePath.IndexOf("UIBigPic") != -1)
//        {
//            category = AssetCategory.UIBigPic;
//        }
//        else if (filePath.IndexOf("UIFont") != -1)
//        {
//            category = AssetCategory.UIFont;
//        }
//        else if (filePath.IndexOf("UISkill") != -1)
//        {
//            category = AssetCategory.UISkill;
//        }
//        else if (filePath.IndexOf("UIBag") != -1)
//        {
//            category = AssetCategory.UIBag;
//        }
//        else if (filePath.IndexOf("UIPrefab") != -1)
//        {
//            category = AssetCategory.UIPrefab;
//        }
//        else if (filePath.IndexOf("UIRes") != -1)
//        {
//            category = AssetCategory.UIRes;
//        }
//        else if (filePath.IndexOf("xLuaLogic") != -1)
//        {
//            category = AssetCategory.xLuaLogic;
//        }

//        return category;
//    }
//    #endregion

//    #region 判断某个资源是否存在于资源列表
//    /// <summary>
//    /// 判断某个资源是否存在于资源列表
//    /// </summary>
//    /// <param name="tempLst"></param>
//    /// <param name="assetFullName"></param>
//    /// <returns></returns>
//    private bool GetIsAsset(List<AssetEntity> tempLst, string assetFullName)
//    {
//        int len = tempLst.Count;
//        for (int i = 0; i < len; i++)
//        {
//            AssetEntity entity = tempLst[i];
//            if (entity.AssetFullName.Equals(assetFullName, StringComparison.CurrentCultureIgnoreCase))
//            {
//                return true;
//            }
//        }
//        return false;
//    }
//    #endregion

//    #region GetAssetBundleName 获取资源包的名称
//    /// <summary>
//    /// 获取资源包的名称
//    /// </summary>
//    /// <param name="newPath"></param>
//    /// <returns></returns>
//    private string GetAssetBundleName(string newPath)
//    {
//        string path = newPath.Replace("\\", "/");

//        int len = Datas.Length;
//        //循环设置文件夹包括子文件里边的项
//        for (int i = 0; i < len; i++)
//        {
//            AssetBundleData assetBundleData = Datas[i];

//            for (int j = 0; j < assetBundleData.Path.Length; j++)
//            {
//                if (path.IndexOf(assetBundleData.Path[j], StringComparison.CurrentCultureIgnoreCase) > -1)
//                {
//                    if (assetBundleData.Overall)
//                    {
//                        //文件夹是个整包 则返回这个特文件夹名字
//                        return assetBundleData.Path[j].ToLower();
//                    }
//                    else
//                    {
//                        //零散资源
//                        return path.Substring(0, path.LastIndexOf('.')).ToLower().Replace("assets/", "");
//                    }
//                }
//            }
//        }
//        return null;
//    }
//    #endregion

//    private void AssetBundleEncrypt()
//    {
//        for (int i = 0; i < Datas.Length; i++)
//        {
//            AssetBundleData assetBundleData = Datas[i];
            
//            if (assetBundleData.IsEncrypt)
//            {
//                for (int j = 0; j < assetBundleData.Path.Length; j++)
//                {
//                    if (!_needUpdateDataPath.Contains(assetBundleData.Path[j])) continue;
//                    string path = OutPath + "/" + assetBundleData.Path[j];
//                    if (assetBundleData.Overall)
//                    {
//                        path += ".assetbundle";
//                        AssetBundleEncryptFile(path);
//                    }
//                    else
//                    {
//                        AssetBundleEncryptFolder(path);
//                    }
//                }
//            }
//        }
//    }

//    private void AssetBundleEncryptFolder(string path)
//    {
//        DirectoryInfo directory = new DirectoryInfo(path);

//        //拿到文件夹下所有文件
//        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

//        foreach (FileInfo file in arrFiles)
//        {
//            AssetBundleEncryptFile(file.FullName);
//        }
//    }

//    private void AssetBundleEncryptFile(string path)
//    {
//        FileInfo fileInfo = new FileInfo(path);
//        byte[] buffer = null;

//        using (FileStream fs = new FileStream(path, FileMode.Open))
//        {
//            buffer = new byte[fs.Length];
//            fs.Read(buffer, 0, buffer.Length);
//        }
//        buffer = SecurityUtil.Xor(buffer);
//        using (FileStream fs = new FileStream(path, FileMode.Create))
//        {
//            fs.Write(buffer, 0, buffer.Length);
//            fs.Flush();
//        }
//    }

//    private void CopyFile(string tempPath)
//    {
//        //if (Directory.Exists(OutPath))
//        //{
//        //    Directory.Delete(OutPath,true);
//        //}
//        IOUtil.CopyDirectory(tempPath, OutPath, true);
//        DirectoryInfo directory = new DirectoryInfo(OutPath);

//        FileInfo[] arrFiles = directory.GetFiles("*.y", SearchOption.AllDirectories);
//        for (int i = 0; i < arrFiles.Length; i++)
//        {
//            FileInfo file = arrFiles[i];
//            string fullName = file.FullName;
//            string abFullName = fullName.Replace(".ab.y", ".assetbundle");
//            if (File.Exists(abFullName))
//            {
//                File.Delete(abFullName);
//            }
//            File.Move(fullName, abFullName);
//        }
//    }

//    private List<string> _needUpdateDataPath = new List<string>();
//    private void BuildAssetBundleForPath(string path, bool overall)
//    {
//        //**********************判断是否更新
//        bool isUpdate = false;
//        var visionFile = ReadABVersionFile();
//        if (visionFile != null)
//        {
//            visionFile.TryGetValue(path, out var md5Dic);
//            if (md5Dic != null)
//            {
//                DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath + "/" + path);
//                FileInfo[] arrSubFiles = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
//                foreach (var item in arrSubFiles)
//                {
//                    if (item.Extension.Equals(".meta") || item.Extension.Equals(".assetbundle"))
//                        continue;
//                    if (md5Dic.TryGetValue(item.FullName.Replace("\\", "/"), out var md5))
//                    {
//                        if (!md5.Equals(MD5Helper.FileMD5(item.FullName)))
//                        {
//                            isUpdate = true;
//                            break;
//                        }
//                    }
//                    else
//                    {
//                        isUpdate = true;
//                        break;
//                    }
//                }
//                if (!isUpdate) return;
//            }
           
//        }
      
//        //*******************************************
//        _needUpdateDataPath.Add(path);
//        string fullPath = Application.dataPath + "/" + path;
//        //拿到所有文件
//        DirectoryInfo directory = new DirectoryInfo(fullPath);

//        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

//        if (overall)
//        {
//            AssetBundleBuild build = new AssetBundleBuild
//            {
//                assetBundleName = path + ".ab",
//                assetBundleVariant = "y"
//            };
//            string[] arr = GetValidateFiles(arrFiles);
//            build.assetNames = arr;
//            builds.Add(build);
//        }
//        else
//        {
//            //每个文件打成一个包
//            string[] arr = GetValidateFiles(arrFiles);

//            for (int i = 0; i < arr.Length; i++)
//            {
//                AssetBundleBuild build = new AssetBundleBuild
//                {
//                    assetBundleName = arr[i].Substring(0, arr[i].LastIndexOf('.')).Replace("Assets/", "") + ".ab",
//                    assetBundleVariant = "y",
//                    assetNames = new string[] { arr[i] }
//                };
//                builds.Add(build);
//            }
//        }

//    }

//    private string[] GetValidateFiles(FileInfo[] arrFiles)
//    {
//        List<string> list = new List<string>();
//        for (int i = 0; i < arrFiles.Length; i++)
//        {
//            FileInfo file = arrFiles[i];
//            if (!file.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
//            {
//                list.Add("Assets" + file.FullName.Replace("\\", "/").Replace(Application.dataPath, ""));
//            }
//        }
//        return list.ToArray();
//    }
//    private Dictionary<string, Dictionary<string, string>> ReadABVersionFile()
//    {
//        string strVersionFilePath = OutPath + "/ABVersionFile.json"; //版本文件路径
//        string str = IOUtil.GetFileText(strVersionFilePath);
//        return MongoHelper.FromJson<Dictionary<string, Dictionary<string, string>>>(str);
//    }

//    public string TempPath
//    {
//        get
//        {
//            return Application.dataPath + "/../" + AssetBundleSavePath + "/" + ResourceVersion + "_Temp/" + CurrBuildTarget;
//        }
//    }
//    public string OutPath => TempPath.Replace("_Temp", "");

//    [LabelText("资源包保存路径")]
//    [FolderPath]
//    public string AssetBundleSavePath;

//    [LabelText("热更新资源")]
//    [FilePath]
//    public string[] ILRFiles;
//    [LabelText("热更新资源打包路径")]
//    [FolderPath]
//    public string ILRPath;

//    [LabelText("是否删除根文件")]
//    public bool IsDeleteRootFile;

//    [LabelText("勾选进行编辑")]
//    public bool IsCanEditor;

//    [EnableIf("IsCanEditor")]
//    [BoxGroup("AssetBundleSettings")]
//    public AssetBundleData[] Datas;
//    private bool isEditorMode;

//    [Serializable]
//    public class AssetBundleData
//    {
//        [LabelText("名称")]
//        public string Name;
//        [FolderPath(ParentFolder = "Assets")]
//        public string[] Path;
//        [LabelText("文件打包成一个资源包")]
//        public bool Overall;
//        [LabelText("是否初始资源")]
//        public bool IsFirstData;
//        [LabelText("是否加密")]
//        public bool IsEncrypt;


//    }
//}
