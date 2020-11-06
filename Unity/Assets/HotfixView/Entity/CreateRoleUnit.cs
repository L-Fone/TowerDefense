using ET;
using Cal.DataTable;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    
    public class CreateRoleUnitAwakeSystem : AwakeSystem<CreateRoleUnit,GameObject>
    {
        public override async void Awake(CreateRoleUnit self,GameObject go)
        {
            self.gameObject = go;
            go.GetOrAddComponent<ComponentView>().Component = self;
            self.SelectEffect = (await ResourceViewHelper.LoadPrefabAsync(PrefabId.Select)).gameObject;
            self.SelectEffect.transform.position = go.transform.position;
            self.SelectEffect.SetActive(false);
        }
    }
    
    public class CreateRoleUnitDestroySystem : DestroySystem<CreateRoleUnit>
    {
        public override void Destroy(CreateRoleUnit self)
        {
            ResourceViewHelper.DestoryPrefabAsync(self.gameObject);
            ResourceViewHelper.DestoryPrefabAsync(self.SelectEffect);
            self.gameObject = null;
            self.SelectEffect = null;
        }
    }

    public class CreateRoleUnit:Entity
    {
        public GameObject gameObject;
        public GameObject SelectEffect { get; set; }

        public int JobId { get; internal set; }
    }
}
