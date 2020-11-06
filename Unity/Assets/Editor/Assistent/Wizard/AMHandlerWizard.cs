using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using ET;
using System;

namespace ETEditor
{
	public class AMHandlerWizard : ScriptableWizard
	{
		public bool isServer = true;
		public string message;

		[Header("指定路劲")]
		[Tooltip("如果为空，使用默认路径")]
		public GlobalPath globalPath;

		private void OnWizardCreate()
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				return;
			}

			// 创建代码的子文件夹
			GlobalSettingData globalSetting = MongoHelper.FromJson<GlobalSettingData>(EditorPrefs.GetString("GlobalSettingData"));
			string subFolder = globalSetting.AMHandlerPath;
			if (globalPath != GlobalPath.None)
			{
				subFolder = globalSetting.GlobalPaths[(int)globalPath];
			}

			// 生成路径
			string generateFolder = $"{Application.dataPath.Replace("Unity/Assets", "Server")}/Hotfix/{subFolder}";
			if (isServer == false)
			{
				generateFolder = $"{Application.dataPath}/Hotfix/{subFolder}";
			}

			// 写入代码
			AssistentHelper.CheckDirectory(generateFolder);
			string scriptName = $"{message}Handler";
			using (FileStream fs = new FileStream($"{generateFolder}/{scriptName}.cs", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
				{
					// 替换模板代码的内容
					string code = ScriptTemplate.AMHandler;
					code = code.Replace("消息类型", message);
					sw.Write(code);
				}
			}

			EditorPrefs.SetInt("AMHandlerGlobalPath", (int)globalPath);
			AssetDatabase.Refresh();
		}

		[MenuItem("助手/Wizard/AMHandler &4", priority = 4)]
		private static void CreateWizard()
		{
			AMHandlerWizard amHandler = ScriptableWizard.DisplayWizard<AMHandlerWizard>("AMHandler Script", "创建");
			amHandler.globalPath = (GlobalPath)EditorPrefs.GetInt("AMHandlerGlobalPath");
		}
	}
}