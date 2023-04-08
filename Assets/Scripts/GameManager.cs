using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private GameObject playerPrefab;

    private static GameObject player;
    public static GameObject Player { get { return player; } }

    private float timer;

    void Awake()
    {
        player = Instantiate(playerPrefab,mapGenerator.GetSpawnPoint(),Quaternion.identity);
    }

    private void Start()
    {
        timer = 0;   
    }

    void Update()
    {
        timer += Time.deltaTime;
        SaveData.Instance.runData.SetTime(Mathf.Round(timer));
    }
}
