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
    }

    public void UndoCommand()
    {
        SaveData.Instance.player.strength--;
        statValue.text = SaveData.Instance.player.strength.ToString();
    }
}
