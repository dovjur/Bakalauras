using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private MapGenerator mapGenerator;
    [SerializeField]
    private List<Enemy> enemyPrefabs;
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

    private int[,] map;
    private float timer = 0f;
    private Transform player;
    void Start()
    {
        map = mapGenerator.GetMap();
        player = RunManager.Player.transform;
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
        Enemy newEnemy;
        for (int i = 0; i < enemyCount; i++)
        {
            Enemy enemyPrefab = enemyPrefabs[Random.Range(0,enemyPrefabs.Count)];
            if (isStart)
            {
                Vector2 randomPosition = mapGenerator.GetRandomGroundTile();
                newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            }
            else
            {
                Vector2 randomPosition = GetEnemySpawnTile(player.transform, spawnDistance);
                newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            }
            newEnemy.transform.SetParent(enemyParent.transform);
        }
    }
    public Vector2 GetEnemySpawnTile(Transform player, float spawnDistance)
    {
        float minDistance = 20f;
        Vector2 randomPosition = player.position + Random.insideUnitSphere * spawnDistance;
        if (Vector2.Distance(randomPosition, player.position) < minDistance)
        {
            randomPosition = player.position + Random.insideUnitSphere * (minDistance + 1f);
        }
        if (mapGenerator.IsInMapRange((int)randomPosition.x, (int)randomPosition.y) && map[(int)randomPosition.x, (int)randomPosition.y] == 0)
        {
            return randomPosition;
        }
        else
        {
            return GetEnemySpawnTile(player, spawnDistance);
        }
    }
}
