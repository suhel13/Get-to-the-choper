using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    float baseDamage;
    float damage;
    float lifeTime;
    float timer;
    float pierce;
    bool pierceOnKill;
    float baseSpeed;
    float speed;
    Rigidbody2D rb2D;
    List<Status> statuses = new List<Status>();
    GameObject owner;

    public void setParameters(GameObject owner, float damage, float pierce, float lifeTime, float speed, float baseSpeed)
    {
        this.owner = owner;
        this.baseDamage = damage;
        this.pierce = pierce;
        this.lifeTime = lifeTime;
        this.baseSpeed = baseSpeed;
        this.speed = speed;
        this.damage = baseDamage * speed / baseSpeed;
        Debug.Log("Bolt init damage: " + damage.ToString() + "speed/baseSpeed: " +speed.ToString() + "/" +baseSpeed.ToString());
        rb2D = GetComponent<Rigidbody2D>();
        transform.eulerAngles = Vector3.forward * (Mathf.Atan2(rb2D.velocity.y, rb2D.velocity.x) * Mathf.Rad2Deg - 90.0f);
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
        rb2D.velocity -= rb2D.velocity.normalized * 1f / 3f * baseSpeed;

        damage = baseDamage * rb2D.velocity.magnitude / baseSpeed;
        if (rb2D.velocity.magnitude < 0.1f)
        {
            Destroy(this.gameObject);
        }
    }
}