using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    }

    public void UndoCommand()
    {
        SaveData.Instance.player.attackSpeed--;
        statValue.text = SaveData.Instance.player.attackSpeed.ToString();
    }
}
