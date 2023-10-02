using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cold : Status
{
    float slow;
    float frozenDuration;

    public Cold(float duration, float tick, float slow) : base(duration, tick)
    {
        this.slow = slow;
        name = statusName.Freeze;
    }
    public Cold(Cold cold, bool newId) : base(cold, newId)
    {
        slow = cold.slow;
        name = statusName.Freeze;
    }

    public override Status copy()
    {
        return new Cold(this, false);
    }

    public Cold(ColdSO coldSO) : base(coldSO.duration, coldSO.tick)
    {
        this.slow = coldSO.slow;
        this.frozenDuration = coldSO.frozenDuration;
        this.name = statusName.Freeze;
    }
    public override void normalEffect(HealthStatusManager HSman)
    {
        return;
    }
    public override void resolvePhysicsEfects(HealthStatusManager HSman)
    {
        Debug.Log("freeze slow = " + slow);
        HSman.GetComponent<Character2dTopDownControler>().addSpeedModifire(id, slow);
    }
    public override void tickEfect(HealthStatusManager HSman)
    {
        return;
    }
    public override void resolveCombinations(HealthStatusManager HSman, Dictionary<int, Status> targetStatuses)
    {
        foreach (KeyValuePair<int, Status> entry in targetStatuses)
        {
            if (entry.Value.name == Status.statusName.Wet)
            {
                slow = -1;
                duration = frozenDuration;
                timer = 0;
                Debug.Log("wather plus Freeze");
                Debug.Log(slow);

                statusesToRemove.Add(entry.Key);
            }
        }
        foreach (int id in statusesToRemove)
        {
            HSman.removeStatus(id);
        }
        statusesToRemove.Clear(); 
    }
}