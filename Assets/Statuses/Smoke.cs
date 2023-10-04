using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Status;

public class Smoke : Status
{
    float acuracityLoss;

    public Smoke(Smoke cold, bool newId) : base(cold, newId)
    {   
        acuracityLoss = cold.acuracityLoss;
        name = statusName.Smoke;
    }

    public override Status copy()
    {
        return new Smoke(this, false);
    }

    public Smoke(SmokeSO smokeSO) : base(smokeSO.duration, smokeSO.tick)
    {
        acuracityLoss = smokeSO.acuracityLoss;
        this.name = statusName.Smoke;
    }
}
