using System.Collections.Generic;
using UnityEngine;

namespace Utility.DataStructures
{
    public abstract class SerializableHashMap<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
    {
        [SerializeField] private K[] _keys;
        [SerializeField] private V[] _values;

        public void OnAfterDeserialize()
        {
            Clear();

            if (_keys.Length != _values.Length)
                throw new System.ArgumentOutOfRangeException($"Length of {GetType().Name} doesn't match.");

            for (int i = 0; i < _keys.Length; i++)
                Add(_keys[i], _values[i]);

            _keys = null;
            _values = null;
        }

        public void OnBeforeSerialize()
        {
            _keys = new K[Count];
            _values = new V[Count];

            int index = 0;

            foreach (KeyValuePair<K, V> kvp in this)
            {
                _keys[index] = kvp.Key;
                _values[index] = kvp.Value;
                index++;
            }
        }
    }
}