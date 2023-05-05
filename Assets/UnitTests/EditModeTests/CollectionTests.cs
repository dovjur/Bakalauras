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
        Card lootCard = ScriptableObject.CreateInstance<Card>();
        lootCard.isUnlocked = false;
        collection.collectionCards.Add(lootCard);

        collection.UnlockCard(lootCard);

        Assert.That(collection.collectionCards[0].isUnlocked == true);
    }

    [Test]
    public void UnlockCard_CardIsFromDifferentCollection_CollectionStaysTheSame()
    {
        Collection collection = ScriptableObject.CreateInstance<Collection>();
        Card lootCard = ScriptableObject.CreateInstance<Card>();
        collection.collectionCards.Add(lootCard);
        Card lootCard1 = ScriptableObject.CreateInstance<Card>();

        collection.UnlockCard(lootCard1);

        Assert.That(lootCard1.isUnlocked == false);
    }

    [Test]
    public void IsCardUnlocked_CardIsLocked_RetrunsFalse()
    {
        Collection collection = ScriptableObject.CreateInstance<Collection>();
        Card lootCard = ScriptableObject.CreateInstance<Card>();
        collection.collectionCards.Add(lootCard);

        bool isUnlocked = collection.IsCardUnlocked(lootCard);

        Assert.IsFalse(isUnlocked);
    }

    [Test]
    public void IsCardUnlocked_CardIsUnlocked_RetrunsTrue()
    {
        Collection collection = ScriptableObject.CreateInstance<Collection>();
        Card lootCard = ScriptableObject.CreateInstance<Card>();
        collection.collectionCards.Add(lootCard);
        collection.UnlockCard(lootCard);

        bool isUnlocked = collection.IsCardUnlocked(lootCard);

        Assert.IsTrue(isUnlocked);
    }

    [Test]
    public void IsCardUnlocked_CardIsNotFromCollection_RetrunsFalse()
    {
        Collection collection = ScriptableObject.CreateInstance<Collection>();
        Card lootCard = ScriptableObject.CreateInstance<Card>();

        bool isUnlocked = collection.IsCardUnlocked(lootCard);

        Assert.IsFalse(isUnlocked);
    }
    [Test]
    public void IsCollectionComplete_CollectionComplete_ReturnsTrue()
    {
        Collection collection = ScriptableObject.CreateInstance<Collection>();
        Card lootCard = ScriptableObject.CreateInstance<Card>();
        collection.collectionCards.Add(lootCard);
        collection.UnlockCard(lootCard);

        bool isCompleted = collection.IsCollectionComplete();

        Assert.IsTrue(isCompleted);
    }

    [Test]
    public void IsCollectionComplete_CollectionIsNotComplete_ReturnsFalse()
    {
        Collection collection = ScriptableObject.CreateInstance<Collection>();
        Card lootCard = ScriptableObject.CreateInstance<Card>();
        collection.collectionCards.Add(lootCard);

        bool isCompleted = collection.IsCollectionComplete();

        Assert.IsFalse(isCompleted);
    }
}