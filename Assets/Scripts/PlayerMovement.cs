using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walking,
    attacking
}
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Player player;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private PlayerState state;

    private void OnEnable()
    {
        Chest.onChestOpened += OpenChest;
    }
    private void OnDisable()
    {
        Chest.onChestOpened -= OpenChest;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        state = PlayerState.walking;
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
            rb.MovePosition(rb.position + movement.normalized * player.moveSpeed * Time.fixedDeltaTime);
        }       
    }

    public void UpdateAnimation()
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
    private IEnumerator AttackCoroutine()
    {
        state = PlayerState.attacking;
        animator.SetBool("IsAttacking", true);
        yield return null;
        animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(.3f);
        state = PlayerState.walking;
    }
    private void OpenChest()
    {
        StartCoroutine(OpenChestCoroutine());
        Chest.onChestOpened -= OpenChest;
    }

    private IEnumerator OpenChestCoroutine()
    {
        state = PlayerState.idle;
        animator.SetBool("OpenChest", true);
        yield return null;
        animator.SetBool("OpenChest", false);
        yield return new WaitForSeconds(1.25f);
        player.lootCard.SetActive(false);
        state = PlayerState.walking;
    }

}
