using ET;
using Google.Protobuf;
using System.Collections.Generic;
using System.Linq;

namespace ET
{

    public class UnitComponentSystem : AwakeSystem<UnitComponent>
    {
        public override void Awake(UnitComponent self)
        {
            self.Awake();
        }
    }

    public class UnitComponent : Entity
    {
        public static UnitComponent Instance { get; private set; }

        public static Unit MyUnit { get; set; }

        private readonly Dictionary<long, Unit> idUnits = new Dictionary<long, Unit>();

        public void Awake()
        {
            Instance = this;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (Unit unit in this.idUnits.Values)
            {
                unit.Dispose();
            }

            this.idUnits.Clear();

            Instance = null;
        }

        public void Add(Unit unit)
        {
            this.idUnits.Add(unit.Id, unit);
        }

        public Unit Get(long id)
        {
            this.idUnits.TryGetValue(id, out Unit unit);
            return unit;
        }

        public void Remove(long id)
        {

            if (this.idUnits.TryGetValue(id, out var unit))
            {
                this.idUnits.Remove(id);
                unit.Dispose();
            }
            else
            {
                Log.Error($"{id}不存在");
            }
        }
        public void Remove(Unit unit, bool isOffLine = false)
        {
            this.idUnits.Remove(unit.Id);
            unit.Dispose();

        }
        private List<Unit> _needRemoveUnitList;
        public void RemoveAll()
        {
            //_needRemoveUnitList = _needRemoveUnitList ?? new List<Unit>();
            //_needRemoveUnitList.Clear();
            foreach (var unit in GetAll())
            {
                //if (unit.Id != MyUnit.Id)
                //{
                    //_needRemoveUnitList.Add(unit);
                //}
               unit.Dispose();
            }
            idUnits.Clear();
            //foreach (var id in _needRemoveUnitList)
            //{
            //    Remove(id);
            //}
        }

        public void RemoveNoDispose(long id)
        {
            this.idUnits.Remove(id);
        }

        public int Count
        {
            get
            {
                return this.idUnits.Count;
            }
        }

        public IEnumerable<Unit> GetAll()
        {
            return this.idUnits.Values;
        }
    }
}