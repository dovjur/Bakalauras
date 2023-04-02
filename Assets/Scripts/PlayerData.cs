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

    public PlayerData()
	{
		coins = 100;
		health = 3;
		moveSpeed = 5;
		attackSpeed = 5;
        strength = 5;
	}
}
