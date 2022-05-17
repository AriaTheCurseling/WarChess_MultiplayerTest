using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver, ICanHaveDuplicates
{
    [SerializeField]
    private Entry[] entries;

    public bool HasDuplicates => Count < entries.Length;

    public void OnAfterDeserialize()
    {
        Clear();

        foreach (var entry in entries)
            this[entry.Key] = entry.Value;
    }

    public void OnBeforeSerialize()
    {

    }

    public void RemoveDuplicates()
    {
        entries = new Entry[Count];

        int i = 0;
        foreach (var entry in this)
            entries[i++] = entry;
    }

    [Serializable]
    public struct Entry
    {
        public TKey Key;
        public TValue Value;

        public static implicit operator Entry(KeyValuePair<TKey, TValue> d)
            => new Entry() { Key = d.Key, Value = d.Value };
    }
}

public interface ICanHaveDuplicates
{
    bool HasDuplicates { get; }

    void RemoveDuplicates();
}