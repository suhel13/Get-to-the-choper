using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Shock : Status 
{
    float damage;
    float slow;

    int chainCount;
    float chainRange;
    List<HealthStatusManager> pastChainTargets;

    public Shock(float duration, float tick, float damage, float slow) : base(duration, tick)
    {
        this.damage = damage;
        this.slow = slow;
        name = statusName.Shock;
    }
    public Shock(Shock Shock, bool newId) : base(Shock, newId)
    {
        damage = Shock.damage;
        slow = Shock.slow;
        chainCount = Shock.chainCount;
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
        this.damage = ShockSO.damage;
        this.slow = ShockSO.slow;
        chainCount = ShockSO.chainCount;
        chainRange = ShockSO.chainRange;
        pastChainTargets = new List<HealthStatusManager>();
        this.name = statusName.Shock;
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