using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    List<Status> statuses = new List<Status>();

    public void setDamage(float damage)
        { this.damage = damage; }
    public void addStatus(Status status)
    {
        statuses.Add(status);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthStatusManager target;
        if (collision.gameObject.TryGetComponent<HealthStatusManager>(out target))
        {
            target.takeDamage(damage);
            foreach (Status status in statuses)
            {
                target.addStatus(status.copy());
            }
        }
        Destroy(this.gameObject);
    }
}