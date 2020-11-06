using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Cal
{
    [CreateAssetMenu]
	public class TeamplateTool:Sirenix.OdinInspector.SerializedScriptableObject
	{
		public TextAsset templateTxt;

		public enum ClassType {UI,UIEvent}

		public ClassType classType;

		[ShowIf("classType",ClassType.UI)]
		public string Name;

        [ShowIf("classType", ClassType.UIEvent)]
        public string FuiName;

        [ShowIf("classType", ClassType.UIEvent)]
        public string FuiClassName;

		public string pathRoot;

		public string pathName;

		[Button("生成UI")]
		public void GenerateUI()
        {
			string str = templateTxt.text;

        }
        [Button("生成UIEvent")]
        public void GenerateUIEvent()
        {
            string str = templateTxt.text;

        }
    }
}
