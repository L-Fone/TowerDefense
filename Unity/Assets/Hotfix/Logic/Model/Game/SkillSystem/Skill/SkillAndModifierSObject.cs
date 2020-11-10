using Cal;
using ET;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
#endif

namespace CalEditor
{
#if UNITY_EDITOR
    [CreateAssetMenu]
    public class SkillAndModifierSObject : Sirenix.OdinInspector.SerializedScriptableObject
    {
        public static SkillAndModifierSObject Instance;
        private void OnEnable()
        {
            Instance = this;
        }

        [BoxGroup("Buff数据")]
        [LabelText("需要加载的技能Id")]
        [ValueDropdown("GetSkillId"), PropertyOrder(0)]
        [ShowInInspector]
        public int skillId
        {
            get => _skillId;
            set
            {
                _skillId = value;
                LoadBuff();
            }
        }
        private int _skillId = -1;
        IEnumerable<int> GetSkillId()
        {
            var ret = new List<int>();
            ret.Add(0);
            var arr = Utility.FileOpation.GetFiles(soPath, "*.asset", SearchOption.AllDirectories);
            foreach (var item in arr)
            {
                ret.Add(int.Parse(item.Name.Replace(".asset", string.Empty)));
            }
            if (ret.Count == 1)
                ret.Add(-1);
            return ret;
        }
        [BoxGroup("Buff数据")]
        [LabelText("创建新的技能Id"), PropertyOrder(1)]
        public int newSkillId;
        private List<string> listenNamelist = new List<string>();
        public void LoadBuff()
        {
            if (skillId == 0)
            {
                if (newSkillId == 0) return;
                Utility.FileOpation.CreateDirectory(soPath);
                string newAssetPath = $"{soPath}/{newSkillId}.asset";
                SkillLogicConfigSObject skillBuffSObject = ScriptableObject.CreateInstance<SkillLogicConfigSObject>();
                skillBuffSObject.config = new  SkillLogicConfig();
                if (File.Exists(newAssetPath))
                {
                    bool isCreate = Utility.UnityEditor.ShowConfirm("提示", "文件已存在是否从新创建？");
                    if (!isCreate)
                    {
                        return;
                    }
                }

                AssetDatabase.CreateAsset(skillBuffSObject, newAssetPath);
                AssetDatabase.Refresh();
                skillId = newSkillId;
                dataSo = skillBuffSObject;
                return;
            }
            string assetPath = $"{soPath}/{skillId}.asset";
            SkillLogicConfigSObject so = AssetDatabase.LoadAssetAtPath<SkillLogicConfigSObject>(assetPath);
            if (so == null)
            {
                Utility.UnityEditor.ShowConfirm("", "未找到此资产");
                return;
            }
            dataSo = so;
            if (dataSo == null) return;
            if (data.skillId != skillId)
            {
                Utility.UnityEditor.ShowConfirm("", "skillId 不一致 已经修改");
                data.skillId = skillId;
            }
            EditorUtility.SetDirty(so);

            AssetDatabase.SaveAssets();
            EditorSceneManager.SaveOpenScenes();
        }
        [LabelText("加载的数据")]
        public SkillLogicConfigSObject dataSo;
        [LabelText("当前skill数据")]
        [ShowInInspector]
        public SkillLogicConfig data
        {
            get => dataSo?.config;
            set
            {
                if (dataSo == null) return;

                dataSo.config = value;
            }
        }


        [FolderPath()]
        [LabelText("SO保存路径"), PropertyOrder(120)]
        public string soPath="Assets/Model/Cal/CalAssets/Ability/Skill";

        [FolderPath(AbsolutePath =true)]
        [LabelText("存档保存路径"), PropertyOrder(120)]
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+ "CTT/Skill/Skill";
        [FolderPath(AbsolutePath =true)]
        [LabelText("日志保存路径"), PropertyOrder(120)]
        public string logPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"CTT/Skill/Logs";
        [LabelText("文件名"), PropertyOrder(120)]
        public string fileName="SkillLogicConfig.json";
        [LabelText("二进制文件名"), PropertyOrder(120)]
        public string fileBytesName = "SkillLogicConfig.bytes";

        private string _key;
        [LabelText("密码"), PropertyOrder(120)]
        public string key => _key;
        //[Button("刷新密码"), PropertyOrder(120)]
        //private void RefreshPsd()
        //{
        //    var scene = EditorSceneManager.OpenScene("Assets/Res/Common/Init.unity");
        //    foreach (var go in scene.GetRootGameObjects())
        //    {
        //        if (go.name.Equals("Global"))
        //        {
        //            _key = go.GetComponent<Init>().XorKey;
        //            break;
        //        }
        //    }
        //    if (_key.IsNullOrWhitespace())
        //        throw new Exception("未找到密码");
        //}

