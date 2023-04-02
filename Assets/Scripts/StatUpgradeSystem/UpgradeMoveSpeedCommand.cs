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
    }

	public void UndoCommand()
	{
		SaveData.Instance.player.moveSpeed--;
        statValue.text = SaveData.Instance.player.moveSpeed.ToString();
    }
}
