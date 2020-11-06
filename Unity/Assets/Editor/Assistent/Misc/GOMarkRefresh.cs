using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ET;
using System.Text;

namespace ETEditor
{
	public class GOMarkRefresh : MonoBehaviour
	{
		//private const string ITEM_NAME = "GameObject/@刷新 - Reference";

		//[MenuItem(ITEM_NAME, priority = 49)]
		//public static void Refresh()
		//{
		//	ReferenceCollector collector = Selection.activeGameObject.GetComponent<ReferenceCollector>();
		//	if (collector)
		//	{
		//		collector.Clear();
		//		//GOMark[] marks = collector.GetComponentsInChildren<GOMark>(true);
		//		var marks = collector.GetChildrenWithTag("UIComponent");
		//		for (int i = 0; i < marks.Length; i++)
		//		{
		//			collector.Add(StringUtility.ToVariableName(marks[i].name), marks[i].gameObject);
		//		}
		//	}
		//}
	}
}
