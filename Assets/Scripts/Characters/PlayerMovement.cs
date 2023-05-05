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
    private float lastAttack = 0;
    private float timeBtwAttacks;

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
        timeBtwAttacks = (1f / player.attackSpeed) + 0.5f;
    }
    void Update()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Q) && state != PlayerState.attacking && Time.time - lastAttack >= timeBtwAttacks)
        {
            StartCoroutine(AttackCoroutine());
            lastAttack = Time.time;
        }
        else if (state == PlayerState.walking)
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
        animator.SetBool("IsAttacking", true);
        state = PlayerState.attacking;
        yield return null;
        animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(.5f);
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
