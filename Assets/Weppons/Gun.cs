using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : Weppon
{

    [SerializeField] protected float relodeTime;
    protected float relodeTimer;
    protected bool isReloding;

    public Slider relodeSlider;

    [SerializeField] protected int magSize;
    public int mag;
    [SerializeField] protected float bulletLifeTime;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float multiShoot;
    [SerializeField] protected float pelets;
    [SerializeField] protected GameObject bulletPrefab;
    protected GameObject tempBulletGO;
    [SerializeField] protected Transform BarrelTransform;


    public override void attack()
    {
        if (mag > 0 && isReloding == false)
        {
            if (fireRateTimer <= 0)
            {
                animator.SetTrigger("Attack"); 
                mag -= 1;
                fireRateTimer = 1 / fireRate;
                //spawn projectail equal to pelets number
                spawnBullets(damage, bulletSpeed);
                if(iconControler != null)
                    iconControler.updateWepponIconAmmo(mag + " / " + magSize);
            }
        }
    }

    protected virtual void spawnBullets(float damage, float speed)
    {

    }
    public override void updateGunsTimers(float delthaTime)
    {
        fireRateTimer -= delthaTime;
    }
    public override void updateGunRelodeTimer(float delthaTime)
    {
        relodeTimer -= delthaTime;
        relodeSlider.value = relodeTimer / relodeTime;
        if (isReloding && relodeTimer <= 0)
        {
            endRelode();
        }
    }
    public override void startRelode(bool forced)
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
    public override void cancelRelode()
    {
        relodeSlider.gameObject.SetActive(false);
        isReloding = false;
        relodeTimer = relodeTime;
    }
    protected override void endRelode()
    {
        relodeSlider.gameObject.SetActive(false);
        isReloding = false;
        mag = magSize;
        fireRateTimer = 0;
        if (iconControler != null)
            iconControler.updateWepponIconAmmo(mag + " / " + magSize);
    }
}