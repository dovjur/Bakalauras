using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
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
        SaveData.Instance = (SaveData)SaveLoad.Load();
    }
    private void Start()
    {
        UpdateUIAfterRun();
        if (isGameStarted)
        {
            EnableGamePanel();
        }
    }

    public void Play()
    {
        isGameStarted = true;
        if (SaveData.Instance.runData != null)
        {
            SaveData.Instance.runData.ResetRun();
        }
        SceneLoadManager.instance.LoadGame(); 
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UpdateUIAfterRun()
    {
        runCoins.text += SaveData.Instance.runData.GetCoins().ToString();
        runKills.text += SaveData.Instance.runData.GetKills().ToString();

        float timeInSeconds = SaveData.Instance.runData.GetTime();
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        runTime.text += string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EnableGamePanel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        isGameStarted = false;
    }
}