using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonControler : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMPro.TextMeshProUGUI name;
    [SerializeField] GameObject statisticPrefab;
    [SerializeField] Vector3 firstStatFielPos;
    [SerializeField] Vector3 statFieldOffSet;

    StatFieldControler tempStatField;
    Upgrade upgradeSO;

    public void setUpgrade(Upgrade upgradeSO)
    {
        this.upgradeSO = upgradeSO;
        icon.sprite = upgradeSO.icon;
        name.text = upgradeSO.upgradeName;

        GetComponent<Button>().onClick.AddListener(delegate { aplayUpgrade(); });

        for (int i = 0; i < upgradeSO.statTypeToUpgrades.Count; i++)
        {
            tempStatField = Instantiate(statisticPrefab, transform, false).GetComponent<StatFieldControler>();
            tempStatField.transform.localPosition = (firstStatFielPos + statFieldOffSet * i);

            tempStatField.nameText.text = Upgrade.getUpgradeName(upgradeSO.statTypeToUpgrades[i]);
            tempStatField.amountText.text = "+ " +upgradeSO.upgrades[i].ToString() + "%";
        }
    }

    public void aplayUpgrade()
    {
        Debug.Log(upgradeSO.upgradeName + " aplayed. ");
        GameManager.Instance.playerUpgrades.aplayUpgrgade(upgradeSO);
        GameManager.Instance.uiManager.hideUpgradeButtons();
    }

    private void OnDisable()
    {
        foreach (var item in GetComponentsInChildren<StatFieldControler>())
        {
            Destroy(item.gameObject);
        }
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

}
