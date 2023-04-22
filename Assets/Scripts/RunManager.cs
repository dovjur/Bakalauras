using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    [SerializeField]
    private GameObject runEndPanel;
    private CollectionManager collectionManager;
    private LootCard lootCard;
    void Start()
    {
        lootCard = null;
        collectionManager = GameObject.Find("CollectionManager").GetComponent<CollectionManager>();
    }

    void Update()
    {
        
    }
    private void OnEnable()
    {
        Chest.onChestOpened += DisplayLootCard;
        ExitCave.onRunEnded += EndRun;
        Player.onPlayerDeath += EndRun;
    }
    private void OnDisable()
    {
        ExitCave.onRunEnded -= EndRun;
        Player.onPlayerDeath -= EndRun;
    }

    public void DisplayLootCard()
    {
        lootCard = collectionManager.GetDroppedLoot();
        Chest.onChestOpened -= DisplayLootCard;
    }

    public void EndRun(bool dead)
    {
        if (dead)
        {

        }
        else
        {
            collectionManager.UpdateCollection(lootCard);
        }
        SaveData.Instance.runData.EndOfRun();
        runEndPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
