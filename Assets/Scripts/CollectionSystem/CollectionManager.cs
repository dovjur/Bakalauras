using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField]
    private List<LootCard> lootCards = new List<LootCard>();
    public List<Collection> collections;
    void Start()
    {
        if (SaveData.Instance.lootCards != null)
        {
            lootCards = SaveData.Instance.lootCards;
        }
    }

    public LootCard GetDroppedLoot()
    {
        int randomNumber = Random.Range(1, 101);
        List<LootCard> possibleLoot = new List<LootCard>();
        foreach (Collection collection in collections)
        {
            foreach (LootCard lootCard in collection.collectionCards)
            {
                if (randomNumber <= lootCard.dropChance)
                {
                    possibleLoot.Add(lootCard);
                }
            }
        }
        
        if (possibleLoot.Count > 0)
        {
            LootCard droppedCard = possibleLoot[Random.Range(0, possibleLoot.Count)];
            return droppedCard;
        }

        return null;
    }

    public void UpdateCollection(LootCard droppedCard)
    {
        foreach (Collection collection in collections)
        {
            if (collection.collectionCards.Contains(droppedCard))
            {
                collection.UnlockCard(droppedCard);
                break;
            }
        }
        SaveData.Instance.lootCards = lootCards;
        SaveLoadSystem.Save(SaveData.Instance);
    }
}
