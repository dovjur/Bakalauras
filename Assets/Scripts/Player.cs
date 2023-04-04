using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public delegate void UpdateHearts(int health);
    public static event UpdateHearts onHealthChange;

    public int currentHealth;
    private int currentMoveSpeed;
    private int currentAttackSpeed;
    private int currentStrength;

    void Awake()
    {
        currentHealth = SaveData.Instance.player.health;
        currentMoveSpeed = SaveData.Instance.player.moveSpeed;
        currentAttackSpeed = SaveData.Instance.player.attackSpeed;
        currentStrength = SaveData.Instance.player.strength;

        Debug.Log(currentHealth);
    }

    void Update()
    {
        
    }
    public int GetHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        onHealthChange(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public void IncreaseStrength(int value)
    {
        currentStrength += value;
    }
}
