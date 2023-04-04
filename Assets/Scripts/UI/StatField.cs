using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Stat
{
    Health,
    MoveSpeed,
    AttackSpeed,
    Strength
}
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

    public Stat stat;

    private void Start()
    {
        decreaseButton.SetActive(false);
        if (!SaveData.Instance.player.IsStatMaxed(stat))
        {
            increaseButton.SetActive(true);
        }
    }
    private void OnEnable()
    {
        LoadStat();
    }

    private void OnDisable()
    {
        ResetStat();
    }

    private void LoadStat()
    {
        switch (stat)
        {
            case Stat.Health:
                statValueText.text = SaveData.Instance.player.health.ToString();
                break;
            case Stat.MoveSpeed:
                statValueText.text = SaveData.Instance.player.moveSpeed.ToString();
                break;
            case Stat.AttackSpeed:
                statValueText.text = SaveData.Instance.player.attackSpeed.ToString();
                break;
            case Stat.Strength:
                statValueText.text = SaveData.Instance.player.strength.ToString();
                break;
        }
    }

    public void IncreaseStat(string stat)
    {
        switch (stat)
        {
            case "Health":
                commandManager.ExecuteCommand(new UpgradeHealhtCommand(statValueText));
                break;
            case "MoveSpeed":
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

        if (SaveData.Instance.player.IsStatMaxed(this.stat))
        {
            increaseButton.SetActive(false);
        }

    }

    public void DecreaseStat()
    {
        commandManager.UndoCommand();
        if (commandManager.GetHistoryCount() == 0)
        {
            decreaseButton.SetActive(false);
        }
        if (!SaveData.Instance.player.IsStatMaxed(stat))
        {
            increaseButton.SetActive(true);
        }
    }

    public void ResetStat()
    {
        while (commandManager.GetHistoryCount() > 0)
        {
            commandManager.UndoCommand();
        }
        decreaseButton.SetActive(false);
    }

    public void SaveStat()
    {
        SaveLoadSystem.Save(SaveData.Instance);
        commandManager.ClearHistory();
        decreaseButton.SetActive(false);
    }
}
