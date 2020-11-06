using ET;
namespace ET
{
//=================主线===========================
	[Message(HotfixOpcode2.M2C_InitMainStoryMap)]
	public partial class M2C_InitMainStoryMap : IActorMessage {}

	[Message(HotfixOpcode2.C2M_StartMainStoryFight)]
	public partial class C2M_StartMainStoryFight : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_StartMainStoryFight)]
	public partial class M2C_StartMainStoryFight : IActorLocationResponse {}

	[Message(HotfixOpcode2.M2C_MainStoryMonsterInfo)]
	public partial class M2C_MainStoryMonsterInfo : IActorMessage {}

	[Message(HotfixOpcode2.MonsterUnitInfo)]
	public partial class MonsterUnitInfo {}

	[Message(HotfixOpcode2.ReMonsterUnitInfo)]
	public partial class ReMonsterUnitInfo {}

	[Message(HotfixOpcode2.M2C_ReMainStoryMonsterInfo)]
	public partial class M2C_ReMainStoryMonsterInfo : IActorMessage {}

	[Message(HotfixOpcode2.M2C_BattleVictory)]
	public partial class M2C_BattleVictory : IActorMessage {}

	[Message(HotfixOpcode2.M2C_BattleDefeat)]
	public partial class M2C_BattleDefeat : IActorMessage {}

//=================Boss===========================
	[Message(HotfixOpcode2.M2C_BossRefresh)]
	public partial class M2C_BossRefresh : IActorMessage {}

	[Message(HotfixOpcode2.C2M_StartBossFightRequest)]
	public partial class C2M_StartBossFightRequest : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_StartBossFightRequest)]
	public partial class M2C_StartBossFightRequest : IActorLocationResponse {}

	[Message(HotfixOpcode2.C2M_StartBossFight)]
	public partial class C2M_StartBossFight : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_StartBossFight)]
	public partial class M2C_StartBossFight : IActorLocationResponse {}

	[Message(HotfixOpcode2.M2C_SendBossInfo)]
	public partial class M2C_SendBossInfo : IActorMessage {}

	[Message(HotfixOpcode2.M2C_ReSendBossInfo)]
	public partial class M2C_ReSendBossInfo : IActorMessage {}

	[Message(HotfixOpcode2.M2C_BossFightVictory)]
	public partial class M2C_BossFightVictory : IActorMessage {}

	[Message(HotfixOpcode2.M2C_BossFightDefeat)]
	public partial class M2C_BossFightDefeat : IActorMessage {}

	[Message(HotfixOpcode2.M2C_BossDead)]
	public partial class M2C_BossDead : IActorMessage {}

	[Message(HotfixOpcode2.M2C_BossBeDefeat)]
	public partial class M2C_BossBeDefeat : IActorMessage {}

//=================战斗============================
	[Message(HotfixOpcode2.RewardItem)]
	public partial class RewardItem {}

	[Message(HotfixOpcode2.StateBuffInfo)]
	public partial class StateBuffInfo {}

	[Message(HotfixOpcode2.C2M_AutoBattle)]
	public partial class C2M_AutoBattle : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_AutoBattle)]
	public partial class M2C_AutoBattle : IActorLocationResponse {}

	[Message(HotfixOpcode2.C2M_SelectEnermy)]
	public partial class C2M_SelectEnermy : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_SelectEnermy)]
	public partial class M2C_SelectEnermy : IActorLocationResponse {}

	[Message(HotfixOpcode2.C2M_SelectTeamMember)]
	public partial class C2M_SelectTeamMember : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_SelectTeamMember)]
	public partial class M2C_SelectTeamMember : IActorLocationResponse {}

	[Message(HotfixOpcode2.M2C_PlaySkill)]
	public partial class M2C_PlaySkill : IActorMessage {}

	[Message(HotfixOpcode2.M2C_MonsterPlaySkill)]
	public partial class M2C_MonsterPlaySkill : IActorMessage {}

	[Message(HotfixOpcode2.M2C_PlaySkillEffect)]
	public partial class M2C_PlaySkillEffect : IActorMessage {}

	[Message(HotfixOpcode2.M2C_BattleSkillRet)]
	public partial class M2C_BattleSkillRet : IActorMessage {}

	[Message(HotfixOpcode2.M2C_BattleChangeMP)]
	public partial class M2C_BattleChangeMP : IActorMessage {}

	[Message(HotfixOpcode2.M2C_BattleChangeState)]
	public partial class M2C_BattleChangeState : IActorMessage {}

	[Message(HotfixOpcode2.M2C_BattleTouchState)]
	public partial class M2C_BattleTouchState : IActorMessage {}

	[Message(HotfixOpcode2.M2C_MainstoryMonsterDead)]
	public partial class M2C_MainstoryMonsterDead : IActorMessage {}

	[Message(HotfixOpcode2.M2C_UnitDead)]
	public partial class M2C_UnitDead : IActorMessage {}

	[Message(HotfixOpcode2.M2C_SendReward)]
	public partial class M2C_SendReward : IActorMessage {}

	[Message(HotfixOpcode2.C2M_GetBattleStateBuff)]
	public partial class C2M_GetBattleStateBuff : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_GetBattleStateBuff)]
	public partial class M2C_GetBattleStateBuff : IActorLocationResponse {}

