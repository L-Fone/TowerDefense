using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using ET;
using System.Text.RegularExpressions;
using System.Text;

namespace ETEditor
{
	public class AEventWizard : ScriptableWizard
	{
		public bool isHotfix = true;
		public string eventName;

		[Header("指定路劲")]
		[Tooltip("如果为空，使用默认路径")]
		public GlobalPath globalPath;

		private void OnWizardCreate()
		{
			if (string.IsNullOrEmpty(eventName))
			{
				return;
			}

			// 创建代码的文件夹
			GlobalSettingData globalSetting = MongoHelper.FromJson<GlobalSettingData>(EditorPrefs.GetString("GlobalSettingData"));
			string generatePath = globalSetting.AEventPath;
			if (globalPath != GlobalPath.None)
			{
				generatePath = globalSetting.GlobalPaths[(int)globalPath];
			}

			string folderName = $"Hotfix/{generatePath}";
			if (isHotfix == false)
			{
				folderName = $"Model/{generatePath}";
			}
			AssistentHelper.CheckDirectory($"{Application.dataPath}/{folderName}/");

			// 匹配EventIdType
			bool needWriteEventIdType = true;
			string mainFoler = isHotfix ? "Hotfix" : "Model";
			string eventIdTypePath = $"{Application.dataPath}/{mainFoler}/Base/Event/EventIdType.cs";
			string eventIdTypeText = File.ReadAllText(eventIdTypePath);
			string pattern = "public const string ([A-Za-z0-9_]+) = \"([A-Za-z0-9_]+)\"";
			MatchCollection matchs = Regex.Matches(eventIdTypeText, pattern);
			for (int i = 0; i < matchs.Count; i++)
			{
				string matchName = matchs[i].Groups[1].Value;
				if (matchName == eventName)
				{
					needWriteEventIdType = false;
				}
			}

			// 写入EventIdType
			if (needWriteEventIdType)
			{
				using (FileStream fs = new FileStream(eventIdTypePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
					{
						StringBuilder sb = new StringBuilder();

						// 把原来的写上
						for (int i = 0; i < matchs.Count; i++)
						{
							string matchName = matchs[i].Groups[1].Value;
							sb.Append($"public const string {matchName} = \"{matchName}\";\r\n\t\t");
						}

						// 加上当前UI
						sb.Append($"public const string {eventName} = \"{eventName}\";");

						// 写入
						string code = ScriptTemplate.EventIdType;
						code = code.Replace("命名空间", $"ET{mainFoler}");
						code = code.Replace("事件类型", sb.ToString());
						sw.Write(code);
					}
				}
			}

			// 写入代码
			string scriptName = $"{eventName}Event";
			using (FileStream fs = new FileStream($"{Application.dataPath}/{folderName}/{scriptName}.cs", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
				{
					// 替换模板代码的内容
					string code = ScriptTemplate.AEvent;
					code = code.Replace("脚本名", scriptName);
					code = code.Replace("事件类型", eventName);

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

			EditorPrefs.SetInt("AEventGlobalPath", (int)globalPath);
			AssetDatabase.Refresh();
		}

		[MenuItem("助手/Wizard/事件 &3", priority = 3)]
		private static void CreateWizard()
		{
			AEventWizard eventWizard = ScriptableWizard.DisplayWizard<AEventWizard>("AEvent Script", "创建");
			eventWizard.globalPath = (GlobalPath)EditorPrefs.GetInt("AEventGlobalPath");
		}
	}
}
