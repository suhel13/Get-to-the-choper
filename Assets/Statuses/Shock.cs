using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : Status 
{
    float damage;
    float baseDamage;
    float slow;

    int chainCount;
    float chainRange;
    float baseChainRange;
    List<HealthStatusManager> pastChainTargets;
    void PlayerUpgrades_StatusDamageUpgraded() { damage = baseDamage * (1 + GameManager.Instance.playerUpgrades.statusDamageBonus); }
    void PlayerUpgrades_RangeUpgraded() { chainRange = baseChainRange * (1 + GameManager.Instance.playerUpgrades.rangeBonus); }

    public Shock(Shock Shock, bool newId) : base(Shock, newId)
    {
        baseDamage = Shock.baseDamage;
        damage = Shock.damage;
        slow = Shock.slow;
        chainCount = Shock.chainCount;
        baseChainRange = Shock.baseChainRange;
        chainRange = Shock.chainRange;
        pastChainTargets = new List<HealthStatusManager>(Shock.pastChainTargets);
        name = statusName.Shock;
        Debug.Log(pastChainTargets.Count);
    }

    public override Status copy()
    {
        return new Shock(this, false);
    }

    public Shock(ShockSO ShockSO) : base(ShockSO.duration, ShockSO.tick)
    {
        GameManager.Instance.playerUpgrades.StatusDamageUpgraded += PlayerUpgrades_StatusDamageUpgraded;
        GameManager.Instance.playerUpgrades.RangeUpgraded += PlayerUpgrades_RangeUpgraded;
        baseDamage = ShockSO.damage;
        damage = baseDamage * (1 + GameManager.Instance.playerUpgrades.statusDamageBonus);
        slow = ShockSO.slow;
        chainCount = ShockSO.chainCount;
        baseChainRange = ShockSO.chainRange;
        chainRange = baseChainRange * (1 + GameManager.Instance.playerUpgrades.rangeBonus);
        pastChainTargets = new List<HealthStatusManager>();
        name = statusName.Shock;
    }

    public override void startEfect(HealthStatusManager HSman)
    {
        if (isStartEfectResolved)
            return;
        
        isStartEfectResolved = true;
        Debug.Log("Shock start effect ChainCount: "+ chainCount + " range: " + chainRange + " Past targets count: " + pastChainTargets.Count);
        if (chainCount > 0)
        {
            HealthStatusManager closesTarget = null;
            float closestTargetRange = chainRange;
            HealthStatusManager tempHSman;
            chainCount--;
            pastChainTargets.Add(HSman);
            int collisoins=0;
            int HSmans=0;
            foreach (var item in Physics2D.OverlapCircleAll(HSman.transform.position, chainRange))
            {
                collisoins++;
                if (item.gameObject.TryGetComponent<HealthStatusManager>(out tempHSman))
                {
                    HSmans++;
                    if (pastChainTargets.Contains(tempHSman) == false)
                    {
                        if (Vector2.Distance(item.transform.position, HSman.transform.position) < closestTargetRange)
                        {
                            closestTargetRange = Vector2.Distance(item.transform.position, HSman.transform.position);
                            closesTarget = item.gameObject.GetComponent<HealthStatusManager>();
                        }
                    }
                }
            }
            Debug.Log("avilable HSMans/Colisions : " + HSmans + "/ " + collisoins + " past tagrets: " + pastChainTargets.Count);

            if (closesTarget != null)
            {
                Debug.Log(closestTargetRange, closesTarget.gameObject);
                closesTarget.addStatus(this.copy());
                Debug.DrawLine(HSman.transform.position, closesTarget.transform.position, Color.yellow, 0.2f);
            }
        }
    }

    public override void resolvePhysicsEfects(HealthStatusManager HSman)
    {
        HSman.GetComponent<Character2dTopDownControler>().addSpeedModifire(id, slow);
    }
    public override void tickEfect(HealthStatusManager HSman)
    {
        HSman.takeDamage(damage);
    }
    public override bool resolveCombinations(HealthStatusManager HSman, Dictionary<int, Status> targetStatuses)
    {
        foreach (KeyValuePair<int, Status> entry in targetStatuses)
        {
            if(entry.Value.name == Status.statusName.Wet)
            {
                chainCount++;
                Debug.Log("wather plus ligthning");
            }
        }
        Debug.Log("chainCount: "+ chainCount + " range: " + chainRange);
        return true;
    }
    public override void resetShock(Shock newShock)
    {
        Debug.Log("reset shock");
        timer = 0;
        isStartEfectResolved = false;
        pastChainTargets = new List<HealthStatusManager>(newShock.pastChainTargets);
        chainCount = newShock.chainCount;
    }
}