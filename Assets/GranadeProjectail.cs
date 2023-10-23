using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AreaAtack))]
[RequireComponent (typeof(Rigidbody2D))]
public class GranadeProjectail : MonoBehaviour
{
    AreaAtack areaAtack;
    Rigidbody2D rb2D;
    Vector2 targetPos;
    Vector2 startPos;
    float distance;
    float normDist;
    float maxScale;

    public void SetParameters(float damage, float radius, Vector2 targetPos, Vector2 velocity, List<Status> statuses, float maxScale)
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
        rb2D.velocity = velocity;
        Debug.Log("granade Velocity: " + velocity, gameObject);
    }
    private void Awake()
    {
        areaAtack = GetComponent<AreaAtack>();
        startPos = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        SetSize();

        if ( Mathf.Acos( Vector3.Dot(rb2D.velocity,new Vector3 ( targetPos.x, targetPos.y) - transform.position )) > Mathf.PI/2)
            Explode();
    }
    void Explode()
    {
        foreach(IOnHitEfect effect in GetComponents<IOnHitEfect>())
        {
            effect.ResorveEffect();
        }
        Destroy(gameObject);
    }
    void SetSize()
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