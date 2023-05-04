using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class Collection : ScriptableObject
{
    public string collectionName;
    public List<Card> collectionCards = new List<Card>();
    public BuffData buffData;

    public void UnlockCard(Card card)
    {
        int index = collectionCards.IndexOf(card);
        if (index != -1)
        {
            collectionCards[index].isUnlocked = true;
        }
    }
    public bool IsCardUnlocked(Card card)
    {
        int index = collectionCards.IndexOf(card);
        if (index != -1)
        {
            return collectionCards[index].isUnlocked;
        }
        return false;
    }
    public bool IsCollectionComplete()
    {
        foreach (Card card in collectionCards)
        {
            if (!card.isUnlocked)
            {
                return false;
            }
        }
        return true;
    }

    public void ApplyBuff()
    {
        if (buffData != null)
        {
            buffData.ApplyBuff();
        }
    }
}
