using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ET
{
  
    public class TilemapTest : Sirenix.OdinInspector.SerializedMonoBehaviour
    {
        public enum PointType { Path,Tower,InitPos,EndPos}

        public PointType pointType;

        private LevelInfo info;


        private bool isRun;
        private int index = 0;
        private Transform tran;
        private bool isTurn;

        private void Start()
        {
            var go = GameObject.Find("Role");
            tran = go.transform;
            string path = $"Assets/Download/Config/Levels/{SceneManager.GetActiveScene().name}.json";
            var str = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            info = MongoHelper.FromJson<LevelInfo>(str.text);
            isRun = true;
            tran.position = info.initPos.ToUnityVector3();
        }

        [Button()]
        private void LoadConfig()
        {
            string path = $"Assets/Download/Config/Levels/{SceneManager.GetActiveScene().name}.json";
            var str = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            info = MongoHelper.FromJson<LevelInfo>(str.text);
        }

        private void Update()
        {
            
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (!Input.GetMouseButtonDown(0))
                    return;
                Camera cam = Camera.main;
                Vector3 mousepos = Input.mousePosition;
                Ray ray = cam.ScreenPointToRay(mousepos);

                var point = ray.GetPoint(-ray.origin.z / ray.direction.z);
                if(info==null)
                {
                    Log.Info($"info==null");
                    info = new LevelInfo();
                }
                Log.Info($"{info}");
                switch (pointType)
                {
                    case PointType.Path:
                        info.path.Add(new System.Numerics.Vector3(point.x,point.y,point.z));
                        break;
                    case PointType.Tower:
                        info.towerList.Add(new System.Numerics.Vector3(point.x,point.y,point.z));
                        break;
                    case PointType.InitPos:
                        info.initPos = new System.Numerics.Vector3(point.x,point.y,point.z);
                        break;
                    case PointType.EndPos:
                        info.endPos = new System.Numerics.Vector3(point.x,point.y,point.z);
                        break;
                    default:
                        break;
                }
            }
        }

        [Button()]
        private void Save()
        {
            if (info == null) return;
            var str = MongoHelper.ToJson(info);
            string path = $"Assets/Download/Config/Levels/{SceneManager.GetActiveScene().name}.json";
            Utility.FileOpation.Delete(path);
            var fs = Utility.FileOpation.Create(path);
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(str);
            }
            AssetDatabase.Refresh();
        }
    }
}
