using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFootSoldier : Enemy
{
    protected override void Die()
    {
        
    }
    override protected void Attack()
    {
        targetObject.GetComponent<Player>().TakeDamage(damage);
    }
}
