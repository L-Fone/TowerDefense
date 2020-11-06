using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using ET;
using System;
using UnityEngine.SceneManagement;

namespace ETEditor
{
	public enum GlobalPath
	{
		None,
		Lockstep,
		LockstepEntity,
		LockstepHandler,
		LockstepEvent,
		LockstepFactory,
	}

	public class GlobalSettingData
	{
		public string InitSceneName = "Init";
		public bool IsLoadInitScene = true;
		public string NormalPath = "Game";
		public string EntityPath = "Game/Entity";
		public string AEventPath = "Game/Event";
		public string UIPath = "Game/UI";
		public string AMHandlerPath = "Game/Handler";
		public string[] GlobalPaths;
	}

	public class GlobalSettingWindow : EditorWindow
	{
		private GlobalSettingData data;

		private void OnEnable()
		{
			this.titleContent = new GUIContent("全局设置");
			if (string.IsNullOrWhiteSpace(EditorPrefs.GetString("GlobalSettingData", null)))
			{
				CreateNewData();
				Save();
			}
			else
			{
				data = MongoHelper.FromJson<GlobalSettingData>(EditorPrefs.GetString("GlobalSettingData", null));
			}
		}

		private void OnGUI()
		{
			GUILayout.BeginVertical("box", GUILayout.Width(500));
			{
				GUILayout.BeginVertical("box");
				{
					// hotfix entity generate path
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("初始场景:", GUILayout.Width(100));
						data.InitSceneName = GUILayout.TextField(data.InitSceneName, GUILayout.Width(400));
					}
					GUILayout.EndHorizontal();

					// model entity generate path
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("加载初始场景:", GUILayout.Width(100));
						data.IsLoadInitScene = EditorGUILayout.Toggle(data.IsLoadInitScene, GUILayout.Width(400));
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndVertical();

				// 一般路径
				GUILayout.BeginVertical("box");
				{
					GUILayout.BeginHorizontal();
					{
						GUILayout.FlexibleSpace();
						GUILayout.Label("脚本生成");
						GUILayout.FlexibleSpace();
					}
					GUILayout.EndHorizontal();

					// model entity generate path
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("一般脚本路径:", GUILayout.Width(100));
						data.NormalPath = GUILayout.TextField(data.NormalPath, GUILayout.Width(400));
					}
					GUILayout.EndHorizontal();

					// hotfix entity generate path
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("实体生成路径:", GUILayout.Width(100));
						data.EntityPath = GUILayout.TextField(data.EntityPath, GUILayout.Width(400));
					}
					GUILayout.EndHorizontal();

					// AEvent generate path
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("事件脚本路径:", GUILayout.Width(100));
						data.AEventPath = GUILayout.TextField(data.AEventPath, GUILayout.Width(400));
					}
					GUILayout.EndHorizontal();

					// ui code generate path
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("UI路径:", GUILayout.Width(100));
						data.UIPath = GUILayout.TextField(data.UIPath, GUILayout.Width(400));
					}
					GUILayout.EndHorizontal();

					// AMRpcHanler
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("AMHanler:", GUILayout.Width(100));
						data.AMHandlerPath = GUILayout.TextField(data.AMHandlerPath, GUILayout.Width(400));
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndHorizontal();

				// 全局路径
				GUILayout.BeginVertical("box");
				{
					GUILayout.BeginHorizontal();
					{
						GUILayout.FlexibleSpace();
						GUILayout.Label("全局路径");
						GUILayout.FlexibleSpace();
					}
					GUILayout.EndHorizontal();

					// 遍历 GlobalPath
					string[] globalGlobalPaths = Enum.GetNames(typeof(GlobalPath));
					try
					{
						for (int i = 1; i < globalGlobalPaths.Length; i++)
						{
							GUILayout.BeginHorizontal();
							{
								GUILayout.Label(globalGlobalPaths[i], GUILayout.Width(100));
								data.GlobalPaths[i] = GUILayout.TextField(data.GlobalPaths[i], GUILayout.Width(400));
							}
							GUILayout.EndHorizontal();
						}
					}
					catch (Exception)
					{
						CreateNewData();
					}
				}
				GUILayout.EndHorizontal();

				// save button
				if (GUILayout.Button("保存设置"))
				{
					Save();
				}
			}
		}

		private void CreateNewData()
		{
			data = new GlobalSettingData();
			Array arr = Enum.GetValues(typeof(GlobalPath));
			data.GlobalPaths = new string[arr.Length];
			for (int i = 0; i < arr.Length; i++)
			{
				data.GlobalPaths[i] = "";
			}
			Save();
		}

		private void Save()
		{
			AssistentHelper.CheckDirectory(@"./Assets/Res/Assistent/Config/");
			EditorPrefs.SetString("GlobalSettingData", MongoHelper.ToJson(data));
			AssetDatabase.Refresh();
		}

		[RuntimeInitializeOnLoadMethod]
		private static void LoadInitScene()
		{
			if (EditorPrefs.GetString("GlobalSettingData") != null)
			{
				GlobalSettingData data = MongoHelper.FromJson<GlobalSettingData>(EditorPrefs.GetString("GlobalSettingData"));
				if (data.IsLoadInitScene == true && string.IsNullOrEmpty(data.InitSceneName) == false)
				{
					if (SceneManager.GetActiveScene().name != data.InitSceneName)
					{
						SceneManager.LoadScene(data.InitSceneName);
					}
				}
			}
		}

		[MenuItem("助手/全局设置 #g", priority = 0)]
		private static void ShowWindow()
		{
			GlobalSettingWindow window = EditorWindow.GetWindow<GlobalSettingWindow>() as GlobalSettingWindow;
			window.Show();
		}
	}
}
