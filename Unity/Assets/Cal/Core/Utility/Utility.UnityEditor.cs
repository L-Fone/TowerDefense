//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ET
{
    public static partial class Utility
    {
        public static class UnityEditor
        {
#if UNITY_EDITOR
            public static void ShowTip(string title, string msg, string okText = "确定")
            {
                if (EditorUtility.DisplayDialog(title, msg, okText))
                {

                }
            }
            public static bool ShowConfirm(string title, string msg, string cancelButtonText = "取消", string okButtonText = "确定")
            {
                if (EditorUtility.DisplayDialog(title, msg, okButtonText, cancelButtonText))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
#endif
        }
    }
}
    

