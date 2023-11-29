using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{

    public class DynamicObjectPool
    {
        private List<GameObject> _objectPoolList;
        private GameObject _objectType;

        readonly Transform _objectPoolContainer;

        public DynamicObjectPool(GameObject objectType,
          Transform poolContainer)
        {
            _objectType = objectType;
            _objectPoolContainer = poolContainer;
        }

        public void InitiatePool(int minCnt)
        {
            _objectPoolList = new List<GameObject>();
            SpawnNew(minCnt);
        }

        public GameObject GetPooledObject()
        {
            GameObject go = GetLastInactiveObject();
            if (go is null)
            {
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