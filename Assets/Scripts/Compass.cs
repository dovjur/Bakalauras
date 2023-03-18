using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    private GameObject player;
    private GameObject chest;

    Vector2 position;
    Vector2 destination;

    private void Start()
    {
        player = GameManager.Player;
        chest = MapGenerator.Chest;
    }

    void Update()
    {
        if (!chest.GetComponent<Animator>().GetBool("Open"))
        {
            destination = chest.transform.position;
        }
        else
        {
            destination = MapGenerator.spawnPoint;
        }
        position = player.transform.position;
        float angle = Mathf.Atan2(destination.y - position.y, destination.x - position.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), 1f);
    }
}
