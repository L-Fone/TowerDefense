using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

namespace ETEditor
{
    public enum ToolKey
    {
         
        ���,
        ��Դ���� ,
        ���ܱ༭�� ,

    }


    public class ToolEditor : OdinMenuEditorWindow
    {

        public readonly static Dictionary<ToolKey, string> toolDic = new Dictionary<ToolKey, string>(){

        {ToolKey.���,"Assets/Model/Cal/CalAssets/BulildAB.asset"},
        {ToolKey.��Դ���� ,"Assets/Model/Cal/CalAssets/ResTool.asset"},
        {ToolKey.���ܱ༭��,"Assets/Model/Cal/CalAssets/Skill And Modifer S Object.asset"},
        };
        [MenuItem("Tool Singlton/Tools Window #o"),]
        private static void OpenEditor()
        {
            var window = GetWindow<ToolEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 700);
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            foreach (var kp in toolDic)
            {
                tree.AddAssetAtPath(kp.Key.ToString(), kp.Value.Substring(kp.Value.IndexOf("Assets/")+7)).AddIcon(EditorIcons.Airplane);
            }

            return tree;

        }
    }
}