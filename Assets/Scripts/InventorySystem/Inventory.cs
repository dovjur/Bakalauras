using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public delegate void InventoryChange(List<InventoryItem> inventory);
    public static event InventoryChange onInventoryChanged;

    private const int size = 6;

    public List<InventoryItem> inventory;
    public Dictionary<int, InventoryItem> itemDictionary = new Dictionary<int, InventoryItem>();

    public Inventory()
    {
        inventory = new List<InventoryItem>(size);
    }
    public void AddItem(ItemObject itemObject)
    {
        if (itemDictionary.TryGetValue(itemObject.ID, out InventoryItem item))
        {
            item.AddToStack();
            onInventoryChanged?.Invoke(inventory);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemObject);
            inventory.Add(newItem);
            itemDictionary.Add(itemObject.ID, newItem);
            onInventoryChanged?.Invoke(inventory);
        }
        SaveLoadSystem.Save(SaveData.Instance);
    }

    public void RemoveItem(ItemObject itemObject)
    {
        if (itemDictionary.TryGetValue(itemObject.ID, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemObject.ID);
            }
            onInventoryChanged?.Invoke(inventory);
            SaveLoadSystem.Save(SaveData.Instance);
        }
    }

    public int GetSize()
    {
        return size;
    }
}
