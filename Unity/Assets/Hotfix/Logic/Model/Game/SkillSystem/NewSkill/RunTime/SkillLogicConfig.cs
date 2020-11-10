using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cal
{
    public class SkillLogicConfigCollection
    {
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, SkillLogicConfig> skillDic = new Dictionary<int, SkillLogicConfig>();
    }

    [BsonIgnoreExtraElements]
    public class SkillLogicConfig
    {
        [ReadOnly]
        [LabelText("技能Id")]
        public int skillId;

        public string skillName;

        public SkillTypes skillType;

        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<SkillEventCondition, SkillOptionBase[]> skillEventDic = new Dictionary<SkillEventCondition, SkillOptionBase[]>()
        {
            {SkillEventCondition.当技能施法完成,new SkillOptionBase[]{
                 new SkillOption_应用Modifier()
            } }
        };


        [LabelText("modifier列表")]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        [OnValueChanged("OnChangedDic", true)]
        public Dictionary<ModifierId, ModifierConfig> modifierDic = new Dictionary<ModifierId, ModifierConfig>();

#if UNITY_EDITOR
        private List<ModifierId> idList = new List<ModifierId>();
        private List<ModifierId> needModifiyIdList = new List<ModifierId>();
        private void OnChangedDic()
        {
            idList = idList ?? new List<ModifierId>();
            needModifiyIdList = needModifiyIdList ?? new List<ModifierId>();
            idList.Clear();
            needModifiyIdList.Clear();
            if (modifierDic == null)
                return;
            foreach (var kp in modifierDic)
            {
                if (kp.Key.Value / 100 != skillId)
                    needModifiyIdList.Add(kp.Key);
                if (kp.Value == null) continue;
                if (kp.Value.Id.Value != kp.Key.Value)
                    idList.Add(kp.Key);
            }
            foreach (var item in idList)
            {
                if (modifierDic.TryGetValue(item, out var modifierConfig))
                {
                    modifierConfig.Id = new ModifierId
                    {
                        Value = item.Value
                    };
                }
            }
            foreach (var item in needModifiyIdList)
            {
                if (!modifierDic.TryGetValue(item, out var modifierConfig))
                    continue;
                modifierDic.Remove(item);
                modifierDic.Add(new ModifierId
                {
                    Value = item.Value % 100 + skillId * 100
                }, modifierConfig);
            }
        }
#endif
    }

}
