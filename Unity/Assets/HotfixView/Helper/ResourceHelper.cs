using System;
using System.Collections.Generic;
using ET;
using Cal.DataTable;
using UnityEngine;
using ET;

namespace ET
{
    public class ResourceViewHelper
    {
        public static async ETTask<ValueTuple<Transform, bool>> LoadPrefabBoolAsync(int prefabId)
        {
            var sysPrefab =DataTableHelper.Get<Sys_Prefab>(prefabId);
            var (trans, isNew) = await GameObjectPool.Instanse.GameObjectSpawn(sysPrefab.Id, sysPrefab.PoolId, sysPrefab.AssetPath, sysPrefab.CullDespawned, sysPrefab.CullAbove, sysPrefab.CullDelay, sysPrefab.CullMaxPerPass);
            return (trans, isNew);
        }
        public static async ETTask<Transform> LoadPrefabAsync(int prefabId)
        {
            return (await LoadPrefabBoolAsync(prefabId)).Item1;
        }
        public static void DestoryPrefabAsync(GameObject go)
        {
            if (!go) return;
            DestoryPrefabAsync(go.transform);
        }
        public static void DestoryPrefabAsync(Transform go)
        {
            if (!go) return;
            GameObjectPool.Instanse.GameObjectDespawn(go);
        }

    }
}
