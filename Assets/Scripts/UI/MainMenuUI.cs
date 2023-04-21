using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinCount;

    [Header("Run Data")]
    [SerializeField] private TextMeshProUGUI runCoins;
    [SerializeField] private TextMeshProUGUI runKills;
    [SerializeField] private TextMeshProUGUI runTime;

    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private GameObject gamePanel;

    private static bool isGameStarted;

    private void Awake()
    {
        SaveData.Instance = (SaveData)SaveLoadSystem.Load();
    }
    private void Start()
    {
        UpdateUIAfterRun();
        if (isGameStarted)
        {
            EnableGamePanel();
        }
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

    public void Play()
    {
        if (SaveData.Instance.runData != null)
        {
            SaveData.Instance.runData.ResetRun();
        }
        SceneLoadManager.instance.LoadGame();
        isGameStarted = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UpdateUIAfterRun()
    {
        UpdateCoinUI();

        runCoins.text += SaveData.Instance.runData.GetCoins().ToString();
        runKills.text += SaveData.Instance.runData.GetKills().ToString();
        runTime.text += SaveData.Instance.runData.GetTime().ToString();
    }

    public void EnableGamePanel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        isGameStarted = false;
    }

    public void UpdateCoinUI()
    {
        coinCount.text = SaveData.Instance.player.coins.ToString();
    }
}