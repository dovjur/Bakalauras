using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;

    private Transform player;
    private Vector3 target;

    void Start()
    {
        player = GameManager.Player.transform;

        target = new Vector3 (player.position.x, player.position.y);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,target, movementSpeed * Time.deltaTime);

        transform.Rotate(new Vector3(0,0,rotationSpeed));

        if (transform.position == target)
        {
            rotationSpeed = 0;
            Destroy(gameObject, .5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Deal damage");
            Destroy(gameObject);
        }
    }
}
