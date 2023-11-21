using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using static Upgrade;

public class PlayerUpgrades : MonoBehaviour
{
    public float maxHpBonus;
    public float relodeSpeedBonus;
    public float fireRateBonus;
    public float wepponDamageBonus;
    public float pierceBonus;
    public float statusDurationBonus;
    public float statusDamageBonus;
    public float movementSpeedBonus;
    public float rangeBonus;
    public float bulletSpeedBonus;
    public float pickUpRangeBonus;

    public List<Upgrade> avilableUpgrade;
    List<int> upgradeToPick = new List<int>();
    bool upgradesPickActive = false;
    public float playerXp;
    public int level = 1;
    int upgradesCount;

    public void AddXp(int amount)
    {
        playerXp += amount;
        while (playerXp >= GetXpToLevel(level))
        {
            playerXp -= GetXpToLevel(level);
            level++;
            upgradesCount++;
        }
        GameManager.Instance.uiManager.updateXpSlider();
    }
    public int GetTotalXptoLevel(int level)
    {
        return (int)Mathf.Pow(level / 0.5f, 2.0f);
    }
    public int GetXpToLevel(int level)
    {
        return GetTotalXptoLevel(level) - GetTotalXptoLevel(level - 1);
    }

    public void Start()
    {
        LoadUpgrades();
    }
    private void Update()
    {
        if (GameManager.Instance.state == GameManager.gameState.play && upgradesCount > 0 && upgradesPickActive == false)
        {
            GeneratePickUpgrade();
        }
    }
    void LoadUpgrades()
    {
        avilableUpgrade.AddRange(Resources.LoadAll("Upgrades", typeof(Upgrade)).Cast<Upgrade>().ToList());
    }
    public void GeneratePickUpgrade()
    {
        upgradesPickActive = true;
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

    public void AplayUpgrgade(Upgrade upgradeSO)
    {
        for (int i = 0; i < upgradeSO.statTypeToUpgrades.Count; i++)
        {
            switch (upgradeSO.statTypeToUpgrades[i])
            {
                case statType.maxHp:
                    maxHpBonus += upgradeSO.upgrades[i] / 100;
                    OnHpUpgraded();
                    break;
                case statType.relodeSpeed:
                    relodeSpeedBonus += (upgradeSO.upgrades[i] / 100);
                    OnRelodeUpgraded();
                    break;
                case statType.fireRate:
                    fireRateBonus += upgradeSO.upgrades[i] / 100;
                    OnFireRateUpgraded();
                    break;
                case statType.wepponDamage:
                    wepponDamageBonus += upgradeSO.upgrades[i] / 100;
                    OnWepponDamageUpgraded();
                    break;
                case statType.statusDuration:
                    statusDurationBonus += upgradeSO.upgrades[i] / 100;
                    OnStatusDurationUpgraded();
                    break;
                case statType.statusDamage:
                    statusDamageBonus += upgradeSO.upgrades[i] / 100;
                    OnStatusDamageUpgraded();
                    break;
                case statType.movementSpeed:
                    movementSpeedBonus += upgradeSO.upgrades[i] / 100;
                    OnMovementSpeedUpgraded();
                    break;
                case statType.range:
                    rangeBonus += upgradeSO.upgrades[i] / 100;
                    OnRangeUpgraded();
                    break;
                case statType.bulletSpeed:
                    bulletSpeedBonus += upgradeSO.upgrades[i] / 100;
                    OnBulletSpeedUpgraded();
                    break;
                case statType.pickUpRange:
                    pickUpRangeBonus += upgradeSO.upgrades[i] / 100;
                    OnPickUpRangeUpgraded();
                    break;
                case statType.pierce:
                    pierceBonus += upgradeSO.upgrades[i];
                    OnPierceUpgraded();
                    break;
                default:
                    Debug.LogWarning("this upgrade is not implemented " + Upgrade.getUpgradeName(upgradeSO.statTypeToUpgrades[i]));
                    break;
            }
        }
        GameManager.Instance.changeState(GameManager.gameState.play);
        upgradesCount--;
        upgradesPickActive = false;
    }
    public delegate void Notify();
    
    public event Notify HpUpgraded;
    void OnHpUpgraded(){ HpUpgraded?.Invoke(); }
    
    public event Notify RelodeUpgraded;
    void OnRelodeUpgraded() {  RelodeUpgraded?.Invoke(); }

    public event Notify FireRateUpgraded;
    void OnFireRateUpgraded() {  FireRateUpgraded?.Invoke(); }
    
    public event Notify WepponDamageUpgraded;
    void OnWepponDamageUpgraded() {  WepponDamageUpgraded?.Invoke(); }

    public event Notify StatusDurationUpgraded;
    void OnStatusDurationUpgraded() {  StatusDurationUpgraded?.Invoke(); }
    
    public event Notify StatusDamageUpgraded;
    void OnStatusDamageUpgraded() { StatusDamageUpgraded?.Invoke(); }
    
    public event Notify MovementSpeedUpgraded;
    void OnMovementSpeedUpgraded() {  MovementSpeedUpgraded?.Invoke(); }    

    public event Notify RangeUpgraded;
    void OnRangeUpgraded() { RangeUpgraded?.Invoke(); }

    public event Notify BulletSpeedUpgraded;
    void OnBulletSpeedUpgraded() {  BulletSpeedUpgraded?.Invoke(); }

    public event Notify PickUpRangeUpgraded;
    void OnPickUpRangeUpgraded() { PickUpRangeUpgraded?.Invoke(); }    
    
    public event Notify PierceUpgraded;
    void OnPierceUpgraded() { PickUpRangeUpgraded?.Invoke(); }

}