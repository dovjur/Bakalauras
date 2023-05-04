using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private InventorySlot slotPrefab;

    private int size;

    public List<InventorySlot> inventorySlots;

    private void Start()
    {
        size = SaveData.Instance.Inventory.GetSize();
        inventorySlots = new List<InventorySlot>(size);
        DisplayInventory(SaveData.Instance.Inventory.items);
    }

    private void OnEnable()
    {
        Inventory.onInventoryChanged += DisplayInventory;
    }

    private void OnDisable()
    {
        Inventory.onInventoryChanged -= DisplayInventory;
    }

    private void ResetInventory()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<InventorySlot>(6);
    }

    private void DisplayInventory(List<InventoryItem> inventory)
    {
        ResetInventory();

        for (int i = 0; i < inventorySlots.Capacity; i++)
        {
            CreateInventorySlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
;            inventorySlots[i].DisplaySlot(inventory[i]);
        }
    }

    private void CreateInventorySlot()
    {
        InventorySlot newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform,false);

        newSlot.DisableSlot();

        inventorySlots.Add(newSlot);
    }
}
