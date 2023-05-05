using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public int health;
    public int currentHealth;
    public float moveSpeed;
    public int strenght;
    public float attackSpeed;

    private void Awake()
    {
        currentHealth = health;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
    }
}
