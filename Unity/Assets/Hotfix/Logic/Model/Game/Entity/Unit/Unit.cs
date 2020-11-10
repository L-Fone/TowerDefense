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
                Game.EventSystem.Publish_Sync(new ET.EventType.UpdateUnitPosition
                {
                    unit = this,
                    pos = value
                });


            }
        }
        private Quaternion rotation;
        public Quaternion Rotation
        {
            get => rotation;
            set
            {
                if (rotation == value)
                    return;
                rotation = value;
                Game.EventSystem.Publish_Sync(new ET.EventType.UpdateUnitRotation
                {
                    unit = this,
                    rotation = value
                });

            }
        }

        private async ETTask ChangeSkin(int skinId)
        {
          
        }

        public void SetPosition(float[] pos)
        {
            if (pos == null || pos.Length != 3)
                throw new Exception("pos is invalid!");
            Position = new Vector3(pos[0], pos[1], pos[2]);
        }
        public void SetYAngle(float angle)
        {
            Rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
        internal void ChangePosition(Vector3 vector3)
        {
            Position +=vector3;
        }
        public override void Dispose()
        {
            if (!this)
                return;
            Game.EventSystem.Publish_Sync(new ET.EventType.OnDisposeUnit
            {
                unit = this
            });
            base.Dispose();
        }
        public void Destroy()
        {
            UnitCharacterComponent.Instance.Remove(this);
            HideUnitComponent.Remove(this);
            skinId = 0;
            rotation = default;
            position = default;
            IsLeader = false;
            IsAlive = false;
            IsFight = false;
        }
    }
}