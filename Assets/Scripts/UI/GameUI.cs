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
        Player.onHealthChange += UpdateInt;
    }
    private void OnDisable()
    {
        Coin.onCoinCollected -= UpdateCoinCount;
        Player.onHealthChange -= UpdateInt;
    }
    private void UpdateCoinCount()
    {
        coinsCollected.text = RunData.current.GetCoins().ToString();
    }

    private void UpdateInt(int aaa) { }
}
