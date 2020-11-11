using Cal;
using MongoDB.Bson.Serialization;
using System;

namespace ET
{
    internal  static class HotfixMongoHelper
    {
        static HotfixMongoHelper()
        {
            Type[] types = typeof(Game).Assembly.GetTypes();
            foreach (Type type in types)
            {
                if (!type.IsSubclassOf(typeof(SkillOptionBase)) &&
                    !type.IsSubclassOf(typeof(SelectTargetBase))
                    )
                {
                    continue;
                }

                BsonClassMap.LookupClassMap(type);
            }
        }
        internal static void Init()
        {

        }
    }
}