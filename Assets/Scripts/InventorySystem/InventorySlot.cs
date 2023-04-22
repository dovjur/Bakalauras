using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    //public TextMeshProUGUI labelText;
    public TextMeshProUGUI stackSizeText;

    private ItemObject inventoryItem;
    public void DisableSlot()
    {
        icon.enabled = false;
        //labelText.enabled = false;
        stackSizeText.enabled = false;
    }

    public void EnableSlot()
    {
        icon.enabled = true;
        //labelText.enabled = true;
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
            //labelText.text = inventoryItem.label;
            stackSizeText.text = item.stackSize.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryItem.Use();
    }
}
