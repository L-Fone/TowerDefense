using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Cal
{
    public enum ValueChangeType
    {
        Zero,
        Plus,
        Minus
    }
    public enum SkillTypes
    {
        主动,
        被动
    }
    public enum SkillCastType
    {
        无消耗,
        消耗精力,
        消耗血量
    }
    public enum SkillSourcetype
    {
        DataTable,
        None,
        Input,
    }
    public enum SkillDataTableArgs
    {
        Args0,
        Args1,
        Args2,
        Args3,
        Args4,
        Args5,
        Args6,
        Args7,
        Args8,
        Args9,
        Args10,
        Args11,
        Args12,
        Args13,
        Args14,
        Args15,
        Args16,
        Args17,
        Args18,
        Args19,
        Args20,
        Args21,
        Args22,
        Args23,
        Args24,
        Args25,
        Args26,
        Args27,
        Args28,
        Args29,
        Args30,
    }
    public enum SkillDamageType
    {
        /// <summary>
        /// 无伤害
        /// </summary>
        None = 0,
        物理伤害,
        精神伤害,
        真实伤害,
        护盾治疗,
    }
    public enum ValueCalculateType
    {
        物理攻击力x系数,
        精神攻击力x系数,
        生命值上限x系数,
        当前生命值x系数,
    }

    public enum SkillEventCondition
    {
        当技能施法开始,
        当技能施法完成,
        当技能施法被中断,//不做
        当技能施法成功,//不做
        当拥有者出生,
        当拥有者死亡,
        当切换为关闭状态,//不做
        当切换为开启状态,//不做
        当技能添加,
        当技能升级,
        当技能移除,
        当人物升级,//不做
        当弹道粒子特效结束,//不做
        当弹道粒子特效命中单位,//不做
        当技能暴击时,
        当技能中途释放,
    }
    public enum SkillOptionType
    {
        添加技能,
        升级技能,//不做
        移除技能,
        使目标模型做出某个动作,
        应用Modifier,
        移除Modifier,
        附着点特效,
        范围攻击,//不做
        创建定时器,
        伤害,
        延迟操作,
        播放特效,
        播放声音,
        治愈,
        吸血,
        反伤,
        几率,
        生产单位,
        击晕,
        额外攻击目标,
        修改技能数据,
        修改Modifier数据,
        创建携带Modifier的子弹,
        驱散状态,
        状态加深,
        状态减弱,
        攻击类型转换,
        改变伤害倍率,
        护盾,
        改变公共CD,
        改变技能CD,
        承担伤害,
        反击,
        根据人数改变伤害,
        改变释放次数,
    }
    [Flags]
    public enum ModifierAttribute
    {
        [LabelText("无，此类型的不能重复添加，无任何效果")]
        无 = 0,
        [LabelText("无敌状态也能添加这个modifier")]
        忽视无敌 = 1 << 1,
        [LabelText("叠加层数，用于持续伤害")]
        可叠加 = 1 << 2,
        [LabelText("重新计时，失去旧的，添加新的，并触发事件")]
        可刷新 = 1 << 3,
        所有 = 可叠加 | 可刷新|忽视无敌,
    }
    [LabelText("可叠加的buff类型")]
    public enum OverlableBuffType
    {
        无,
        中毒,
        流血,
        燃烧,
        治疗,
    }
    public enum BuffType
    {
        Buff,
        Debuff,
        Unknow,
    }
    public enum EffectAttachType
    {
        中央,
        头点,
        脚底,
        前方,
        后方
    }
    public enum ModifierStateType
    {
        无 = 0,
        无敌,
        免疫伤害,
        免疫控制,
        不死,
        死亡复活,
        无法选中,
        禁用被动,
        沉默,
        眩晕,
        冰冻,
        石化,
        禁用物品状态,
        离开游戏状态,
        必定暴击,
        隐身 = 15,

        护盾 = 100,
        反伤,
        反击,
        承伤,
        中毒,
        流血,
        燃烧,
        治疗,
    }
    public enum ModifierValueType
    {
        无,

        修改基础_生命值上限,
        修改基础_精力值上限,

        修改攻防_攻击力,
        修改攻防_物理攻击力,
        修改攻防_精神攻击力,
        修改攻防_防御力,
        修改攻防_物理防御力,
        修改攻防_精神防御力,

        修改暴击_暴击率,
        修改暴击_物理暴击率,
        修改暴击_精神暴击率,
        修改暴击_暴击值,
        修改暴击_物理暴击值,
        修改暴击_精神暴击值,

        修改抗暴击_抗暴击率,
        修改抗暴击_抗物理暴击率,
        修改抗暴击_抗精神暴击率,
        修改抗暴击_抗暴击值,
        修改抗暴击_抗物理暴击值,
        修改抗暴击_抗精神暴击值,

        修改免伤_免伤,
        修改免伤_物理免伤,
        修改免伤_精神免伤,

        修改辅助值,
        修改速度,
        修改命中,
        修改抵抗,
    }
    public enum ModifierEventCondition
    {
        当modifier被创建时,
        当modifier被移除时,
        当拥有modifier的单位被攻击时,
        当拥有modifier的单位攻击到某个目标时,//没做
        当拥有modifier的单位开始攻击某个目标,
        当拥有modifier的单位施加伤害时,//没做
        当拥有modifier的单位受到伤害时,
        当拥有modifier的单位暴击时,//没做
        循环执行定时器操作,
        当拥有modifier的单位被暴击时,
        当拥有modifier的单位攻击失败时,//没做
        当拥有modifier的单位攻击友方时,//没做
        当拥有modifier的单位装备物品,//没做
        当拥有modifier的单位结束持续施法,//没做
        当拥有modifier的单位开始使用技能,//没做
        当拥有modifier的单位状态改变时,//没做
        当拥有modifier的单位复活时,
        当拥有modifier的单位死亡时,
        当拥有modifier的单位花费魔法时,//没做
        当拥有modifier的单位获得最大生命值时,//没做
        当拥有modifier的单位获得最大精力时,//没做
        当拥有modifier的单位受到治疗时,
        当拥有modifier的单位击杀目标时,//没做
        当拥有modifier的单位生命值变化时,
    }
    public enum StateStateType
    {
        启用,
        不作为,
        禁用,
    }
    public enum ThinkerType
    {
        无,
        中毒,
        流血,
        燃烧,
        治疗,
        自定义
    }
}

