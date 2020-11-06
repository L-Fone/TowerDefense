using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

using UnityEditor;
using UnityEngine;

namespace ET
{
    [CustomEditor(typeof(ComponentView))]
    public class ComponentViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ComponentView componentView = (ComponentView)target;
            object component = componentView.Component;
            ComponentViewHelper.Draw(component);
        }
    }

    public static class ComponentViewHelper
    {
        private static readonly List<ITypeDrawer> typeDrawers = new List<ITypeDrawer>();

        static ComponentViewHelper()
        {
            Assembly assembly = typeof(ComponentViewHelper).Assembly;
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsDefined(typeof(TypeDrawerAttribute)))
                {
                    continue;
                }

                ITypeDrawer iTypeDrawer = (ITypeDrawer)Activator.CreateInstance(type);
                typeDrawers.Add(iTypeDrawer);
            }
        }
        static bool openField = true;
        static bool openProperty = true;
        public static void Draw(object obj)
        {
            try
            {
                FieldInfo[] fields = obj.GetType()
                        .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                PropertyInfo[] properties = obj.GetType().GetProperties();

                openField = EditorGUILayout.Foldout(openField, "×Ö¶Î", true);
                if (openField)
                    foreach (FieldInfo fieldInfo in fields)
                    {
                        Type type = fieldInfo.FieldType;
                        if (type.IsDefined(typeof(HideInInspector), false))
                        {
                            continue;
                        }

                        if (fieldInfo.IsDefined(typeof(HideInInspector), false))
                        {
                            continue;
                        }

                        object value = fieldInfo.GetValue(obj);

                        foreach (ITypeDrawer typeDrawer in typeDrawers)
                        {
                            if (!typeDrawer.HandlesType(type))
                            {
                                continue;
                            }

                            string fieldName = fieldInfo.Name;
                            if (fieldName.Length > 17 && fieldName.Contains("k__BackingField"))
                            {
                                fieldName = fieldName.Substring(1, fieldName.Length - 17);
                            }
                            value = typeDrawer.DrawAndGetNewValue(type, fieldName, value, null);
                            fieldInfo.SetValue(obj, value);
                            break;
                        }
                        DrawParent(value, fieldInfo);
                    }
                openProperty = EditorGUILayout.Foldout(openProperty, "ÊôÐÔ");
                if (openProperty)
                    foreach (PropertyInfo fieldInfo in properties)
                    {
                        Type type = fieldInfo.PropertyType;
                        if (type.IsDefined(typeof(HideInInspector), false))
                        {
                            continue;
                        }

                        if (fieldInfo.IsDefined(typeof(HideInInspector), false))
                        {
                            continue;
                        }

                        object value = fieldInfo.GetValue(obj);

                        foreach (ITypeDrawer typeDrawer in typeDrawers)
                        {
                            if (!typeDrawer.HandlesType(type))
                            {
                                continue;
                            }

                            string fieldName = fieldInfo.Name;
                            if (fieldName.Length > 17 && fieldName.Contains("k__BackingField"))
                            {
                                fieldName = fieldName.Substring(1, fieldName.Length - 17);
                            }
                            value = typeDrawer.DrawAndGetNewValue(type, fieldName, value, null);
                            //fieldInfo.SetValue(obj, value);
                            break;
                        }
                    }

            }
            catch (Exception e)
            {
                Log.Error($"component view error: {obj.GetType().FullName}\n{e}");
            }

        }
        static bool openScene;
        static bool openParent;
        private static void DrawParent(object obj, FieldInfo _fieldInfo)
        {
            if (obj == null) return;
            var fieldType = obj.GetType();
            string name = _fieldInfo.Name;
            if(name.Equals("parent"))
            {
                FieldInfo[] fields =fieldType
                               .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                openParent = EditorGUILayout.Foldout(openParent, "Parent");
                if (openParent)
                {
                    GUI.contentColor = new Color(0.9f, 0.5f, 0);
                    EditorGUILayout.TextField("Name", fieldType.Name);
                    foreach (FieldInfo fieldInfo in fields)
                    {
                        Type type = fieldInfo.FieldType;
                        if (type.IsDefined(typeof(HideInInspector), false))
                        {
                            continue;
                        }

                        if (fieldInfo.IsDefined(typeof(HideInInspector), false))
                        {
                            continue;
                        }

                        object value = fieldInfo.GetValue(obj);

                        foreach (ITypeDrawer typeDrawer in typeDrawers)
                        {
                            if (!typeDrawer.HandlesType(type))
                            {
                                continue;
                            }

                            string fieldName = fieldInfo.Name;
                            if (fieldName.Length > 17 && fieldName.Contains("k__BackingField"))
                            {
                                fieldName = fieldName.Substring(1, fieldName.Length - 17);
                            }
                            value = typeDrawer.DrawAndGetNewValue(type, fieldName, value, null);
                            fieldInfo.SetValue(obj, value);
                            break;
                        }
                    }
                }
                    
            }
            else if (name.Equals("domain"))
            {
                FieldInfo[] fields = fieldType
                               .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                openScene = EditorGUILayout.Foldout(openScene, "Scene");
                if (openScene)
                {
                    GUI.contentColor = new Color(0.8f, 0.2f, 0);
                    foreach (FieldInfo fieldInfo in fields)
                    {
                        Type type = fieldInfo.FieldType;
                        if (type.IsDefined(typeof(HideInInspector), false))
                        {
                            continue;
                        }

                        if (fieldInfo.IsDefined(typeof(HideInInspector), false))
                        {
                            continue;
                        }

                        object value = fieldInfo.GetValue(obj);


                        foreach (ITypeDrawer typeDrawer in typeDrawers)
                        {
                            if (!typeDrawer.HandlesType(type))
                            {
                                continue;
                            }

                            string fieldName = fieldInfo.Name;
                            if (fieldName.Length > 17 && fieldName.Contains("k__BackingField"))
                            {
                                fieldName = fieldName.Substring(1, fieldName.Length - 17);
                            }
                            value = typeDrawer.DrawAndGetNewValue(type, fieldName, value, null);
                            fieldInfo.SetValue(obj, value);
                            break;
                        }
                    }
                }
            }
            GUI.contentColor = Color.white;
        }

    }
}