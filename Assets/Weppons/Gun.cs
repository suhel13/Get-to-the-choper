using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : Weppon
{
    [Header("Magazine", order = 2)]
    [SerializeField] protected float baseRelodeTime;
    protected float relodeTime;
    protected float relodeTimer;
    protected bool isReloding;

    [HideInInspector] public Slider relodeSlider;
    [SerializeField] protected int magSize;
    public int mag;
    [Header("Bullets ", order = 20)]
    [SerializeField] protected float bulletLifeTime;
    [SerializeField] protected float baseBulletSpeed;
    protected float bulletSpeed;
    [SerializeField] protected float multiShoot;
    [SerializeField] protected float pelets;
    [SerializeField] protected float basePierce;
    protected float pierce;
    [SerializeField] protected GameObject bulletPrefab;
    protected GameObject tempBulletGO;
    [SerializeField] protected Transform bulletSpawnPoint;

    [Header("Camera Shake")]
    [SerializeField] float shakeIntecity;
    [SerializeField] float shakeDuration;

    [HideInInspector] public TMPro.TextMeshProUGUI ammoCounterText;

    protected void PlayerUpgrades_RelodeUpgraded() { relodeTime = baseRelodeTime / (1 + GameManager.Instance.playerUpgrades.relodeSpeedBonus); }
    protected void PlayerUpgrades_BulletSpeedUpgraded() { bulletSpeed = baseBulletSpeed * (1 + GameManager.Instance.playerUpgrades.bulletSpeedBonus); }
    protected void PlayerUpgrades_PierceUpgraded() { pierce = basePierce  + GameManager.Instance.playerUpgrades.pierceBonus; }
    protected new void Start()
    {
        base.Start();
        GameManager.Instance.playerUpgrades.RelodeUpgraded += PlayerUpgrades_RelodeUpgraded;
        GameManager.Instance.playerUpgrades.BulletSpeedUpgraded += PlayerUpgrades_BulletSpeedUpgraded;
        GameManager.Instance.playerUpgrades.PierceUpgraded += PlayerUpgrades_PierceUpgraded;
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
                Debug.Log("gun attqack");
                animator.ResetTrigger("Relode");
                animator.ResetTrigger("EndRelode");
                animator.SetTrigger("Attack");
                mag -= 1;
                fireRateTimer = 1 / fireRate;
                //spawn projectail equal to pelets number
                SpawnBullets(damage, bulletSpeed);
                UpdateGunIconInfo();
                UpdateAmmoCounterText();
                ShakeManager.Instance.CameraShake(shakeIntecity, shakeDuration);
            }
            else return;
        }
        else return;
    }
    public void UpdateAmmoCounterText()
    {
        if (ammoCounterText != null)
            ammoCounterText.text = mag.ToString();
    }
    public void UpdateGunIconInfo()
    {
        if (iconControler != null)
            iconControler.updateWepponIconAmmo(mag + " / " + magSize);
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
        if (isReloding == false)
            return;

        relodeTimer -= delthaTime;
        relodeSlider.value = relodeTimer / relodeTime;
        //Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        animator.SetFloat("RelodeSpeed", animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / relodeTime);

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
                Debug.Log("gun start relode");
                animator.SetTrigger("Relode");
                relodeSlider.gameObject.SetActive(true);
                isReloding = true;
                relodeTimer = relodeTime;
            }
            else if (mag == 0)
            {
                Debug.Log("gun start relode");
                animator.SetTrigger("Relode");
                relodeSlider.gameObject.SetActive(true);
                isReloding = true;
                relodeTimer = relodeTime;
            }
        }
    }
    public override void CancelRelode()
    {
        animator.ResetTrigger("EndRelode");
        animator.ResetTrigger("Relode");
        animator.SetTrigger("CancelRelode");
        relodeSlider.gameObject.SetActive(false);
        isReloding = false;
        relodeTimer = relodeTime;
    }
    protected override void EndRelode()
    {
        Debug.Log("Gun end relode");
        animator.ResetTrigger("Relode");
        animator.ResetTrigger("Attack");
        animator.SetTrigger("EndRelode");
        animator.speed = 1;
        relodeSlider.gameObject.SetActive(false);
        isReloding = false;
        ammoCounterText.text = mag.ToString();
        mag = magSize;
        fireRateTimer = 0;
        UpdateGunIconInfo();
        UpdateAmmoCounterText();
    }
}