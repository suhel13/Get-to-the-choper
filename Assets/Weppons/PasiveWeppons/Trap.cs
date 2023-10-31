using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AreaAtack))]
public class Trap : MonoBehaviour
{
    AreaAtack areaAtack;
    float lifeTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Active();
    }

    private void Update()
    {
        if(lifeTime> 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void setParameters( float damage, float radius, float trigerRadius, List<Status> statuses, float lifeTime)
    {
        areaAtack = GetComponent<AreaAtack>();
        areaAtack.damage = damage;
        areaAtack.radius = radius;
        this.lifeTime = lifeTime;
        GetComponent<CircleCollider2D>().radius = trigerRadius;
        foreach(Status status in statuses)
        {
            areaAtack.statuses.Add(status);
        }
    }
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, areaAtack.radius);
    }

    void Active()
    {
        foreach(IOnHitEfect onHitEfect in GetComponents<IOnHitEfect>())
        {
            onHitEfect.ResorveEffect();
        }
        Destroy(gameObject);
    }
}