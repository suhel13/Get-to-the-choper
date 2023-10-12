using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAtack : MonoBehaviour, IOnHitEfect
{
    [SerializeField] float damage;
    [SerializeField] float radius;
    [SerializeField] float distance;
    [SerializeField] bool pushFallOff;
    [SerializeField] List<StatusSO> statusesSO = new List<StatusSO>();
    List<Status> statuses = new List<Status>();

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

    public void resorveEffect()
    {
        foreach (var item in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            if (item.TryGetComponent(out tempHSMan))
            {
                tempHSMan.takeDamage(damage);
                foreach (Status status in statuses)
                {
                    if (pushFallOff)

                        tempHSMan.addPush(status as Push, (item.transform.position - transform.position).normalized * Vector3.Distance(item.transform.position, transform.position) / radius * 0.5f);
                    else
                        tempHSMan.addPush(status as Push, (item.transform.position - transform.position).normalized);
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