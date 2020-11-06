using Cal.DataTable;
using ET;
using ET.DataNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ET
{
    
    
    public class CreateRoleUnitComponentUpdateSystem : UpdateSystem<CreateRoleUnitComponent>
    {
        public override void Update(CreateRoleUnitComponent self)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //射线检测怪物
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100, 1 << LayerMask.NameToLayer("Player"));
                if (hit.collider != null)
                {
                    var go = hit.collider.gameObject;
                    CreateRoleUnit unit = (CreateRoleUnit)go.GetComponentInParent<ComponentView>().Component;
                    self.ZoneScene().GetComponent<CreateRoleUnitComponent>().SetAllEffectInactive();
                    unit.SelectEffect.SetActive(true);
                    GlobalVariable.JobId = unit.JobId;
                }
            }
        }
    }
    public class CreateRoleUnitComponentDestroySystem : DestroySystem<CreateRoleUnitComponent>
    {
        public override void Destroy(CreateRoleUnitComponent self)
        {
            self.Clear();
        }
    }
    public static class CreateRoleUnitComponentSystem
    {
        public static void SetAllEffectInactive(this CreateRoleUnitComponent self)
        {
            foreach (var item in self.GetAll())
            {
                item.SelectEffect.SetActive(false);
            }
        }
    }
    public class CreateRoleUnitComponent:Entity
    {
        private HashSet<CreateRoleUnit> roleHashSet = new HashSet<CreateRoleUnit>();

        public void Add(CreateRoleUnit unit)
        {
            roleHashSet.Add(unit);
        }
        public IEnumerable<CreateRoleUnit> GetAll()
        {
            return roleHashSet;
        }
        public async ETVoid GenerateSelectRole()
        {
            GlobalVariable.JobId = 1;
            for (int id = 1; id < 9; id++)
            {
                var positions = DataTableHelper.Get<RolesPosition>(id);
                var tran =await ResourceViewHelper.LoadPrefabAsync(id);
                var unit = EntityFactory.CreateWithParent<CreateRoleUnit, GameObject>(Game.Scene, tran.gameObject);
                tran.position = new UnityEngine.Vector3(positions.Posx, positions.Posy, PosHelper.PlayerPos_Z);
                unit.JobId = id;
                Add(unit);
            }
            Game.EventSystem.Publish(new ET.EventType.TranslateSceneEnd
            {

            }).Coroutine();
        }
        public void Clear()
        {
            foreach (var unit in roleHashSet)
            {
                unit.Dispose();
            }
            roleHashSet.Clear();
        }

    }
}
