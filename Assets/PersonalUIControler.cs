using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalUIControler : MonoBehaviour
{
    public List<StatusIconControler> statusIcons = new List<StatusIconControler>();
    public GameObject statusIconPrefab;

    public Transform firstStatusIcon;
    public Vector3 statusIconOffset;
    public Slider hpSlider;
    public Slider relodeSlider;

    public StatusIconControler createStatusIcon(Status.statusName statusName)
    {
        StatusIconControler iconControler = Instantiate(statusIconPrefab, transform).GetComponent<StatusIconControler>();
        iconControler.setPosition(firstStatusIcon.GetComponent<RectTransform>().localPosition + statusIconOffset * statusIcons.Count);
        switch (statusName)
        {
            case Status.statusName.Bleed:
                iconControler.setSprites(GameManager.Instance.iconManager.bleedIcon, GameManager.Instance.iconManager.bleedIconBG);
                break;
            case Status.statusName.Frozen:
                iconControler.setSprites(GameManager.Instance.iconManager.frozenIcon, GameManager.Instance.iconManager.frozenIconBG);
                break;
           case Status.statusName.Insinirate:
                iconControler.setSprites(GameManager.Instance.iconManager.incinirateIcon, GameManager.Instance.iconManager.incinirateIconBG);
                break;
            case Status.statusName.Poison:
                break;
            case Status.statusName.Wet:
                iconControler.setSprites(GameManager.Instance.iconManager.wetIcon, GameManager.Instance.iconManager.wetIconBG);
                break;
            case Status.statusName.Shock:
                iconControler.setSprites(GameManager.Instance.iconManager.shockIcon, GameManager.Instance.iconManager.shockIconBG);
                break;
            case Status.statusName.Acid:
                iconControler.setSprites(GameManager.Instance.iconManager.acidIcon, GameManager.Instance.iconManager.acidIconBG);
                break;
            case Status.statusName.Smoke:
                iconControler.setSprites(GameManager.Instance.iconManager.smokeIcon, GameManager.Instance.iconManager.smokeIconBG);
                break;
            case Status.statusName.Cold:
                iconControler.setSprites(GameManager.Instance.iconManager.coldIcon, GameManager.Instance.iconManager.coldIconBG);
                break;
        }
        return iconControler;
    }
    public void updateStatusIconPositions()
    {
        for (int i = 0; i < statusIcons.Count; i++)
        {
            statusIcons[i].setPosition(firstStatusIcon.GetComponent<RectTransform>().localPosition + i * statusIconOffset);
        }
    }
    public void updateHpSlider(float hp)
    {
        hpSlider.value = hp;
    }
}