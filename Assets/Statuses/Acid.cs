using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : Status
{
    float damage;
    float armorReduction;

    public Acid(Acid acid, bool newID) : base(acid, newID)
    {
        damage = acid.damage;
        armorReduction = acid.armorReduction;
        name = statusName.Acid;
    }
    public override Status copy()
    {
        return new Acid(this, false);
    }

    public Acid(AcidSO acidSO) : base(acidSO.duration, acidSO.tick)
    {
        this.damage = acidSO.damage;
        this.armorReduction = acidSO.armorReduction;
        this.name = statusName.Acid;
    }

    public override void normalEffect(HealthStatusManager HSman)
    {
        return;
    }
    public override void tickEfect(HealthStatusManager HSman)
    {
        HSman.takeDamage(damage);
    }
}