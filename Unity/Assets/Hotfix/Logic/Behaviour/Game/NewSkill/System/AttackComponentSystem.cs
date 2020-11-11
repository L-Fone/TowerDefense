using Cal;
using System;
using System.Collections.Generic;

namespace ET
{
    public class AttackComponentAwakeSystem : AwakeSystem<AttackComponent>
    {
        public override void Awake(AttackComponent self)
        {
            self.multipleDamageX10000 = 1;
            self.playAmount = 1;
        }
    }
    public class AttackComponentDstroySystem : DestroySystem<AttackComponent>
    {
        public override void Destroy(AttackComponent self)
        {
            self.attacker = null;
        }
    }
    public static class AttackComponentSystem
    {
        /// <summary>
        /// 《选择目标》
        /// 技能释放流程，需要选择目标，产生伤害
        /// 触发开始释放技能事件，包括技能的和modifier的事件
        /// 然后让【弹道附带伤害数据】飞向敌人，服务器计时就可以了，客户端做特效
        /// 问题：
        ///     伤害根据技能数据来的，此处并不知道技能数据，无法计算伤害，那怎么办呢？
        ///     
        /// 解决方法：
        ///     技能流程分为
        ///     
        ///技能事件        开始释放技能              技能释放结束（选择目标、结算伤害、）        
        ///modifier事件                             modifier拥有者开始攻击                    modifier拥有者攻击到了目标
        ///                                 
        ///特效            刚开始播放攻击动画        开始发出攻击特效                          攻击特效销毁，产生受击特效
        ///弹道                                     创建弹道                                 弹道移动。根据触发器或者距离选择目标
        ///                                                                               
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask<bool> StartSpellSkill(this AttackComponent self, SkillLogic skillLogic)
        {
            var unit = self.GetParent<Unit>();
            var targetCompoennt = self.Parent.GetComponent<TargetableUnitComponent>();
            var allList = targetCompoennt.GetAllTarget();

            for (int i = skillLogic.playAmount - 1; i >= 0; i--)
            {
                SpellSkill(self, unit, skillLogic, allList).Coroutine();
            }
            await ETTask.CompletedTask;
            return true;
        }
        private static async ETVoid SpellSkill(AttackComponent self,Unit unit, SkillLogic skillLogic, List<Unit> allList)
        {
            Log.Info($"{unit}释放技能：【{skillLogic.skillConfig.Name}({skillLogic.skillConfigId})】");
            skillLogic.HandleEvent(SkillEventCondition.当技能施法开始, new SkillSender
            {
                owner = unit,
                skillLogic = skillLogic
            });

            //!广播释放技能
            //foreach (var u in allList)
            //{
                //M2C_PlaySkill m2C_PlayerSkill = new M2C_PlaySkill
                //{
                //    SkillId = skillLogic.skillId,
                //    UnitId = unit.Id,
                //};
                //MessageHelper.SendActor(u, m2C_PlayerSkill);

            //}
            //!动画开始到产生特效的时间
            int delayTime = 1000;
            await TimerComponent.Instance.WaitAsync(delayTime);
            self.EndSpellSkill(unit,skillLogic);
        }
        public static void EndSpellSkill(this AttackComponent self,Unit unit, SkillLogic skillLogic)
        {
            skillLogic.HandleEvent(SkillEventCondition.当技能施法完成, new SkillSender
            {
                owner = unit,
                skillLogic = skillLogic
            });
        }
        /// <summary>
        /// 获取伤害数据
        /// </summary>
        public static BallisticData GetAttackData(this AttackComponent self)
        {
            return self.attackData;
        }
        /// <summary>
        /// 执行伤害
        /// </summary>
        public static void AttackTarget(this AttackComponent self, Unit target, BallisticData data, ISkillSender skillSender, bool applyBallisticData = true)
        {
            var unit = self.GetParent<Unit>();
            Log.Debug($"{unit.Id}对{target.Id}，伤害数据：{data}");
            if (data.value == 0)
            {

            }
            if (applyBallisticData)
                self.attackData = data;

            var attackComponent = target.GetComponent<AttackComponent>();
            attackComponent.attacker = unit;
            //target.GetComponent<UnitEnermy>().unit = unit;
            target.GetComponent<BattleComponent>().Damage(data, skillSender);

        }
        /// <summary>
        /// 执行治疗
        /// </summary>
        public static void TreatTarget(this AttackComponent self, Unit target, BallisticData ballisticData, ISkillSender skillSender)
        {
            var unit = self.GetParent<Unit>();
            Log.Debug($"{self.GetParent<Unit>().Id}对{target.Id}，治疗数据：{ballisticData}");
            var attackComponent = target.GetComponent<AttackComponent>();
            attackComponent.attacker = unit;
            target.GetComponent<BattleComponent>().Treat(ballisticData, skillSender);
        }


    }
}
