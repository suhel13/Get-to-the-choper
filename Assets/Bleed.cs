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

    public override void efect(HealthStatusManager HSman)
    {
        HSman.takeDamage(damage);
        Debug.Log("bleed efect trigeer");
    }
}
