using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : Weppon
{

    [SerializeField] protected float baseRelodeTime;
    protected float relodeTime;
    protected float relodeTimer;
    protected bool isReloding;

    [HideInInspector] public Slider relodeSlider;

    [SerializeField] protected int magSize;
    public int mag;
    [SerializeField] protected float bulletLifeTime;
    [SerializeField] protected float baseBulletSpeed;
    protected float bulletSpeed;
    [SerializeField] protected float multiShoot;
    [SerializeField] protected float pelets;
    [SerializeField] protected GameObject bulletPrefab;
    protected GameObject tempBulletGO;
    [SerializeField] protected Transform bulletSpawnPoint;

    void PlayerUpgrades_RelodeUpgraded() { relodeTime = baseRelodeTime / (1 + GameManager.Instance.playerUpgrades.relodeSpeedBonus); }
    void PlayerUpgrades_BulletSpeedUpgraded() { bulletSpeed = baseBulletSpeed * (1 + GameManager.Instance.playerUpgrades.bulletSpeedBonus); }
    protected new void Start()
    {
        base.Start();
        GameManager.Instance.playerUpgrades.RelodeUpgraded += PlayerUpgrades_RelodeUpgraded;
        GameManager.Instance.playerUpgrades.BulletSpeedUpgraded += PlayerUpgrades_BulletSpeedUpgraded;
        relodeTime = baseRelodeTime / (1 + GameManager.Instance.playerUpgrades.relodeSpeedBonus);
        bulletSpeed = baseBulletSpeed * (1 + GameManager.Instance.playerUpgrades.bulletSpeedBonus);
        Debug.Log("Gun Start");
    }

    public override void Attack(Vector2 targetPos)
    {
        if (mag > 0 && isReloding == false)
        {
            if (fireRateTimer <= 0)
            {
                animator.SetTrigger("Attack");
                mag -= 1;
                fireRateTimer = 1 / fireRate;
                //spawn projectail equal to pelets number
                SpawnBullets(damage, bulletSpeed);
                if (iconControler != null)
                    iconControler.updateWepponIconAmmo(mag + " / " + magSize);
            }
            else return;
        }
        else return;
    }

    protected virtual void SpawnBullets(float damage, float speed)
    {

    }
    public override void UpdateGunsTimers(float delthaTime)
    {
        fireRateTimer -= delthaTime;
    }
    public override void UpdateGunRelodeTimer(float delthaTime)
    {
        relodeTimer -= delthaTime;
        relodeSlider.value = relodeTimer / relodeTime;
        if (isReloding && relodeTimer <= 0)
        {
            EndRelode();
        }
    }
    public override void StartRelode(bool forced)
    {
        if (isReloding == false)
        {
            if (forced)
            {
                relodeSlider.gameObject.SetActive(true);
                isReloding = true;
                relodeTimer = relodeTime;
            }
            else if (mag == 0)
            {
                relodeSlider.gameObject.SetActive(true);
                isReloding = true;
                relodeTimer = relodeTime;
            }
        }
    }
    public override void CancelRelode()
    {
        relodeSlider.gameObject.SetActive(false);
        isReloding = false;
        relodeTimer = relodeTime;
    }
    protected override void EndRelode()
    {
        relodeSlider.gameObject.SetActive(false);
        isReloding = false;
        mag = magSize;
        fireRateTimer = 0;
        if (iconControler != null)
            iconControler.updateWepponIconAmmo(mag + " / " + magSize);
    }
}