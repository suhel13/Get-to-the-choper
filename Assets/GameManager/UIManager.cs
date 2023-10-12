using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<WepponIconControler> wepponIconControlers = new List<WepponIconControler>();
    public List<UpgradeButtonControler> upgradeButton = new List<UpgradeButtonControler>();

    public Upgrade upgradeSO;
    public Upgrade upgradeSO2;
    public Upgrade upgradeSO3;
    private void Start()
    {
        setUpgredeButton(0, upgradeSO);
        setUpgredeButton(1, upgradeSO2);
    }

    void setUpgredeButton(int id, Upgrade upgradeSO)
    {
        upgradeButton[id].setUpgrade(upgradeSO);
    }
}