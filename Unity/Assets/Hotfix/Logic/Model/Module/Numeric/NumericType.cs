namespace ET
{
    public enum NumericType
    {
		Max = 10000,

	    Hp = 1001,
	    HpBase = Hp * 10 + 1,

        Mp = 1002,
        MpBase = Mp * 10 + 1,

        MaxHp = 1003,
	    MaxHpBase = MaxHp * 10 + 1,
	    MaxHpAdd = MaxHp * 10 + 2,
	    MaxHpPct = MaxHp * 10 + 3,
	    MaxHpFinalAdd = MaxHp * 10 + 4,
		MaxHpFinalPct = MaxHp * 10 + 5,

        MaxMp = 1004,
        MaxMpBase = MaxMp * 10 + 1,
        MaxMpAdd = MaxMp * 10 + 2,
        MaxMpPct = MaxMp * 10 + 3,
        MaxMpFinalAdd = MaxMp * 10 + 4,
        MaxMpFinalPct = MaxMp * 10 + 5,

        Atk = 1005,
        AtkBase = Atk * 10 + 1,
        AtkAdd = Atk * 10 + 2,
        AtkPct = Atk * 10 + 3,
        AtkFinalAdd = Atk * 10 + 4,
        AtkFinalPct = Atk * 10 + 5,

        Def = 1006,
        DefBase = Def * 10 + 1,
        DefAdd = Def * 10 + 2,
        DefPct = Def * 10 + 3,
        DefFinalAdd = Def * 10 + 4,
        DefFinalPct = Def * 10 + 5, 
        
        AtkField = 1007,
        AtkFieldBase = AtkField * 10 + 1,
        AtkFieldAdd = AtkField * 10 + 2,
        AtkFieldPct = AtkField * 10 + 3,
        AtkFieldFinalAdd = AtkField * 10 + 4,
        AtkFieldFinalPct = AtkField * 10 + 5,
        
        AtkSpd = 1008,
        AtkSpdBase = AtkSpd * 10 + 1,
        AtkSpdAdd = AtkSpd * 10 + 2,
        AtkSpdPct = AtkSpd * 10 + 3,
        AtkSpdFinalAdd = AtkSpd * 10 + 4,
        AtkSpdFinalPct = AtkSpd * 10 + 5,   
        
        MoveSpd = 1009,
        MoveSpdBase = MoveSpd * 10 + 1,
        MoveSpdAdd = MoveSpd * 10 + 2,
        MoveSpdPct = MoveSpd * 10 + 3,
        MoveSpdFinalAdd = MoveSpd * 10 + 4,
        MoveSpdFinalPct = MoveSpd * 10 + 5,

    }
}
