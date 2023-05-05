using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class Card : ScriptableObject, ISerializable
{
    public Sprite sprite;
    public string label;
    public string description;
    [Range(0f, 100f)]
    public int dropChance;
    public int value;
    public bool isUnlocked;
    public BuffData buffData;

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("label", label);
        info.AddValue("unlocked", isUnlocked);
    }

    protected Card(SerializationInfo info, StreamingContext context)
    {
        label = info.GetString("label");
        isUnlocked = info.GetBoolean("unlocked");
    }

    public Card()
    {

    }

    public void ApplyBuff()
    {
        if (buffData != null)
        {
            buffData.ApplyBuff();
        }
    }
}
