using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using MongoDB.Bson;
using UnityEngine;

namespace ET
{
    [TypeDrawer]
    public class ListTypeDrawer: ITypeDrawer
    {
        public bool HandlesType(Type type)
        {
            return type.IsGenericType;
        }

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            string str = value?.ToCustomString();
            return EditorGUILayout.DelayedTextField(memberName + ":\n" + str, GUILayout.Height((str == null ? 2 : str.Length / 30 + 2) * 20));
        }
    }
}