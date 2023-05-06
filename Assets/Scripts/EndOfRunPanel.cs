using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndOfRunPanel : MonoBehaviour
{
    private const string deadMessage = "You got injured pretty bad, but managed to save some loot";
    private const string aliveMessage = "You managed to get back alive";
    private const string newItem = "New item unlocked!";
    private const string duplicateItem = "Duplicate item found";

    [SerializeField]
    private TextMeshProUGUI runText;
    [SerializeField]
    private TextMeshProUGUI coinsCollectedText;
    [Header("Item")]
    [SerializeField]
    private TextMeshProUGUI itemText;
    [SerializeField]
    private GameObject itemPanel;
    [SerializeField]
    private Image itemIamge;
    [Header("DuplicateItem")]
    [SerializeField]
    private GameObject duplicateItemPanel;
    [SerializeField]
    private TextMeshProUGUI itemValueText;

    public void DisplayPanel(Card lootCard, bool dead)
    {
        gameObject.SetActive(true);
        coinsCollectedText.text = RunData.Instance.GetCoins().ToString();
        if (dead)
        {
            runText.text = deadMessage;
        }
        else
        {
            runText.text = aliveMessage;
            if (lootCard != null)
            {
                itemPanel.SetActive(true);
                itemText.text = newItem;
                itemIamge.sprite = lootCard.sprite;
                if (lootCard.isUnlocked)
                {
                    itemText.text = duplicateItem;
                    duplicateItemPanel.SetActive(true);
                    itemValueText.text = lootCard.value.ToString();
                }
            }
        }
    }
}
