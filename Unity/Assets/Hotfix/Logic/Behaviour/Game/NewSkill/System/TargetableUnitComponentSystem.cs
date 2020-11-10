using System;
using System.Collections.Generic;
using System.Text;

namespace ET
{
    public class TargetableUnitComponentDstroySystem : DestroySystem<TargetableUnitComponent>
    {
        public override void Destroy(TargetableUnitComponent self)
        {
            self.selectedEnermy = null;
            self.selectedTeamMember = null;
            self.allEnermy.Clear();
            self.allTeam.Clear();
            self.allTarget.Clear();
            self.currTarget = null;
            self.targetList.Clear();
        }
    }

    public static class TargetableUnitComponentSystem
    {
        public static void AddEnermy(this TargetableUnitComponent self,IEnumerable<Unit> list)
        {
            foreach (var item in list)
            {
                self.allEnermy.Add(item);
                self.allTarget.Add(item);
            }
        }
        public static void AddTeam(this TargetableUnitComponent self, IEnumerable<Unit> list)
        {
            foreach (var item in list)
            {
                self.allTeam.Add(item);
                self.allTarget.Add(item);
            }
        }
        public static List<Unit> GetAllTarget(this TargetableUnitComponent self)
        {
            return self.allTarget;
        }
        public static int GetTagetAliveCount(this TargetableUnitComponent self)
        {
            int count = 0;
            foreach (var item in self.allEnermy)
            {
                if (item.IsAlive)
                    count++;
            }
            return count;
        }
    }
}
