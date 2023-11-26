using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float lifeTime;
    float timer;
    float pierce;
    bool pierceOnKill;
    List<Status> statuses = new List<Status>();
    GameObject owner;

    public void setParameters(GameObject owner, float damage,float pierce ,float lifeTime)
    { 
        this.owner = owner;
        this.damage = damage;
        this.pierce = pierce;
        this.lifeTime = lifeTime;
    }

    public void addStatus(Status status)
    { statuses.Add(status); }

    private void Update()
    {
        if(timer >= lifeTime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthStatusManager target;
        if (collision.gameObject.TryGetComponent<HealthStatusManager>(out target))
        {
            if (target.TakeDamage(damage) == false && pierceOnKill)
                pierce += 1;

            foreach (Status status in statuses)
            {
                if (status is Push)
                {
                    target.addPush(status.copy() as Push, (target.transform.position - owner.transform.position).normalized);
                }
                else
                    target.addStatus(status.copy());
            }
        }
        if (pierce < 1)
            Destroy(this.gameObject);
        else
            pierce -= 1;
    }
}