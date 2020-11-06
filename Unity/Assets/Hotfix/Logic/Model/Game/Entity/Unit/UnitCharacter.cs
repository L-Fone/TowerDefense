using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class UnitCharacterDestroySystem : DestroySystem<UnitCharacter>
    {
        public override void Destroy(UnitCharacter self)
        {
            self.Destroy();
        }
    }

    public class UnitCharacter : Entity
    {
        private string _nickName;
        public string NickName
        {
            get => _nickName; set
            {
                if (_nickName == value)
                    return;
                _nickName = value;
            }
        }
        public JobType JobType { get; private set; }
        public string Job
        {
            get
            {
                switch (JobType)
                {
                    case JobType.Officer:
                        return "军官";
                    case JobType.Sportsman:
                        return "运动员";
                    case JobType.Nurse:
                        return "护士";
                    case JobType.Superman:
                        return "超能力";
                    default:
                    case JobType.UnKnown:
                        return "未知";
                }
            }
        }
        private int _jobId;
        public int JobId
        {
            get => _jobId;
            set
            {
                if (_jobId == value)
                    return;
                _jobId = value;
            }
        }
        private CampType _campType;
        public CampType CampType
        {
            get => _campType; set
            {
                if (_campType == value)
                    return;
                _campType = value;
            }
        }
        public string Camp
        {
            get
            {
                switch (CampType)
                {
                    default:
                    case CampType.NoneCamp:
                        return "无";
                    case CampType.Pioneer:
                        return "开拓者";
                    case CampType.Guardian:
                        return "守护者";
                }
            }
        }
        private int _level;
        public int Level
        {
            get => _level; set
            {
                if (_level == value)
                    return;
                _level = value;
            }
        }
        private int hp;
        public int Hp
        {
            get => hp; set
            {
                if (hp == value)
                    return;
                hp = value;
            }
        }
        private int maxHp;
        public int MaxHp
        {
            get => maxHp; set
            {
                if (maxHp == value)
                    return;
                maxHp = value;
            }
        }
        private int mp;
        public int Mp
        {
            get => mp; set
            {
                if (mp == value)
                    return;
                mp = value;
            }
        }
        private int maxMp;
        public int MaxMp
        {
            get => maxMp; set
            {
                if (maxMp == value)
                    return;
                maxMp = value;
            }
        }
        private long exp;
        public long Exp
        {
            get => exp; set
            {
                if (exp == value)
                    return;
                exp = value;
            }
        }
        private long maxExp;
        public long MaxExp
        {
            get => maxExp; set
            {
                if (maxExp == value)
                    return;
                maxExp = value;
            }
        }

        private int atk;
        public int Atk
        {
            get => atk; set
            {
                if (atk == value)
                    return;
                atk = value;
            }
        }
        private int def; 
        public int Def
        {
            get => def; set
            {
                if (def == value)
                    return;
                def = value;
            }
        }
        internal void Destroy()
        {
            _nickName = null;
            _level = 0;
            _jobId = 0;
            _campType = CampType.NoneCamp;
            hp = 0;
            maxHp = 0;
            mp = 0;
            maxMp = 0;
            exp = 0;
            maxExp = 0;

        }
    }
}
