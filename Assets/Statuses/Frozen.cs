using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frozen : Status
{
    public Frozen(float duration, float tick, float slow) : base(duration, tick)
    {
        name = statusName.Frozen;
    }
    public Frozen(Frozen frozen, bool newId) : base(frozen, newId)
    {
        name = statusName.Frozen;
    }

    public Frozen(FrozenSO FrozenSO) : base(FrozenSO.duration, FrozenSO.tick)
    {
        this.name = statusName.Frozen;
    }

    public override Status copy() { return new Frozen(this, false); }

    public override void resolvePhysicsEfects(HealthStatusManager HSman)
    {
        HSman.GetComponent<Character2dTopDownControler>().canMove = false;
    }
}