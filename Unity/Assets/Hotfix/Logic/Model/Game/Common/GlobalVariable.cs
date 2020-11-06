using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class GlobalVariable
    {

        public static long UserId{ get; set; }
        public static long MyId{ get; set; }
        public static int MapId{ get; set; }
        public static int JobId{ get; set; }
        public static List<Vector4> PlayerPosList_BeforeBattle { get; set; } = new List<Vector4>();
        public static List<Vector4> EnermyPlayerPosList_BeforeBattle{ get; set; }= new List<Vector4>();
        public static long LeaderId  { get;  set; }
        public static long SelectBattleUnitId { get;  set; }
        public static long SelectUnitId { get;  set; }
        public static long SelectUnitHeadInfoId { get;  set; }
        public static bool IsFirst { get;  set; } = true;
        public static bool CanTrans { get;  set; }
    }
}
