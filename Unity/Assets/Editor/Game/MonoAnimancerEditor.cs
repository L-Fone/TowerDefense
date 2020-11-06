using System;
using System.Collections.Generic;
using ET;
using UnityEditor;
using UnityEngine;

namespace Animancer.Editor
{
    [CustomEditor(typeof(MonoAnimancer), true), CanEditMultipleObjects]
    public class MonoAnimancerEditor : AnimancerComponentEditor
    {
        private bool _ShowResetOnDisableWarning;
        protected override bool DoOverridePropertyGUI(string path, SerializedProperty property, GUIContent label)
        {
            if (path == "_Clips")
            {
                EditorGUILayout.PropertyField(property, label, true);

                for (int i = 0; i < property.arraySize; i++)
                {
                    var clipInof = property.GetArrayElementAtIndex(i);
                    var clipGo = clipInof.FindPropertyRelative("AnimationClip").objectReferenceValue;
                    if (clipGo == null) continue;
                    GetKey(clipInof, clipGo.name);
                }
                return true;
            }
            return base.DoOverridePropertyGUI(path, property, label);
        }
        private void GetKey(SerializedProperty clipInof, string clipName)
        {
            if (clipName.Contains("Idle"))
            {
                clipInof.FindPropertyRelative("Key").enumValueIndex = (int)AnimationKey.Idle;
            }
            else if (clipName.Contains("Run"))
            {
                clipInof.FindPropertyRelative("Key").enumValueIndex = (int)AnimationKey.Run;
            }
            else if (clipName.Contains("Attack"))
            {
                clipInof.FindPropertyRelative("Key").enumValueIndex = (int)AnimationKey.Atk;
            }
            else if (clipName.Contains("Hurt")
                )
            {
                clipInof.FindPropertyRelative("Key").enumValueIndex = (int)AnimationKey.Hurt;
            }
            else
                clipInof.FindPropertyRelative("Key").enumValueIndex = (int)AnimationKey.Other;
        }
        private void MoveResettingTargetsAboveTheirAnimator()
        {
            throw new NotImplementedException();
        }

        private bool AreAllResettingTargetsAboveTheirAnimator()
        {
            throw new NotImplementedException();
        }
    }
}
