using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public delegate void Death(bool dead);
    public static event Death onPlayerDeath;

    void Awake()
    {
        health = SaveData.Instance.player.health;
        moveSpeed = SaveData.Instance.player.moveSpeed;
        attackSpeed = SaveData.Instance.player.attackSpeed;
        strenght = SaveData.Instance.player.strength;

        currentHealth = health;
    }

    void Update()
    {
        
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            onPlayerDeath?.Invoke(true);
        }
    }
    public void IncreaseHealth(int value)
    {
        if (currentHealth + value > health)
        {
            currentHealth = health;
        }
        else
        {
            currentHealth += value;
        }
        
        Debug.Log(currentHealth);
        //onHealthChange?.Invoke(health);
    }

    public void IncreaseStrength(int value)
    {
        strenght += value;
    }
}
