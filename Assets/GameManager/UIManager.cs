using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<WepponIconControler> wepponIconControlers = new List<WepponIconControler>();
    public List<UpgradeButtonControler> upgradeButton = new List<UpgradeButtonControler>();

    public Slider xpSlider;
    public TMPro.TextMeshProUGUI xpAMountText;

    public void updateXpSlider()
    {
        xpSlider.maxValue = GameManager.Instance.playerUpgrades.GetXpToLevel(GameManager.Instance.playerUpgrades.level);
        xpSlider.value = GameManager.Instance.playerUpgrades.playerXp;
        xpAMountText.text = "" + GameManager.Instance.playerUpgrades.playerXp + "/" + GameManager.Instance.playerUpgrades.GetXpToLevel(GameManager.Instance.playerUpgrades.level);
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