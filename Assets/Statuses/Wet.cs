using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wet : Status
{
    public Wet(float duration, float tick) : base(duration, tick)
    {
        name = statusName.Wet;
    }
    public Wet(Wet Wet, bool newId) : base(Wet, newId)
    {
        name = statusName.Wet;
    }

    public override Status copy()
    {
        return new Wet(this, false);
    }

    public Wet(WetSO wetSO) : base(wetSO.duration, wetSO.tick)
    {
        this.name = statusName.Wet;
    }
    public override void normalEffect(HealthStatusManager HSman)
    {
        return;
    }
    public override void tickEfect(HealthStatusManager HSman)
    {
        Debug.Log("Wet efect trigeer");
    }

    public override bool resolveCombinations(HealthStatusManager HSman, Dictionary<int, Status> targetStatuses)
    {
        returnVal = true;
        foreach (KeyValuePair<int, Status> entry in targetStatuses)
        {
            if (entry.Value.name == statusName.Cold)
            {
                statusesToAdd.Add(GameManager.Instance.baseStatuses.frozen.copy());
                statusesToRemove.Add(entry.Key);
                returnVal= false;
                break;
            }
            if(entry.Value.name == statusName.Insinirate)
            {
                statusesToAdd.Add(GameManager.Instance.baseStatuses.smoke.copy());
                statusesToRemove.Add(entry.Key);
                returnVal = false;
            }
        }
        foreach (Status status in statusesToAdd)
        {
            HSman.addStatus(status);
        }
        foreach (int id in statusesToRemove)
        {
            HSman.removeStatus(id);
        }
        return returnVal;
    }
}