using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    
    public Pistol(float damage, float fireRate, float relodeTimer, int magSize, float bulletSpeed, float multiShoot, float pelets) :
        base(damage, fireRate, relodeTimer, magSize, bulletSpeed, multiShoot, pelets)
    {

    }

    private void Start()
    {
        base.Start();
    }

    protected override void spawnBullets(float damage, float speed)
    {
        tempBulletGO = Instantiate(bulletPrefab, BarrelTransform.position, Quaternion.identity);
        tempBulletGO.GetComponent<Rigidbody2D>().velocity = (BarrelTransform.position - this.transform.position).normalized * speed;
        tempBulletGO.GetComponent<Bullet>().setDamage(damage);
        foreach (var status in statuses)
        {
            tempBulletGO.GetComponent<Bullet>().addStatus(status.copy());
        }
    }
}
