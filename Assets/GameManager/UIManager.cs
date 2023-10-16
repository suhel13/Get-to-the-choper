using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<WepponIconControler> wepponIconControlers = new List<WepponIconControler>();
    public List<UpgradeButtonControler> upgradeButton = new List<UpgradeButtonControler>();

    private void Start()
    {

    }

    public void setUpgredeButton(int id, Upgrade upgradeSO)
    {
        upgradeButton[id].gameObject.SetActive(true);
        upgradeButton[id].setUpgrade(upgradeSO);
    }

    public void hideUpgradeButtons()
    {
        foreach (var button in upgradeButton) 
        { 
            button.gameObject.SetActive(false);
        }
    }
}