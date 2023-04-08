using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionTemplate : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private Image image;

    public void SetTitle(string title)
    {
        titleText.text = title;
    }

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
