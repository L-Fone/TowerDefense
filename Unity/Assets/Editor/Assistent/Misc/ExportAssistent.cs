using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

namespace ETEditor
{
	public static class ExportAssistent
	{
		[MenuItem("助手/导出ET助手 #e", priority = 1)]
		private static void ExportAssitentPackage()
		{
			string[] assetPath =
			{
				"Assets/Editor/Assistent",
				"Assets/Hotfix/Assistent",
				"Assets/Model/Assistent",
				"Assets/Res/Assistent",
				"Assets/Editor/ReferenceCollectorEditor"
			};
			string packageName = $"ETAssistent_{DateTime.Now.ToString("yyyyMMdd_HHmm")}.unitypackage";
			AssetDatabase.ExportPackage(assetPath, packageName, ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse);
			Application.OpenURL($"file://{Path.Combine(Application.dataPath, "../")}");
		}
	}
}