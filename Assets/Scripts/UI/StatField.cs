using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatField : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI statValueText;
    [SerializeField]
    private GameObject increaseButton;
    [SerializeField]
    private GameObject decreaseButton;
    [SerializeField]
    private CommandManager commandManager;

    private int statValue = 0;

    private void Start()
    {
        statValue = SaveData.Instance.player.health;
        decreaseButton.SetActive(false);
        statValueText.text = statValue.ToString();
    }

    public void IncreaseStat(string stat)
    {
        switch (stat)
        {
            case "Health":
                commandManager.ExecuteCommand(new UpgradeHealhtCommand(statValueText));
                break;
            case "Movespeed":
                commandManager.ExecuteCommand(new UpgradeMoveSpeedCommand(statValueText));
                break;
            case "Strength":
                commandManager.ExecuteCommand(new UpgradeStrengthCommand(statValueText));
                break;
            case "AttackSpeed":
                commandManager.ExecuteCommand(new UpgradeAttackSpeedCommand(statValueText));        
                break;
            default:
                break;
        }
        decreaseButton.SetActive(true);
    }

    public void DecreaseStat()
    {
        commandManager.UndoCommand();
        if (commandManager.GetHistoryCount() == 0)
        {
            decreaseButton.SetActive(false);
        }
    }
}
