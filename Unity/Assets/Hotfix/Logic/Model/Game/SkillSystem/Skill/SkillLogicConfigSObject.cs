using Cal;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalEditor
{
#if UNITY_EDITOR
	public class SkillLogicConfigSObject:SerializedScriptableObject
	{
		public string configName;

        public SkillLogicConfig config;


    }
#endif
}
