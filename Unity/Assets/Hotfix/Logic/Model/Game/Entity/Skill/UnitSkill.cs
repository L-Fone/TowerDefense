using Sirenix.OdinInspector.Editor.Validation;
using System;

namespace ET
{
    public struct UnitSkill:IComparable<UnitSkill>,IEquatable<UnitSkill>
    {
        public static UnitSkill Empty = default;
        public int Id;
        public int Level;
        public bool IsPassive;
        public int CompareTo(UnitSkill other)
        {
            return Id.CompareTo(other.Id);
        }

        public bool Equals(UnitSkill other)
        {
            return Id.Equals(other.Id);
        }
        public static bool operator ==(UnitSkill a,UnitSkill b)
        {
            return a.Id == b.Id;
        }
        public static bool operator !=(UnitSkill a, UnitSkill b)
        {
            return a.Id  != b.Id;
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
            return $"skillId:{Id} level:{Level} isPasaive:{IsPassive}";
        }
    }
}