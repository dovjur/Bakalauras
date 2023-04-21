using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private Collider2D objectCollider;

    private float timeBtwAttacks;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameManager.Player.transform;
        timeBtwAttacks = attackSpeed;
    }
    private void Update()
    {
        CheckDistance();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        if (currentState == EnemyState.walking)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void CheckDistance()
    {
        movement = Vector2.zero;
        float distance = Vector2.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius)
        {
            ChangeState(EnemyState.walking);
            movement = target.position - transform.position;
        }
        if (distance <= attackRadius && timeBtwAttacks <= 0)
        {
            StartCoroutine(AttackCoroutine());
            timeBtwAttacks = attackSpeed;
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }
    private void UpdateAnimation()
    {
        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.normalized.x);
            animator.SetFloat("Vertical", movement.normalized.y);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
    private IEnumerator AttackCoroutine()
    {
        ChangeState(EnemyState.attacking);
        animator.SetBool("IsAttacking", true);
        projectile.SpawnProjectile(transform,strenght,gameObject);
        yield return null;
        animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(0.4f);
        ChangeState(EnemyState.walking);
    }
}
