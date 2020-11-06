using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace ET
{
	public static class AssistentHelper
	{
		public static void CheckDirectory(string dir)
		{
			if (Directory.Exists(dir) == false)
			{
				Directory.CreateDirectory(dir);
			}
		}

		public static void CheckFile(string path)
		{
			if (File.Exists(path) == false)
			{
				FileStream fs = File.Create(path);
				fs.Close();
				AssetDatabase.Refresh();
			}
		}
	}
}