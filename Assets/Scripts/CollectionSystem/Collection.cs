using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class Collection : ScriptableObject
{
    public string collectionName;
    public List<LootCard> collectionCards = new List<LootCard>();

    public void UnlockCard(LootCard lootCard)
    {
        int index = collectionCards.IndexOf(lootCard);
        if (index != -1)
        {
            collectionCards[index].isUnlocked = true;
        }
    }
    public bool IsCardUnlocked(LootCard card)
    {
        int index = collectionCards.IndexOf(card);
        if (index != -1)
        {
            return collectionCards[index].isUnlocked;
        }
        return false;
    }
}
