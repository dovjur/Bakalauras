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
        coinCount = Random.Range(0, 5);
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
    private IEnumerator DeathCoroutine()
    {
        animator.SetBool("IsDead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        yield return null;
        animator.SetBool("IsDead", false);
        yield return new WaitForSeconds(0.3f);
        loot.SpawnCoins(coinCount, transform);
        RunData.Instance.AddKill();
        Destroy(gameObject);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHealth <= 0)
        {
            StartCoroutine(DeathCoroutine());
        }
    }
}
