using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public ItemObject itemObject;
    public int stackSize;

    public InventoryItem(ItemObject itemObject)
    {
        this.itemObject = itemObject;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}
