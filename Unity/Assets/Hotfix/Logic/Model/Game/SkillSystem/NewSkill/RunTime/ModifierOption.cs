using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cal
{
    public abstract class ModifierOptionBase
    {
        [LabelText("操作类型")]
        [ShowInInspector]
        public abstract ModifierEventCondition eventCondition { get; }
    }
    public class ModifierOption_当modifier被创建时:ModifierOptionBase
    {
        [LabelText("操作类型")]
        [ShowInInspector]
        public override ModifierEventCondition eventCondition => ModifierEventCondition.当modifier被创建时;
    }

}
