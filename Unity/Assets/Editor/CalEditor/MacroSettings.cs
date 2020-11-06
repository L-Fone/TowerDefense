using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class MacroSettings : ScriptableObject
{
    private string m_Macor;

    [BoxGroup("MacroSettings")]
    [TableList(ShowIndexLabels = true, AlwaysExpanded = true)]
    [HideLabel]
    public MacroData[] Settings;

    [Button(ButtonSizes.Medium),ResponsiveButtonGroup("DefaultButtonSize"),PropertyOrder(1)]
    public void SaveMacor()
    {
        m_Macor = string.Empty;
        foreach (var item in Settings)
        {
            if (item.Enable)
            {
                m_Macor += string.Format("{0};", item.Macro);
            }

            if (item.Macro.Equals("EDITOR_MODE", System.StringComparison.CurrentCultureIgnoreCase))
            {
                EditorBuildSettingsScene[] arrScene = EditorBuildSettings.scenes;
                for (int i = 0; i < arrScene.Length; i++)
                {
                    if (arrScene[i].path.IndexOf("download", System.StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        arrScene[i].enabled = item.Enable;
                    }
                } 

                EditorBuildSettings.scenes = arrScene;
            }
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macor);
        Debug.Log("Save Mactor Success");
    }
    private void OnEnable()
    {
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);

        for (int i = 0; i < Settings.Length; i++)
        {
            if (!string.IsNullOrEmpty(m_Macor) && m_Macor.IndexOf(Settings[i].Macro) != -1)
            {
                Settings[i].Enable = true;
            }
            else
            {
                Settings[i].Enable = false;
            }
        }
    }
}
[System.Serializable]
public class MacroData
{
    [TableColumnWidth(80,Resizable =false)]
    public bool Enable;

    public string Name;
    /// <summary>
    /// ºê
    /// </summary>
    public string Macro;
}