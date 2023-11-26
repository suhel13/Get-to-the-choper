using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinirate : Status
{
    float baseDamage;
    float damage;
    float baseBleedDamage;
    float bleedDamage;
    void PlayerUpgrades_StatusDamageUpgraded() 
    { 
        damage = baseDamage * (1 + GameManager.Instance.playerUpgrades.statusDamageBonus);
        bleedDamage = baseBleedDamage * (1 + GameManager.Instance.playerUpgrades.statusDamageBonus);
    }
    public Incinirate(Incinirate incinirate, bool newID) : base(incinirate, newID)
    {
        baseDamage = incinirate.baseDamage;
        damage = incinirate.damage;
        baseBleedDamage = incinirate.baseBleedDamage;
        bleedDamage = incinirate.bleedDamage;
        name = statusName.Insinirate;
    }
    public override Status copy()
    {
        return new Incinirate(this, false);
    }

    public Incinirate(IncinirateSO incinirateSO) : base(incinirateSO.duration, incinirateSO.tick)
    {
        GameManager.Instance.playerUpgrades.StatusDamageUpgraded += PlayerUpgrades_StatusDamageUpgraded;
        baseDamage = incinirateSO.damage;
        damage = baseDamage * (1 + GameManager.Instance.playerUpgrades.statusDamageBonus);
        baseBleedDamage = incinirateSO.bleedDamage;
        bleedDamage = baseBleedDamage * (1 + GameManager.Instance.playerUpgrades.statusDamageBonus);
        this.name = statusName.Insinirate;
    }

    public override void tickEfect(HealthStatusManager HSman)
    {
        HSman.TakeDamage(damage);
    }
    public override bool resolveCombinations(HealthStatusManager HSman, Dictionary<int, Status> targetStatuses)
    {
        returnVal = true;
        foreach (KeyValuePair<int, Status> entry in targetStatuses)
        {
            if(entry.Value.name == statusName.Acid)
            {
                statusesToRemove.Add(entry.Key);
                returnVal = false;
            }
            if (entry.Value.name == statusName.Bleed)
            {
                statusesToRemove.Add(entry.Key);
                HSman.TakeDamage(bleedDamage);
                break;
            }
            if(entry.Value.name == statusName.Cold)
            {
                statusesToRemove.Add(entry.Key);
                returnVal = false;
                break;
            }
            if(entry.Value.name == statusName.Frozen)
            {
                statusesToRemove.Add(entry.Key);
                statusesToAdd.Add(GameManager.Instance.baseStatuses.wet.copy());
                returnVal = false;
                break;
            }
            if(entry.Value.name == statusName.Wet)
            {
                statusesToRemove.Add(entry.Key);
                statusesToAdd.Add(GameManager.Instance.baseStatuses.smoke.copy());
                returnVal = false;
                break;
            }
        }
        foreach (Status status in statusesToAdd)
        {
            HSman.addStatus(status);
        }
        foreach (int id in statusesToRemove)
        {
            HSman.RemoveStatus(id);
        }
        return returnVal;
    }
}