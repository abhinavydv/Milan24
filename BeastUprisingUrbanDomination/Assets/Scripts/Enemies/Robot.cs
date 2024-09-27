using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawnPoint;
    [SerializeField] float bulletDamage = 10.0f;
    [SerializeField] float bulletFireDelay = 1.0f;

    [SerializeField] GameObject LaserStartPrefab;
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject LaserSpawnPoint;
    [SerializeField] float laserDamage = 30.0f;
    [SerializeField] float laserFireDelay = 1.0f;

    [SerializeField] float minAttackInterval = 1.0f;
    [SerializeField] float maxAttackInterval = 3.0f;

    private float nextAttackTime = 0.0f;
    private bool isLaser = false;

    public AudioSource[] gunshots;
    public AudioSource laserSound;

    new void Start()
    {
        canAttack = false;  // We won't be using the base class's attack invocation
        base.Start();
        stoppingDistance = attackRange * .95f;
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
            if (direction == 0)
            {
                Attack();
            }
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
        GameObject laser = Instantiate(LaserStartPrefab, LaserSpawnPoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(laserFireDelay / 2);
        GameObject laserBeam = Instantiate(LaserPrefab, LaserSpawnPoint.transform.position, Quaternion.Euler(0, target.x < transform.position.x? 180 : 0, 0));
        laserSound.PlayOneShot(laserSound.clip);
        yield return new WaitForSeconds(laserFireDelay * 1.5f);
        Destroy(laser);
        Destroy(laserBeam);
    }

    IEnumerator FireBullets()
    {
        yield return new WaitForSeconds(bulletFireDelay);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<GunBullet>().direction = (targetObject.transform.position - bullet.transform.position).normalized;
        bullet.GetComponent<GunBullet>().damage = (int)bulletDamage;
        int gunshotChoice = Random.Range(0, gunshots.Length);
        AudioSource gunshot = gunshots[gunshotChoice];
        gunshot.PlayOneShot(gunshot.clip);
    }
}
