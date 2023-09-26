using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatusManager : MonoBehaviour
{
    public float Hp;
    public float maxHp;
    public List<Status> Statuses = new List<Status>();

    public Sprite bleedIcon;
    PersonalUIControler personalUIControler;

    // Start is called before the first frame update
    void Start()
    {
        personalUIControler = GetComponentInChildren<PersonalUIControler>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = Statuses.Count - 1; i >= 0; i--)
        {
            if (Statuses[i].resolveStatus(Time.deltaTime, this))
            {
                Statuses.RemoveAt(i);
                Destroy(personalUIControler.statusIcons[i].gameObject);
                personalUIControler.statusIcons.RemoveAt(i);
                personalUIControler.updateStatusIconPositions();
            }
            else
            {
                Statuses[i].statusIconUpdate();
            }
        }
        personalUIControler.updateHpSlider(Hp / maxHp);
    }


    public void takeDamage(float amount)
    {
        Hp -= amount;
        if (Hp <= 0)
            death();
    }

    public void addStatus(Status status)
    {
        Statuses.Add(status);
        StatusIconControler iconControler = Instantiate(personalUIControler.statusIconPrefab, personalUIControler.transform).GetComponent<StatusIconControler>();
        iconControler.setPosition(personalUIControler.firstStatusIcon.GetComponent<RectTransform>().localPosition + personalUIControler.statusIconOffset * personalUIControler.statusIcons.Count);
        status.statusIcon = iconControler;
        personalUIControler.statusIcons.Add(iconControler);
    }

    void death()
    {

    }
}