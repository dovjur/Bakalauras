using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walking,
    attacking
}
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackSpeed = .5f;

    private PlayerState state;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    private void Start()
    {
        state = PlayerState.walking;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Horizontal",0);
        animator.SetFloat("Vertical", -1);
    }
    void Update()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Q) && state != PlayerState.attacking)
        {
            StartCoroutine(AttackCoroutine());
        }
        if (state == PlayerState.walking)
        {
            UpdateAnimation();
        }
        
    }
    private void FixedUpdate()
    {
        if (state == PlayerState.walking)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }       
    }
    private IEnumerator AttackCoroutine()
    {
        state = PlayerState.attacking;
        animator.SetBool("IsAttacking", true);
        yield return null;
        animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(.3f);
        state = PlayerState.walking;
    }
    private void UpdateAnimation()
    {
        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
