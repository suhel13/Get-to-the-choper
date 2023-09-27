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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public StatusIconControler createStatusIcon(Status.statusName statusName)
    {
        StatusIconControler iconControler = Instantiate(statusIconPrefab, transform).GetComponent<StatusIconControler>();
        iconControler.setPosition(firstStatusIcon.GetComponent<RectTransform>().localPosition + statusIconOffset * statusIcons.Count);
        switch (statusName)
        {
            case Status.statusName.Bleed:
                iconControler.setSprites(GameManager.Instance.iconManager.bleedIcon, GameManager.Instance.iconManager.bleedIconBG);
                break;
            case Status.statusName.Freeze:
                iconControler.setSprites(GameManager.Instance.iconManager.frezeIcon, GameManager.Instance.iconManager.frezeIconBG);
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