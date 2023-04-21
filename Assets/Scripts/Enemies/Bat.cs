using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameManager.Player.transform;
    }

    void Update()
    {
        CheckDistance();
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
        if (distance <= attackRadius)
        {
            //StartCoroutine(AttackCoroutine());
        }
    }
    private IEnumerator AttackCoroutine()
    {
        ChangeState(EnemyState.attacking);
        animator.SetBool("IsAttacking", true);
        yield return null;
        animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(0.4f);
        ChangeState(EnemyState.walking);
    }
}
