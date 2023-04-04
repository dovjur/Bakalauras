using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public delegate void StatsUpgraded();
    public static event StatsUpgraded onStatsUpgraded;

    [SerializeField]
    private List<StatField> stats = new List<StatField>();

    public static int upgradePrice = 0;

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
            Debug.Log("Neuztenka Pinigu: " + upgradePrice);
        }
    }

    public void ResetStats()
    {
        upgradePrice = 0;

        foreach (StatField stat in stats)
        {
            stat.ResetStat();
        }
    }
}
