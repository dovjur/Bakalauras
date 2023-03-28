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

    public PlayerData()
	{
		coins = 0;
		health = 3;
		moveSpeed = 5;
		attackSpeed = 5;
	}

	public int GetCoins()
	{
		return coins;
	}

	public void AddCoins(int amount)
	{
		coins += amount;
	}

	public int GetHealth()
	{
		return health;
	}

	public float GetMoveSpeed()
	{
		return moveSpeed;
	}

	public float GetAttackSpeed()
	{
		return attackSpeed;
	}
}
