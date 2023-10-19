using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AreaAtack))]
[RequireComponent (typeof(Rigidbody2D))]
public class GranadeProjectail : MonoBehaviour
{
    AreaAtack areaAtack;
    Rigidbody2D rb;
    Vector2 targetPos;
    Vector2 startPos;
    float distance;
    float normDist;
    float maxScale;

    public void setParameters(float damage, float radius, Vector2 targetPos, Vector2 velocity, List<Status> statuses, float maxScale)
    {
        areaAtack.damage = damage;
        areaAtack.radius = radius;
        this.maxScale = maxScale;
        this.targetPos = targetPos;
        distance = Vector2.Distance(startPos, this.targetPos);
        foreach (Status status in statuses)
        {
            areaAtack.statuses.Add(status);
        }
        GetComponent<Rigidbody2D>().velocity = velocity;
        Debug.Log("granade Velocity: " + velocity, gameObject);
    }
    private void Awake()
    {
        areaAtack = GetComponent<AreaAtack>();
        startPos = transform.position;
    }
    private void Update()
    {
        setSize();
        if ( Vector3.Dot(rb.velocity,new Vector3 ( targetPos.x, targetPos.y) - transform.position ) > Mathf.PI/2)
            explode();
    }
    void explode()
    {
        foreach(IOnHitEfect effect in GetComponents<IOnHitEfect>())
        {
            effect.resorveEffect();
        }
        Destroy(gameObject);
    }
    void setSize()
    {
        normDist = Vector2.Distance(transform.position, targetPos) / distance;
        if(normDist >= 0.5f)
        {
            //incrise scale to imitate troing upwards
            transform.localScale = Vector3.one + Vector3.one * (maxScale - 1) * (1 - (normDist - 0.5f) * 2);
        }
        else
        {
            //decrise scale to imitate falling down
            transform.localScale = Vector3.one + Vector3.one * (maxScale - 1) * normDist/0.5f;
        }
    }
}