using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField]
    private GameObject coinPanel;

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
        if (!coinPanel.activeSelf)
        {
            coinPanel.SetActive(true);
        }
        coinPanel.GetComponentInChildren<TextMeshProUGUI>().text = RunData.Instance.GetCoins().ToString();
    }

    public void MainMenu()
    {
        SceneLoadManager.instance.LoadMenu();
    }
}
