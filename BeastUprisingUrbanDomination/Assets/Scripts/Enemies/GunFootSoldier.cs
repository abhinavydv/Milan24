using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFootSoldier : Enemy
{
    [SerializeField] GameObject bulletPrefab;

    protected override void Die()
    {
        
    }
    override protected void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<GunBullet>().direction = (targetObject.transform.position - transform.position).normalized;
    }
}
