using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public interface ISkillSender
    {
        Unit target { get; set; }

        Unit owner { get; set; }

        SkillLogic skillLogic{ get; set; }
        
    }
    public struct SkillSender : ISkillSender
    {
        public Unit target { get; set; }
        public Unit owner { get; set; }
        public SkillLogic skillLogic { get; set; }

        public override string ToString()
        {
            return $"{owner.Id} 对 {target?.Id} 技能逻辑为{skillLogic?.skillId}";
        }
    }
    public struct ModifierSkillSender : ISkillSender
    {
        public Unit target { get; set; }
        public Unit owner { get; set; }
        public SkillLogic skillLogic { get; set; }
        public ModifierLogic modifierLogic { get; set; }
        public override string ToString()
        {
            return $"{owner.Id} 对 {target?.Id} 技能逻辑为{skillLogic?.skillId} modifierId = {modifierLogic?.modifierId}";
        }
    }
}
