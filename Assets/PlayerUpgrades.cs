using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<Upgrade> avilableUpgrade;
    List<int> upgradeToPick = new List<int>();

    public float playerXp;
    public int level = 1;
    int upgradesCount;

    public void addXp(int amount)
    {
        playerXp += amount;
        while (playerXp >= getXpToLevel(level))
        {
            playerXp -= getXpToLevel(level);
            level++;
            upgradesCount++;
        }
    }
    public int getTotalXptoLevel(int level)
    {
        return (int) Mathf.Pow(level / 0.5f, 2.0f);
    }
    public int getXpToLevel(int level)
    {
        return getTotalXptoLevel(level) - getTotalXptoLevel(level - 1);
    }

    public void Start()
    {
        loadUpgrades();
    }
    private void Update()
    {
        if(GameManager.Instance.state == GameManager.gameState.play && upgradesCount>0)
        {
            generatePickUpgrade();
        }
    }
    void loadUpgrades()
    {
        avilableUpgrade.AddRange(Resources.LoadAll("Upgrades", typeof(Upgrade)).Cast<Upgrade>().ToList());
    }
    public void generatePickUpgrade()
    {
        upgradeToPick.Clear();
        int nextRandom;
        if (avilableUpgrade.Count > 3)
        {
            for (int i = 0; i < 3; i++)
            {
                nextRandom = Random.Range(0, avilableUpgrade.Count);
                while (upgradeToPick.Contains(nextRandom))
                {
                    nextRandom = Random.Range(0, avilableUpgrade.Count);
                }
                upgradeToPick.Add(nextRandom);
            }
        }
        else
        {
            for (int i = 0; i < avilableUpgrade.Count; i++)
            {
                upgradeToPick.Add(i);
            }
        }
        for (int i = 0; i < upgradeToPick.Count; i++)
        {
            GameManager.Instance.uiManager.setUpgredeButton(i, avilableUpgrade[upgradeToPick[i]]);
        }
        GameManager.Instance.changeState(GameManager.gameState.upgradePause);
    }

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
                    relodeSpeedBonus += (upgradeSO.upgrades[i] / 100);
                    break;
                case statType.fireRate:
                    fireRateBonus += upgradeSO.upgrades[i] / 100;
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
        GameManager.Instance.changeState(GameManager.gameState.play);
        upgradesCount--;
    }
}