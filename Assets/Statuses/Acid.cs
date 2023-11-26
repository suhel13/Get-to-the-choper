using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : Status
{
    float baseDamage;
    float damage;
    float armorReduction;
    float bleedDamageMultiplayer;
    private void PlayerUpgrades_StatusDamageUpgraded() { damage = baseDamage * (1 + GameManager.Instance.playerUpgrades.statusDamageBonus); }

    public Acid(Acid acid, bool newID) : base(acid, newID)
    {
        baseDamage = acid.baseDamage;
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
        GameManager.Instance.playerUpgrades.StatusDamageUpgraded += PlayerUpgrades_StatusDamageUpgraded;
        baseDamage = acidSO.damage;
        damage = baseDamage * (1 + GameManager.Instance.playerUpgrades.statusDamageBonus);
        armorReduction = acidSO.armorReduction;
        bleedDamageMultiplayer = acidSO.bleedDamageMultiplayer;
        name = statusName.Acid;
    }


    public override void normalEffect(HealthStatusManager HSman)
    {
        return;
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
            HSman.RemoveStatus(id);
        }
        statusesToRemove.Clear();
        return returnVal;
    }
}