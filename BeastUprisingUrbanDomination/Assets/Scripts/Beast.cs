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
            if (enemy.CompareTag("Enemy"))
                enemy.GetComponent<Enemy>().Stun(10f);
        }
    }

    void Roar()
    {

    }
}
