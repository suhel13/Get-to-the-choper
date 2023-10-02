using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Shock : Status
{
    float damage;
    float slow;
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
        name = statusName.Shock;
    }

    public override Status copy()
    {
        return new Shock(this, false);
    }

    public Shock(ShockSO ShockSO) : base(ShockSO.duration, ShockSO.tick)
    {
        this.damage = ShockSO.damage;
        this.slow = ShockSO.slow;
        this.name = statusName.Shock;
    }
    public override void resolvePhysicsEfects(HealthStatusManager HSman)
    {
        HSman.GetComponent<Character2dTopDownControler>().addSpeedModifire(id, slow);
    }
    public override void tickEfect(HealthStatusManager HSman)
    {
        HSman.takeDamage(damage);
        Debug.Log("Shock efect trigeer");
    }
    public override void resolveCombinations(HealthStatusManager HSman, Dictionary<int, Status> targetStatuses)
    {
        foreach (KeyValuePair<int, Status> entry in targetStatuses)
        {
            if(entry.Value.name == Status.statusName.Wet)
            {
                Debug.Log("wather plus ligthning");
            }
        }
    }

}
