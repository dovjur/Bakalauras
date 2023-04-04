using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeStrengthCommand : ICommand
{
    private TextMeshProUGUI statValue;

	public UpgradeStrengthCommand(TextMeshProUGUI statValue)
	{
        this.statValue = statValue;
    }

    public void ExecuteCommand()
    {
        SaveData.Instance.player.strength++;
        statValue.text = SaveData.Instance.player.strength.ToString();
        StatManager.upgradePrice += SaveData.Instance.player.strength * 100;
    }

    public void UndoCommand()
    {
        StatManager.upgradePrice -= SaveData.Instance.player.strength * 100;
        SaveData.Instance.player.strength--;
        statValue.text = SaveData.Instance.player.strength.ToString();
    }
}
