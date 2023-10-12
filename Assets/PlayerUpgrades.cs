using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Upgrade;

public class PlayerUpgrades : MonoBehaviour
{
    public float maxHpBonus;
    public float relodeSpeedBonus;
    public float fireRateBonus;
    public float wepponDamageBonus;
    public float statusDurationBonus;
    public float statusDamageBonus;
    public float movementSpeedBonus;

    public void aplayUpgrgade(Upgrade upgradeSO)
    {
        for(int i = 0; i < upgradeSO.statTypeToUpgrades.Count; i++) 
        {
            switch(upgradeSO.statTypeToUpgrades[i]) 
            {
                case statType.maxHp:
                    maxHpBonus += upgradeSO.upgrades[i] / 100;
                    break;
                case statType.relodeSpeed:
                    relodeSpeedBonus += upgradeSO.upgrades[i] / 100;
                    break;
                case statType.fireRate:
                    relodeSpeedBonus += upgradeSO.upgrades[i] / 100;
                    break;
                case statType.wepponDamage:
                    wepponDamageBonus += upgradeSO.upgrades[i] / 100;
                    break;
                case statType.statusDuration:
                    statusDurationBonus += upgradeSO.upgrades[i] / 100;
                    break;
                case statType.statusDamage:
                    statusDamageBonus += upgradeSO.upgrades[i] / 100;
                    break;
                case statType.movementSpeed:
                    movementSpeedBonus += upgradeSO.upgrades[i] / 100;
                    break;
                default:
                    Debug.LogWarning("this upgrade is not implemented " + Upgrade.getUpgradeName(upgradeSO.statTypeToUpgrades[i]));
                    break;
            }
        }
    }
}
