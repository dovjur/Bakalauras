using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public delegate void OnPlayerDeath(bool dead);
    public static event OnPlayerDeath onPlayerDeath;

    public delegate void OnHealthChange(int health);
    public static event OnHealthChange onHealthChange;

    public GameObject lootCard;

    public float dodgeChance = 0;
    public bool isMagnetOn = false;
    void Awake()
    {
        health = SaveData.Instance.player.health;
        moveSpeed = SaveData.Instance.player.moveSpeed;
        attackSpeed = SaveData.Instance.player.attackSpeed;
        strenght = SaveData.Instance.player.strength;

        currentHealth = health;
    }

    public override void TakeDamage(int damage)
    {
        int rng = Random.Range(1,101);
        if (rng >= dodgeChance)
        {
            base.TakeDamage(damage);
            onHealthChange?.Invoke(currentHealth);
            if (currentHealth <= 0)
            {
                onPlayerDeath?.Invoke(true);
            }
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
        onHealthChange?.Invoke(currentHealth);
    }

    public void IncreaseStrength(int value)
    {
        strenght += value;
    }
}
