using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Weppon : MonoBehaviour
{

    [SerializeField] protected float baseDamage;
    protected float damage;
    [SerializeField] protected float baseFireRate;
    protected float fireRate;
    protected float fireRateTimer;
    [HideInInspector] public bool stopedShooting;

    [SerializeField] protected List<StatusSO> statusesSO = new List<StatusSO>();
    [SerializeField] protected List<Status> statuses = new List<Status>();

    [HideInInspector] public WepponIconControler iconControler;
    [HideInInspector] public Animator animator;

    void PlayerUpgrades_WepponDamageUpgraded() { damage = baseDamage * (1 + GameManager.Instance.playerUpgrades.wepponDamageBonus); }
    void PlayerUpgrades_FireRateUpgraded() { fireRate = baseFireRate * (1 + GameManager.Instance.playerUpgrades.fireRateBonus); }

    protected void Start()
    {
        GameManager.Instance.playerUpgrades.WepponDamageUpgraded += PlayerUpgrades_WepponDamageUpgraded;
        GameManager.Instance.playerUpgrades.FireRateUpgraded += PlayerUpgrades_FireRateUpgraded;
        foreach (var statSO in statusesSO)
        {
            Debug.Log(statSO.name);
            statuses.Add(statSO.createObject());
        }
        animator = GetComponent<Animator>();
        damage = baseDamage * (1 + GameManager.Instance.playerUpgrades.wepponDamageBonus);
        fireRate = baseFireRate * (1 + GameManager.Instance.playerUpgrades.fireRateBonus);
    }

    public virtual void attack() { }
    public virtual void Attack(Vector2 targetPos)
    {
        if (fireRateTimer <= 0)
        {
            animator.SetTrigger("Attack"); 
            fireRateTimer = 1 / baseFireRate;
        }
    }
    public virtual void UpdateGunsTimers(float delthaTime)
    {
        fireRateTimer -= delthaTime;
    }

    public virtual void UpdateGunRelodeTimer(float delthaTime)
    {

    }
    public virtual void StartRelode(bool forced)
    {

    }
    public virtual void CancelRelode()
    {

    }
    protected virtual void EndRelode()
    {

    }
}