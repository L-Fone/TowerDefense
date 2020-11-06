using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class ParamsSettings : ScriptableObject
{
    [BoxGroup("InitUrl")]
    public string WebAccountUtl;

    [BoxGroup("InitUrl")]
    public string TestWebAccountUtl;

    [BoxGroup("InitUrl")]
    public bool IsTest;

    [BoxGroup("GeneralParams")]
    [TableList(ShowIndexLabels =true,AlwaysExpanded =true)]
    [HideLabel]
    public GeneralParamData[] GeneralParams;

    [BoxGroup("GradeParams")]
    [TableList(ShowIndexLabels = true, AlwaysExpanded = true)]
    [HideLabel]
    public GradeParamData[] GradeParams;

    public enum DeviceGrade
    {
        Low,
        Middle,
        High
    }

    private int m_LenGradeParams;
    public int GetGradeParamData(string key, DeviceGrade grade)
    {
        m_LenGradeParams = GradeParams.Length;
        for (int i = 0; i < m_LenGradeParams; i++)
        {
            GradeParamData gradeParamData = GradeParams[i];
            if (gradeParamData.Key.Equals(key, System.StringComparison.CurrentCultureIgnoreCase))
            {
                return gradeParamData.GetValuByGrade(grade);
            }
        }
        return 0;
    }

    [System.Serializable]
    public class GeneralParamData
    {
        [TableColumnWidth(160, Resizable = false)]
        public string Key;

        public string Value;
    }

    [System.Serializable]
    public class GradeParamData
    {
        [TableColumnWidth(160, Resizable = false)]
        public string Key;

        public int LowValue;
        public int MiddleValue;
        public int HighValue;

        public int GetValuByGrade(DeviceGrade grade)
        {
            switch (grade)
            {
                default:
                case DeviceGrade.Low:
                    return LowValue;
                case DeviceGrade.Middle:
                    return MiddleValue;
                case DeviceGrade.High:
                    return HighValue;
            }
        }
    }
}
