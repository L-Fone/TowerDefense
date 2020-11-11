using Cal;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class SkillConfigComponentAwakeSystem : AwakeSystem<SkillConfigComponent>
    {
    	public override void Awake(SkillConfigComponent self)
    	{
    		self.Awake();
    	}
    }
    public class SkillConfigComponentStartSystem : StartSystem<SkillConfigComponent>
    {
        public override void Start(SkillConfigComponent self)
        {
            self.Start();
        }
    }

    public class SkillConfigComponent:Entity
    {
        private static SkillConfigComponent inst;
        private SkillLogicConfigCollection skillLogicConfigCollection;
        public static SkillLogicConfig GetSkillLogicConfig(int skillId)
        {
            inst.skillLogicConfigCollection.skillDic.TryGetValue(skillId, out var skillLogicConfig);
            return skillLogicConfig;
        }
        internal void Awake()
        {
            inst = this;
        }

        internal void Start()
        {
            StartAsync().Coroutine() ;

        }

        private async ETVoid StartAsync()
        {
            Type[] types = typeof(SkillConfigComponent).Assembly.GetTypes();

            foreach (Type type in types)
            {
                if (
                    !type.IsSubclassOf(typeof(SelectTargetBase)) &&
                    !type.IsSubclassOf(typeof(SkillOptionBase))
                    )
                {
                    continue;
                }

                if (type.IsGenericType)
                {
                    continue;
                }

                try
                {
                    BsonClassMap.LookupClassMap(type);
                }
                catch (Exception e)
                {
                    Log.Error($"11: {type.Name} {e}");
                }

            }
            string path = "Assets/Download/Config/SkillLogicConfig.json";
            var asset = await ResourceHelper.LoadAssetAsync<TextAsset>(path);
            string str = asset.text;
            skillLogicConfigCollection = MongoHelper.FromJson<SkillLogicConfigCollection>(str);
        }
    }
}
