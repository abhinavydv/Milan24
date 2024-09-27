using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFootSoldier : Enemy
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawnPoint;

    new void Start()
    {
        base.Start();
        stoppingDistance = attackRange * 0.9f;
    }

    protected override void Die()
    {
        
    }
    override protected void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<GunBullet>().direction = new Vector2(targetObject.transform.position.x - transform.position.x, 0).normalized;
    }
}
