using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemTemplate : ItemTemplate
{
    public delegate void PurchaseItem(string title);
    public static event PurchaseItem onItemPurchased;

    public TextMeshProUGUI priceText;
    public Button button;

    public void PurchaceItem() 
    {
        onItemPurchased(titleText.text);
    }
}
