using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAttackSpeedCommand : ICommand
{
    private TextMeshProUGUI statValue;

	public UpgradeAttackSpeedCommand(TextMeshProUGUI statValue)
	{
        this.statValue = statValue;
    }

    public void ExecuteCommand()
    {
        SaveData.Instance.player.attackSpeed++;
        statValue.text = SaveData.Instance.player.attackSpeed.ToString();
        StatManager.upgradePrice += SaveData.Instance.player.attackSpeed * 100;
    }

    public void UndoCommand()
    {
        StatManager.upgradePrice -= SaveData.Instance.player.attackSpeed * 100;
        SaveData.Instance.player.attackSpeed--;
        statValue.text = SaveData.Instance.player.attackSpeed.ToString(); 
    }
}
