//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-06-11 12:53:31
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UI;

public static class GameUtil
{
    /// <summary>
    /// 添加子物体
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject AddChild(this Transform parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;

        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.SetParent(parent, false);
            go.layer = parent.gameObject.layer;
        }
        return go;
    }
    public static GameObject[] GetChildrenWithTag(this Component component,string tag)
    {
        List<GameObject> list = new List<GameObject>();
        var arr = component.GetComponentsInChildren<Transform>();
        foreach (Transform child in arr)
        {
            if (child.gameObject.CompareTag(tag))
            {
                list.Add(child.gameObject);
            }
        }
        return list.ToArray();
    }
    public static Transform[] GetChildren(this Component component)
    {
        Transform transform = component.transform;
        var arr = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            arr[i]=transform.GetChild(i);
        }
        return arr;
    }

    
}