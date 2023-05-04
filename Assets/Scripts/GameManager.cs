using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private Player playerPrefab;
    [SerializeField]
    private Compass compass;
    private GameObject chest;

    private static Player player;
    public static Player Player { get { return player; } }

    private float timer;

    void Awake()
    {
        Time.timeScale = 1;
        mapGenerator.Generate();
        chest = mapGenerator.SpawnLootChest();
        player = Instantiate(playerPrefab,mapGenerator.spawnPoint,Quaternion.identity);
    }

    private void Start()
    {
        compass.SetDestination(chest.transform.position);
        timer = 0;   
    }
    private void OnEnable()
    {
        Chest.onChestOpened += ChangeCompassDestination;
    }

    void Update()
    {
        timer += Time.deltaTime;
        SaveData.Instance.runData.SetTime(Mathf.Round(timer));
    }

    private void ChangeCompassDestination()
    {
        compass.SetDestination(mapGenerator.spawnPoint);
        Chest.onChestOpened -= ChangeCompassDestination;
    }
}
