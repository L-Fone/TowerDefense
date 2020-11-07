using System;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ET
{
    
    public class GameObjectPoolAwakeSystem : AwakeSystem<GameObjectPool>
    {
        public override void Awake(GameObjectPool self)
        {
            GameObjectPool.Instanse = self;
            self.Awake();
        }
    }

    
    public class GameObjectPoolDestroySystem : DestroySystem<GameObjectPool>
    {
        public override void Destroy(GameObjectPool self)
        {
            self.Destroy();
        }
    }

    public class GameObjectPool : Entity
    {
        public static GameObjectPool Instanse;
        /// <summary>
        /// 游戏物体对象池字典
        /// </summary>
        private Dictionary<int, GameObjectPoolEntity> m_SpawnPoolDic;
        /// <summary>
        /// 实例ID对应对象池Id
        /// </summary>
        private Dictionary<int, int> m_InstanceIdPoolDic;
        /// <summary>
        /// 空闲预设池队列
        /// </summary>
        private Queue<PrefabPool> m_PrefabPoolQueue;

        public void Awake()
        {
            m_SpawnPoolDic = new Dictionary<int, GameObjectPoolEntity>();
            m_InstanceIdPoolDic = new Dictionary<int, int>();
            m_PrefabPoolQueue = new Queue<PrefabPool>();

            InstanceHandler.InstantiateDelegates += this.InstantiateDelegate;
            InstanceHandler.DestroyDelegates += this.DestroyDelegate;

            Init(UnityRoot.Instance.GameObjectPoolGroups, UnityRoot.Instance.ObjPoolParent);
        }
        /// <summary>
        /// 对象池物体创建回调
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        private GameObject InstantiateDelegate(GameObject prefab, Vector3 pos, Quaternion rot, object userData)
        {
            ResourceEntity resourceEntity = userData as ResourceEntity;
            if (resourceEntity == null)
            {
                Log.Error("资源信息不存在 resourceEntity=" + resourceEntity.ResourceName);
                return null;
            }

            GameObject obj = UnityEngine.Object.Instantiate(prefab, pos, rot) as GameObject;

            //注册
            //Game.Scene.GetComponent<Pool>().RegistInstanceResource(obj.GetInstanceID(), resourceEntity);
            resourceEntity.Dispose();
            return obj;
        }
        /// <summary>
        /// 对象池物体销毁回调
        /// </summary>
        /// <param name="instance"></param>
        private void DestroyDelegate(GameObject instance)
        {
            UnityEngine.Object.Destroy(instance);
            //Game.Scene.GetComponent<Resource>()?.ResourceLoaderComponent.UnloadGameObject(instance);
        }


        /// <summary>
        /// 物体对象池的初始化
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public void Init(GameObjectPoolEntity[] arr, Transform parent)
        {
            int len = arr.Length;
            for (int i = 0; i < len; i++)
            {
                GameObjectPoolEntity entity = arr[i];
                if (entity.Pool != null)
                {
                    Destroy(entity);
                }
                //创建对象池
                SpawnPool pool = PathologicalGames.PoolManager.Pools.Create(entity.PoolName);
                pool.group.parent = parent;
                pool.group.localPosition = Vector3.zero;
                entity.Pool = pool;

                m_SpawnPoolDic[entity.PoolId] = entity;
            }
        }
        private void Destroy(GameObjectPoolEntity entity)
        {
            UnityEngine.Object.Destroy(entity.Pool.gameObject);
            entity.Pool = null;
        }


        public async ETTask<ValueTuple<Transform, bool>> GameObjectSpawn(int id, int poolId, string path, bool cullDespawned, int cullAbove, int cullDelay, int cullMaxPerPass)
        {
            return await Spawn(id, poolId, path, cullDespawned, cullAbove, cullDelay, cullMaxPerPass);
        }

        /// <summary>
        /// 对象回池
        /// </summary>
        /// <param name="instance"></param>
        public void GameObjectDespawn(Transform instance)
        {
            Despawn(instance);
        }

        #region Spawn
        /// <summary>
        /// 从对象池中获取对象
        /// </summary>
        /// <param name="prefabId"></param>
        /// <param name="onComplete"></param>
        public async ETTask<ValueTuple<Transform, bool>> Spawn(int id, int poolId,  string path, bool cullDespawned, int cullAbove, int cullDelay, int cullMaxPerPass)
        {

            //using (await Game.Scene.GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.ResourcesLoader, 10000))
            //{
                GameObjectPoolEntity gameObjectPoolEntity = m_SpawnPoolDic[poolId];

                //使用预设编号 当做池ID
                PrefabPool prefabPool = gameObjectPoolEntity.Pool.GetPrefabPool(id);
                if (prefabPool != null)
                {
                    //拿到一个实例
                    Transform retTrans = prefabPool.TrySpawnInstance();
                    if (retTrans != null)
                    {
                        int instanceID = retTrans.gameObject.GetInstanceID();
                        m_InstanceIdPoolDic[instanceID] = poolId;
                        return (retTrans, false);
                    }
                }

                GameObject go = await ResourceHelper.LoadAssetAsync<GameObject>(path);

                ResourceEntity resEntity = EntityFactory.Create<ResourceEntity>(Game.Scene);
                resEntity.Target = go;
                resEntity.ResourceName = path;
                Transform prefab = go.transform;

                prefabPool = gameObjectPoolEntity.Pool.GetPrefabPool(id);
                if (prefabPool == null)
                {
                    //先去队列里面找空闲的池
                    if (m_PrefabPoolQueue.Count > 0)
                    {
                        Log.Info("从队列取");
                        prefabPool = m_PrefabPoolQueue.Dequeue();

                        prefabPool.PrefabPoolId = id;
                        gameObjectPoolEntity.Pool.AddPrefabPool(prefabPool);

                        prefabPool.prefab = prefab;
                        prefabPool.prefabGO = prefab.gameObject;
                        prefabPool.AddPrefabToDic(prefab.name, prefab);
                    }
                    else
                    {
                        prefabPool = new PrefabPool(prefab, id);
                        gameObjectPoolEntity.Pool.CreatePrefabPool(prefabPool, resEntity);
                    }
                    prefabPool.OnPrefabPoolClear = (PrefabPool pool) =>
                    {
                        //预设池加入队列
                        pool.PrefabPoolId = 0;
                        gameObjectPoolEntity.Pool.RemovePrefabPool(pool);
                        m_PrefabPoolQueue.Enqueue(pool);
                    };

                    //这些属性从表格读取
                    prefabPool.cullDespawned = cullDespawned;
                    prefabPool.cullAbove = cullAbove;
                    prefabPool.cullDelay = cullDelay;
                    prefabPool.cullMaxPerPass = cullMaxPerPass;
                }
                bool isNewInstance = false;
                Transform trans = gameObjectPoolEntity.Pool.Spawn(prefab, ref isNewInstance, resEntity);
                int instanceId = trans.gameObject.GetInstanceID();
                m_InstanceIdPoolDic[instanceId] = poolId;

                return (trans, isNewInstance);
            }

        //}


        #endregion

        #region Despawn
        /// <summary>
        /// 对象回池
        /// </summary>
        /// <param name="poolId"></param>
        /// <param name="instances"></param>
        public void Despawn(int poolId, Transform instance)
        {
            if (m_SpawnPoolDic.TryGetValue(poolId, out GameObjectPoolEntity entity))
            {
                instance.SetParent(entity.Pool.transform);
                entity.Pool.Despawn(instance);
            }
        }
        /// <summary>
        /// 对象回池
        /// </summary>
        /// <param name="instance"></param>
        public void Despawn(Transform instance)
        {
            int instanceId = instance.gameObject.GetInstanceID();
            if (m_InstanceIdPoolDic.TryGetValue(instanceId, out var poolId))
            {
                m_InstanceIdPoolDic.Remove(instanceId);
                Despawn(poolId, instance);
            }

        }
        #endregion


        public void Destroy()
        {
            m_SpawnPoolDic?.Clear();
        }
    }
}