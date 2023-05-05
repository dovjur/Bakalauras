using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotClickable : InventorySlot, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryItem.Use();
    }
}
