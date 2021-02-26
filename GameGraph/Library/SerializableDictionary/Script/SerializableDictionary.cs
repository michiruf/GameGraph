using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField] private TKey[] keys;
    [SerializeField] private TValue[] values;

    [NonSerialized] public Dictionary<TKey, TValue> dictionary;

    public SerializableDictionary()
    {
        dictionary = new Dictionary<TKey, TValue>();
    }

    public void OnAfterDeserialize()
    {
        var c = keys.Length;
        dictionary = new Dictionary<TKey, TValue>(c);
        for (var i = 0; i < c; i++)
        {
            dictionary[keys[i]] = values[i];
        }
        keys = null;
        values = null;
    }

    public void OnBeforeSerialize()
    {
        var c = dictionary.Count;
        keys = new TKey[c];
        values = new TValue[c];
        var i = 0;
        using (var e = dictionary.GetEnumerator())
            while (e.MoveNext())
            {
                var kvp = e.Current;
                keys[i] = kvp.Key;
                values[i] = kvp.Value;
                i++;
            }
    }
}
