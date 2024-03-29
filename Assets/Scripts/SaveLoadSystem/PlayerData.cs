using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coins;
    public int health;
    public float moveSpeed;
    public float attackSpeed;
    public int strength;

    private int maxHealth;
    private float maxMoveSpeed;
    private float maxAttackSpeed;
    private int maxStrength;

    public PlayerData()
	{
		coins = 10;
		health = 3;
		moveSpeed = 5;
		attackSpeed = 5;
        strength = 1;

        maxHealth = 10;
        maxMoveSpeed = 10;
        maxAttackSpeed = 10;
        maxStrength = 10;
	}

    public bool IsStatMaxed(Stat stat)
    {
        switch (stat)
        {
            case Stat.Health:
                return (maxHealth - health == 0);
            case Stat.MoveSpeed:
                return (maxMoveSpeed - moveSpeed == 0);
            case Stat.AttackSpeed:
                return (maxAttackSpeed - attackSpeed == 0);
            case Stat.Strength:
                return (maxStrength - strength == 0);
            default:
                return true;
        }
    }
}
