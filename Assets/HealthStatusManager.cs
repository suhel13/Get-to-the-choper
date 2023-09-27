using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatusManager : MonoBehaviour
{
    public float Hp;
    public float maxHp;
    public Dictionary<int, Status> Statuses = new Dictionary<int, Status>();

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
        int i = Statuses.Count;
        foreach (KeyValuePair<int, Status> entry in Statuses)
        {
            // do something with entry.Value or entry.Key
            if (entry.Value.resolveStatus(Time.deltaTime, this))
            {
                Statuses.Remove(entry.Key);

                Destroy(personalUIControler.statusIcons[i].gameObject);
                personalUIControler.statusIcons.RemoveAt(i);
                personalUIControler.updateStatusIconPositions();
            }
            else
            {
                Statuses[entry.Key].statusIconUpdate();
            }
            i--;
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
        Debug.Log(status.id);

        if (Statuses.ContainsKey(status.id))
        {
            Statuses[status.id].resetDuration();
        }
        else
        {
            Statuses.Add(status.id, status);
            StatusIconControler statusIconControler = personalUIControler.createStatusIcon(status.name);
            personalUIControler.statusIcons.Add(statusIconControler);
            status.statusIcon = statusIconControler;
        }
    }

    void death()
    {

    }
}