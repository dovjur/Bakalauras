using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionCardTemplate : ItemTemplate
{
    public void SetSprite(Sprite sprite, bool isUnlocked)
    {
        image.sprite = sprite;
        if (isUnlocked)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = Color.black;
        }  
    }
}
