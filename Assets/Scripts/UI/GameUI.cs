using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField]
    private TextMeshProUGUI coinsCollected;

    private void OnEnable()
    {
        Coin.onCoinCollected += UpdateCoinCount;
    }
    private void OnDisable()
    {
        Coin.onCoinCollected -= UpdateCoinCount;
    }
    private void UpdateCoinCount()
    {
        coinsCollected.text = SaveData.Instance.runData.GetCoins().ToString();
    }
}
