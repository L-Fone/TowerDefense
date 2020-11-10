using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using ET;
using System.IO;
using System;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using CalEditor;
using System.Linq;
#endif

namespace Cal
{
    [GUIColor(1, 0.6f, 0)]
    public struct ModifierId : IComparable, IComparable<ModifierId>, IEquatable<ModifierId>
    {
#if UNITY_EDITOR
        [LabelText("技能Id")]
        [ValueDropdown("Getparam1")]
        [BsonIgnore]
        [ShowInInspector]
        private int param1
        {
            get
            {
                return Value / 100;
            }
            set
            {
                Value = value * 100 + param2;
            }
        }

        [LabelText("编号")]
        [ValueDropdown("Getparam2")]
        [BsonIgnore]
        [ShowInInspector]
        private int param2
        {
            get
            {
                return Value % 100;
            }
            set
            {
                Value = param1 * 100 + value;
            }
        }

        private IEnumerable<int> Getparam1()
        {
            SkillAndModifierSObject data = SkillAndModifierSObject.Instance;
            var arr = Utility.FileOpation.GetFiles(data.soPath, "*.asset", SearchOption.AllDirectories);
            var ret = new List<int>();
            foreach (var item in arr)
            {
                ret.Add(int.Parse(item.Name.Replace(".asset", string.Empty)));
            }
            return ret;
        }
        private IEnumerable<int> Getparam2()
        {
            int levelCount = 50;
            int[] arr = new int[levelCount];
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                arr[i] = i + 11;
            }
            return arr;
        }

#endif
        public int CompareTo(object obj)
        {
            return this.Value.CompareTo(((ModifierId)obj).Value);
        }

        public int CompareTo(ModifierId other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public bool Equals(ModifierId other)
        {
            return Value.Equals(other.Value);
        }
        public static bool operator ==(ModifierId a, ModifierId b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(ModifierId a, ModifierId b)
        {
            return !a.Equals(b);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return Value.ToString();
        }
        [HideInEditorMode]
        public int Value;


#if UNITY_EDITOR
        [ShowInInspector]
        [ReadOnly]
        [BsonIgnore]
        [LabelText("Value")]
        private string ValueInInspector { get => $"{Value / 100}+{Value % 100}"; set { } }
        [ButtonGroup("自动匹配")]
        [Button("自动匹配SkillId")]
        void AutoId()
        {
            try
            {
                int skillId = SkillAndModifierSObject.Instance.skillId;
                param1 = skillId;
                UnityEditor.AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
#endif
    }
    [BsonIgnoreExtraElements]
    public class ModifierConfig
    {
        [LabelText("Modifier Id"), PropertyOrder(0)]
        public ModifierId Id;

        [PropertyOrder(1)]
        public string name;

#if UNITY_EDITOR
        [FoldoutGroup("属性"), PropertyOrder(2)]
        [LabelText("包括的等级数量")]
        [ShowInInspector]
        [PropertyRange(1, 10)]
        private int levelCount
        {
            get
            {
                if (levelList == null) return 0;
                return levelList.Count;
            }
            set
            {
                levelList = new List<int>();
                for (int i = 0; i < value; i++)
                {
                    levelList.Add( i + 1);
                }
            }
        }
#endif
        [FoldoutGroup("属性")]
        [LabelText("包括的等级"), PropertyOrder(3)]
        public List<int> levelList = new List<int> { 1 };

        [FoldoutGroup("属性")]
        [LabelText("属性"), PropertyOrder(4)]
        public ModifierAttribute attribute = ModifierAttribute.可刷新;

        [FoldoutGroup("属性")]
        [LabelText("可叠加类型"), PropertyOrder(5)]
        [OnValueChanged("ResetThinkInterval")]
        public OverlableBuffType overlayType = OverlableBuffType.无;

        void ResetThinkInterval()
        {
            if (overlayType == OverlableBuffType.无)
                thinkInterval = new SkillParam { skillSourcetype = SkillSourcetype.None };
        }

        [FoldoutGroup("属性")]
        [HideIf("overlayType", OverlableBuffType.无)]
        [LabelText("单次叠加层数"), PropertyOrder(5)]
        public SkillParam perOverlay = new SkillParam { skillSourcetype = SkillSourcetype.DataTable };


        [FoldoutGroup("属性")]
        [LabelText("持续时间"), PropertyOrder(5)]
        public SkillParam continueTime;

        [FoldoutGroup("属性"), PropertyOrder(8)]
        public BuffType buffType = BuffType.Buff;

        [FoldoutGroup("属性"), PropertyOrder(10)]
        [LabelText("是否能被驱散")]
        public bool canBeClear = false;

        [FoldoutGroup("属性"), PropertyOrder(13)]
        [LabelText("定时器类型")]
        public ThinkerType thinkerType = ThinkerType.无;

        [FoldoutGroup("属性"), PropertyOrder(13)]
        [LabelText("定时器间隔")]
        [ShowIf("thinkerType", ThinkerType.自定义)]
        public SkillParam thinkInterval = new SkillParam { skillSourcetype = SkillSourcetype.None };

        [LabelText("值类型"), PropertyOrder(14)]
        public ModifierValueType valueK;
        [LabelText("值类型"), PropertyOrder(14)]
        [HideIf("valueK", ModifierValueType.无)]
        public SkillParam valueV;

        [LabelText("状态类型"), PropertyOrder(15)]
        public ModifierStateType stateK;
        [LabelText("状态类型"), PropertyOrder(15)]
        [HideIf("stateK", ModifierStateType.无)]
        public StateStateType stateV;

        [LabelText("Modifier列表"), PropertyOrder(16)]
#if UNITY_EDITOR
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)]
#endif
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<ModifierEventCondition, SkillOptionBase[]> modifierEventDic;
    }
}