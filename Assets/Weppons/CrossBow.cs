using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : Gun
{
    protected void PlayerUpgrades_PierceUpgraded_BulletSpeedUpgraded() { bulletSpeed = baseBulletSpeed * ((1 + GameManager.Instance.playerUpgrades.bulletSpeedBonus) + (GameManager.Instance.playerUpgrades.pierceBonus) / 3); }
    // + 1 pierce give + 33% bonus bullet speed becouse bolt losses 33% of base speed for evey pierced enemy

    protected new void Start()
    {
        base.Start();
        GameManager.Instance.playerUpgrades.BulletSpeedUpgraded -= PlayerUpgrades_BulletSpeedUpgraded;
        GameManager.Instance.playerUpgrades.PierceUpgraded -= PlayerUpgrades_PierceUpgraded;

        GameManager.Instance.playerUpgrades.BulletSpeedUpgraded += PlayerUpgrades_PierceUpgraded_BulletSpeedUpgraded;
        GameManager.Instance.playerUpgrades.PierceUpgraded += PlayerUpgrades_PierceUpgraded_BulletSpeedUpgraded;

        relodeTime = baseRelodeTime / (1 + GameManager.Instance.playerUpgrades.relodeSpeedBonus);
        bulletSpeed = baseBulletSpeed * (1 + GameManager.Instance.playerUpgrades.bulletSpeedBonus + (GameManager.Instance.playerUpgrades.pierceBonus) / 3);
        Debug.Log("Crossbow Start");
    }
    protected override void SpawnBullets(float damage, float speed)
    {
        tempBulletGO = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        tempBulletGO.GetComponent<Rigidbody2D>().velocity = (bulletSpawnPoint.position - this.transform.position).normalized * speed;
        tempBulletGO.GetComponent<Bolt>().setParameters(this.gameObject, damage, pierce, bulletLifeTime, baseBulletSpeed);
        foreach (var status in statuses)
        {
            tempBulletGO.GetComponent<Bullet>().addStatus(status.copy());
        }
    }
}