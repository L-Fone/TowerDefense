//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using ET;
//using UnityEditor;
//using System.Text.RegularExpressions;
//using System.IO;
//using System.Text;
//using UnityEngine.UI;

//namespace ETEditor
//{
//	public class GenerateUICode
//	{
//		public const string ITEM_NAME = "Assets/生成 - UI";

//		[MenuItem(ITEM_NAME, false, priority = 10001)]
//		public static void Generate()
//		{
//			string path = AssetDatabase.GetAssetPath(Selection.activeGameObject);

//			// 匹配对象名
//			string gameObjectName = GetGameObjectName(path);
//			if (string.IsNullOrWhiteSpace(gameObjectName)) return;

//			// 写入代码
//			string uiPath = MongoHelper.FromJson<GlobalSettingData>(EditorPrefs.GetString("GlobalSettingData")).UIPath;
//			AssistentHelper.CheckDirectory($"{Application.dataPath}/Hotfix/{uiPath}/");
//			AssistentHelper.CheckDirectory($"{Application.dataPath}/Hotfix/{uiPath}/Reference");
//			string UIPath = $"{Application.dataPath}/Hotfix/{uiPath}/{gameObjectName}.cs";
//			string referencePath = $"{Application.dataPath}/Hotfix/{uiPath}/Reference/{gameObjectName}Reference.cs";

//			// 实体代码
//			if (File.Exists(UIPath) == false)
//			{
//				using (FileStream fs = new FileStream(UIPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
//				{
//					using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
//					{
//						// 替换模板代码的内容
//						string code = ScriptTemplate.UIScript;
//						code = code.Replace("类名", gameObjectName);
//						sw.Write(code);
//					}
//				}
//			}

//			// 引用代码
//			ReferenceCollector collector = Selection.activeGameObject.GetComponent<ReferenceCollector>();
//			if (collector)
//			{
//				// 刷新ReferenceCollector
//				collector.Clear();
//				//GOMark[] marks = collector.GetComponentsInChildren<GOMark>(true);
//				var marks = collector.GetChildrenWithTag("UIComponent");
//				for (int i = 0; i < marks.Length; i++)
//				{
//					collector.Add(StringUtility.ToVariableName(marks[i].name), marks[i].gameObject);
//				}

//				// 生成代码
//				AssistentHelper.CheckFile(referencePath);
//				using (FileStream fs = new FileStream(referencePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite))
//				{
//					using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
//					{
//						// 替换模板代码的内容
//						string code = ScriptTemplate.UIReference;
//						code = code.Replace("类名", gameObjectName);

//						// 生成引用代码
//						StringBuilder componentCode = new StringBuilder();
//						StringBuilder referenceCode = new StringBuilder();
//						foreach (var d in collector.data)
//						{
//							if (d.gameObject is GameObject == false) continue;

//							string componentName = GetCompoenntName(d.gameObject as GameObject);
//							componentCode.Append($"public {componentName} {d.key};\r\n\t\t");
							
//							if (componentName == "GameObject")
//							{
//								referenceCode.Append($"{d.key} = Collector.Get<GameObject>(\"{d.key}\");\r\n\t\t\t");
//							}
//							else
//							{
//								referenceCode.Append($"{d.key} = Collector.Get<GameObject>(\"{d.key}\").GetComponent<{componentName}>();\r\n\t\t\t");
//							}
//						}

//						code = code.Replace("组件", componentCode.ToString().Trim());
//						code = code.Replace("获取引用", referenceCode.ToString().Trim());

//						sw.Write(code);
//					}
//				}
//			}

//			// 匹配UIType
//			bool needWriteUIType = true;
//			string uiTypePath = $"{Application.dataPath}/Hotfix/Module/UI/UIType.cs";
//			string uiTypeText = File.ReadAllText(uiTypePath);
//			string pattern = "public const string ([A-Za-z0-9_]+) = \"([A-Za-z0-9_]+)\"";
//			MatchCollection matchs = Regex.Matches(uiTypeText, pattern);

//			// 检查是否需要写入
//			for (int i = 0; i < matchs.Count; i++)
//			{
//				string matchName = matchs[i].Groups[1].Value;
//				if (matchName == gameObjectName)
//				{
//					needWriteUIType = false;
//				}
//			}

//			// 写入UIType
//			if (needWriteUIType)
//			{
//				using (FileStream fs = new FileStream(uiTypePath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite))
//				{
//					using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
//					{
//						StringBuilder sb = new StringBuilder();

//						// 把原来的写上
//						for (int i = 0; i < matchs.Count; i++)
//						{
//							string matchName = matchs[i].Groups[1].Value;
//							sb.Append($"public const string {matchName} = \"{matchName}\";\r\n\t\t");
//						}

//						// 加上当前UI
//						sb.Append($"public const string {gameObjectName} = \"{gameObjectName}\";");

//						// 写入
//						string code = ScriptTemplate.UIType;
//						code = code.Replace("面板", sb.ToString());
//						sw.Write(code);
//					}
//				}
//			}

//			AssetDatabase.Refresh();
//		}

//		public static string GetCompoenntName(GameObject go)
//		{
//			// UI
//			if (go.GetComponent<DoubleClickButton>())
//			{
//				return "DoubleClickButton";
//			}
//			else if (go.GetComponent<Button>())
//			{
//				return "Button";
//			}
//			else if (go.GetComponent<InputField>())
//			{
//				return "InputField";
//			}
//			else if (go.GetComponent<Text>())
//			{
//				return "Text";
//			}
//			else if (go.GetComponent<Image>())
//			{
//				return "Image";
//			}
//			else if (go.GetComponent<RawImage>())
//			{
//				return "RawImage";
//			}
//			else if (go.GetComponent<Toggle>())
//			{
//				return "Toggle";
//			}
//			else if (go.GetComponent<Slider>())
//			{
//				return "Slider";
//			}
//			else if (go.GetComponent<Scrollbar>())
//			{
//				return "Scrollbar";
//			}
//			else if (go.GetComponent<Dropdown>())
//			{
//				return "Dropdown";
//			}
//			else
//			{
//				return "GameObject";
//			}
//		} 

//		public static string GetGameObjectName(string path)
//		{
//			string pattern = @"/([^/]+)\.prefab";
//			Match match = Regex.Match(path, pattern);
//			if (match.Success == false)
//			{
//				return "";
//			}
//			return match.Groups[1].Value;
//		}

//		[MenuItem(ITEM_NAME, true, priority = 10001)]
//		public static bool GenerateCheck()
//		{
//			string path = AssetDatabase.GetAssetPath(Selection.activeGameObject);
//			Menu.SetChecked(ITEM_NAME, File.Exists($"{Application.dataPath}/Hotfix/{MongoHelper.FromJson<GlobalSettingData>(EditorPrefs.GetString("GlobalSettingData")).UIPath}/{GetGameObjectName(path)}.cs"));
//			if (string.IsNullOrWhiteSpace(path) || Selection.activeGameObject.GetComponent<ReferenceCollector>() == false)
//			{
//				return false;
//			}
//			return true;
//		}
//	}
//}
