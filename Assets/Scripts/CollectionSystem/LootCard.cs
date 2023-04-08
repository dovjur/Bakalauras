using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LootCard : ScriptableObject
{
    public Sprite lootSprite;
    public string lootName;
    [Range(0f, 100f)]
    public int dropChance;
    public bool isUnlocked;

    public LootCard(string lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
        this.isUnlocked = false;
    }
}
