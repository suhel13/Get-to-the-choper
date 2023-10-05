using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Weppon : MonoBehaviour
{

    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    protected float fireRateTimer;


    [SerializeField] protected List<StatusSO> statusesSO = new List<StatusSO>();
    [SerializeField] protected List<Status> statuses;

    public WepponIconControler iconControler;
    public AnimatorController animatorController;

    protected void Start()
    {
        statuses = new List<Status>();
        foreach (var statSO in statusesSO)
        {
            Debug.Log(statSO.name);
            statuses.Add(statSO.createObject());
        }
        animatorController = GetComponent<AnimatorController>();
    }
    public void shoot()
    {
        if (fireRateTimer <= 0)
        {
            fireRateTimer = 1 / fireRate;
        }
    }

    public void updateGunsTimers(float delthaTime)
    {
        fireRateTimer -= delthaTime;
    }

    public virtual void updateGunsRelodeTimers(float delthaTime)
    {

    }

    public virtual void startRelode()
    {

    }
    public virtual void cancelRelode()
    {

    }
    protected virtual void endRelode()
    {

    }
}