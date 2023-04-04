using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeHealhtCommand : ICommand
{
    private TextMeshProUGUI statValue;
    public UpgradeHealhtCommand(TextMeshProUGUI statValue)
    {
        this.statValue = statValue;
    }
    public void ExecuteCommand()
    {
        SaveData.Instance.player.health++;
        statValue.text = SaveData.Instance.player.health.ToString();
        StatManager.upgradePrice += SaveData.Instance.player.health * 100;
    }

    public void UndoCommand()
    {
        StatManager.upgradePrice -= SaveData.Instance.player.health * 100;
        SaveData.Instance.player.health--;
        statValue.text = SaveData.Instance.player.health.ToString();
    }
}
