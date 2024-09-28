using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFootSoldier : Enemy
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawnPoint;

    public List<AudioSource> gunshots;
    bool isGunshotsLoaded = false;

    new void Start()
    {
        base.Start();
        stoppingDistance = attackRange * 0.9f;
    }

    new void Update()
    {
        base.Update();
        if (!isGunshotsLoaded)
        {
            if (targetObject.GetComponent<AudioPrefabManager>().gunshots.Count > 0)
            {
                gunshots = targetObject.GetComponent<AudioPrefabManager>().gunshots;
                isGunshotsLoaded = true;
            }
        }
    }

    protected override void Die()
    {
        
    }
    override protected void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<GunBullet>().direction = new Vector2(targetObject.transform.position.x - transform.position.x, 0).normalized;
        
        int gunshotChoice = Random.Range(0, gunshots.Count);
        AudioSource gunshot = gunshots[gunshotChoice];
        gunshot.PlayOneShot(gunshot.clip);
    }
}
