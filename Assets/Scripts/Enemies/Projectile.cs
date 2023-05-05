using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Projectile prefab;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;

    private Transform player;
    private GameObject shooter;
    private Vector3 target;
    private int damage;

    void Start()
    {
        player = RunManager.Player.transform;

        target = new Vector3(player.position.x, player.position.y);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

        transform.Rotate(new Vector3(0, 0, rotationSpeed));

        if (transform.position == target)
        {
            rotationSpeed = 0;
            Destroy(gameObject, .5f);
        }
    }
    public void SpawnProjectile(Transform transform, int damage, GameObject shooter)
    {
        Projectile projectile = Instantiate(prefab,transform.position, Quaternion.identity);
        projectile.shooter = shooter;
        projectile.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null && !GameObject.ReferenceEquals(other.gameObject,shooter))
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
