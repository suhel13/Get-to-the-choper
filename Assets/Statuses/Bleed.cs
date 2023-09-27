using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Bleed : Status
{
    float damage;
    public Bleed(float duration, float tick, float damage, Sprite icon): base(duration, tick)
    {
        this.damage = damage;
        name = statusName.Bleed;
    }
    public Bleed(Bleed bleed, bool newId): base(bleed, newId)
    {
        damage = bleed.damage;
        name = statusName.Bleed;
    }

    public override Status copy()
    {
        return new Bleed(this, false);
    }

    public Bleed(BleedSO bleedSO) : base(bleedSO.duration, bleedSO.tick)
    {
        this.damage = bleedSO.damage;
        this.name = statusName.Bleed;
    }
    public override void normalEffect(HealthStatusManager HSman)
    {
        return;
    }
    public override void tickEfect(HealthStatusManager HSman)
    {
        HSman.takeDamage(damage);
        Debug.Log("bleed efect trigeer");
    }

}
