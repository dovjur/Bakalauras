using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class DictionarySerializationSurrogate<TKey, TValue> : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        var dictionary = (Dictionary<TKey, TValue>)obj;
        var keys = new List<TKey>(dictionary.Keys);
        var values = new List<TValue>(dictionary.Values);

        info.AddValue("keys", keys);
        info.AddValue("values", values);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        var dictionary = (Dictionary<TKey, TValue>)obj;

        var keys = (List<TKey>)info.GetValue("keys", typeof(List<TKey>));
        var values = (List<TValue>)info.GetValue("values", typeof(List<TValue>));

        dictionary.Clear();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary.Add(keys[i], values[i]);  
        }

        return dictionary;
    }
}
