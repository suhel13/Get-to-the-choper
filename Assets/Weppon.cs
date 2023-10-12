using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Weppon : MonoBehaviour
{

    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    protected float fireRateTimer;
    [HideInInspector] public bool stopedShooting;

    [SerializeField] protected List<StatusSO> statusesSO = new List<StatusSO>();
    [SerializeField] protected List<Status> statuses = new List<Status>();

    [HideInInspector] public WepponIconControler iconControler;
    [HideInInspector] public Animator animator;

    protected void Start()
    {
        foreach (var statSO in statusesSO)
        {
            Debug.Log(statSO.name);
            statuses.Add(statSO.createObject());
        }
        animator = GetComponent<Animator>();
    }
    public virtual void attack()
    {
        if (fireRateTimer <= 0)
        {
            animator.SetTrigger("Attack"); 
            fireRateTimer = 1 / fireRate;
        }
    }

    public virtual void updateGunsTimers(float delthaTime)
    {
        fireRateTimer -= delthaTime;
    }

    public virtual void updateGunRelodeTimer(float delthaTime)
    {

    }

    public virtual void startRelode(bool forced)
    {

    }
    public virtual void cancelRelode()
    {

    }
    protected virtual void endRelode()
    {

    }
}