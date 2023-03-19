using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinsCollected;
    private void Start()
    {
        Coin.onCoinCollected += UpdateCoinCount;
    }
    private void UpdateCoinCount()
    {
        coinsCollected.text = RunData.current.GetCoins().ToString();
    }
}
