using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    /*
    * Manages multiple object pools 
    */
    public class GameObjectPools
    {
        private readonly Dictionary<string, DynamicObjectPool> _pools;
        private readonly Transform _parentTransform;
        private readonly int _defMinCnt;

        public GameObjectPools(Transform parentTransform, int defMinCnt)
        {
            _pools = new Dictionary<string, DynamicObjectPool>();
            _parentTransform = parentTransform;
            _defMinCnt = defMinCnt;
        }

        public void EnsurePoolDefinition(GameObject objectPrefab, int minCnt)
        {
            if (!_pools.ContainsKey(objectPrefab.name))
            {
                DynamicObjectPool newPool = new DynamicObjectPool(objectPrefab, _parentTransform);
                newPool.InitiatePool(minCnt);
                _pools.Add(objectPrefab.name, newPool);
            }
        }

        public GameObject PoolObject(GameObject objectPrefab)
        {
            DynamicObjectPool pool;
            if (_pools.TryGetValue(objectPrefab.name, out pool))
            {
                return pool.GetPooledObject();
            }
            EnsurePoolDefinition(objectPrefab, _defMinCnt);
            pool = _pools[objectPrefab.name];
            return pool.GetPooledObject();
        }

    }
}