using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinCount;

    private void Start()
    {
        coinCount.text = SaveData.Instance.player.coins.ToString();
    }

    private void OnEnable()
    {
        ShopManager.onCoinsSpend += UpdateCoinUI;
        StatManager.onStatsUpgraded += UpdateCoinUI;
    }

    private void OnDisable()
    {
        ShopManager.onCoinsSpend -= UpdateCoinUI;
        StatManager.onStatsUpgraded -= UpdateCoinUI;
    }

    public void UpdateCoinUI()
    {
        coinCount.text = SaveData.Instance.player.coins.ToString();
    }
}
