using Cal.DataTable;
using ET;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    namespace EventType
    {

        public struct AfterCreateZoneScene
        {
            public Scene zoneScene;
        }

        public struct InitSceneEnd
        {

            public Scene zoneScene;
        }
        public struct Quit
        {

        }
        public struct EnterGame_InitData
        {
            public Scene zoneScene;
        }
        public struct EnterGameFinish_CreateMainUI
        {
            public Scene zoneScene;
        }
        public struct EnterGameFinish_SetCharacter
        {
            public bool isOnline;
        }
        public struct OnCreateUnit
        {
            public Unit unit;
            public RoleConfig roleConfig;
        }
        public struct OnDisposeUnit
        {
            public Unit unit;
        }
        public struct UpdateUnitType
        {
            public Unit unit;

        }
        public struct LoadUnityScene
        {
            public int Id;
        }
        public struct OnCreateUNPC
        {
            public Unit unit;
            public int prefabId;
        }
        public struct OnCreateTransPoint
        {
            public Unit unit;
            public Scene zoneScene;
        }
        public struct PathfindingResult
        {
            public Unit unit;
            public Vector2 currPos;
            public Vector2 targetPos;
            public float speed;
        }
        public struct BattleStart
        {
            public Scene zoneScene;
        }
        public struct BattleEnd
        {
            public Scene zoneScene;
        }
        public struct TranslateSceneStart
        {
            public bool isAutoEnd;
        }
        public struct TranslateSceneEnd
        {

        }
        public struct UpdateCharacterUI
        {
            public BattleCharacter battleCharacter;
        }
        public struct UpdateCommonCharacterUI
        {
            public long Id;
        }
        public struct UpdateUnitPosition
        {
            public Unit unit;
            public Vector3 pos;
        }
        public struct UpdateUnitRotation
        {
            public Unit unit;
            public Quaternion rotation;
        }
        public struct UpdateNPCTaskState
        {
            public Unit unit;
            public TaskState taskState;
        }
        public struct StartBossFight
        {
            public Scene zoneScene;
            public long Id;
            public int BossId;
            public int hp;
        }
        public struct StartMainStoryFight
        {
            public Scene zoneScene;
            public RepeatedField<MonsterUnitInfo> list;
        }
        public struct ReStartMainStoryFight
        {
            public Scene zoneScene;
            public RepeatedField<ReMonsterUnitInfo> list;
        }
        public struct StartTrialCopyFight
        {
            public Scene zoneScene;
            public int copyId;
            public RepeatedField<long> list;
        }
        public struct ChangeMap
        {
            public Scene zoneScene;
            public int mapId;
            public Vector3 pos;
        }
        public struct OpenCharacterUI
        {
            public Scene zoneScene;
        }
        public struct OpenBagUI
        {
            public Scene zoneScene;
        }
        public struct OpenMeltingUI
        {
            public Scene zoneScene;
        }
        public struct OpenForgeUI
        {
            public Scene zoneScene;
        }
        public struct OpenUpgradeUI
        {
            public Scene zoneScene;
        }
        public struct OpenSkillUI
        {
            public Scene zoneScene;
        }
        public struct OpenStoreUI
        {
            public Scene zoneScene;
        }
        public struct OpenShopUI
        {
            public Scene zoneScene;
        }
        public struct OpenMarketUI
        {
            public Scene zoneScene;
        }
        public struct OpenConsignmentUI
        {
            public Scene zoneScene;
            public int totalPage;
            public RepeatedField<ConsignMap> mapList;
        }
        public struct StateBuffUI_Open
        {
            public Scene zoneScene;
        }
        public struct StateBuffUI_Init
        {
            public Scene zoneScene;
        }
        public struct OpenFriendUI
        {
            public Scene zoneScene;
        }
        public struct ButlerUI_Open
        {
            public Scene zoneScene;
        }
        public struct TaskUI_Open
        {
            public Scene zoneScene;
        }
        public struct NPCTaskUI_Open
        {
            public Scene zoneScene;
            public RepeatedField<NPCTask> taskList;
        }
        public struct MailUI_Open
        {
            public Scene zoneScene;
        }
        public struct RankingUI_Open
        {
            public Scene zoneScene;
        }
        public struct TransPointUI_Left_Open
        {
            public Scene zoneScene;
        }
        public struct TransPointUI_Right_Open
        {
            public Scene zoneScene;
        }
        public struct ShowTipUI
        {
            public Scene zoneScene;
            public TipType tipType;
            public bool isClearIpt;
            public string tip;
            public Action<string> okAction;
            public Action<string> noAction;
        }
        public struct ShowHpBar
        {
            public Scene zoneScene;
            public Unit unit;
        }
        public struct HideHpBar
        {
            public Scene zoneScene;
            public Unit unit;
        }
        public struct ClickBoss_ShowAttackUI
        {
            public Scene zoneScene;
            public long key;
        }
        public struct SetHudCharacter
        {
            public Scene zoneScene;
            public Unit unit;
            public int hp;
            public int maxHp;
            public int level;
            public string name;
            public FairyGUI.ProgressTitleType progressTitleType;
        }
        public struct SetHudCharacter_ChangeHp
        {
            public Scene zoneScene;
            public Unit unit;
        }
        public struct SetHudCharacter_ChangeLevel
        {
            public Scene zoneScene;
            public Unit unit;
            public int level;
            public long exp;
        }
        public struct SetGameObjectActive
        {
            public Unit unit;
            public bool isVisible;
        }
        public struct PlayAnimation
        {
            public Unit unit;
            public AinmationKey ainmationKey;
            public Vector3 dir;
        }
        //public struct UnitMove
        //{
        //    public Unit unit;
        //    public float spd;
        //    public Vector3 dir;
        //}        
        public struct PlayAnimation_Attack
        {
            public Unit unit;
        }
        public struct PlayAnimation_Hurt
        {
            public Unit unit;
        }
        public struct PlayEffect
        {
            public Unit unit;
            public Unit targetUnit;
            public int skilId;
        }
        public struct ShowSkillNameUI
        {
            public Scene zoneScene;
            public Unit unit;
            public string skillName;
        }
        public struct ChangeMainUISlotCD
        {
            public Scene zoneScene;
            public int Id;
            public int CD;
        }
        public struct PopupDamage
        {
            public Unit unit;
            public int value;
            public bool isCrit;
        }
        public struct ShowBattleReword
        {
            public Unit unit;
            public long exp;
            public long coin;
            public RepeatedField<RewardItem> list;
        }
        public struct CreateTransPoint
        {
            public Scene zoneScene;
            public int targetSceneId;
            public int targetMaplayer;
            public Vector2 pos;
        }
        public struct RemoveTransPoint
        {
            public Scene zoneScene;
        }
        public struct ShowOthersUnit
        {
            public Scene zoneScene;
        }
        public struct HideOthersUnit
        {
            public Scene zoneScene;
        }
        public struct AddChatUIText
        {
            public Scene zoneScene;
            public string name;
            public ChatType chatType;
            public string chatContent;
        }
        public struct UpdateEnergy
        {
            public Scene zoneScene;
            public int energy;
        }
        public struct UpdateDeadTomb
        {
            public Unit unit;
            public bool isGenerate;
        }

        public struct EnterGame_Open
        {
            public Scene zoneScene;
        }
        public struct GenerateTowerPoint
        {
            public Scene zoneScene;
            public Vector3 point;

            public TowerPointInfo towerPointInfo;
        }
        public struct AddTrigger
        {
            public Unit unit;
            public Action<Unit> onEnter;
            public Action<Unit> onExit;
        }
        public struct ShowDebugAtkLine
        {
            public Unit unit;
            public Unit target;
            public int damage;
            public int hp;
            public int maxHp;
        }
        public struct ShowPopupUI
        {
            public struct PopupMenuInfo:IComparable<PopupMenuInfo>,IEquatable<PopupMenuInfo>
            {
                public string name;
                public Action action;

                public int CompareTo(PopupMenuInfo other)
                {
                   return name.CompareTo(other.name);
                }

                public bool Equals(PopupMenuInfo other)
                {
                    return name.Equals(other.name);
                }
            }
            public Scene zoneScene;
            public List<PopupMenuInfo> popupMenuInfo;
        }
    }
}
