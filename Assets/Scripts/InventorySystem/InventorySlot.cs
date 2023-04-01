using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI stackSizeText;

    public void DisableSlot()
    {
        icon.enabled = false;
        labelText.enabled = false;
        stackSizeText.enabled = false;
    }

    public void EnableSlot()
    {
        icon.enabled = true;
        labelText.enabled = true;
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

            icon.sprite = item.itemObject.icon;
            labelText.text = item.itemObject.label;
            stackSizeText.text = item.stackSize.ToString();
        }
    }
}
