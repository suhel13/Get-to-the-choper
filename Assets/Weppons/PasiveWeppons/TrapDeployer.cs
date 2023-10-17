using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDeployer : PasiveWeppon
{
    public GameObject trapPrefab;
    [SerializeField] float trapRange;
    [SerializeField] float trapTrigerRange;

    [SerializeField] float nimDeployRange;
    [SerializeField] float maxDeployRange;

    public override void Effect()
    {
        float radius = Random.Range(nimDeployRange, maxDeployRange);
        float angle = Random.Range(0f, 2 * Mathf.PI);
        Vector2 trapPos = new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
        Instantiate(trapPrefab, trapPos + new Vector2(transform.position.x, transform.position.y), Quaternion.identity).GetComponent<Trap>().setParameters(damage, trapRange, trapTrigerRange, statuses);
    }
}