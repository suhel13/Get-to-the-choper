using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class HealthStatusManager : MonoBehaviour
{
    public float Hp;
    public float maxHp;
    public Dictionary<int, Status> Statuses = new Dictionary<int, Status>();
    List<int> statusesToRemove = new List<int>();
    PersonalUIControler personalUIControler;

    // Start is called before the first frame update
    void Start()
    {
        personalUIControler = GetComponentInChildren<PersonalUIControler>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<int, Status> entry in Statuses)
        {
            // do something with entry.Value or entry.Key
            if (entry.Value.resolveStatus(Time.deltaTime, this))
            {
                statusesToRemove.Add(entry.Key);
            }
            else
            {
                Statuses[entry.Key].statusIconUpdate();
            }
        }
        foreach (int key in statusesToRemove)
        {

            Destroy(Statuses[key].statusIcon.gameObject);
            personalUIControler.statusIcons.Remove(Statuses[key].statusIcon);
            personalUIControler.updateStatusIconPositions();

            Statuses.Remove(key);
        }

        statusesToRemove.Clear();

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