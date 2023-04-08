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
        
    }
    private void OnEnable()
    {
        Chest.onChestOpened += UpdateCollection;
    }
    private void OnDisable()
    {
        Chest.onChestOpened -= UpdateCollection;
    }

    public LootCard GetDroppedLoot()
    {
        int randomNumber = Random.Range(1, 101);
        List<LootCard> possibleLoot = new List<LootCard>();
        foreach (LootCard lootCard in lootCards)
        {
            if (randomNumber <= lootCard.dropChance)
            {
                possibleLoot.Add(lootCard);
            }
        }
        if (possibleLoot.Count > 0)
        {
            LootCard droppedCard = possibleLoot[Random.Range(0, possibleLoot.Count)];
            return droppedCard;
        }

        return null;
    }

    private void UpdateCollection()
    {
        LootCard droppedCard = GetDroppedLoot();
        Debug.Log(droppedCard);
        foreach (Collection collection in collections)
        {
            if (collection.collectionCards.Contains(droppedCard))
            {
                collection.UnlockCard(droppedCard);
                break;
            }
        }
    }
}
