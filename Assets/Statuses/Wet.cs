using System.Collections;
using System.Collections.Generic;
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

}