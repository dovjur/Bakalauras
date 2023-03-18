using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int startEnemies = 5;
    [SerializeField]
    private int enemyNumber = 3;
    [SerializeField]
    private float spawnInterval = 60f;
    [SerializeField]
    private float spawnDistance = 10f;
    [SerializeField]
    private GameObject enemyParent;


    private float timer = 0f;
    private Transform player;
    void Start()
    {
        player = GameManager.Player.transform;
        SpawnEnemy(startEnemies, true);     
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy(enemyNumber, false);
            timer = 0f;
            if (spawnInterval >= 8)
            {
                spawnInterval = spawnInterval / 2;
                //enemyNumber++;
            } 
        }
    }

    private void SpawnEnemy(int enemyCount, bool isStart)
    {
        GameObject newEnemy;
        for (int i = 0; i < enemyCount; i++)
        {
            if (isStart)
            {
                Vector2 randomPosition = mapGenerator.GetRandomGroundTile();
                newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            }
            else
            {
                Vector2 randomPosition = mapGenerator.GetEnemySpawnTile(player.transform, spawnDistance);
                newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            }
            newEnemy.transform.SetParent(enemyParent.transform);
        }
    }
}