        [Button("序列化"),PropertyOrder(-1)]
        [ButtonGroup]
        public void Serialized()
        {
            RegisterMongo();
            Utility.FileOpation.CreateDirectory(path);
            string newpath = Path.GetFullPath(Path.Combine(path, fileName));
            Utility.FileOpation.Delete(newpath);
            var data = CollectionBuff();
            var str = MongoHelper.ToJson(data);
            using (FileStream fs = new FileStream(newpath, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(str);
                }
            }
            newpath = Path.GetFullPath(Path.Combine("Assets/Download/Config", fileName));
            using (FileStream fs = new FileStream(newpath, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(str);
                }
            }
            Log.Info($"序列化成功:{str.Length/1000}K " + DateTime.Now);
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            string saveath = logPath;
            Utility.FileOpation.CreateDirectory(saveath);
            saveath += $"/SkillLogicConfig+{DateTime.Now:MM-dd + HH-mm-ss}.json";
            Utility.FileOpation.WriteString(saveath, str);
        }
        [Button("反序列化")]
        [ButtonGroup]
        public void Deserialized()
        {
            RegisterMongo();
            string newpath = Path.GetFullPath(Path.Combine(path, fileName));
            string str=File.ReadAllText(newpath);
            SkillLogicConfigCollection data = MongoHelper.FromJson<SkillLogicConfigCollection>(str);
            foreach (var item in data.skillDic)
            {
                string assetPath = $"{soPath}/{item.Key}.asset";
                SkillLogicConfigSObject so = AssetDatabase.LoadAssetAtPath<SkillLogicConfigSObject>(assetPath);
                so.config = item.Value;
                EditorUtility.SetDirty(so);
            }
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            Log.Info($"反序列化成功:{str.Length/1000}K " + DateTime.Now);
        }
        [Button("序列化为二进制")]
        [ButtonGroup]
        public void SerializedBytes()
        {
            RegisterMongo();
            string newpath = Path.GetFullPath(Path.Combine(path, fileBytesName));
            string serPath = Path.GetFullPath(Path.Combine("../Config/Skill", fileBytesName));
            Utility.FileOpation.Delete(newpath);
            Utility.FileOpation.Delete(serPath);
            var data = CollectionBuff();
            var bytes = MongoHelper.ToBson(data);
            //Utility.Encryption.GetSelfXorBytes(bytes, Encoding.UTF8.GetBytes(key));
            using (FileStream fs = new FileStream(newpath, FileMode.OpenOrCreate))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            if (!File.Exists(serPath))
            {
                var fs = File.Create(serPath);
                fs.Dispose();
            }
            using (FileStream fs = new FileStream(serPath, FileMode.OpenOrCreate))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            Log.Info($"序列化成功:{bytes.Length/1000}K " + DateTime.Now);
            AssetDatabase.Refresh();
        }
        [Button("从二进制反序列化")]
        [ButtonGroup]
        public void DeserializedBytes()
        {
            RegisterMongo();
            string newpath = Path.GetFullPath(Path.Combine(path, fileBytesName));
            var bytes = File.ReadAllBytes(newpath);
            Utility.Encryption.GetSelfXorBytes(bytes, Encoding.UTF8.GetBytes(key));
            //data = MongoHelper.FromBson<SkillLogicConfigCollection>(bytes);
            Log.Info($"反序列化成功 才怪" + DateTime.Now);
        }



        SkillLogicConfigCollection CollectionBuff()
        {
            SkillLogicConfigCollection data = new SkillLogicConfigCollection();
            var buffArr = Utility.FileOpation.GetFiles(soPath, "*.asset", SearchOption.AllDirectories);
            foreach (var item in buffArr)
            {
                string name = item.Name.Replace(".asset", string.Empty);
                if (!int.TryParse(name, out int id))
                {
                    Log.Error($"{name} cann't be Serialized");
                    continue;
                }
                SkillLogicConfigSObject so = AssetDatabase.LoadAssetAtPath<SkillLogicConfigSObject>(Utility.PathOption.DataPathToAssetPath(item.FullName));
                if (so == null)
                {
                    Log.Error($"so ==null where name = {item.FullName}");
                }
                data.skillDic.Add(id, so.config);
            }

            return data;
        }
        public static void RegisterMongo()
        {
            Type[] types = typeof(SkillLogicConfig).Assembly.GetTypes();
            foreach (Type type in types)
            {
                if (!type.IsSubclassOf(typeof(SelectTargetBase)) &&
                    !type.IsSubclassOf(typeof(SkillOptionBase)) 
                    )
                {
                    continue;
                }

                if (type.IsGenericType)
                {
                    continue;
                }

                try
                {
                    BsonClassMap.LookupClassMap(type);
                }
                catch (Exception e)
                {
                    Log.Error($"11: {type.Name} {e}");
                }
            }
        }
    }
#endif
}
