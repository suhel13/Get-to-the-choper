using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGranade : Gun
{
    public float maxRange;
    [SerializeField] float explosionRadius;
    [SerializeField] [Range(1,2f)] float maxScale;
    GranadeProjectail granade;
    public override void attack(Vector2 targetPos)
    {
        if (mag > 0 && isReloding == false)
        {
            if (fireRateTimer <= 0)
            {
                animator.SetTrigger("Attack");
                mag -= 1;
                fireRateTimer = 1 / fireRate;
                //spawn projectail equal to pelets number
                spawnBullets(damage, bulletSpeed, targetPos);
                if (iconControler != null)
                    iconControler.updateWepponIconAmmo(mag + " / " + magSize);
            }
            else return;
        }
        else return;
    }

    protected void spawnBullets(float damage, float speed, Vector2 targetPos)
    {
        tempBulletGO = Instantiate(bulletPrefab, BarrelTransform.position, Quaternion.identity);
        granade = tempBulletGO.GetComponent<GranadeProjectail>();
        Vector2 velocity = (BarrelTransform.position - transform.position).normalized * speed;
        granade.setParameters(damage, explosionRadius, targetPos, velocity, statuses, maxScale);
    }
}