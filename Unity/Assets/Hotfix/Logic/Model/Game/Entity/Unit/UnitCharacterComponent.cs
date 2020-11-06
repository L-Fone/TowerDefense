using ET;
using Cal.DataTable;
using System;
using System.Collections.Generic;
using UnityEngine;
using ET.EventType;

namespace ET
{

    public class UnitCharacterComponentAwakeSystem : AwakeSystem<UnitCharacterComponent>
    {
        public override void Awake(UnitCharacterComponent self)
        {
            UnitCharacterComponent.Instance = self;
        }
    }
    public class UnitCharacterComponentDestroySystem : DestroySystem<UnitCharacterComponent>
    {
        public override void Destroy(UnitCharacterComponent self)
        {
            self.Destroy();
        }
    }
    public class UnitCharacterComponent : Entity
    {
        public static UnitCharacterComponent Instance { get; set; }


        private readonly Dictionary<long, UnitCharacter> _clientUnitCharacterDic = new Dictionary<long, UnitCharacter>();
        public void Add(long Id,string name,int level,int hp=0,int maxHp=0)
        {
            UnitCharacter unitCharacter = EntityFactory.CreateWithParentAndId<UnitCharacter>(this,Id);
            unitCharacter.NickName = name;
            unitCharacter.Level = level;
            unitCharacter.Hp = hp;
            unitCharacter.MaxHp = maxHp;

            Add(unitCharacter.Id, unitCharacter);
        }
        public void Add(_UnitCharacter unitServerCharacter)
        {
            UnitCharacter unitCharacter = EntityFactory.CreateWithParentAndId<UnitCharacter>(this,unitServerCharacter.Id);
            unitCharacter.NickName = unitServerCharacter.NickName;
            unitCharacter.JobId = unitServerCharacter.JobId;
            unitCharacter.CampType = unitServerCharacter.CampType;
            unitCharacter.Level = unitServerCharacter.Level;
            unitCharacter.Hp = unitServerCharacter.Hp;
            unitCharacter.MaxHp = unitServerCharacter.MaxHp;
            unitCharacter.Mp = unitServerCharacter.Mp;
            unitCharacter.MaxMp = unitServerCharacter.MaxMp;
            if (unitCharacter.Id == GlobalVariable.MyId)
            {
                unitCharacter.Exp = unitServerCharacter.Exp;
                GetMaxExpByLevel(unitCharacter);
            }

            Add(unitCharacter.Id, unitCharacter);
        }
        private void Add(long id,UnitCharacter clientUnitCharacter)
        {
            if (_clientUnitCharacterDic.ContainsKey(Id))
            {
                Log.Error($"_clientUnitCharacterDic already has the key = {Id}");
            }
            _clientUnitCharacterDic[id] = clientUnitCharacter;
        }
        public void GetMaxExpByLevel(UnitCharacter clientUnitCharacter)
        {
            RoleGrowth roleGrowth = (RoleGrowth)DataTableComponent.Instance.Get(DataTypeConst.RoleGrowth, (int)clientUnitCharacter.JobType);
            if (roleGrowth == null)
            {
                return;
            }
            long needExp = (int)(roleGrowth.ExpArr(0).ExpBaseNumber * Mathf.Pow(clientUnitCharacter.Level, roleGrowth.ExpArr(0).ExpPower));
            clientUnitCharacter.MaxExp = needExp;
        }
        public UnitCharacter Update(long id, string nickName = null, int jobId = -1, CampType campType = CampType.NoneCamp, string family = null, string title = null, int level = -1, int transLevel = -1, int hp = -1, int maxHp = -1, int mp = -1, int maxMp = -1, long exp = -1, long coin = -1, int yuanBao = -1, int voucher = -1, IEnumerable<int> characterPointList = null)
        {
            if (id == 0)
            {
                id = GlobalVariable.MyId;
            }
            if (_clientUnitCharacterDic.TryGetValue(id, out var unitCharacter))
            {
                unitCharacter.NickName = nickName ?? unitCharacter.NickName;
                unitCharacter.JobId = jobId == -1 ? unitCharacter.JobId : jobId;
                unitCharacter.CampType = campType == CampType.NoneCamp ? unitCharacter.CampType : campType;
                unitCharacter.Level = level == -1 ? unitCharacter.Level : level;
                unitCharacter.Hp = hp == -1 ? unitCharacter.Hp : hp;
                unitCharacter.MaxHp = maxHp == -1 ? unitCharacter.MaxHp : maxHp;
                unitCharacter.Mp = mp == -1 ? unitCharacter.Mp : mp;
                unitCharacter.MaxMp = maxMp == -1 ? unitCharacter.MaxMp : maxMp;
                unitCharacter.Exp = exp == -1 ? unitCharacter.Exp : exp;
            }
            return unitCharacter;
        }
        public UnitCharacter Update(_UnitCharacter unitServerCharacter)
        {
            long Id = unitServerCharacter.Id;
            if (!_clientUnitCharacterDic.TryGetValue(Id, out var unitCharacter))
            {
                unitCharacter = EntityFactory.CreateWithParentAndId<UnitCharacter>(this,Id);
                Add(unitCharacter.Id, unitCharacter);
            }
            unitCharacter.Id = unitServerCharacter.Id;
            unitCharacter.NickName = unitServerCharacter.NickName;
            unitCharacter.JobId = unitServerCharacter.JobId;
            unitCharacter.CampType = unitServerCharacter.CampType;
            unitCharacter.Level = unitServerCharacter.Level;
            unitCharacter.Hp = unitServerCharacter.Hp;
            unitCharacter.MaxHp = unitServerCharacter.MaxHp;
            unitCharacter.Mp = unitServerCharacter.Mp;
            unitCharacter.MaxMp = unitServerCharacter.MaxMp;
            if (unitCharacter.Id == GlobalVariable.MyId)
            {
                unitCharacter.Exp = unitServerCharacter.Exp;
                GetMaxExpByLevel(unitCharacter);
            }
            return unitCharacter;
        }
        public UnitCharacter AddUpdate(long id, int level = 0, int hp = 0, int maxHp = 0, int mp = 0, int maxMp = 0, long exp = 0, long coin = 0, int yuanBao = 0, int voucher = 0)
        {
            if (id == 0)
            {
                id = UnitComponent.MyUnit.Id;
            }
            if (_clientUnitCharacterDic.TryGetValue(id, out var unitCharacter))
            {
                unitCharacter.Level += level;
                unitCharacter.Hp += hp;
                unitCharacter.MaxHp += maxHp;
                unitCharacter.Mp += mp;
                unitCharacter.MaxMp += maxMp;
                unitCharacter.Exp += exp;

                Add(unitCharacter.Id, unitCharacter);
            }
            return unitCharacter;
        }
        public UnitCharacter Get(long id)
        {
            _clientUnitCharacterDic.TryGetValue(id, out var unitCharacter);
            return unitCharacter;
        }
        public UnitCharacter Get()
        {
            _clientUnitCharacterDic.TryGetValue(UnitComponent.MyUnit.Id, out var unitCharacter);
            return unitCharacter;
        }
        public void Remove(long id)
        {
            var character = Get(id);
            if (!_clientUnitCharacterDic.Remove(id))
            {
                Log.Error($"character == null where Id = {id}");
            }
            character.Dispose();
        }
        public void Remove(Unit unit)
        {
            switch (unit.UnitType)
            {
                case UnitType.None:
                    break;
                case UnitType.Player:
                case UnitType.MainStoryMonster:
                case UnitType.TrialCopyMonster:
                case UnitType.BossMonster:
                case UnitType.OtherPlayer:
                case UnitType.Enermy:
                case UnitType.TeamMember:
                    Remove(unit.Id);
                    break;
                case UnitType.NPC:
                    break;
                default:
                    break;
            }
        }

        public void Destroy()
        {
            foreach (var item in _clientUnitCharacterDic.Values)
            {
                item.Dispose();
            }
            _clientUnitCharacterDic.Clear();
        }
    }
}
