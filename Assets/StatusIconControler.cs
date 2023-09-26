using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIconControler : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image iconBG;
    
    public void setStartValues(Sprite icon, Sprite iconBG)
    {
        this.icon.sprite = icon;
        this.iconBG.sprite = iconBG;
    }

    public void setPosition(Vector3 pos)
    {
        GetComponent<RectTransform>().localPosition = pos;
    }

    public void setFillAmount(float amount)
    {
        icon.fillAmount = amount;
    }
}
