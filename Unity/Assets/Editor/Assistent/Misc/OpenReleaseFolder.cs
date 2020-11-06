using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace ETEditor
{
	public class OpenReleaseFolder
	{
		[MenuItem("助手/打开Release #r", priority = 4)]
		private static void Open()
		{
			Application.OpenURL($"file://{Path.Combine(Application.dataPath, "../../Release")}");
		}
        [MenuItem("助手/打开PersistentDataPath #p", priority = 5)]
        private static void OpenPersistentDataPath()
        {
            Application.OpenURL($"file://{Application.persistentDataPath}");
        }
    }
}
