using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public float jumpForce;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    public GameObject currentBeast = null;
    public Animator animator;

    public float health;
    public float attack;
    public float defence;
    public float speed;
    public float jump;

    Rigidbody2D rb;
    public bool isDead;
    bool isGrounded;
    bool canDoubleJump;
    Beast beast;
    public float meleeRadius;
    float deathTime = 0f;
    public BeastManager beastManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead)
            if (Time.time - deathTime > 1.0f)
                Quit();

        beast = currentBeast.GetComponent<Beast>();
        animator = currentBeast.GetComponent<Animator>();
        attack = beast.attack;
        defence = beast.defence;
        speed = beast.speed / 10f;
        jump = beast.jump / 10f;

        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
        if (isGrounded)
            canDoubleJump = true;

        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (moveX == 0)
            animator.SetBool("isRunning", false);
        else
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            animator.SetBool("isRunning", true);
        }
        if (isGrounded)
            animator.SetBool("isJumping", false);
        else
            animator.SetBool("isJumping", true);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                canDoubleJump = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            beast.Ability();
        }
        else if (Input.GetKeyDown(KeyCode.K) && currentBeast != beastManager.skunk)
        {
            animator.SetBool("isAttacking", true);
            Attack();
        }
        else
        {
            if (currentBeast != beastManager.skunk)
                animator.SetBool("isAttacking", false);
        }
    }

    void Attack()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, meleeRadius);
        foreach (Collider2D enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().TakeDamage(attack);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage / ((defence + 100f) / 100f);
        if (health <= 0)
        {
            isDead = true;
            deathTime = Time.time;
            animator.SetBool("isDead", true);
            //Die();
        }
    }

    public void Quit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
