using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : Status
{
    float damage;
    float armorReduction;
    float bleedDamageMultiplayer;

    public Acid(Acid acid, bool newID) : base(acid, newID)
    {
        damage = acid.damage;
        armorReduction = acid.armorReduction;
        bleedDamageMultiplayer = acid.bleedDamageMultiplayer;
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
        bleedDamageMultiplayer = acidSO.bleedDamageMultiplayer;
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
    public override bool resolveCombinations(HealthStatusManager HSman, Dictionary<int, Status> targetStatuses)
    {
        returnVal = true;
        foreach (KeyValuePair<int, Status> entry in targetStatuses)
        {
            if (entry.Value.name == Status.statusName.Bleed)
            {
                damage *= bleedDamageMultiplayer;
                break;
            }
            if (entry.Value.name == Status.statusName.Insinirate)
            {
                statusesToRemove.Add(entry.Key);
                returnVal = false;
                Debug.LogWarning("Spawn acid cloud not implemented");
                break;
            }
        }
        foreach (Status status in statusesToAdd)
        {
            HSman.addStatus(status);
        }
        foreach (int id in statusesToRemove)
        {
            HSman.removeStatus(id);
        }
        statusesToRemove.Clear();
        return returnVal;
    }
}