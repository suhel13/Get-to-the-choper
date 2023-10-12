using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "ScriptableObjects/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public Sprite icon;
    public string upgradeName;
    public enum statType {maxHp, relodeSpeed, fireRate, wepponDamage, statusDuration, statusDamage, movementSpeed}
    public List<statType> statTypeToUpgrades = new List<statType>();   
    public List<float> upgrades = new List<float>();

    public static string getUpgradeName(statType statType)
    {
        switch (statType)
        {
            case statType.maxHp:
                return "Max HP";
            case statType.relodeSpeed:
                return "Relode speed";
            case statType.fireRate:
                return "Fire rate";
            case statType.wepponDamage:
                return "Weppon damage";
            case statType.statusDuration:
                return "Status duration";
            case statType.statusDamage:
                return "Status damage";
            case statType.movementSpeed:
                return "Movement speed";
            default:
                return "Not implemented";
        }
    }
}