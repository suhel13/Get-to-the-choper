using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGranade : Gun
{
    public float maxRange;
    float explosionRadius;
    [SerializeField] float baseExplosionRadius;
    [SerializeField] [Range(1,2f)] float maxScale;
    GranadeProjectail granade;
    void PlayerUpgrades_RangeUpgraded() { explosionRadius = baseExplosionRadius * (1 + GameManager.Instance.playerUpgrades.rangeBonus); }
    private void Start()
    {
        base.Start();
        GameManager.Instance.playerUpgrades.RangeUpgraded += PlayerUpgrades_RangeUpgraded;
        explosionRadius = baseExplosionRadius * (1 + GameManager.Instance.playerUpgrades.rangeBonus);
    }

    public override void Attack(Vector2 targetPos)
    {
        if (mag > 0 && isReloding == false)
        {
            if (fireRateTimer <= 0)
            {
                animator.SetTrigger("Attack");
                mag -= 1;
                fireRateTimer = 1 / baseFireRate;
                //spawn projectail equal to pelets number
                SpawnBullets(baseDamage, bulletSpeed, targetPos);
                if (iconControler != null)
                    iconControler.updateWepponIconAmmo(mag + " / " + magSize);
            }
            else return;
        }
        else return;
    }

    protected void SpawnBullets(float damage, float speed, Vector2 targetPos)
    {
        tempBulletGO = Instantiate(bulletPrefab, BarrelTransform.position, Quaternion.identity);
        granade = tempBulletGO.GetComponent<GranadeProjectail>();
        Vector2 velocity = (BarrelTransform.position - transform.position).normalized * speed;
        granade.SetParameters(damage, explosionRadius, targetPos, velocity, statuses, maxScale);
    }
}