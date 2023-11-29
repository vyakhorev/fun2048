using System;
using System.Collections.Generic;

namespace Utility.Containers
{
    public sealed class Pool<T> where T : class
    {
        private readonly Func<T> _initializer;
        private readonly Func<T, bool> _isFreePredicate;
        private readonly Action<T> _returnToPoolAction;
        private readonly List<T> _pool;

        public Pool(Func<T> initializer, Func<T, bool> isFreePredicate, Action<T> returnToPoolAction, int initialCapacity)
        {
            _initializer = initializer;
            _isFreePredicate = isFreePredicate;
            _returnToPoolAction = returnToPoolAction;
            _pool = new List<T>(initialCapacity);
            PopulatePool();
        }

        public T GetFreePooledObject()
        {
            if(HasFreePooledObject(out T freePooledObject))
                return freePooledObject;

            return CreateObject();
        }

        public bool TryReturnToPool(T pooledObject)
        {
            bool hasObject = _pool.Contains(pooledObject);
            
            if(hasObject)
                _returnToPoolAction.Invoke(pooledObject);

            return hasObject;
        }

        public void ReturnObjectsToPool()
        {
            for (int i = 0; i < _pool.Count; i++)
                _returnToPoolAction.Invoke(_pool[i]);
        }

        private bool HasFreePooledObject(out T freePooledObject)
        {
            freePooledObject = null;

            foreach(T pooledObject in _pool)
            {
                if (_isFreePredicate.Invoke(pooledObject))
                {
                    freePooledObject = pooledObject;
                    break;
                }
            }

            return freePooledObject != null;
        }

        private T CreateObject()
        {
            T newObject = _initializer.Invoke();
            _pool.Add(newObject);
            _returnToPoolAction.Invoke(newObject);
            return newObject;
        }

        private void PopulatePool()
        {
            for (int i = 0; i < _pool.Capacity; i++)
                CreateObject();
        }
    }
}