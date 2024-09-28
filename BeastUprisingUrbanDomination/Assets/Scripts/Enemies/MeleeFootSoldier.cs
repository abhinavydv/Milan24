using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFootSoldier : Enemy
{
    public List<AudioSource> punches;
    bool isPunchesLoaded = false;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
        if (!isPunchesLoaded)
        {
            if(targetObject.GetComponent<AudioPrefabManager>().melee.Count > 0)
            {
                punches = targetObject.GetComponent<AudioPrefabManager>().melee;
                isPunchesLoaded = true;
            }
        }
    }

    protected override void Die()
    {
    }
    override protected void Attack()
    {
        targetObject.GetComponent<Player>().TakeDamage(damage);
        int punchChoice = Random.Range(0, punches.Count);
        AudioSource punch = punches[punchChoice];
        punch.PlayOneShot(punch.clip);
    }
}
