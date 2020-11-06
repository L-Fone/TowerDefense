using System;
using System.Collections.Generic;
using System.IO;
using ET;
using ET;
using UnityEditor;
using UnityEngine;

namespace ETEditor
{
	public class DeleteAllAssetBundlesEditor: EditorWindow
	{

		[MenuItem("Tools/Del All assetbundle")]
		private static void DelAllAssetBundles()
		{
			//UnityEngine.Object[] obj = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
            string[] strs = Selection.assetGUIDs;

            string path = AssetDatabase.GUIDToAssetPath(strs[0]);
			path = Path.Combine(Application.dataPath.Replace("Assets",""), path);
			DirectoryInfo info = new DirectoryInfo(path);
			int count = 0;
            foreach (var item in info.GetFiles("*",SearchOption.AllDirectories))
            {
				if(item.FullName.EndsWith(".assetbundle") || item.FullName.EndsWith(".assetbundle.meta"))
                {
					count++;
					File.Delete(item.FullName);
                }
            }
			Log.Info($"删除了{count}个");
        }

	}
}