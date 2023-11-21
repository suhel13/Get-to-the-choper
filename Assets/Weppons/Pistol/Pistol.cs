using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    protected override void SpawnBullets(float damage, float speed)
    {
        tempBulletGO = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        tempBulletGO.GetComponent<Rigidbody2D>().velocity = (bulletSpawnPoint.position - this.transform.position).normalized * speed;
        tempBulletGO.GetComponent<Bullet>().setParameters(this.gameObject, damage, pierce, bulletLifeTime);
        foreach (var status in statuses)
        {
            tempBulletGO.GetComponent<Bullet>().addStatus(status.copy());
        }
    }
}
