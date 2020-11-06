using ET;
using Cal.DataTable;
using System;
using UnityEngine;

namespace ET
{

    public class UnitAwakeSystem : AwakeSystem<Unit>
    {
        public override void Awake(Unit self)
        {
            self.Awake();
        }
    }

    public class UnitDestroySystem : DestroySystem<Unit>
    {
        public override void Destroy(Unit self)
        {
            self.Destroy();
        }
    }

    public sealed class Unit : Entity
    {
        public int ConfigId;
        public bool IsLeader { get; set; }
        public bool IsFight { get; set; }
        public bool IsAlive { get; set; }

        private UnitType unitType;
        public UnitType UnitType
        {
            get => unitType;

            set
            {
                if (unitType == value)
                    return;
                unitType = value;
                switch (unitType)
                {
                    case UnitType.None:
                    case UnitType.Player:
                    case UnitType.MainStoryMonster:
                    case UnitType.TrialCopyMonster:
                    case UnitType.BossMonster:
                    case UnitType.Enermy:
                    case UnitType.TeamMember:
                        HideUnitComponent.Remove(this);
                        break;
                    case UnitType.OtherPlayer:
                    case UnitType.NPC:
                    case UnitType.MapMonster:
                    case UnitType.TransPoint:
                        HideUnitComponent.Add(this);
                        break;
                    default:
                        break;
                }
                Game.EventSystem.Publish(new ET.EventType.UpdateUnitType
                { 
                   unit =this,
                }).Coroutine();
            }
        }

        private int skinId;
        public int SkinId
        {
            get => skinId;
            set
            {
                if (skinId != 0 && skinId != value)
                {
                    ChangeSkin(value).Coroutine();
                }
                skinId = value;
            }
        }
        public void Awake()
        {
            IsLeader = true;
            IsFight = false;
            IsAlive = true;
        }

        private Vector3 position;
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                if (position == value)
                    return;
                position = value;
                Game.EventSystem.Publish(new ET.EventType.UpdateUnitPosition
                {
                    unit = this,
                    pos = value
                }).Coroutine();


            }
        }
        private int yAngle;
        public int YAngle
        {
            get => yAngle;
            set
            {
                if (yAngle == value)
                    return;
                yAngle = value;
                Game.EventSystem.Publish(new ET.EventType.UpdateUnitAngle
                {
                    unit = this,
                    yAngle = value
                }).Coroutine();

            }
        }

        private async ETTask ChangeSkin(int skinId)
        {
            if (skinId == 0)
                return;
            SkinBase skinBase = DataTableHelper.Get<SkinBase>(skinId);
            if (skinBase == null)
            {
                Log.Error($"没有skinId = {skinId}");
                return;
            }
            //if (unitView)
            //    await unitView.ChangeSkin(skinBase.PrfabId);
        }
        public override void Dispose()
        {
            if (!this)
                return;
            Game.EventSystem.Publish(new ET.EventType.OnDisposeUnit
            {
                unit = this
            }).Coroutine();
            base.Dispose();
        }
        public void Destroy()
        {
            UnitCharacterComponent.Instance.Remove(this);
            HideUnitComponent.Remove(this);
            skinId = 0;
            yAngle = 0;
            position = default;
            IsLeader = false;
            IsAlive = false;
            IsFight = false;
        }
    }
}