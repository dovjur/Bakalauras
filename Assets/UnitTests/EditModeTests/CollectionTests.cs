using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CollectionTests
{
    [Test]
    public void UnlockCard_CardIsInCollectionAndNotUnlocked_UnlocksCard()
    {
        Collection collection = ScriptableObject.CreateInstance<Collection>();
        LootCard lootCard = ScriptableObject.CreateInstance<LootCard>();
        lootCard.isUnlocked = false;
        collection.collectionCards.Add(lootCard);

        collection.UnlockCard(lootCard);

        Assert.That(collection.collectionCards[0].isUnlocked == true);
    }

    [Test]
    public void UnlockCard_CardIsFromDifferentCollection_CollectionStaysTheSame()
    {
        Collection collection = ScriptableObject.CreateInstance<Collection>();
        LootCard lootCard = ScriptableObject.CreateInstance<LootCard>();
        collection.collectionCards.Add(lootCard);
        LootCard lootCard1 = ScriptableObject.CreateInstance<LootCard>();

        collection.UnlockCard(lootCard1);

        Assert.That(lootCard1.isUnlocked == false);
    }

    [Test]
    public void IsCardUnlocked_CardIsLocked_RetrunsFalse()
    {
        Collection collection = ScriptableObject.CreateInstance<Collection>();
        LootCard lootCard = ScriptableObject.CreateInstance<LootCard>();
        collection.collectionCards.Add(lootCard);

        bool isUnlocked = collection.IsCardUnlocked(lootCard);

        Assert.IsFalse(isUnlocked);
    }

    [Test]
    public void IsCardUnlocked_CardIsUnlocked_RetrunsTrue()
    {
        Collection collection = ScriptableObject.CreateInstance<Collection>();
        LootCard lootCard = ScriptableObject.CreateInstance<LootCard>();
        collection.collectionCards.Add(lootCard);
        collection.UnlockCard(lootCard);

        bool isUnlocked = collection.IsCardUnlocked(lootCard);

        Assert.IsTrue(isUnlocked);
    }
}
