using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // stats
    [SerializeField] protected float speed = 2.0f;
    [SerializeField] protected float health = 100.0f;
    [SerializeField] protected float defence = 20.0f;
    [SerializeField] protected float damage = 10.0f;
    [SerializeField] protected float attackInterval = 1.0f;
    [SerializeField] protected float attackRange = 1.2f;
    [SerializeField] protected float seekDistance = 30.0f;
    protected bool canAttack = true;

    // references
    protected GameObject targetObject;
    [SerializeField] protected Animator animator;

    // movement
    [SerializeField] protected float stoppingDistance = 1.0f;
    protected Vector3 target = new Vector3(0, 0, 0);
    protected bool isAttacking = false;
    protected bool isDead = false;
    public float multiplier = 1.0f; // A multiplier to increase/decrease speed, attackSpeed of the enemy
    protected int direction = 1; // 1 for right, -1 for left
    protected bool isStunned = false;
    protected float unStunnedTime = 0.0f;

    // state
    protected float lastAttackTime = 0.0f;
    protected float deathTime = 0.0f;
    protected bool isVisible = false;

    protected void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
    }

    protected void Update()
    {
        if (isDead)
        {
            if (Time.time - deathTime > 1.0f)
            {
                Destroy(gameObject);
            }
        }
        if (isStunned)
        {
            if (Time.time > unStunnedTime)
            {
                isStunned = false;
            }
        }
        if (!shouldUpdate())
        {
            return;
        }
        UpdateVars();
        Act();
    }

    protected bool shouldUpdate()
    {
        return !isDead && !isStunned && isVisible;
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }

    protected void UpdateVars()
    {
        if (targetObject != null)
        {
            target = targetObject.transform.position;
        }
        direction = target.x > transform.position.x ? 1 : -1;
        if (Mathf.Abs(target.x - transform.position.x) < stoppingDistance || Mathf.Abs(target.x - transform.position.x) > seekDistance)
        {
            direction = 0;
        }
        animator.SetBool("isRunning", direction != 0);
        this.transform.rotation = Quaternion.Euler(0, target.x > transform.position.x ? 0 : 180, 0);
    }

    abstract protected void Attack();

    protected void Act()
    {
        transform.position += new Vector3(speed * direction * multiplier * Time.deltaTime, 0, 0);
        if (canAttack)
        {
            if ((Time.time - lastAttackTime > attackInterval && (Vector3.Distance(target, transform.position) < attackRange)) && direction == 0)
            {
                animator.SetBool("isAttacking", true);
                Attack();
                lastAttackTime = Time.time;
            } else
            {
                animator.SetBool("isAttacking", false);
            }
        }
    }

    abstract protected void Die();

    public void TakeDamage(float damage)
    {
        health -= damage / ((defence + 100f)/100f);
        if (health <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            Die();
        }
    }

    public void Stun(float time)
    {
        isStunned = true;
        unStunnedTime = Time.time + time;
    }

}
