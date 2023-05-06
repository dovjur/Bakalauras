using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    private static Player player;
    public static Player Player { get { return player; } }

    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private Player playerPrefab;
    [SerializeField]
    private Compass compass;
    [SerializeField]
    private EndOfRunPanel runEndPanel;

    private CollectionManager collectionManager;
    private Card lootCard;
    private GameObject chest;
    private float timer = 0;

    void Awake()
    {
        Time.timeScale = 1;
        mapGenerator.Generate();
        chest = mapGenerator.SpawnLootChest();
        player = Instantiate(playerPrefab, mapGenerator.spawnPoint, Quaternion.identity);
    }

    void Start()
    {
        lootCard = null;
        collectionManager = GameObject.Find("CollectionManager").GetComponent<CollectionManager>();
        collectionManager.ApplyBuffs();
        compass.SetDestination(chest.transform.position);
    }

    void Update()
    {
        timer += Time.deltaTime;
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
        player.lootCard.GetComponent<SpriteRenderer>().sprite = lootCard.sprite;
        compass.SetDestination(mapGenerator.spawnPoint);
        Chest.onChestOpened -= DisplayLootCard;
    }

    public void EndRun(bool dead)
    {
        Time.timeScale = 0;
        RunData.Instance.SetTime(Mathf.Round(timer));
        RunData.Instance.EndOfRun(dead);
        runEndPanel.DisplayPanel(lootCard, dead);

        if (!dead)
        {
            if (lootCard != null && lootCard.isUnlocked)
            {
                SaveData.Instance.player.coins += lootCard.value;
            }
            collectionManager.UpdateCollection(lootCard);
        }

        SaveLoad.Save(SaveData.Instance);
    }
}
