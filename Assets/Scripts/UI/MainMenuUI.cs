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

    [Header("Panels")]
    [SerializeField] private List<GameObject> panels;

    [Header("Panels Buttons")]
    [SerializeField] private List<GameObject> buttons;

    [Header("Button Sprites")]
    [SerializeField] private Sprite whiteButton;
    [SerializeField] private Sprite brownButton;

    private void Start()
    {
        SaveData.Instance.player = (PlayerData)SaveLoadSystem.Load();
        UpdateUIAfterRun();
    }
    public void Play()
    {
        RunData.current.ResetInstance();
        SceneLoadManager.instance.LoadGame();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UpdateUIAfterRun()
    {
        coinCount.text = SaveData.Instance.player.coins.ToString();

        runCoins.text += RunData.current.GetCoins().ToString();
        runKills.text += RunData.current.GetKills().ToString();
        runTime.text += RunData.current.GetTime().ToString();
    }

    public void ChangePanel(GameObject button)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (button == buttons[i])
            {
                buttons[i].GetComponent<Image>().sprite = whiteButton;
                panels[i].SetActive(true);
            }
            else
            {
                buttons[i].GetComponent<Image>().sprite = brownButton;
                panels[i].SetActive(false);
            }
        }
    }
}
