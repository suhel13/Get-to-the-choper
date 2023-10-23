using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAtack : MonoBehaviour, IOnHitEfect
{
    public float damage;
    public float radius;
    [SerializeField] bool pushFallOff;
    [SerializeField] List<StatusSO> statusesSO = new List<StatusSO>();
    public List<Status> statuses = new List<Status>();

    HealthStatusManager tempHSMan;
    IPushAble tempPushAble;
    public void Start()
    {
        foreach (var statSO in statusesSO)
        {
            Debug.Log(statSO.name);
            statuses.Add(statSO.createObject());
        }
    }

    public void ResorveEffect()
    {
        foreach (var item in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            if (item.TryGetComponent(out tempHSMan))
            {
                if (tempHSMan.takeDamage(damage) == false)
                    continue; //skip adding statuses if target is dead

                foreach (Status status in statuses)
                {
                    if (status is Push)
                    {
                        if (pushFallOff)
                        {

                            tempHSMan.addPush(status as Push, (item.transform.position - transform.position).normalized * Vector3.Distance(item.transform.position, transform.position) / radius * 0.5f);
                        }
                        else
                        {
                            tempHSMan.addPush(status as Push, (item.transform.position - transform.position).normalized);
                        }
                    }
                    else
                        tempHSMan.addStatus(status);
                }
            }
            else if (item.TryGetComponent(out tempPushAble))
            {
                foreach (Status status in statuses)
                {
                    if (status is Push)
                    {
                        if (pushFallOff)
                            tempPushAble.addPush(status as Push, (item.transform.position - transform.position).normalized * Vector3.Distance(item.transform.position, transform.position) / radius * 0.5f);
                        else
                            tempPushAble.addPush(status as Push, (item.transform.position - transform.position).normalized);
                    }
                }
            }
        }
    }
}