//==================战斗挂机====================
	[Message(HotfixOpcode2.C2M_StartBattleIdleFight)]
	public partial class C2M_StartBattleIdleFight : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_StartBattleIdleFight)]
	public partial class M2C_StartBattleIdleFight : IActorLocationResponse {}

	[Message(HotfixOpcode2.C2M_EndBattleIdleFight)]
	public partial class C2M_EndBattleIdleFight : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_EndBattleIdleFight)]
	public partial class M2C_EndBattleIdleFight : IActorLocationResponse {}

//=================试炼之地副本=================
	[Message(HotfixOpcode2.M2C_InitTrialCopyMap)]
	public partial class M2C_InitTrialCopyMap : IActorMessage {}

	[Message(HotfixOpcode2.C2M_StartTrialCopyFight)]
	public partial class C2M_StartTrialCopyFight : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_StartTrialCopyFight)]
	public partial class M2C_StartTrialCopyFight : IActorLocationResponse {}

//===================PK==========================
	[Message(HotfixOpcode2.C2M_StartPKFight)]
	public partial class C2M_StartPKFight : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_StartPKFight)]
	public partial class M2C_StartPKFight : IActorLocationResponse {}

	[Message(HotfixOpcode2.M2C_SendStartPK)]
	public partial class M2C_SendStartPK : IActorMessage {}

	[Message(HotfixOpcode2.M2C_PKFightVictory)]
	public partial class M2C_PKFightVictory : IActorMessage {}

	[Message(HotfixOpcode2.M2C_PKFightDefeat)]
	public partial class M2C_PKFightDefeat : IActorMessage {}

	[Message(HotfixOpcode2.C2M_StartTestBattleFight)]
	public partial class C2M_StartTestBattleFight : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_StartTestBattleFight)]
	public partial class M2C_StartTestBattleFight : IActorLocationResponse {}

	[Message(HotfixOpcode2.C2M_EndTestBattleFight)]
	public partial class C2M_EndTestBattleFight : IActorLocationRequest {}

	[Message(HotfixOpcode2.M2C_EndTestBattleFight)]
	public partial class M2C_EndTestBattleFight : IActorLocationResponse {}

}
namespace ET
{
	public static partial class HotfixOpcode2
	{
		 public const ushort M2C_InitMainStoryMap = 12001;
		 public const ushort C2M_StartMainStoryFight = 12002;
		 public const ushort M2C_StartMainStoryFight = 12003;
		 public const ushort M2C_MainStoryMonsterInfo = 12004;
		 public const ushort MonsterUnitInfo = 12005;
		 public const ushort ReMonsterUnitInfo = 12006;
		 public const ushort M2C_ReMainStoryMonsterInfo = 12007;
		 public const ushort M2C_BattleVictory = 12008;
		 public const ushort M2C_BattleDefeat = 12009;
		 public const ushort M2C_BossRefresh = 12010;
		 public const ushort C2M_StartBossFightRequest = 12011;
		 public const ushort M2C_StartBossFightRequest = 12012;
		 public const ushort C2M_StartBossFight = 12013;
		 public const ushort M2C_StartBossFight = 12014;
		 public const ushort M2C_SendBossInfo = 12015;
		 public const ushort M2C_ReSendBossInfo = 12016;
		 public const ushort M2C_BossFightVictory = 12017;
		 public const ushort M2C_BossFightDefeat = 12018;
		 public const ushort M2C_BossDead = 12019;
		 public const ushort M2C_BossBeDefeat = 12020;
		 public const ushort RewardItem = 12021;
		 public const ushort StateBuffInfo = 12022;
		 public const ushort C2M_AutoBattle = 12023;
		 public const ushort M2C_AutoBattle = 12024;
		 public const ushort C2M_SelectEnermy = 12025;
		 public const ushort M2C_SelectEnermy = 12026;
		 public const ushort C2M_SelectTeamMember = 12027;
		 public const ushort M2C_SelectTeamMember = 12028;
		 public const ushort M2C_PlaySkill = 12029;
		 public const ushort M2C_MonsterPlaySkill = 12030;
		 public const ushort M2C_PlaySkillEffect = 12031;
		 public const ushort M2C_BattleSkillRet = 12032;
		 public const ushort M2C_BattleChangeMP = 12033;
		 public const ushort M2C_BattleChangeState = 12034;
		 public const ushort M2C_BattleTouchState = 12035;
		 public const ushort M2C_MainstoryMonsterDead = 12036;
		 public const ushort M2C_UnitDead = 12037;
		 public const ushort M2C_SendReward = 12038;
		 public const ushort C2M_GetBattleStateBuff = 12039;
		 public const ushort M2C_GetBattleStateBuff = 12040;
		 public const ushort C2M_StartBattleIdleFight = 12041;
		 public const ushort M2C_StartBattleIdleFight = 12042;
		 public const ushort C2M_EndBattleIdleFight = 12043;
		 public const ushort M2C_EndBattleIdleFight = 12044;
		 public const ushort M2C_InitTrialCopyMap = 12045;
		 public const ushort C2M_StartTrialCopyFight = 12046;
		 public const ushort M2C_StartTrialCopyFight = 12047;
		 public const ushort C2M_StartPKFight = 12048;
		 public const ushort M2C_StartPKFight = 12049;
		 public const ushort M2C_SendStartPK = 12050;
		 public const ushort M2C_PKFightVictory = 12051;
		 public const ushort M2C_PKFightDefeat = 12052;
		 public const ushort C2M_StartTestBattleFight = 12053;
		 public const ushort M2C_StartTestBattleFight = 12054;
		 public const ushort C2M_EndTestBattleFight = 12055;
		 public const ushort M2C_EndTestBattleFight = 12056;
	}
}
