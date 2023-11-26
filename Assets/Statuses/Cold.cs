using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cold : Status
{
    float slow;

    public Cold(float duration, float tick, float slow) : base(duration, tick)
    {
        this.slow = slow;
        name = statusName.Cold;
    }
    public Cold(Cold cold, bool newId) : base(cold, newId)
    {
        slow = cold.slow;
        name = statusName.Cold;
    }
    public override Status copy()
    {
        return new Cold(this, false);
    }
    public Cold(ColdSO coldSO) : base(coldSO.duration, coldSO.tick)
    {
        this.slow = coldSO.slow;
        this.name = statusName.Cold;
    }
    public override void normalEffect(HealthStatusManager HSman)
    {
        return;
    }
    public override void resolvePhysicsEfects(HealthStatusManager HSman)
    {
        Debug.Log("cold slow = " + slow);
        HSman.GetComponent<Character2dTopDownControler>().addSpeedModifire(id, slow);
    }
    public override void tickEfect(HealthStatusManager HSman)
    {
        return;
    }
    public override bool resolveCombinations(HealthStatusManager HSman, Dictionary<int, Status> targetStatuses)
    {
        returnVal = true;
        foreach (KeyValuePair<int, Status> entry in targetStatuses)
        {
            if (entry.Value.name == Status.statusName.Wet)
            {
                statusesToAdd.Add(GameManager.Instance.baseStatuses.frozen.copy());

                statusesToRemove.Add(entry.Key);
                returnVal = false;
                break;
            }
            if(entry.Value.name == Status.statusName.Frozen)
            {
                statusesToAdd.Add(GameManager.Instance.baseStatuses.frozen.copy());
                returnVal = false;
                break;
            }
            if(entry.Value.name == Status.statusName.Insinirate)
            {
                statusesToRemove.Add(entry.Key);
                returnVal = false;
                break;  
            }
        }
        foreach (Status status in statusesToAdd)
        {
            HSman.addStatus(status);
        }
        foreach (int id in statusesToRemove)
        {
            HSman.RemoveStatus(id);
        }
        statusesToRemove.Clear();
        return returnVal;
    }
}