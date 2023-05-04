using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public delegate void InventoryChange(List<InventoryItem> inventory);
    public static event InventoryChange onInventoryChanged;

    private const int size = 6;

    public List<InventoryItem> items;
    public Dictionary<int, InventoryItem> itemDictionary = new Dictionary<int, InventoryItem>();

    public Inventory()
    {
        items = new List<InventoryItem>(size);
    }
    public void AddItem(ItemObject itemObject)
    {
        if (itemDictionary.TryGetValue(itemObject.ID, out InventoryItem item))
        {
            item.AddToStack();
            onInventoryChanged?.Invoke(items);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemObject);
            items.Add(newItem);
            itemDictionary.Add(itemObject.ID, newItem);
            onInventoryChanged?.Invoke(items);
        }
        SaveLoad.Save(SaveData.Instance);
    }

    public void RemoveItem(ItemObject itemObject)
    {
        if (itemDictionary.TryGetValue(itemObject.ID, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.stackSize == 0)
            {
                items.Remove(item);
                itemDictionary.Remove(itemObject.ID);
            }
            onInventoryChanged?.Invoke(items);
            SaveLoad.Save(SaveData.Instance);
        }
    }

    public int GetSize()
    {
        return size;
    }
}
