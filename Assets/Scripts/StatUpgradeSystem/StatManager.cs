using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public delegate void StatsUpgraded();
    public static event StatsUpgraded onStatsUpgraded;

    [SerializeField]
    private TextMeshProUGUI upgradePriceText;
    [SerializeField]
    private GameObject upgradePanel;
    [SerializeField]
    private GameObject notEnoughMoneytext;
    [SerializeField]
    private List<StatField> stats = new List<StatField>();

    public static int upgradePrice = 0;

    private void Update()
    {
        if (upgradePrice > 0)
        {
            upgradePanel.SetActive(true);
            upgradePriceText.text = upgradePrice.ToString();
        }
        else
        {
            upgradePanel.SetActive(false);
        }
    }

    public void SaveStats()
    {
        if (upgradePrice <= SaveData.Instance.player.coins)
        {
            SaveData.Instance.player.coins -= upgradePrice;
            upgradePrice = 0;
            onStatsUpgraded();

            foreach (StatField stat in stats)
            {
                stat.SaveStat();
            }
        }
        else
        {
            StartCoroutine(NotEnoughMoney());
        }
    }

    public void ResetStats()
    {
        foreach (StatField stat in stats)
        {
            stat.ResetStat();
        }
    }

    private IEnumerator NotEnoughMoney()
    {
        notEnoughMoneytext.SetActive(true);
        yield return new WaitForSeconds(3);
        notEnoughMoneytext.SetActive(false);
    }
}
