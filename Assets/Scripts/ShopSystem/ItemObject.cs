using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class ItemObject : ScriptableObject, ISerializable
{
    public int ID;
    public string label;
    public int price;
    public string description;
    public Sprite icon;

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("title", label);
        info.AddValue("description", description);
        info.AddValue("price", price);
    }

    protected ItemObject(SerializationInfo info, StreamingContext context)
    {
        label = info.GetString("title");
        description = info.GetString("description");
        price = info.GetInt32("price");
        icon = Resources.Load<Sprite>(label);
    }
}
