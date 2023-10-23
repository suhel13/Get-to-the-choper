using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDeployer : PasiveWeppon
{
    public GameObject trapPrefab;
    [SerializeField] float baseTrapRange;
    float trapRange;

    [SerializeField] float trapTrigerRange;

    [SerializeField] float baseMinDeployRange;
    [SerializeField] float baseMaxDeployRange;
    float minDeployRange;
    float maxDeployRange;

    private void Start()
    {
        base.Start();
        GameManager.Instance.playerUpgrades.RangeUpgraded += applayRangeUpgrade;
        trapRange = baseTrapRange * (1 + GameManager.Instance.playerUpgrades.rangeBonus);
        minDeployRange = baseMinDeployRange * (1 + GameManager.Instance.playerUpgrades.rangeBonus);
        maxDeployRange = baseMaxDeployRange * (1 + GameManager.Instance.playerUpgrades.rangeBonus);
    }
    void applayRangeUpgrade()
    {
        trapRange = baseTrapRange * (1 + GameManager.Instance.playerUpgrades.rangeBonus);
        minDeployRange = baseMinDeployRange * (1 + GameManager.Instance.playerUpgrades.rangeBonus);
        maxDeployRange = baseMaxDeployRange * (1 + GameManager.Instance.playerUpgrades.rangeBonus);
    }
    public override void Effect()
    {
        float radius = Random.Range(minDeployRange, maxDeployRange);
        float angle = Random.Range(0f, 2 * Mathf.PI);
        Vector2 trapPos = new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
        Instantiate(trapPrefab, trapPos + new Vector2(transform.position.x, transform.position.y), Quaternion.identity).GetComponent<Trap>().setParameters(baseDamage, trapRange, trapTrigerRange, statuses);
    }
}