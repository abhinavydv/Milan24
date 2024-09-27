using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public float jumpForce;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    public GameObject currentBeast = null;

    public float hp;
    public float attack;
    public float defence;
    public float speed;
    public float jump;

    Rigidbody2D rb;
    bool isGrounded;
    bool canDoubleJump;
    Beast beast;
    public float meleeRadius;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        beast = currentBeast.GetComponent<Beast>();
        attack = beast.attack;
        defence = beast.defence;
        speed = beast.speed / 10f;
        jump = beast.jump / 10f;

        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
        if (isGrounded)
            canDoubleJump = true;

        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

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
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
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
        // Debug.Log("Player took " + damage + " damage!");
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
