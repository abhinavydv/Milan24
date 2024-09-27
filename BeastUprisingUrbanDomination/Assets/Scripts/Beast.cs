using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast : MonoBehaviour
{
    public float attack;
    public float defence;
    public float speed;
    public float jump;

    public float sprayRadius;
    public float sprayCooldown;
    public float sprayCooldownTimer;

    public float roarBuff;
    public float roarCooldown;
    public float roarCooldownTimer;
    public float roarEffect;
    public float roarEffectTimer;
    public bool roarActive = false;

    void Start()
    {
        sprayCooldownTimer = 0f;
        roarCooldownTimer = 0f;
        roarEffectTimer = 0f;
    }

    void Update()
    {
        sprayCooldownTimer -= Time.deltaTime;
        roarCooldownTimer -= Time.deltaTime;
        if (roarActive)
        {
            roarEffectTimer -= Time.deltaTime;
            if (roarEffectTimer <= 0f)
            {
                attack -= roarBuff;
                roarActive = false;
            }
        }
    }

    public void Ability()
    {
        if (gameObject.name == "Skunk")
            Spray();

        else if (gameObject.name == "Lion")
            Roar();
    }

    void Spray()
    {
        Debug.Log("Spray!");
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, sprayRadius);
        foreach (Collider2D enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy") && sprayCooldownTimer <= 0)
            {
                enemy.GetComponent<Enemy>().Stun(10f);
                sprayCooldownTimer = sprayCooldown;
            }
        }
    }

    void Roar()
    {
        if (!roarActive && roarCooldownTimer <= 0f)
        {
            Debug.Log("Roar!");
            attack += roarBuff;
            roarCooldownTimer = roarCooldown;
            roarEffectTimer = roarEffect;
            roarActive = true;
        }
    }
}
