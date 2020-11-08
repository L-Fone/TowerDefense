using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ET
{
    [InitializeOnLoad]
    public static class EditorClickChelper
    {
        static EditorClickChelper()
        {
            EditorApplication.update += Update;
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private static Tilemap tilemap;
        private static void OnSceneGUI(SceneView sceneView)
        {
            //if (Event.current.type == UnityEngine.EventType.MouseDown)
            //{
            //    Camera cam = sceneView.camera;
            //    Vector3 mousepos = Event.current.mousePosition;
            //    //mousepos.z = -cam.worldToCameraMatrix.MultiplyPoint(parent.position).z;
            //    //mousepos.y = cam.pixelHeight - mousepos.y;
            //    //var point = cam.ScreenToWorldPoint(mousepos);
            //    Ray ray = cam.ScreenPointToRay(mousepos);
            //    if (tilemap == null)
            //    {
            //        tilemap = GameObject.Find("Grid_Level1").GetComponentInChildren<Tilemap>();
            //        if (tilemap == null)
            //            return;
            //    }
            //    var point = ray.GetPoint(-ray.origin.z / ray.direction.z);
            //    //point.z = 0;
            //    GameObject gameObject = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Download/Texture/Role/图层 45.prefab"),GameObject.Find("GameObject (1)").transform).As<GameObject>();
            //    gameObject.transform.position =point;
            //    Log.Info($"{point}");
            //}

        }

        private static void Update()
        {

        }
    }

    public class TilemapEditor : Sirenix.OdinInspector.SerializedScriptableObject
    {


    }
}
