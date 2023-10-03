using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] float relodeTime;
    float relodeTimer;
    bool isReloding;
    public bool stopedShooting;
    [HideInInspector] public Slider relodeSlider;

    [SerializeField] int magSize;
    public int mag;
    [SerializeField] float bulletSpeed;
    [SerializeField] float multiShoot;
    [SerializeField] float pelets;
    [SerializeField] protected GameObject bulletPrefab;
    protected GameObject tempBulletGO;
    [SerializeField] protected Transform BarrelTransform;
    [SerializeField] List<StatusSO> statusesSO = new List<StatusSO>();
    [SerializeField] protected List<Status> statuses;

    public WepponIconControler iconControler;

    protected void Start()
    {
        statuses = new List<Status>();
        foreach (var statSO in statusesSO)
        {
            Debug.Log(statSO.name);
            statuses.Add(statSO.createObject());
        }
    }
    public Gun(float damage, float fireRate, float relodeTime, int magSize, float bulletSpeed, float multiShoot, float pelets)
    {
        this.damage = damage;
        this.fireRate = fireRate;
        this.relodeTime = relodeTime;
        this.magSize = magSize;
        this.mag = magSize;
        this.bulletSpeed = bulletSpeed;
        this.multiShoot = multiShoot;
        this.pelets = pelets;
    }
    public void shoot()
    {
        if (mag > 0 && isReloding == false)
        {
            if (fireRateTimer <= 0)
            {
                mag -= 1;
                fireRateTimer = 1 / fireRate;
                //spawn projectail equal to pelets number
                spawnBullets(damage, bulletSpeed);
                if(iconControler != null)
                    iconControler.updateWepponIconAmmo(mag + " / " + magSize);
            }
            stopedShooting = false;
        }
        else if (isReloding == false && stopedShooting)
        {
            startRelode();
        }

    }
    protected virtual void spawnBullets(float damage, float speed)
    {

    }
    public void updateGunsTimers(float delthaTime)
    {
        fireRateTimer -= delthaTime;
    }
    public void updateGunsRelodeTimers(float delthaTime)
    {
        relodeTimer -= delthaTime;
        relodeSlider.value = relodeTimer / relodeTime;
        if (isReloding && relodeTimer <= 0)
        {
            endRelode();
        }
    }


    public void startRelode()
    {
        relodeSlider.gameObject.SetActive(true);
        isReloding = true;
        relodeTimer = relodeTime;
    }
    public void cancelRelode()
    {
        relodeSlider.gameObject.SetActive(false);
        isReloding = false;
        relodeTimer = relodeTime;
    }
    void endRelode()
    {
        relodeSlider.gameObject.SetActive(false);
        isReloding = false;
        mag = magSize;
        fireRateTimer = 0;
        if (iconControler != null)
            iconControler.updateWepponIconAmmo(mag + " / " + magSize);
    }
}
