using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public float jumpForce;
    public LayerMask groundMask;
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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        attack = currentBeast.GetComponent<Beast>().attack;
        defence = currentBeast.GetComponent<Beast>().defence;
        speed = currentBeast.GetComponent<Beast>().speed / 10f;
        jump = currentBeast.GetComponent<Beast>().jump / 10f;

        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundMask);
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
    }

    public void TakeDamage(float damage)
    {
        // Debug.Log("Player took " + damage + " damage!");
    }
}
