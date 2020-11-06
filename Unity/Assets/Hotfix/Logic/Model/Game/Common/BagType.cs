using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET
{
    public enum Quality
    {
        Common = 1,
        UnCommon,
        Rare,
        Epic
    }
    public enum EquipType
    {
        Weapon = 0, BG = 1, Skin = 2,
        HUWAN = 3, XUNZHANG = 4, Wing = 5,

        Hat = 6,
        XIANGLIAN = 7,
        Cloth = 8,
        Pants = 9,
        SHOUTAO = 10,
        Shoe = 11,
    }
    public enum MaterialsType
    {
        Normal = 1,
        Gem
    }
    public enum GemType
    {
        XIWANG = 1,
        ZHIHUI,
        XUANWEI,
        XUANMING,
        SHENGMING,
        MEILI,
        MINGYUN,
        TIANJI,
    }
    public enum AtttributeType
    {
        MaxHp = 1,
        MaxMp,
        Str,
        Quk,
        Spi,
        Wim,
        PAtk,
        MAtk,
        PDef,
        MDef,
        PCriR,
        MCriR,
        PCri,
        MCri,
        RPCriR,
        RMCriR,
        RPCri,
        RMCri,
        Dvo,
    }
}
