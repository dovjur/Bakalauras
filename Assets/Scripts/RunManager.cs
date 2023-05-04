using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    [SerializeField]
    private GameObject runEndPanel;
    private CollectionManager collectionManager;
    private Card lootCard;
    void Start()
    {
        lootCard = null;
        collectionManager = GameObject.Find("CollectionManager").GetComponent<CollectionManager>();
        collectionManager.ApplyBuffs();
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
        GameManager.Player.lootCard.GetComponent<SpriteRenderer>().sprite = lootCard.sprite;
        Chest.onChestOpened -= DisplayLootCard;
    }

    public void EndRun(bool dead)
    {
        if (dead)
        {
            lootCard = null;
        }
        else
        {
            if (lootCard != null && lootCard.isUnlocked)
            {

            }
            collectionManager.UpdateCollection(lootCard);
        }
        SaveData.Instance.runData.EndOfRun();
        runEndPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
