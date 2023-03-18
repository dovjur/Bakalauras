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

    void Start()
    {
        player = Instantiate(playerPrefab,mapGenerator.GetSpawnPoint(),Quaternion.identity);
    }

    
    void Update()
    {
        
    }
}
