using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float lifeTime;
    float timer;
    List<Status> statuses = new List<Status>();
    GameObject owner;

    public void setParameters(GameObject owner, float damage, float lifeTime)
    { 
        this.owner = owner;
        this.damage = damage;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthStatusManager target;
        if (collision.gameObject.TryGetComponent<HealthStatusManager>(out target))
        {
            target.takeDamage(damage);
            foreach (Status status in statuses)
            {
                if (status is Push)
                {
                    target.addPush(status as Push, (target.transform.position - owner.transform.position).normalized);
                }
                else
                    target.addStatus(status.copy());
            }
        }
        Destroy(this.gameObject);
    }
}