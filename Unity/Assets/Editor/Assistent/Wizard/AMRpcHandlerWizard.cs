using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using ET;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace ETEditor
{
	public class AMRpcHandlerWizard : ScriptableWizard
	{
		public string request;
		public string response;

		[Header("指定路劲")]
		[Tooltip("如果为空，使用默认路径")]
		public GlobalPath globalPath;

		private void OnWizardCreate()
		{
			if (string.IsNullOrWhiteSpace(request) || string.IsNullOrWhiteSpace(response))
			{
				return;
			}

			// 创建代码的文件夹
			GlobalSettingData globalSetting = MongoHelper.FromJson<GlobalSettingData>(EditorPrefs.GetString("GlobalSettingData"));
			string generatePath = globalSetting.AMHandlerPath;
			if (globalPath != GlobalPath.None)
			{
				generatePath = globalSetting.GlobalPaths[(int)globalPath];
			}

			string generateFolder = $"{Application.dataPath.Replace("Unity/Assets", "Server")}/Hotfix/{generatePath}";

			// 写入代码
			AssistentHelper.CheckDirectory(generateFolder);
			string scriptName = $"{request}Handler";
			using (FileStream fs = new FileStream($"{generateFolder}/{scriptName}.cs", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
				{
					// 替换模板代码的内容
					string code = ScriptTemplate.ET6AMRpcHanler;
					code = code.Replace("请求", request);
					code = code.Replace("响应", response);
					sw.Write(code);
				}
			}

			EditorPrefs.SetInt("AMRpcHandlerGlobalPath", (int)globalPath);
			AssetDatabase.Refresh();
		}

		[MenuItem("助手/Wizard/AMRpcHandler &q", priority = 5)]
		private static void CreateWizard()
		{
			AMRpcHandlerWizard rmRpcHandler = DisplayWizard<AMRpcHandlerWizard>("AMRpcHandler Script", "创建");
			rmRpcHandler.globalPath = (GlobalPath)EditorPrefs.GetInt("AMRpcHandlerGlobalPath");
		}
	}
}
