using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    float damage;
    float lifeTime;
    float timer;
    float pierce;
    bool pierceOnKill;
    float baseSpeed;
    Rigidbody2D rb2D;
    List<Status> statuses = new List<Status>();
    GameObject owner;

    public void setParameters(GameObject owner, float damage, float pierce, float lifeTime, float baseSpeed)
    {
        this.owner = owner;
        this.damage = damage;
        this.pierce = pierce;
        this.lifeTime = lifeTime;
        this.baseSpeed = baseSpeed;
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void addStatus(Status status)
    { statuses.Add(status); }

    private void Update()
    {
        if (timer >= lifeTime)
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
            if (target.takeDamage(damage) == false && pierceOnKill)
                pierce += 1;

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
        rb2D.velocity -= rb2D.velocity.normalized * 1f / 3f * baseSpeed;
        if(rb2D.velocity.magnitude < 0.1f)
        {
            Destroy(this.gameObject);
        }
    }
}