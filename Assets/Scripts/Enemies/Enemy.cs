using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walking,
    attacking,
    wandering
}
public class Enemy : MonoBehaviour
{
    public int health;
    public float moveSpeed;
    public int damage;
    public float attackSpeed;
    public float chaseRadius;
    public float attackRadius;
    public EnemyState currentState;

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
