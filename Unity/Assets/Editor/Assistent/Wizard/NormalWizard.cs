using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using ET;

namespace ETEditor
{
	public class NormalWizard : ScriptableWizard
	{
		public bool isServer = false;
		public bool isHotfix = true;
		public string scriptName = "";

		[Header("指定路劲")]
		[Tooltip("如果为空，使用默认路径")]
		public GlobalPath globalPath;

		private void OnWizardCreate()
		{
			if (string.IsNullOrWhiteSpace(scriptName))
			{
				return;
			}

			// 创建代码的文件夹
			GlobalSettingData globalSetting = MongoHelper.FromJson<GlobalSettingData>(EditorPrefs.GetString("GlobalSettingData"));
			string generatePath = globalSetting.NormalPath;
			if (globalPath != GlobalPath.None)
			{
				generatePath = globalSetting.GlobalPaths[(int)globalPath];
			}

			string folderName = $"Hotfix/{generatePath}";
			if (isHotfix == false)
			{
				folderName = $"Model/{generatePath}";
			}

			string generateFolder = $"{Application.dataPath}/{folderName}";
			if (isServer == true)
			{
				generateFolder = $"{Application.dataPath.Replace("Unity/Assets", "Server")}/{folderName}";
			}

			// 写入代码
			AssistentHelper.CheckDirectory(generateFolder);
			using (FileStream fs = new FileStream($"{generateFolder}/{scriptName}.cs", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
				{
					// 替换模板代码的内容
					string code = ScriptTemplate.Normal;
					code = code.Replace("类名", scriptName);

					string currentNamespace = "ETHotfix";
					string otherNamespace = "ET";
					if (isHotfix == false)
					{
						string temp = currentNamespace;
						currentNamespace = otherNamespace;
						otherNamespace = temp;
					}
					code = code.Replace("当前命名空间", currentNamespace);
					code = code.Replace("另一个命名空间", otherNamespace);

					sw.Write(code);
				}
			}

			EditorPrefs.SetInt("NormalGlobalPath", (int)globalPath);
			AssetDatabase.Refresh();
		}

		[MenuItem("助手/Wizard/普通 &1", priority = 1)]
		private static void CreateWizard()
		{
			NormalWizard normal = ScriptableWizard.DisplayWizard<NormalWizard>("Normal Script", "创建");
			normal.globalPath = (GlobalPath)EditorPrefs.GetInt("NormalGlobalPath");
		}
	}
}