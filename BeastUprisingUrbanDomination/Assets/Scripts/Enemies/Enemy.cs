using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // stats
    [SerializeField] protected float speed = 2.0f;
    [SerializeField] protected float health = 100.0f;
    [SerializeField] protected float damage = 10.0f;
    [SerializeField] protected float attackInterval = 1.0f;
    [SerializeField] protected float attackRange = 1.2f;

    // references
    [SerializeField] protected GameObject targetObject;

    // movement
    [SerializeField] protected float stoppingDistance = 1.0f;
    protected Vector3 target = new Vector3(0, 0, 0);
    protected bool isAttacking = false;
    protected bool isDead = false;
    public float multiplier = 1.0f; // A multiplier to increase/decrease speed, attackSpeed of the enemy
    protected int direction = 1; // 1 for right, -1 for left

    // state
    protected float lastAttackTime = 0.0f;
    protected float deathTime = 0.0f;

    // components
    protected Rigidbody2D rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        if (isDead)
        {
            if (Time.time - deathTime > 1.0f)
            {
                Destroy(gameObject);
            }
            return;
        }
        UpdateVars();
        Act();
    }

    protected void UpdateVars()
    {
        direction = target.x > transform.position.x ? 1 : -1;
        if (targetObject != null)
        {
            target = targetObject.transform.position;
        }
        if (Mathf.Abs(target.x - transform.position.x) < stoppingDistance)
        {
            direction = 0;
        }
    }

    abstract protected void Attack();

    protected void Act()
    {
        rb.position += new Vector2(speed * direction * multiplier * Time.deltaTime, 0);
        if (Time.time - lastAttackTime > attackInterval)
        {
            if (Vector3.Distance(target, transform.position) < attackRange)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    abstract protected void Die();

    protected void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            Die();
        }
    }

}
