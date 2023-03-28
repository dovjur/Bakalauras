using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private int currentHealh;

    void Start()
    {
        currentHealh = SaveData.Instance.player.health;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealh -= damage;
    }
}
