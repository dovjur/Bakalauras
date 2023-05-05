using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI stackSizeText;

    protected ItemObject inventoryItem;
    public void DisableSlot()
    {
        icon.enabled = false;
        stackSizeText.enabled = false;
    }

    public void EnableSlot()
    {
        icon.enabled = true;
        stackSizeText.enabled = true;
    }

    public void DisplaySlot(InventoryItem item)
    {
        if (item == null)
        {
            DisableSlot();
            return;
        }
        else
        {
            EnableSlot();
            inventoryItem = Resources.Load<ItemObject>("Items/" +item.itemID.ToString());

            icon.sprite = inventoryItem.icon;
            stackSizeText.text = item.stackSize.ToString();
        }
    }
}
