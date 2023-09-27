using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Status
{
    float slow;
    public Freeze(float duration, float tick, float slow) : base(duration, tick)
    {
        this.slow = slow;
        name = statusName.Freeze;
    }
    public Freeze(Freeze freze, bool newId) : base(freze, newId)
    {
        slow = freze.slow;
        name = statusName.Freeze;
    }

    public override Status copy()
    {
        return new Freeze(this, false);
    }

    public Freeze(FreezeSO freezeSO) : base(freezeSO.duration, freezeSO.tick)
    {
        this.slow = freezeSO.slow;
        this.name = statusName.Freeze;
    }
    public override void normalEffect(HealthStatusManager HSman)
    {
        HSman.GetComponent<Character2dTopDownControler>().addSpeedModifire(id, slow);
    }

    public override void tickEfect(HealthStatusManager HSman)
    {
        return;
    }
}
