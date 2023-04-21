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
public class Enemy : Character
{
    [SerializeField]
    protected Coin loot;

    public float chaseRadius;
    public float attackRadius;
    public EnemyState currentState;

    protected Rigidbody2D rb;
    protected Animator animator;
    protected Vector2 movement;
    protected Transform target;
    protected int coinCount;

    private void Start()
    {

    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
    
}
