using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTemplate : MonoBehaviour
{
    public delegate void PurchaseItem(string title);
    public static event PurchaseItem onItemPurchased;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI priceText;
    public Image itemSprite;
    public Button button;

    public void PurchaceItem() 
    {
        onItemPurchased(titleText.text);
    }
}
