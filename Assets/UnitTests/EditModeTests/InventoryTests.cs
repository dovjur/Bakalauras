using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTests
{
    ItemObject itemObject = Resources.Load<ItemObject>("Items/0");
    ItemObject itemObject1 = Resources.Load<ItemObject>("Items/1");
    [Test]
    public void AddItem_EmptyInventory_AddsItemToFirstSlot()
    {
        Inventory inventory = new Inventory();

        inventory.AddItem(itemObject);

        Assert.That(inventory.items[0].itemID == itemObject.ID);
        Assert.That(inventory.items[0].stackSize == 1);
    }

    [Test]
    public void AddItem_AlreadyHasItemInInventory_AddsToItemsStack()
    {
        Inventory inventory = new Inventory();
        inventory.AddItem(itemObject);

        inventory.AddItem(itemObject);

        Assert.That(inventory.items[0].stackSize == 2);
        Assert.That(inventory.items[0].itemID == itemObject.ID);
    }

    [Test]
    public void AddItem_AlreadyHasItemAddNewItem_AddsItemToNextSlot()
    {
        Inventory inventory = new Inventory();
        inventory.AddItem(itemObject);

        inventory.AddItem(itemObject1);

        Assert.That(inventory.items[0].itemID == itemObject.ID);
        Assert.That(inventory.items[1].itemID == itemObject1.ID);
    }

    [Test]
    public void RemoveItem_ItemsExists_RemovesItem()
    {
        Inventory inventory = new Inventory();
        inventory.AddItem(itemObject);
        inventory.AddItem(itemObject1);

        inventory.RemoveItem(itemObject);

        Assert.That(inventory.items[0].itemID != itemObject.ID);
        Assert.That(inventory.items.Count == 1);
    }

    [Test]
    public void RemoveItem_ItemDoesntExists_InventoryStaysTheSame()
    {
        Inventory inventory = new Inventory();
        inventory.AddItem(itemObject);

        inventory.RemoveItem(itemObject1);

        Assert.That(inventory.items[0].itemID == itemObject.ID);
    }
}
