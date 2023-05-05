using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public List<Collection> collections;

    public Card GetDroppedLoot()
    {
        int randomNumber = Random.Range(1, 101);
        List<Card> possibleLoot = new List<Card>();
        foreach (Collection collection in collections)
        {
            foreach (Card lootCard in collection.collectionCards)
            {
                if (randomNumber <= lootCard.dropChance)
                {
                    possibleLoot.Add(lootCard);
                }
            }
        }
        
        if (possibleLoot.Count > 0)
        {
            Card droppedCard = possibleLoot[Random.Range(0, possibleLoot.Count)];
            return droppedCard;
        }

        return null;
    }

    public void UpdateCollection(Card droppedCard)
    {
        foreach (Collection collection in collections)
        {
            if (collection.collectionCards.Contains(droppedCard))
            {
                collection.UnlockCard(droppedCard);
                if (collection.IsCollectionComplete())
                {
                    collection.ApplyBuff();
                }
                break;
            }
        }
    }

    public void ApplyBuffs()
    {
        foreach (Collection collection in collections)
        {
            if (collection.IsCollectionComplete())
            {
                collection.ApplyBuff();
            }
            else
            {
                foreach (Card card in collection.collectionCards)
                {
                    if (card.isUnlocked && card.buffData != null)
                    {
                        card.ApplyBuff();
                    }
                }
            }
        }
    }
}
