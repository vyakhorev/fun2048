using System.Collections.Generic;
using UnityEngine;

namespace Fun2048
{
    /*
    * Manages multiple object pools 
    */
    public class GameObjectPools
    {
        private readonly Dictionary<string, DynamicObjectPool> _pools;
        readonly Transform _objectPoolContainer = new GameObject("Object pool container").transform;

        public GameObjectPools()
        {
            _pools = new Dictionary<string, DynamicObjectPool>();
        }

        public void EnsurePoolDefinition(GameObject objectPrefab)
        {
            if (!_pools.ContainsKey(objectPrefab.name))
            {
                DynamicObjectPool newPool = new DynamicObjectPool(objectPrefab, _objectPoolContainer);
                newPool.InitiatePool();
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

            EnsurePoolDefinition(objectPrefab);
            pool = _pools[objectPrefab.name];
            //pool.InitiatePool();
            return pool.GetPooledObject();
        }

    }
}