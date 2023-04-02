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
    }

    public void UndoCommand()
    {
        SaveData.Instance.player.health--;
        statValue.text = SaveData.Instance.player.health.ToString();
    }
}
