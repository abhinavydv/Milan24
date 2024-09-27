using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawnPoint;
    [SerializeField] float bulletDamage = 10.0f;
    [SerializeField] float bulletFireDelay = 1.0f;

    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject LaserSpawnPoint;
    [SerializeField] float laserDamage = 30.0f;
    [SerializeField] float laserFireDelay = 1.0f;

    [SerializeField] float minAttackInterval = 1.0f;
    [SerializeField] float maxAttackInterval = 3.0f;

    private float nextAttackTime = 0.0f;
    private bool isLaser = false;

    new void Start()
    {
        base.Start();
        stoppingDistance = attackRange * .9f;
    }

    new void Update()
    {
        base.Update();
        if (!shouldUpdate())
        {
            return;
        }
        animator.SetBool("isActivatingBeam", false);
        animator.SetBool("isFiringBullets", false);
        if (Time.time > nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + Random.Range(minAttackInterval, maxAttackInterval);
            if (Random.Range(0.0f, 1.0f) < 0.2f)
            {
                isLaser = true;
                nextAttackTime += 2.0f;
            }
        }
    }

    protected override void Die()
    {

    }

    override protected void Attack()
    {
        if (isLaser)
        {
            StartCoroutine(ActivateBeam());
            isLaser = false;
            animator.SetBool("isActivatingBeam", true);
        }
        else
        {
            StartCoroutine(FireBullets());
            animator.SetBool("isFiringBullets", true);
        }
    }

    IEnumerator ActivateBeam()
    {
        yield return new WaitForSeconds(laserFireDelay);
        GameObject laser = Instantiate(LaserPrefab, LaserSpawnPoint.transform.position, Quaternion.identity);
        laser.GetComponent<GunBullet>().direction = new Vector2(targetObject.transform.position.x - transform.position.x, 0).normalized;
        laser.GetComponent<GunBullet>().damage = (int)laserDamage;
    }

    IEnumerator FireBullets()
    {
        yield return new WaitForSeconds(bulletFireDelay);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<GunBullet>().direction = new Vector2(targetObject.transform.position.x - transform.position.x, 0).normalized;
        bullet.GetComponent<GunBullet>().damage = (int)bulletDamage;
    }
}
