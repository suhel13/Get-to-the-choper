using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Bleed : Status
{
    float damage;
    public Bleed(float duration, float tick, float damage, Sprite icon): base(duration, tick, icon)
    {
        this.damage = damage;
        name = statusName.Bleed;
    }
    public Bleed(Bleed bleed): base(bleed)
    {
        damage = bleed.damage;
        name = statusName.Bleed;
    }

    public override Status copy()
    {
        return new Bleed(this);
    }

    public Bleed(BleedSO bleedSO) : base(bleedSO.duration, bleedSO.tick, bleedSO.icon)
    {
        this.damage = bleedSO.damage;
        this.name = statusName.Bleed;
    }

    public override void efect(HealthStatusManager HSman)
    {
        HSman.takeDamage(damage);
        Debug.Log("bleed efect trigeer");
    }
}
