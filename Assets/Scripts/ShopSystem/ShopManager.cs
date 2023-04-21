using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public delegate void CoinsSpend();
    public static event CoinsSpend onCoinsSpend;

    [SerializeField]
    private List<ItemObject> shopItemsSO;
    [SerializeField]
    private ShopItemTemplate shopTemplate;

    private List<ShopItemTemplate> shopItems = new List<ShopItemTemplate>();

    public List<InventoryItem> inventoryItems;

    void Start()
    {
        inventoryItems = SaveData.Instance.Inventory.inventory;
        LoadShop();
        CanBePurchased();
    }

    private void OnEnable()
    {
        ShopItemTemplate.onItemPurchased += Purchase;
    }

    private void OnDisable()
    {
        ShopItemTemplate.onItemPurchased -= Purchase;
    }

    private void CanBePurchased()
    {
        for (int i = 0; i < shopItemsSO.Count; i++)
        {
            if (shopItemsSO[i].price <= SaveData.Instance.player.coins)
            {
                shopItems[i].button.interactable = true;
            }
            else
            {
                shopItems[i].button.interactable = false;
            }
        }
    }

    public void Purchase(string title)
    {
        ItemObject item = shopItemsSO.Find(x => x.label == title);
        if (SaveData.Instance.player.coins >= item.price)
        {
            SaveData.Instance.player.coins -= item.price;
            SaveData.Instance.Inventory.AddItem(item);
            SaveLoadSystem.Save(SaveData.Instance);
            onCoinsSpend();
            CanBePurchased();
        }
    }
    public void LoadShop()
    {
        for (int i = 0; i < shopItemsSO.Count; i++)
        {
            ShopItemTemplate shopItem = Instantiate(shopTemplate, transform);

            shopItem.titleText.text = shopItemsSO[i].label;
            shopItem.descriptionText.text = shopItemsSO[i].description;
            shopItem.priceText.text = shopItemsSO[i].price.ToString();
            shopItem.image.sprite = shopItemsSO[i].icon;

            shopItems.Add(shopItem);
        }
    }
}
