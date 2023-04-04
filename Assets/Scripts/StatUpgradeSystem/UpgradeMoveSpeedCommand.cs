using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMoveSpeedCommand : ICommand
{
    private TextMeshProUGUI statValue;
    public UpgradeMoveSpeedCommand(TextMeshProUGUI statValue)
	{
		this.statValue = statValue;
	}

	public void ExecuteCommand()
	{
		SaveData.Instance.player.moveSpeed++;
        statValue.text = SaveData.Instance.player.moveSpeed.ToString();
        StatManager.upgradePrice += SaveData.Instance.player.moveSpeed * 100;
    }

	public void UndoCommand()
	{
        StatManager.upgradePrice -= SaveData.Instance.player.moveSpeed * 100;
        SaveData.Instance.player.moveSpeed--;
        statValue.text = SaveData.Instance.player.moveSpeed.ToString();   
    }
}
