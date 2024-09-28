using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFootSoldier : Enemy
{
    public AudioSource[] punches;

    new void Start()
    {
        base.Start();
        punches = targetObject.GetComponent<AudioPrefabManager>().melee;
    }

    protected override void Die()
    {
    }
    override protected void Attack()
    {
        targetObject.GetComponent<Player>().TakeDamage(damage);
        int punchChoice = Random.Range(0, punches.Length);
        AudioSource punch = punches[punchChoice];
        punch.PlayOneShot(punch.clip);
    }
}
