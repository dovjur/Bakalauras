using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class LootCard : ScriptableObject, ISerializable
{
    public Sprite lootSprite;
    public string lootName;
    public string description;
    [Range(0f, 100f)]
    public int dropChance;
    public bool isUnlocked;

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("name", lootName);
        info.AddValue("unlocked", isUnlocked);
    }

    protected LootCard(SerializationInfo info, StreamingContext context)
    {
        lootName = info.GetString("name");
        isUnlocked = info.GetBoolean("unlocked");
    }
}
