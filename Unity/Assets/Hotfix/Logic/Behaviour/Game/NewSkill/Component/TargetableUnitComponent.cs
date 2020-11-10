using System;
using System.Collections.Generic;
using System.Text;

namespace ET
{
    public class TargetableUnitComponent:Entity
    {
        public Unit selectedTeamMember;
        public Unit selectedEnermy;

        /// <summary>
        /// 当前目标
        /// </summary>
        public Unit currTarget{ get; set; }
        /// <summary>
        /// 可选择的目标
        /// </summary>
        public List<Unit> targetList{ get;} = new List<Unit>();
        /// <summary>
        /// 所有敌方
        /// </summary>
        public List<Unit> allEnermy = new List<Unit>();
        /// <summary>
        /// 所有友方
        /// </summary>
        public List<Unit> allTeam = new List<Unit>();

        public List<Unit> allTarget = new List<Unit>();
    }
}
