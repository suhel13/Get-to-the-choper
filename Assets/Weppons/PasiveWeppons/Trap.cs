using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AreaAtack))]
public class Trap : MonoBehaviour
{
    AreaAtack areaAtack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        active();
    }
    public void setParameters( float damage, float radius, float trigerRadius, List<Status> statuses)
    {
        areaAtack = GetComponent<AreaAtack>();
        areaAtack.damage = damage;
        areaAtack.radius = radius;
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

    void active()
    {
        foreach(IOnHitEfect onHitEfect in GetComponents<IOnHitEfect>())
        {
            onHitEfect.ResorveEffect();
        }
        Destroy(gameObject);
    }
}