using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Bleed : Status
{
    float damage;
    float baseDamage;
    void PlayerUpgrades_StatusDamageUpgraded() { damage = baseDamage * (1 + GameManager.Instance.playerUpgrades.statusDamageBonus); }

    public Bleed(Bleed bleed, bool newId): base(bleed, newId)
    {
        baseDamage = bleed.baseDamage;
        damage = bleed.damage;
        name = statusName.Bleed;
    }

    public override Status copy()
    {
        return new Bleed(this, false);
    }

    public Bleed(BleedSO bleedSO) : base(bleedSO.duration, bleedSO.tick)
    {
        this.baseDamage = bleedSO.damage;
        damage = baseDamage * (1 + GameManager.Instance.playerUpgrades.statusDamageBonus); 
        GameManager.Instance.playerUpgrades.StatusDamageUpgraded += PlayerUpgrades_StatusDamageUpgraded;
        this.name = statusName.Bleed;
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
