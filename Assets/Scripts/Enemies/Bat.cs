using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = RunManager.Player.transform;
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
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(strenght);
        }
    }
}
