using System.Collections.Generic;
using UnityEngine;

namespace Fun2048
{

    public class DynamicObjectPool
    {
        private List<GameObject> _objectPoolList;
        private GameObject _objectType;

        // How long it takes before destroying every object above spawnAvrCnt.
        //private float unusedLifeSpan = 30f;
        // Time window used for spawnAvrCnt calculations
        //private float averageWindowSpan = 120f;
        // Initial and minimal spawned valuein
        private int _spawnMinCnt = 10;

        // Average counter of gameObjects in use
        private int inUseAvrCnt;
        readonly Transform _objectPoolContainer;

        public DynamicObjectPool(GameObject objectType,
          Transform poolContainer,
          int spawnMinCnt = 5)
        {
            _objectType = objectType;
            _spawnMinCnt = spawnMinCnt;
            _objectPoolContainer = poolContainer;
        }

        public void InitiatePool()
        {
            _objectPoolList = new List<GameObject>();
            SpawnNew(_spawnMinCnt);
        }

        public GameObject GetPooledObject()
        {
            // Try to find inactive object
            GameObject go = GetLastInactiveObject();
            if (go is null)
            {
                // Spawn new object for guaranteed return value
                go = SpawnNewGameObject();
                _objectPoolList.Add(go);
            }

            return go;
        }

        private void SpawnNew(int howManyToSpawn)
        {
            for (int i = 0; i < howManyToSpawn; i++)
            {
                _objectPoolList.Add(SpawnNewGameObject());
            }
        }

        private GameObject GetLastInactiveObject()
        {
            for (int i = 0; i < _objectPoolList.Count; i++)
            {
                if (!_objectPoolList[i].activeInHierarchy)
                {
                    return _objectPoolList[i];
                }
            }

            return null;
        }

        private GameObject SpawnNewGameObject()
        {
            GameObject go = GameObject.Instantiate(_objectType, _objectPoolContainer);
            go.SetActive(false);
            return go;
        }
    }
}